package com.example.logicuniversityemployee;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;
import org.json.JSONObject;

import android.app.ListActivity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.os.StrictMode;
import android.os.StrictMode.ThreadPolicy;
import android.preference.PreferenceManager;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ListView;

public class DistributedRequisitionActivity extends ListActivity{

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_distributed);
		 StrictMode.setThreadPolicy(ThreadPolicy.LAX);
		SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
		String access_token = preferences.getString("access_token", null);
		RequisitionAdapter adapter = new RequisitionAdapter(this, loadAllDistributedRequisitions(access_token));
		setListAdapter(adapter);
	}
	
	 @Override
	    public boolean onCreateOptionsMenu(Menu menu) {
	        // Inflate the menu; this adds items to the action bar if it is present.
	        getMenuInflater().inflate(R.menu.main, menu);
	        return true;
	    }
	    
	 public void logout(){
	    	SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
			preferences.edit().remove("access_token").commit();
	    }
	    public boolean onOptionsItemSelected(MenuItem item) {
			   Intent i;
			   switch (item.getItemId()) {
			   case R.id.pending_requisitions :{
				   i = new Intent(this,PendingRequisitionActivity.class);				  
					  startActivity(i);
				      return true;
			   }
			   case R.id.approved_requisitions :{
				   i = new Intent(this,ApprovedRequisitionActivity.class);				  
					  startActivity(i);
				      return true;
			   }
			   case R.id.rejected_requisitions :{
				   i = new Intent(this,RejectedRequisitionActivity.class);				  
					  startActivity(i);
				      return true;
			   }
			   case R.id.processing_requisitions :{
				   i = new Intent(this,ProcessingRequisitionActivity.class);				  
					  startActivity(i);
				      return true;
			   }
			   case R.id.distributed_requisitions :{
				   i = new Intent(this,DistributedRequisitionActivity.class);				  
					  startActivity(i);
				      return true;
			   }
			   case R.id.new_requisition :{
				   i = new Intent(this,MainActivity.class);				  
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
	@Override
	protected void onListItemClick(ListView l, View v, int position, long id) {
		// TODO Auto-generated method stub
		super.onListItemClick(l, v, position, id);
	}
	
	
	public ArrayList<Requisition> loadAllDistributedRequisitions(String accessToken){
		ArrayList<Requisition> requisitions = new ArrayList<Requisition>();
		List<NameValuePair> params = new ArrayList<NameValuePair>();
		params.add(new BasicNameValuePair("accessToken", accessToken));
		params.add(new BasicNameValuePair("method", "loadAllDistributedRequisitions"));
		try{
		JSONArray jsonArray = new JSONArray(Helper.loadJSONString(params));
		for(int i =0;i<jsonArray.length();i++){
			Requisition requisition = new Requisition();
			JSONObject jsonObject = new JSONObject(jsonArray.getString(i)); 
			requisition.setStationaryName(jsonObject.getString("StationaryName"));
			requisition.setReqDate(jsonObject.getString("RequestedDate"));
			requisition.setQuantity(Integer.parseInt(jsonObject.getString("Quantity")));
			requisitions.add(requisition);
		}
		
	   }catch(Exception e){
		e.printStackTrace();
	   }
		return requisitions;
	}
}

