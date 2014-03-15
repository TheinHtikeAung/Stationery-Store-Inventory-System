package storeclerk;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.lang.Character.UnicodeBlock;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.params.BasicHttpParams;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.logicuniversity.R;
import com.logicuniversity.Util;
import com.logicuniversity.R.layout;
import com.logicuniversity.R.menu;

import entity.MFulfillment_View;
import entity.MStationery;

import android.os.Bundle;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Window;
import android.webkit.WebChromeClient.CustomViewCallback;
import android.widget.AutoCompleteTextView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

public class ItemDetailScreen extends Activity implements OnClickListener{

	MFulfillment_View mf;
	private TextView txtavailableQty;
	private TextView txtRequistedQty;
	private EditText txtFulfillQty;
	
	int editedFulfillQty;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.item_detail_screen_activity);
		Util.prefSetUp(this);
		fillData();
	}
	
	private void fillData()
	{
		mf=Util.mf;
		Util.mf=null; //to prevent memory wasted.
		
		txtavailableQty= (TextView)findViewById(R.id.lblRequestedQty);
		txtRequistedQty= (TextView)findViewById(R.id.txtRequistedQty);
		txtFulfillQty= (EditText)findViewById(R.id.txtFulfillQty);
		
		txtavailableQty.setText(String.valueOf(mf.stock_control));
		txtRequistedQty.setText(String.valueOf(mf.request_quantity));
		txtFulfillQty.setText(String.valueOf(mf.fulfill_quantity));
		
		setTitle(mf.stationery_name);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.item_detail_screen, menu);
		return true;
	}
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
	   switch (item.getItemId()) {
	   case R.id.mnuSave:
	   {
		   //Toast.makeText(this, item + " selected", Toast.LENGTH_LONG).show();
		   editedFulfillQty=Integer.parseInt(txtFulfillQty.getText().toString());
		   
		   if(editedFulfillQty>mf.stock_control){
			   showAlertBox("Fulfill Qty is highter than stock qty.");
		   }else if(editedFulfillQty>mf.request_quantity){
			   showAlertBox("Fulfill Qty is highter than requested qty.");
			   
		   }else if(editedFulfillQty<=mf.stock_control)
		   {
			   if(mf.fulfill_quantity!=editedFulfillQty)
			   {
				   String result=updateFulfillQty();
				   Toast.makeText(this,result.trim(), Toast.LENGTH_LONG).show();
				   
				   Intent i = getIntent();
				   setResult(RESULT_OK, i);
				   finish();
			   }
			   else   // no modification, no call for new
			   {
				   finish();
			   }
		   }
		   
	      return(true);
	   }
	   case R.id.mnuReportDiscrepancy:
	   {
		   Intent i = new Intent(this, ReportDiscrepancy_Dialog.class);
		   i.putExtra(Util.STATIONERY_ID, String.valueOf(mf.stationery));
		   startActivityForResult( i, Util.REQ_DIALOG);
	   }
	   default:
	      return super.onOptionsItemSelected(item);
	   }

	}
	
	 @Override
	 protected void onActivityResult(int requestCode, int resultCode, Intent data) {
	         super.onActivityResult(requestCode, resultCode, data);
	 
	         if(data.getExtras().containsKey(Util.REFRESH_NEED)){
	        	 String result=data.getStringExtra(Util.REFRESH_NEED);
	        	 Log.w("onActivityResult", result);
	        	 
	        	 if(result.equals("true")){
	        		 JSONtoMFulfillment_View_Only(mf.department, mf.stationery);
	        		 fillData();
	        	 }
	         }
	 
	 }


	@Override
	public void onClick(View v) {
		final int id = v.getId();
	    switch (id) {
		    case R.id.btnFulfill:
		    {	
		    	int diff=mf.stock_control-mf.request_quantity;
		    	if(diff>=0)  // Can give
		    	{
		    		txtFulfillQty.setText(String.valueOf(mf.request_quantity));
		    	}else{
		    		txtFulfillQty.setText(String.valueOf(mf.stock_control)); // diff become minize
		    	}
		    	
		        break;
		    }
	    }
	}
	
	
	public String updateFulfillQty()
	{
		DefaultHttpClient   httpclient = new DefaultHttpClient(new BasicHttpParams());
		HttpPost httppost = new HttpPost(Util.url);
		Log.i("URL: ",Util.url);
		
		List<NameValuePair> postParameters = new ArrayList<NameValuePair>();
		postParameters.add(new BasicNameValuePair("token", Util.VAL_TOKEN));
		postParameters.add(new BasicNameValuePair("method", "updateFulfillQty"));
		postParameters.add(new BasicNameValuePair("deptId", String.valueOf(mf.department)));
		postParameters.add(new BasicNameValuePair("stationeryId", String.valueOf(mf.stationery)));
		postParameters.add(new BasicNameValuePair("originalQty", String.valueOf(mf.fulfill_quantity)));
		postParameters.add(new BasicNameValuePair("editableQty", String.valueOf(editedFulfillQty)));
		
		httppost.addHeader("Content-type", "application/x-www-form-urlencoded");

		InputStream inputStream = null;
		String result = null;
		try {
			httppost.setEntity(new UrlEncodedFormEntity(postParameters));
		    HttpResponse response = httpclient.execute(httppost);
		    
		    Log.w("OUTPUT",response.toString());
		    HttpEntity entity = response.getEntity();

		    inputStream = entity.getContent();
		    // json is UTF-8 by default
		    BufferedReader reader = new BufferedReader(new InputStreamReader(inputStream, "UTF-8"), 8);
		    StringBuilder sb = new StringBuilder();

		    String line = null;
		    while ((line = reader.readLine()) != null)
		    {
		       sb.append(line + "\n");
		    }
		    result = sb.toString();
		} catch (Exception e) { 
		    // Oops
		}
		finally {
		    try{if(inputStream != null)inputStream.close();}catch(Exception squish){}
		}
		Log.d("Result : ",result);
		
		return result;
	}
	
	public void showAlertBox(String msg)
	{
		AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
 
		// set title
		alertDialogBuilder.setTitle("Out of stock");
 
		// set dialog message
		alertDialogBuilder
				.setMessage(msg)
				.setCancelable(true)
				.setPositiveButton("Ok",new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog,int id) {
						dialog.cancel();
					}
				  });
 
				// create alert dialog
				AlertDialog alertDialog = alertDialogBuilder.create();
 
				// show it
				alertDialog.show();
	}
	
	// Get JSON string from server
			public String getJSONByDepartmentAndItem(int deptId, int stationeryId)
			{
				DefaultHttpClient   httpclient = new DefaultHttpClient(new BasicHttpParams());
				HttpPost httppost = new HttpPost(Util.url);
				Log.i("URL: ",Util.url);
				
				List<NameValuePair> postParameters = new ArrayList<NameValuePair>();
				postParameters.add(new BasicNameValuePair("token", Util.VAL_TOKEN));
				postParameters.add(new BasicNameValuePair("method", "getJSONByDepartmentAndItem"));
				postParameters.add(new BasicNameValuePair("deptId", String.valueOf(deptId)));
				postParameters.add(new BasicNameValuePair("stationeryId", String.valueOf(stationeryId)));
				
				httppost.addHeader("Content-type", "application/x-www-form-urlencoded");

				InputStream inputStream = null;
				String result = null;
				try {
					httppost.setEntity(new UrlEncodedFormEntity(postParameters));
				    HttpResponse response = httpclient.execute(httppost);
				    
				    Log.w("JSON",response.toString());
				    HttpEntity entity = response.getEntity();

				    inputStream = entity.getContent();
				    // json is UTF-8 by default
				    BufferedReader reader = new BufferedReader(new InputStreamReader(inputStream, "UTF-8"), 8);
				    StringBuilder sb = new StringBuilder();

				    String line = null;
				    while ((line = reader.readLine()) != null)
				    {
				       sb.append(line);
				    }
				    result = sb.toString();
				} catch (Exception e) { 
				    // Oops
				}
				finally {
				    try{if(inputStream != null)inputStream.close();}catch(Exception squish){}
				}
				Log.d("Result : ",result);
				
				return result;
			}
			
			public void JSONtoMFulfillment_View_Only(int deptId, int stationeryId)
			{
				String JSON = getJSONByDepartmentAndItem(deptId, stationeryId);
				
				try {
					
					JSONObject mObj=new JSONObject(JSON);
					Util.mf=new MFulfillment_View(mObj.getInt("id"), 
							mObj.getInt("department"), mObj.getInt("stationery"), 
							mObj.getString("stationery_name"),mObj.getInt("requested_quantity"), 
							mObj.getInt("fulfill_quantity"), mObj.getInt("unfulfill_quantity"), 
							mObj.getInt("stock_control"));	
					
				} catch (JSONException e) {
					e.printStackTrace();
				} finally{ //return mList;
					
				}
			}
}
