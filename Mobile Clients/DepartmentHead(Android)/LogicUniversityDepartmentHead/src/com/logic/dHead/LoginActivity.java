package com.logic.dHead;
import java.util.ArrayList;
import java.util.List;
import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONObject;
import android.os.Bundle;
import android.os.StrictMode;
import android.preference.PreferenceManager;
import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
public class LoginActivity extends Activity {
	EditText username_editText;
	EditText password_editText;
	Button loginButton;
	String access_token=null;
	 SharedPreferences preferences;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_login);
		  StrictMode.ThreadPolicy policy = new StrictMode.
		  ThreadPolicy.Builder().permitAll().build();
		  StrictMode.setThreadPolicy(policy); 
		  
		  username_editText = (EditText)findViewById(R.id.username_editText);
		  password_editText = (EditText)findViewById(R.id.password_editText);
		  Button login_Button = (Button)findViewById(R.id.loginButton);
		  preferences = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
		   access_token = preferences.getString("access_token", null);
		   if(access_token!=null){
			   Intent i = new Intent(this,PendingRequisitionActivity.class);
				startActivity(i); 
		   }
		   
		   
		   
		  login_Button.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
                String userName = username_editText.getText().toString();
                String password = password_editText.getText().toString();
                login(userName,password);
               
			}
		});//onClick
		  
		  
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.login, menu);
		return true;
	}
	
	public void login(String username,String password){
		System.out.println("ch3");
		 List<NameValuePair> params = new ArrayList<NameValuePair>();
			params.add(new BasicNameValuePair("username", username));
			params.add(new BasicNameValuePair("password", password));
			params.add(new BasicNameValuePair("method", "get_access_token"));
			
			
			try{
				System.out.println("ch4");
				//provide URL
	          JSONObject json = new JSONObject(Helper.loadJSONString(params));
	          System.out.println("ch5");
	          String access_token = (String)json.get("access_token");
				if(access_token.isEmpty()){
					Toast.makeText(getApplicationContext(), "Invilid Username or Password or you don't have permission to use this application.", Toast.LENGTH_LONG).show();
					return;
				}
				
				SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
				SharedPreferences.Editor editor = preferences.edit();
				editor.putString("access_token",access_token);
				editor.commit();
				Intent i = new Intent(this, PendingRequisitionActivity.class);
				startActivity(i);
			}catch(Exception e){
				Log.e("error =>> ", "can't loginin" + e.toString());
				
			}
			
			
	 }//login

}
