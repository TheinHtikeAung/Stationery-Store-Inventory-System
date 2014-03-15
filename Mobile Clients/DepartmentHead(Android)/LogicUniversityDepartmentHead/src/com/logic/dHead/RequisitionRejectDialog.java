package com.logic.dHead;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONObject;

import android.annotation.SuppressLint;
import android.app.DialogFragment;
import android.app.FragmentManager;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebView.FindListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;


@SuppressLint("ValidFragment")
public class RequisitionRejectDialog  extends DialogFragment{
	 private Requisition requisition;
	 String access_token=null;
	 SharedPreferences preferences;
	 public RequisitionRejectDialog(Requisition requisition,String access_token){
		 this.requisition = requisition;
		 this.access_token = access_token;
	 } 
	 
	 public void show(FragmentManager fm) {
		 super.show(fm, "rejectRequisition");
	 }
	 
	 @Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		 View view = inflater.inflate(R.layout.requisition_reject_dialog, container);
		final  EditText rejection_reason_editText = (EditText)view.findViewById(R.id.rejection_reason_editText);
		  TextView requisition_rejection_header = (TextView)view.findViewById(R.id.requisition_rejection_header);
		  requisition_rejection_header.setText("Are you sure you want to reject request for "+requisition.getStationaryName()+", quantity:"+requisition.getQuantity()+"?");
		  Button reject_requisition_button = (Button)view.findViewById(R.id.reject_requisition_button);
		  
		  Button cancel_button = (Button)view.findViewById(R.id.cancel_button);
		  //cancel_button
		  reject_requisition_button.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				try{
				String rejectionReason = 	rejection_reason_editText.getText().toString();
			    rejectRequisition(access_token,requisition.getReqId(),rejectionReason);
			    startActivity(new Intent(getActivity(),PendingRequisitionActivity.class));
				}catch(Exception e){
					System.out.println("zexception occured in click of reject button.."+e.getMessage());
				}
				
			}
		});//reject
		  
		  
		  cancel_button.setOnClickListener(new View.OnClickListener() {
				@Override
				public void onClick(View v) {
					
					getDialog().dismiss();
				}
			});//cancel
		  
		  
		return view;
	}
	 
	 
	 
	 public String rejectRequisition(String accessToken,int reqId,String rejectionReason){
		 String result=null;
		 List<NameValuePair> params = new ArrayList<NameValuePair>();
			params.add(new BasicNameValuePair("method", "rejectRequisition"));
			params.add(new BasicNameValuePair("accessToken", accessToken));
			params.add(new BasicNameValuePair("requisitionId", reqId+""));
			params.add(new BasicNameValuePair("reason",rejectionReason));
           try{
        	  // JSONObject json = new JSONObject(Helper.loadJSONString(params,"http://10.10.6.80/LogicU/Mobile/MDepartmentHead.ashx"));
        	   String str=Helper.loadJSONString(params);
   	           //System.out.println("REJECT REQUISITION result::"+str);
        	   
        	   // result = json.getString("result");
        	   
			}catch(Exception e){
				System.out.println("Exception occured in rejectRequisition:"+e.getMessage());
				e.printStackTrace();
			}
		return result;
	}//rejectRequisition
	 
	 
}
