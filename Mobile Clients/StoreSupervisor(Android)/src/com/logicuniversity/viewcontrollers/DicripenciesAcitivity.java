package com.logicuniversity.viewcontrollers;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;
import org.json.JSONObject;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.util.Log;
import android.view.ContextMenu;
import android.view.ContextMenu.ContextMenuInfo;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.AdapterContextMenuInfo;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.Toast;

import com.logicuniversity.storesupervisor.R;
import com.logicuniversity.models.DiscrepencyView;
import com.logicuniversity.util.Helper;

public class DicripenciesAcitivity extends Activity implements OnItemClickListener{

	ArrayList<DiscrepencyView> discrepencies;
	ListView listview;
	SimpleAdapter adapter;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R .layout.activity_dicripencies);
		
		listview = (ListView) findViewById(R.id.listview);
		
		listview.setOnItemClickListener(this);
		
		registerForContextMenu(listview);
		
		//loginCheck();
		
	}
	
	/*private void loginCheck() {
	
		SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
		
		String access_token = preferences.getString("access_token", null);
		
		boolean to_login = true;
		if(access_token != null){
			List<NameValuePair> params = new ArrayList<NameValuePair>();
			params.add(new BasicNameValuePair("method", "check_access_token"));
			params.add(new BasicNameValuePair("access_token", access_token));
			
			boolean condition = Helper.commandExecutionStatusCondition(Helper.loadJSONString(params));
			
			if(condition){
				to_login = false;
			}
		}
		
		if(to_login){
			Log.e("cp8", "abcd");
			Intent l = new Intent(this, LoginActivity.class);
			startActivity(l);
			return;
		}
	}*/

	private void loadListView() {
		SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
		String access_token = preferences.getString("access_token", null);
		
		List<NameValuePair> params = new ArrayList<NameValuePair>();
		params.add(new BasicNameValuePair("access_token", access_token));
		params.add(new BasicNameValuePair("method", "get_descrepencies"));
//		/Log.e("token =>" , access_token);
		try{
			JSONArray jsonArr = new JSONArray(Helper.loadJSONString(params));
			
			discrepencies = new ArrayList<DiscrepencyView>();
			for(int i =0; i < jsonArr.length(); i++){
				DiscrepencyView dv = new DiscrepencyView();
				String st = jsonArr.getString(i);
				JSONObject json = new JSONObject(st);
				
				dv.put("id", json.getString("id"));
				dv.put("stationery_id" , json.getString("stationery_id"));
				dv.put("stationery_name" , json.getString("stationery_name"));
				dv.put("quantity" , json.getString("quantity"));
				dv.put("remark" , json.getString("remark"));
				dv.put("datetime_reported" , json.getString("datetime_reported"));
				dv.put("price" , json.getString("price"));
				dv.put("totalprice" , json.getString("totalprice"));
				
				discrepencies.add(dv);
			}
			
		} catch (Exception e){
			
		}
		
		adapter = new SimpleAdapter(this,discrepencies,
				R.layout.row_discrepency,
				new String[]{"stationery_name", "remark", "totalprice"},
				new int[]{ R.id.firstLine, R.id.secondLine, R.id.totalLine}
		);
		
	    listview.setAdapter(adapter);
		
	}

	@Override
	protected void onResume() {
	    super.onResume();
	    loadListView();
	}
	
	@Override
	  public void onItemClick(AdapterView<?> arg0, View arg1, int position, long arg3) {

			DiscrepencyView d = (DiscrepencyView) listview.getItemAtPosition(position);
			moveToDetailScreen(d);
	  }
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
		String access_token = preferences.getString("access_token", null);
		
	    switch (item.getItemId()) {
	    	case R.id.action_approveall:
	    		int c = discrepencies.size();
	    		
				List<NameValuePair> params = new ArrayList<NameValuePair>();
				params.add(new BasicNameValuePair("access_token", access_token));
				params.add(new BasicNameValuePair("method", "approve_discrepency"));
				
				for(int i=0; i < c; i++){
	    			params.add(new BasicNameValuePair("id", discrepencies.get(i).get("id")));
	    		}
				
				String jsonString = Helper.loadJSONString(params);
				Toast.makeText(getApplicationContext(), Helper.commandExecutionStatusMessage(jsonString), Toast.LENGTH_LONG).show();
				
				loadListView();
	    		break;
	    	case R.id.action_logout:
	    		List<NameValuePair> params1 = new ArrayList<NameValuePair>();
	    		params1.add(new BasicNameValuePair("access_token", access_token));
				params1.add(new BasicNameValuePair("method", "logout"));
				
	    		boolean condition = Helper.commandExecutionStatusCondition(Helper.loadJSONString(params1));
				if(condition){
					Toast.makeText(getApplicationContext(), "Successfully Logout.", Toast.LENGTH_LONG).show();
					
					SharedPreferences.Editor editor = preferences.edit();
					preferences.getString("access_token", null);
					editor.commit();
				}
				
				
				Intent i = new Intent(this, LoginActivity.class);
	    		startActivity(i);
	    		
	    		break;
	    }
	    return super.onOptionsItemSelected(item);
	}
	
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.dicripencies_acitivity, menu);
		return true;
	}
	
	
	public void onCreateContextMenu(ContextMenu menu, View v,
			ContextMenuInfo menuInfo) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.discrepencies_context_menu, menu);
	}
	
	public boolean onContextItemSelected(MenuItem item) {

		AdapterContextMenuInfo adapInfo = (AdapterContextMenuInfo) item
				.getMenuInfo();
		
		DiscrepencyView d = discrepencies.get((int) adapInfo.id);

		switch (item.getItemId()) {
		case R.id.contextmenu_approve:
			
			SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
			String access_token = preferences.getString("access_token", null);
			List<NameValuePair> params = new ArrayList<NameValuePair>();
			params.add(new BasicNameValuePair("access_token", access_token));
			params.add(new BasicNameValuePair("method", "approve_discrepency"));
			params.add(new BasicNameValuePair("id", d.get("id")));
			
			String jsonString = Helper.loadJSONString(params);
			Toast.makeText(getApplicationContext(), Helper.commandExecutionStatusMessage(jsonString), Toast.LENGTH_LONG).show();
			loadListView();
			return true;
		
		case R.id.contextmenu_details:
			moveToDetailScreen(d);
			return true;
			
		default:
			return false;
		}
	}
	
	public void moveToDetailScreen(DiscrepencyView d){
		Intent i = new Intent(this, DiscrepancyDetailsActivity.class);
		
		i.putExtra("id", d.get("id"));
		i.putExtra("stationery_name", d.get("stationery_name"));
		i.putExtra("remark", d.get("remark"));
		i.putExtra("datetime_reported", d.get("datetime_reported"));
		i.putExtra("price", d.get("price"));
		i.putExtra("quantity", d.get("quantity"));
		i.putExtra("totalprice", d.get("totalprice"));
		
		startActivityForResult(i, 1);
		
	}

}
