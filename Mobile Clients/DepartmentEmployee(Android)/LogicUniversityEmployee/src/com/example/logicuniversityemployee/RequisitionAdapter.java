package com.example.logicuniversityemployee;


import java.util.ArrayList;
import java.util.List;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

public class RequisitionAdapter extends ArrayAdapter<Requisition>{
	//public  ArrayList<Requisition> requisitions=null;
	
	@Override
	  public void notifyDataSetChanged() {
	    super.notifyDataSetChanged();
	  }
	public RequisitionAdapter(Activity context, ArrayList<Requisition> requisitions) {
		super(context, 0,requisitions);
		//this.requisitions = requisitions;
	}
	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		  View rowView=null;
		  try{
			  Activity activity = (Activity) getContext(); 
		        LayoutInflater inflater = activity.getLayoutInflater();
		       rowView = inflater.inflate(R.layout.requisition_row,null);
		        
		       Requisition requisition = getItem(position);
		      // System.out.println("check::"+requisition.getStationaryName());
		       
		     TextView requisition_stationaryName = (TextView)rowView.findViewById(R.id.requisition_stationaryName);
		     TextView requisition_stationaryQuantity = (TextView)rowView.findViewById(R.id.requisition_stationaryQuantity);
		     TextView requisition_stationaryDate = (TextView)rowView.findViewById(R.id.requisition_stationaryDate);
		     requisition_stationaryName.setText(requisition.getStationaryName());
		     requisition_stationaryQuantity.setText(requisition.getQuantity()+"");
		     requisition_stationaryDate.setText(requisition.getReqDate());
		     
		  }catch(Exception e){
			  System.out.println("Exception occured in RequisitionADapter: "+e.getMessage());
		  }
		  return rowView;
	}
		  

}
