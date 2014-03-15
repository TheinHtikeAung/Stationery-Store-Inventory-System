package storeclerk;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
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
import com.logicuniversity.R.layout;
import com.logicuniversity.R.menu;
import com.logicuniversity.Util;

import entity.MDepartment;
import entity.MFulfillment_View;

import android.os.AsyncTask;
import android.os.Bundle;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.ListActivity;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Color;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.Window;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemClickListener;

public class ItemScreen extends ListActivity{

	private static ProgressDialog dialog;
	public List<MFulfillment_View> MFList;
	int deptId = 0;
	String deptName=null;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		setContentView(R.layout.item_screen_activity);
		Util.prefSetUp(this);
	    
		Bundle extras = getIntent().getExtras();
		if (extras != null) {
			deptId = extras.getInt(Util.KEY_DEPTID);
			deptName = extras.getString(Util.KEY_DEPTNAME);
		}
		fillData();
		
	}
	
	private void fillData()
	{
		JSONtoMFulfillment_View(getJSONByItem(deptId));
		SimpleAdapter s=new SimpleAdapter(this,MFList,R.layout.itemrow,new String[]{"stationery_name","requested_quantity","fulfill_quantity"},new int[]{ R.id.lblStationery,R.id.lblRequestedQty,R.id.lblFulfillQty});
		setListAdapter(s);
		
		setTitle(deptName);
	}
	
	// Menu hide
	@Override
	 public boolean onPrepareOptionsMenu (Menu menu) {
		if(MFList.size()==0)
			return false;
		
	     return true;
	 }

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.item_screen, menu);
		return true;
	}
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
	   switch (item.getItemId()) {
	   case R.id.mnuDistribute:
	   {
		   showConfirmDialog();
		  
	      return(true);
	   }
	   default:
	      return super.onOptionsItemSelected(item);
	   }

	}
	
	public void doDistribution()
	{
		// Aync Task is very fast so rare to see Progress Dialog
		dialog = ProgressDialog.show(this, "Distribute Process", "processing ...");

		//Toast.makeText(this, item + " selected", Toast.LENGTH_LONG).show();
		  for (MFulfillment_View mf : MFList) {
			  //updateToDisbursement(mf.department, mf.stationery);
			  new updateOperation(mf.department, mf.stationery).execute();
		  }
		  dialog.dismiss(); 
		  Toast.makeText(this, "Successfully Recorded Distributed Items.", Toast.LENGTH_LONG).show();
		  
		  Intent i = new Intent(this, FulfillmentScreen.class);
		  startActivity(i);
		  
	}
	
	@Override
	  protected void onListItemClick(ListView l, View v, int position, long id) {
	      Util.mf = (MFulfillment_View) getListAdapter().getItem(position);
	      
	      Intent i = new Intent(this, ItemDetailScreen.class);
	      startActivityForResult(i, Util.REQ_ITEMDETAIL);
	}
	
	// Get JSON string from server
		public String getJSONByItem(int deptId)
		{
			DefaultHttpClient   httpclient = new DefaultHttpClient(new BasicHttpParams());
			HttpPost httppost = new HttpPost(Util.url);
			Log.i("URL: ",Util.url);
			
			List<NameValuePair> postParameters = new ArrayList<NameValuePair>();
			postParameters.add(new BasicNameValuePair("token", Util.VAL_TOKEN));
			postParameters.add(new BasicNameValuePair("method", "getJSONByItem"));
			postParameters.add(new BasicNameValuePair("deptId", String.valueOf(deptId)));
			
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
		
		public void JSONtoMFulfillment_View(String JSON)
		{
			MFList=new ArrayList<MFulfillment_View>();
			
			try {
				JSONArray JFList = new JSONArray(JSON);
				
				for (int i = 0; i < JFList.length(); i++) {
					
					String mFObj=JFList.getString(i);
					JSONObject deptObj=new JSONObject(mFObj);
					
					MFulfillment_View MF=new MFulfillment_View(deptObj.getInt("id"), 
							deptObj.getInt("department"), deptObj.getInt("stationery"), 
							deptObj.getString("stationery_name"),deptObj.getInt("requested_quantity"), 
							deptObj.getInt("fulfill_quantity"), deptObj.getInt("unfulfill_quantity"), 
							deptObj.getInt("stock_control"));
					
					MFList.add(MF);
				}
			} catch (JSONException e) {
				e.printStackTrace();
			}
		}
		
		public void updateToDisbursement(int deptId, int stationeryId)
		{
			DefaultHttpClient   httpclient = new DefaultHttpClient(new BasicHttpParams());
			HttpPost httppost = new HttpPost(Util.url);
			Log.i("URL: ",Util.url);
			
			List<NameValuePair> postParameters = new ArrayList<NameValuePair>();
			postParameters.add(new BasicNameValuePair("token", Util.VAL_TOKEN));
			postParameters.add(new BasicNameValuePair("method", "updateToDisbursement"));
			postParameters.add(new BasicNameValuePair("deptId", String.valueOf(deptId)));
			postParameters.add(new BasicNameValuePair("stationeryId", String.valueOf(stationeryId)));
			
			httppost.addHeader("Content-type", "application/x-www-form-urlencoded");

			InputStream inputStream = null;
			try {
				httppost.setEntity(new UrlEncodedFormEntity(postParameters));
			    httpclient.execute(httppost);
			    
			    Log.i("Sent", "Updated Qty");
			    
			} catch (Exception e) { 
			    // Oops
			}
			finally {
			    try{if(inputStream != null)inputStream.close();}catch(Exception squish){}
			}
		}
	
		private class updateOperation extends AsyncTask<String, Void, String> {
			private int deptId;
			private int stationeryId;
			
			public updateOperation(int deptId, int stationeryId)
			{
				this.deptId=deptId;
				this.stationeryId=stationeryId;
			}

	          @Override
	          protected String doInBackground(String... params) {
	        	  /*
	        	  try {
					Thread.sleep(20000);
				} catch (InterruptedException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				*/
	        	  
	        	  updateToDisbursement(deptId, stationeryId);

	              return "Executed";
	          }      

	          @Override
	          protected void onPostExecute(String result) {
	               Log.i("Processing Result :", "executed");
	          }

	          @Override
	          protected void onPreExecute() {
	          }

	          @Override
	          protected void onProgressUpdate(Void... values) {
	          }
	    }
		
		public void showConfirmDialog()
		{
			AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
	 
			// set title
			alertDialogBuilder.setTitle("Distribute Process");
	 
				// set dialog message
				alertDialogBuilder
					.setMessage("All items will be proceeded for distribution.")
					.setCancelable(false)
					.setPositiveButton("Ok",new DialogInterface.OnClickListener() {
						public void onClick(DialogInterface dialog,int id) {
							doDistribution();
						}
					  })
					.setNegativeButton("Cancel",new DialogInterface.OnClickListener() {
						public void onClick(DialogInterface dialog,int id) {
							dialog.cancel();
						}
					});
	 
					// create alert dialog
					AlertDialog alertDialog = alertDialogBuilder.create();
	 
					// show it
					alertDialog.show();
		}

		@Override
		protected void onResume() {
		    super.onResume();
		    fillData();
		}
}


