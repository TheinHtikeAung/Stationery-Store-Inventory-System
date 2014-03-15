package com.logic.dHead;

import java.util.ArrayList;
import java.util.List;
import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONObject;
import android.app.Activity;
import android.app.FragmentManager;
import android.content.Context;
import android.content.SharedPreferences;
import android.graphics.Typeface;
import android.preference.PreferenceManager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseExpandableListAdapter;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

public class UserRequisitionAdapter extends BaseExpandableListAdapter{
	String access_token=null;
	SharedPreferences preferences;
	private Activity context;
	private UserRequisitionCollection userRequisitionCollection;
	
	public UserRequisitionAdapter(Activity context,UserRequisitionCollection userRequisitionCollection){
		this.context= context;
		this.userRequisitionCollection= userRequisitionCollection;
		preferences = PreferenceManager.getDefaultSharedPreferences(context);
		   access_token = preferences.getString("access_token", null);
	}
	@Override
	public Object getChild(int groupPosition, int childPosition) {                                                           
		return	userRequisitionCollection.requisitionCollection.get(userRequisitionCollection.getEmployees().get(groupPosition)).get(childPosition);
	}

	@Override
	public long getChildId(int groupPosition, int childPosition) {
		// TODO Auto-generated method stub
		return childPosition;
	}

	@Override
	public View getChildView(int groupPosition, int childPosition,
	    boolean isLastChild, View convertView, ViewGroup parent) {
		
		final Requisition requisition = (Requisition)getChild(groupPosition, childPosition);
		final int grpPos = groupPosition;
		final int childPos= childPosition;
		LayoutInflater inflater = context.getLayoutInflater();
		if (convertView == null) {
			convertView = inflater.inflate(R.layout.child_requisition, null);
		}
		
		TextView stationaryname_textView = (TextView)convertView.findViewById(R.id.stationaryname_textView);
		TextView quantity_textView = (TextView)convertView.findViewById(R.id.quantity_textView);
		TextView date_textView = (TextView)convertView.findViewById(R.id.date_textView);
		
		Button approve_button = (Button)convertView.findViewById(R.id.approve_button);
		Button reject_button = (Button)convertView.findViewById(R.id.reject_button);
		
		stationaryname_textView.setText(requisition.getStationaryName());
		quantity_textView.setText(String.valueOf(requisition.getQuantity()));
		date_textView.setText(requisition.getRequisitionDate());
		
		approve_button.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				//do something
				//System.out.println("approve button is pressed:: requisition id:"+requisition.getReqId());
				
				
				//access_token
				approveRequisition(access_token,requisition.getReqId());
				
				//userRequisitionCollection
				String empName = userRequisitionCollection.employees.get(grpPos);
				userRequisitionCollection.requisitionCollection.get(empName).remove(childPos);
				Toast.makeText(context, "Requisition is approved", Toast.LENGTH_LONG).show();
				
				notifyDataSetChanged();
				
			}//onClick
		});//approve_button
		
		reject_button.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				//do something
				System.out.println("reject button is pressed:: requisition id:"+requisition.getReqId());
				Toast.makeText(context, "Requisition is rejected", Toast.LENGTH_LONG).show();
				//rejectRequisition(access_token, requisition.getReqId());
				
				showRequisitionRejectDialog(requisition,access_token);
				
			}
		});//rejectButton
		
		
		return convertView;
	}//getChildView

	@Override
	public int getChildrenCount(int groupPosition) {
		 return userRequisitionCollection.requisitionCollection.get(userRequisitionCollection.employees.get(groupPosition)).size();
	}//getChildrenCount

	@Override
	public Object getGroup(int groupPosition) {
		return userRequisitionCollection.employees.get(groupPosition);
	}//getGroup

	@Override
	public int getGroupCount() {
		return userRequisitionCollection.employees.size();
	}//getGroupCount

	@Override
	public long getGroupId(int groupPosition) {
		return groupPosition;
	}

	@Override
	public View getGroupView(int groupPosition, boolean isExpanded,
			View convertView, ViewGroup parent) {
		String employeeName = (String) getGroup(groupPosition);
		if (convertView == null) {
            LayoutInflater infalInflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            convertView = infalInflater.inflate(R.layout.group_requisition,null);
        }
		
		TextView group_requisition_employeeName = (TextView)convertView.findViewById(R.id.group_requisition_employeeName);
		group_requisition_employeeName.setText(employeeName);
		group_requisition_employeeName.setTypeface(null,Typeface.BOLD);
		
		return convertView;
	}

	@Override
	public boolean hasStableIds() {
		// TODO Auto-generated method stub
		return true;
	}

	@Override
	public boolean isChildSelectable(int groupPosition, int childPosition) {
		// TODO Auto-generated method stub
		return true;
	}
	
	
	
	public String approveRequisition(String accessToken,int reqId){
		
		    List<NameValuePair> params = new ArrayList<NameValuePair>();
			params.add(new BasicNameValuePair("method", "approveRequisition"));
			params.add(new BasicNameValuePair("requisitionId", reqId+""));
			params.add(new BasicNameValuePair("accessToken", accessToken));
			try{
				String result =Helper.loadJSONString(params);
	  	       // System.out.println("approve requisition result:"+result);
			}catch(Exception e){
				//System.out.println("Exception occured in approveRequisition:"+e.getMessage());
			}
			
		return null;
	}//approveRequisition
	
	
	
	public void showRequisitionRejectDialog(Requisition requisition, String accessToken){
	   FragmentManager fm = context.getFragmentManager();
	   RequisitionRejectDialog requisitionRejectDialog = new RequisitionRejectDialog(requisition,accessToken);
	   requisitionRejectDialog.show(fm);
		 
	}//showRequisitionRejectDialog
	
	
	
	public String rejectRequisition(String accessToken,int reqId){
		 List<NameValuePair> params = new ArrayList<NameValuePair>();
			params.add(new BasicNameValuePair("method", "rejectRequisition"));
			params.add(new BasicNameValuePair("accessToken", accessToken));
			params.add(new BasicNameValuePair("requisitionId", reqId+""));
           try{
        	   JSONObject json = new JSONObject(Helper.loadJSONString(params));
  	           String result = json.getString("result");
        	   
			}catch(Exception e){
				System.out.println("Exception occured in rejectRequisition:"+e.getMessage());
			}
		return null;
	}//rejectRequisition	
}
