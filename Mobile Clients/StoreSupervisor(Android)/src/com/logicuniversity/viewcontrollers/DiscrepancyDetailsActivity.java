package com.logicuniversity.viewcontrollers;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import android.app.Activity;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.logicuniversity.storesupervisor.R;
import com.logicuniversity.util.Helper;

public class DiscrepancyDetailsActivity extends Activity implements OnClickListener{
	
	TextView stationery_name;
	TextView remark;
	TextView quantity;
	TextView total;
	TextView price;
	TextView date;
	
	//Button btn_back;
	//Button btn_approve;
	
	String discrepency_id;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R .layout.activity_discripency);
	
		Bundle extras = getIntent().getExtras();
		if(extras == null)
			return;
		
		stationery_name = (TextView) findViewById(R.id.d_stationery_name);
		remark = (TextView) findViewById(R.id.d_remark);
		quantity = (TextView)findViewById(R.id.textView4);
		total = (TextView)findViewById(R.id.textView5);
		date = (TextView)findViewById(R.id.d_date);
		price = (TextView)findViewById(R.id.d_price);
		//btn_back = (Button)findViewById(R.id.btn_back);
		//btn_approve = (Button)findViewById(R.id.btn_approve);
		//btn_back.setOnClickListener(this);
		//btn_approve.setOnClickListener(this);
		
		discrepency_id = extras.getString("id");
		stationery_name.setText(extras.getString("stationery_name"));
		remark.setText(extras.getString("remark"));
		quantity.setText(extras.getString("quantity"));
		total.setText(extras.getString("totalprice"));
		date.setText(extras.getString("datetime_reported"));
		price.setText(extras.getString("price"));
		
		
	}
	
	@Override
	public void onClick(View v) {
		switch(v.getId()){
		}
		
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.descripencydetail_activity_menu, menu);
		return true;
	}
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
	    switch (item.getItemId()) {
	    	case R.id.action_approve_single:
	    		approveDiscrepency();
	    		break;
	    }
	    return super.onOptionsItemSelected(item);
	}
	
	public void approveDiscrepency(){
		SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
		String access_token = preferences.getString("access_token", null);
		List<NameValuePair> params = new ArrayList<NameValuePair>();
		params.add(new BasicNameValuePair("access_token", access_token));
		params.add(new BasicNameValuePair("method", "approve_discrepency"));
		params.add(new BasicNameValuePair("id", discrepency_id));
		
		String jsonString = Helper.loadJSONString(params);
		Toast.makeText(getApplicationContext(), Helper.commandExecutionStatusMessage(jsonString), Toast.LENGTH_LONG).show();
		
		super.finish();
	}
}
