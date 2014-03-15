package storeclerk;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.params.BasicHttpParams;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.logicuniversity.MainActivity;
import com.logicuniversity.R;
import com.logicuniversity.Util;
import com.logicuniversity.R.layout;
import com.logicuniversity.R.menu;

import entity.MStationery;
import entity.OAuthViewModel;

import android.os.Build;
import android.os.Bundle;
import android.os.StrictMode;
import android.preference.PreferenceManager;
import android.annotation.TargetApi;
import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

public class Login extends Activity implements OnClickListener{

	private EditText txtUsername;
	private EditText txtPassword;
	private TextView lblLoginError;
	private CheckBox chkKeepMe;
	
	SharedPreferences prefs;
	
	@TargetApi(Build.VERSION_CODES.GINGERBREAD)
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.login_activity);
		
		Util.prefSetUp(this);
		
		StrictMode.ThreadPolicy policy = new StrictMode.
		ThreadPolicy.Builder().permitAll().build();
		StrictMode.setThreadPolicy(policy);
		
		prefs = PreferenceManager.getDefaultSharedPreferences(this);
		setTitle(Util.TITLE_APPNAME);
		
		txtUsername=(EditText) findViewById(R.id.txtUsername);
		txtPassword=(EditText) findViewById(R.id.txtPassword);
		lblLoginError=(TextView) findViewById(R.id.lblLoginError);
		lblLoginError.setVisibility(View.GONE);
		chkKeepMe=(CheckBox) findViewById(R.id.chkKeepMe);
		
		// to login if user desire 
		Boolean keepme = prefs.getBoolean(Util.KEY_KEEPME, false);
		if(keepme){
			
			txtUsername.setText(prefs.getString(Util.KEY_USERNAME, ""));
			txtPassword.setText(prefs.getString(Util.KEY_PASSWORD, ""));
			chkKeepMe.setChecked(keepme);
			
			try{
				JSONtoOAuthViewModel(String.valueOf(txtUsername.getText()), String.valueOf(txtPassword.getText()));
			}catch(Exception e)
			{
				Toast.makeText(this, "Please change your ip address.", Toast.LENGTH_LONG).show();
			}
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.login, menu);
		return true;
	}
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
		case R.id.mnuappSetting:
			
			Intent i = new Intent(this, AppPreferencesActivity.class);
			startActivityForResult( i, Util.REQ_APPSETTING);
			
			break;

		default:
			break;
		}

		return true;
	}
	
	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		
		String ip = prefs.getString(Util.KEY_DOMAINIP, "");
		if(!ip.equals(""))
		{
			Util.ip=ip;
			Util.url=Util.protocol+Util.ip+Util.sPath+Util.fPath;
		}
	}
	
	@Override
	public void onClick(View v) {
		final int id = v.getId();
	    switch (id) {
		    case R.id.btnLogin:
		    {	
		    	try{
		    		
		    		JSONtoOAuthViewModel(String.valueOf(txtUsername.getText()), String.valueOf(txtPassword.getText()));
		    		
		    	}catch(Exception e)
		    	{
		    		Toast.makeText(this, "Please change your ip address.", Toast.LENGTH_LONG).show();
		    	}
		    	
		        break;
		    }
	    }
	}

	// Get JSON string from server
			public String getJSONByAuthentication(String username, String password)
			{
				DefaultHttpClient   httpclient = new DefaultHttpClient(new BasicHttpParams());
				HttpPost httppost = new HttpPost(Util.url);
				Log.i("URL: ",Util.url);
				
				List<NameValuePair> postParameters = new ArrayList<NameValuePair>();
				postParameters.add(new BasicNameValuePair("token", ""));
				postParameters.add(new BasicNameValuePair("method", "get_access_token"));
				postParameters.add(new BasicNameValuePair("username", username));
				postParameters.add(new BasicNameValuePair("password", password));
				
				httppost.addHeader("Content-type", "application/x-www-form-urlencoded");

				InputStream inputStream = null;
				String result = null;
				try {
					httppost.setEntity(new UrlEncodedFormEntity(postParameters));
				    HttpResponse response = httpclient.execute(httppost);
				    
				    Log.w("JSON",response.toString());
				    HttpEntity entity = response.getEntity();

				    inputStream = entity.getContent();
				    // json is UTF-8 by default
				    BufferedReader reader = new BufferedReader(new InputStreamReader(inputStream, "UTF-8"), 8);
				    StringBuilder sb = new StringBuilder();

				    String line = null;
				    while ((line = reader.readLine()) != null)
				    {
				       sb.append(line);
				    }
				    result = sb.toString();
				} catch (Exception e) { 
				    // Oops
				}
				finally {
				    try{if(inputStream != null)inputStream.close();}catch(Exception squish){}
				}
				Log.d("Result : ",result);
				
				return result;
			}
			
			public void JSONtoOAuthViewModel(String username, String password)
			{
				String JSON = getJSONByAuthentication(username, password);
				
				if(!JSON.equals("null"))
				{
					try {
					
						JSONObject mObj=new JSONObject(JSON);
						
						// For user setting preferences
						prefs.edit().putString(Util.KEY_USERNAME, txtUsername.getText().toString()).commit();
						prefs.edit().putString(Util.KEY_PASSWORD, txtPassword.getText().toString()).commit();
						prefs.edit().putBoolean(Util.KEY_KEEPME, chkKeepMe.isChecked()).commit();
						prefs.edit().putString(Util.KEY_TOKEN, mObj.getString("token")).commit();
							
						Log.i("Token : ",mObj.getString("token"));
							
						Intent i = new Intent(this, MainActivity.class);
						startActivityForResult(i,Util.REQ_HOME);
						
					} catch (JSONException e) {
						e.printStackTrace();
					} finally{ //return mList;
						
					}
				}else{
					Log.w("Null Json", "JSON NULL");
					lblLoginError.setText("* Invalid Login. Please check username and password.");
		    		lblLoginError.setVisibility(View.VISIBLE);
				}
			}
}
