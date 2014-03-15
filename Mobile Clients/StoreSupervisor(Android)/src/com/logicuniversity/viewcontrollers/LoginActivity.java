package com.logicuniversity.viewcontrollers;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONObject;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.os.StrictMode;
import android.os.StrictMode.ThreadPolicy;
import android.preference.PreferenceManager;
import android.util.Log;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.logicuniversity.storesupervisor.R;
import com.logicuniversity.util.Helper;

public class LoginActivity extends Activity implements OnClickListener{
	
	EditText ui_username_textbox;
	EditText ui_password_textbox;
	Button ui_login_button;
	
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_login);
		
		ui_username_textbox = (EditText) findViewById(R.id.ui_username_textbox);
		ui_password_textbox = (EditText) findViewById(R.id.ui_password_textbox);
		ui_login_button = (Button) findViewById(R.id.ui_login_btn);
		
		ui_login_button.setOnClickListener(this);
		
		checkTokenAndRedirect();
	}

	
	public void checkTokenAndRedirect(){
		SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
		
		String access_token = preferences.getString("access_token", null);
		
		if(access_token != null){
			
			List<NameValuePair> params = new ArrayList<NameValuePair>();
			params.add(new BasicNameValuePair("method", "check_access_token"));
			params.add(new BasicNameValuePair("access_token", access_token));
			boolean condition = Helper.commandExecutionStatusCondition(Helper.loadJSONString(params));
			if(condition){
				
				Intent i = new Intent(this, DicripenciesAcitivity.class);
				startActivity(i);
			}
		}
		
	}
	
	@Override
	public void onResume(){
		super.onResume();
		
		checkTokenAndRedirect();
	}
	
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		
		switch (item.getItemId()) {
		case android.R.id.home:
			
			return true;
		}
		return super.onOptionsItemSelected(item);
	}

	@Override
	public void onClick(View v) {
		// TODO Auto-generated method stub
		switch(v.getId()){
			case R.id.ui_login_btn :
				String username = ui_username_textbox.getText().toString();
				String password = ui_password_textbox.getText().toString();
				this.login(username, password);
				break;
			
		}
		
	}
	
	public void login(String username, String password){
		
		List<NameValuePair> params = new ArrayList<NameValuePair>();
		params.add(new BasicNameValuePair("username", username));
		params.add(new BasicNameValuePair("password", password));
		params.add(new BasicNameValuePair("method", "get_access_token"));
		
		try{
			JSONObject json = new JSONObject(Helper.loadJSONString(params));
			
			String access_token = (String)json.get("access_token");
			if(access_token.isEmpty()){
				Toast.makeText(getApplicationContext(), "Invilid Username or Password or you don't have permission to use this application.", Toast.LENGTH_LONG).show();
				return;
			}
			
			SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
			SharedPreferences.Editor editor = preferences.edit();
			editor.putString("access_token",access_token);
			editor.commit();
			
			Intent i = new Intent(this, DicripenciesAcitivity.class);
			startActivity(i);
			
		} catch (Exception e){
			Log.e("error =>> ", "can't loginin" + e.toString());	
		}
		
		
	}
	
	
}
