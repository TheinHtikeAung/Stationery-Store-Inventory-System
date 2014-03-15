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

import android.annotation.TargetApi;
import android.app.Activity;
import android.app.ListActivity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Build;
import android.os.Bundle;
import android.os.StrictMode;
import android.preference.PreferenceManager;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.Toast;

import com.logicuniversity.R;
import com.logicuniversity.Util;

import entity.MDepartment;

@TargetApi(Build.VERSION_CODES.GINGERBREAD)
public class FulfillmentScreen extends ListActivity {
	
	List<MDepartment> mDeptList;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		StrictMode.ThreadPolicy policy = new StrictMode.
		ThreadPolicy.Builder().permitAll().build();
		StrictMode.setThreadPolicy(policy);
		
		Util.prefSetUp(this);
		
		setTitle(Util.TITLE_FULFILLMENT);
		//setContentView(R.layout.main_screen_activity);
		
		// Bind to own list from JSON
		JSONtoMDepartment(getJSONByDepartment());
		
		//SimpleAdapter s=new SimpleAdapter(this,mDeptList,android.R.layout.simple_list_item_2,new String[]{"department_name"},new int[]{ android.R.id.text1});
		SimpleAdapter s=new SimpleAdapter(this,mDeptList,R.layout.itemrow2,new String[]{"department_name","name","representative_name"},new int[]{ R.id.lblDepartment,R.id.lblCollectionPoint,R.id.lblRepresentative});
		setListAdapter(s);
	}
	
	@Override
	  protected void onListItemClick(ListView l, View v, int position, long id) {
	    MDepartment dept = (MDepartment) getListAdapter().getItem(position);
	    //Toast.makeText(this, item.department_name + " selected", Toast.LENGTH_LONG).show();
	    
	    Intent i = new Intent(this, ItemScreen.class);
		i.putExtra(Util.KEY_DEPTID, dept.id);
		i.putExtra(Util.KEY_DEPTNAME, dept.department_name);
		startActivity(i);
	  }

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		//getMenuInflater().inflate(R.menu.main_screen, menu);
		return true;
	}
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
	  switch (item.getItemId()) {
	  // We have only one menu option
	  case R.id.userSetting:
	    // Launch Preference activity
	    Intent i = new Intent(this, MyPreferencesActivity.class);
	    startActivity(i);
	    // Some feedback to the user
	    Toast.makeText(this, "Enter your user credentials.",
	      Toast.LENGTH_LONG).show();
	    break;

	  }
	  return true;
	} 
	
	// Get JSON string from server
	public String getJSONByDepartment()
	{
		DefaultHttpClient   httpclient = new DefaultHttpClient(new BasicHttpParams());
		HttpPost httppost = new HttpPost(Util.url);
		Log.i("URL: ","http://"+Util.ip+"/api/mfulfillment.ashx");
		
		List<NameValuePair> postParameters = new ArrayList<NameValuePair>();
		postParameters.add(new BasicNameValuePair("token", Util.VAL_TOKEN));
		postParameters.add(new BasicNameValuePair("method", "getJSONByDepartment"));
		
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

	public void JSONtoMDepartment(String JSON)
	{
		mDeptList=new ArrayList<MDepartment>();
		
		try {
			JSONArray  mDepts = new JSONArray(JSON);
			
			for (int i = 0; i < mDepts.length(); i++) {
				
				String mDepartment=mDepts.getString(i);
				JSONObject deptObj=new JSONObject(mDepartment);
				
				MDepartment mDept=new MDepartment(deptObj.getInt("id"),
						deptObj.getString("department_name"),
						deptObj.getString("representative_name"),
						deptObj.getString("name"));
				
				mDeptList.add(mDept);
			}
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
}
