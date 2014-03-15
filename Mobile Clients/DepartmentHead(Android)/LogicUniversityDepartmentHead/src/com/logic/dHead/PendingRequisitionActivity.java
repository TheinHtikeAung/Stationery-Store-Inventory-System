package com.logic.dHead;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.os.StrictMode;
import android.preference.PreferenceManager;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.ExpandableListView;
import android.widget.ExpandableListView.OnChildClickListener;
import android.widget.Toast;
import org.json.simple.*;
public class PendingRequisitionActivity extends Activity{	
	Button testButton;
	String access_token=null;
	 SharedPreferences preferences;
	 ExpandableListView pending_requisition_listview;
	 UserRequisitionCollection userRequisitionCollection;
	  UserRequisitionAdapter userRequisitionAdapter=null;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_pending_requisitions);
		StrictMode.ThreadPolicy policy = new StrictMode.
		ThreadPolicy.Builder().permitAll().build();
	    StrictMode.setThreadPolicy(policy); 
	    try{
		//testButton = (Button)findViewById(R.id.button1);
		preferences = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
		access_token = preferences.getString("access_token", null);
		
		pending_requisition_listview = (ExpandableListView) findViewById(R.id.pending_requisition_listview);
		userRequisitionCollection =loadAllPendingRequisitions(access_token,Configs.api_root_url);
		userRequisitionAdapter = new UserRequisitionAdapter(this,userRequisitionCollection);
		pending_requisition_listview.setAdapter(userRequisitionAdapter);
		pending_requisition_listview.setOnChildClickListener(new OnChildClickListener() {
            public boolean onChildClick(ExpandableListView parent, View v,
                    int groupPosition, int childPosition, long id) {
                final String selected = (String) userRequisitionAdapter.getChild(
                        groupPosition, childPosition);
                Toast.makeText(getBaseContext(), selected, Toast.LENGTH_LONG).show();
                return true;
            }
        });
		}//try
	    catch(Exception e){
			System.out.println("Exception occured..."+e);
			e.printStackTrace();
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.pending_activity_menu, menu);
		return true;
	}
	
	public boolean onOptionsItemSelected(MenuItem item) {
		Intent i;
		 switch (item.getItemId()) {
		 case R.id.approve_all_requisitions : {
			 approveAllRequisition();
			 userRequisitionCollection.employees=new ArrayList<String>();
			 userRequisitionCollection.requisitionCollection = new HashMap<String,ArrayList<Requisition>>();
			 userRequisitionAdapter = new UserRequisitionAdapter(this,userRequisitionCollection);
		     pending_requisition_listview.setAdapter(userRequisitionAdapter);
			// notifyAll();
			 return true;
		 }
		   case R.id.view_all_approved_requisitions :{
			   i = new Intent(this,ApprovedRequisitionActivity.class);				  
				  startActivity(i);
			      return true;
		   }
		   case R.id.view_all_rejected_requisitions :{
			   i = new Intent(this,RejectedRequisitionActivity.class);				  
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


	public String approveAllRequisition(){
		try{
			List<NameValuePair> params = new ArrayList<NameValuePair>();
			params.add(new BasicNameValuePair("access_token", access_token));
			params.add(new BasicNameValuePair("method", "approveAllRequisition"));
			ArrayList<RequisitionId> requisitionIds = new ArrayList<RequisitionId>();
			org.json.simple.JSONArray jsonArray = new org.json.simple.JSONArray();

			for(int i = 0;i<userRequisitionCollection.employees.size();i++){
				for(int j=0;j<userRequisitionCollection.requisitionCollection.get(userRequisitionCollection.employees.get(i)).size();j++){
					int reqId = userRequisitionCollection.requisitionCollection.get(userRequisitionCollection.employees.get(i)).get(j).getReqId();
				  //  requisitionIds.add(new RequisitionId(reqId));
					org.json.simple.JSONObject jObj = new org.json.simple.JSONObject();
					jObj.put("reqId", reqId);
				    jsonArray.add(jObj);
				    
				}//inner for
			}//outer for
		
			params.add(new BasicNameValuePair("requisitionIds", jsonArray.toJSONString()));
			String result = Helper.loadJSONString(params);
			return "success";
		}catch(Exception e){
			e.printStackTrace();
			return "fail";
			
		}
	}//approveAllRequisition
	
	
	public UserRequisitionCollection loadAllPendingRequisitions(String access_token,String url){
		UserRequisitionCollection userRequisitionCollection = new UserRequisitionCollection();
		
		
		
		ArrayList<UserRequisition> allUserRequisitions = new ArrayList<UserRequisition>();
		try{
			 List<NameValuePair> params = new ArrayList<NameValuePair>();
				params.add(new BasicNameValuePair("access_token", access_token));
				params.add(new BasicNameValuePair("method", "showAllPendingRequisition"));
		   JSONArray json = new JSONArray(Helper.loadJSONString(params));
		   for(int i=0;i<json.length();i++){
			  ArrayList<Requisition> requisitions = new ArrayList<Requisition>();
			  JSONObject jsonObject = new JSONObject(json.getString(i));
			  String empName = jsonObject.getString("EmployeeName");
			  userRequisitionCollection.employees.add(empName);
			   JSONArray reqArray = new JSONArray(jsonObject.getString("UserRequisitions"));
			   for(int j=0;j<reqArray.length();j++){
				   JSONObject reqObject = new JSONObject(reqArray.getString(j));
				   Requisition requisition = new Requisition();
				   requisition.setReqId(reqObject.getInt("ReqId"));
				   requisition.setStationaryName(reqObject.getString("StationaryName"));
				   requisition.setQuantity(reqObject.getInt("Quantity"));
				   requisition.setRequisitionDate(reqObject.getString("RequisitionDate"));
				   requisitions.add(requisition);
			   }//inner for
			   userRequisitionCollection.requisitionCollection.put(empName, requisitions);
		   }//outer for
		 }catch(Exception e){
			// System.out.println("Exception occured:"+e.getMessage());
			 Log.e("error =>> ", "can't load pending requisition" + e.toString());
		 }
		return userRequisitionCollection;
	}
	
	public class RequisitionId{
		private int reqId;
       public RequisitionId(int reqId){
    	   this.reqId = reqId;
       }
		public int getReqId() {
			return reqId;
		}

		public void setReqId(int reqId) {
			this.reqId = reqId;
		}
		
	}//RequisitionId
}
