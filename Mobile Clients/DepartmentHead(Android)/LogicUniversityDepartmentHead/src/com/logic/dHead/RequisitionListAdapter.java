package com.logic.dHead;
import java.util.ArrayList;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;
public class RequisitionListAdapter extends ArrayAdapter<Requisition>{
	// ArrayAdapter<Requisition> requisitions=null;
	public RequisitionListAdapter(Activity context, ArrayList<Requisition> requisitions) {
		super(context, 0,requisitions);
		//this.requisitions = requisitions;		
	}
	
	@Override
	  public void notifyDataSetChanged() {
	    super.notifyDataSetChanged();
	  }
	
	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		View rowView=null;
		
		try{
			Activity activity = (Activity) getContext(); 
	        LayoutInflater inflater = activity.getLayoutInflater();
	        rowView = inflater.inflate(R.layout.requisition_row,null);
	        Requisition requisition = getItem(position);
	        
	        TextView requisition_stationaryName = (TextView)rowView.findViewById(R.id.requisition_stationaryName);
		     TextView requisition_stationaryQuantity = (TextView)rowView.findViewById(R.id.requisition_stationaryQuantity);
		     TextView requisition_stationaryDate = (TextView)rowView.findViewById(R.id.requisition_stationaryDate);
		     TextView requisition_employeeName = (TextView)rowView.findViewById(R.id.requisition_employeeName);		 
		     requisition_stationaryName.setText(requisition.getStationaryName());
		     requisition_stationaryQuantity.setText(requisition.getQuantity()+"");
		     requisition_stationaryDate.setText(requisition.getRequisitionDate());
		    System.out.println("DATE CHeck::"+requisition.getRequisitionDate());
		     
		     requisition_employeeName.setText(requisition.getEmployeeName());
		}catch(Exception e){
			e.printStackTrace();
		}
		
		return rowView;
	}

}//
