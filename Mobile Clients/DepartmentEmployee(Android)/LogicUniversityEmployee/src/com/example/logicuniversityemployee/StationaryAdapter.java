package com.example.logicuniversityemployee;

import java.util.ArrayList;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

public class StationaryAdapter  extends ArrayAdapter<Stationary> {
	//ArrayList<Stationary> stationaries=null;
	
	@Override
	  public void notifyDataSetChanged() {
	    super.notifyDataSetChanged();
	  }
	
	public StationaryAdapter(Context context, ArrayList<Stationary> stationaries) {
		super(context, 0,stationaries);
		//this.stationaries = stationaries;
		//this.stationaries = stationaries;
		try{
		 }catch(Exception e){
			 System.out.println("ERROR DDD::"+e.getMessage());
		  }
	}
	
	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		  View rowView=null;
		  try{
		Activity activity = (Activity) getContext(); 
        LayoutInflater inflater = activity.getLayoutInflater();
       rowView = inflater.inflate(R.layout.stationary_row,null);
        Stationary stationary = getItem(position);
        TextView stationaryName = (TextView)rowView.findViewById(R.id.stationary_name);
        stationaryName.setText(stationary.getStationaryName());
		  }catch(Exception e){
			 System.out.println("ERROR::"+e.getMessage());
		  }
		return rowView;
	}

}
