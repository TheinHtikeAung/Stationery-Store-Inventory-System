package com.logic.dHead;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.app.ListActivity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.os.StrictMode;
import android.os.StrictMode.ThreadPolicy;
import android.preference.PreferenceManager;
import android.view.MenuItem;

public class RejectedRequisitionActivity extends ListActivity{
@Override
protected void onCreate(Bundle savedInstanceState) {
	// TODO Auto-generated method stub
	super.onCreate(savedInstanceState);
	setContentView(R.layout.activity_rejected);
	 StrictMode.setThreadPolicy(ThreadPolicy.LAX);
	 
	SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
	String access_token = preferences.getString("access_token", null);
	setListAdapter(new RequisitionListAdapter(this, loadAllRejectedRequisitions(access_token)));
	
}

 public boolean onCreateOptionsMenu(android.view.Menu menu) {
	 getMenuInflater().inflate(R.menu.rejected_activity_menu, menu);
	 return true;
 }
 
 
 
  @Override
	public boolean onOptionsItemSelected(MenuItem item) {
	  Intent i;
	  switch (item.getItemId()) {
	   case R.id.view_all_pending_requisitions :{
		   i = new Intent(this,PendingRequisitionActivity.class);				  
			  startActivity(i);
		      return true;
	   }
	  
	   case R.id.view_all_approved_requisitions :{
		   i = new Intent(this,ApprovedRequisitionActivity.class);				  
			  startActivity(i);
		      return true;
	   }
	   case R.id.logout :{
		   //call logout method
		   logout();
		   i = new Intent(this,LoginActivity.class);				  
			  startActivity(i);
		      return true;
	   }
	   default:
		      return super.onOptionsItemSelected(item);
	   
	   }
	}


	public void logout(){
	SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
	String access_token = preferences.getString("access_token", null);
	 List<NameValuePair> params = new ArrayList<NameValuePair>();
		params.add(new BasicNameValuePair("access_token", access_token));
		params.add(new BasicNameValuePair("method", "logout"));
		 try {
			JSONObject json = new JSONObject(Helper.loadJSONString(params));
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		preferences.edit().remove("access_token").commit();
	}


  
  
  public ArrayList<Requisition> loadAllRejectedRequisitions(String accessToken){
		ArrayList<Requisition> requisitions = new ArrayList<Requisition>();
		List<NameValuePair> params = new ArrayList<NameValuePair>();
		params.add(new BasicNameValuePair("access_token", accessToken));
		params.add(new BasicNameValuePair("method", "loadAllRejectedRequisitions"));
		try{
		//JSONArray jsonArray = new JSONArray(Helper.loadJSONString(params));
			JSONArray jsonArray = new JSONArray(Helper.loadJSONString(params));
		for(int i =0;i<jsonArray.length();i++){
			Requisition requisition = new Requisition();
			JSONObject jsonObject = new JSONObject(jsonArray.getString(i)); 
			requisition.setStationaryName(jsonObject.getString("StationaryName"));
			requisition.setRequisitionDate(jsonObject.getString("ReqDate"));
			requisition.setQuantity(Integer.parseInt(jsonObject.getString("Quantity")));
			requisition.setEmployeeName(jsonObject.getString("EmployeeName"));
			requisitions.add(requisition);
		}
		
	   }catch(Exception e){
		e.printStackTrace();
	   }
		return requisitions;
	}
 
}
