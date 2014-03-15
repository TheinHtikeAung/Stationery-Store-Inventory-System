package com.logicuniversity;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.URI;
import java.net.URISyntaxException;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.StatusLine;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.AbstractHttpEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.params.BasicHttpParams;
import org.apache.http.protocol.HTTP;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import storeclerk.FulfillmentScreen;
import storeclerk.MyPreferencesActivity;
import storeclerk.ReportDiscrepancy;
import storeclerk.RetrievalList;

import android.annotation.TargetApi;
import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Build;
import android.os.Bundle;
import android.os.StrictMode;
import android.preference.PreferenceManager;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;

@TargetApi(Build.VERSION_CODES.GINGERBREAD)
public class MainActivity extends Activity implements OnClickListener {

	SharedPreferences prefs;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		prefs = PreferenceManager.getDefaultSharedPreferences(this);
		setTitle(Util.TITLE_APPNAME);

		StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder()
				.permitAll().build();
		StrictMode.setThreadPolicy(policy);

		setContentView(R.layout.activity_main);

	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

	@Override
	public void onClick(View v) {
		Intent i = null;

		switch (v.getId()) {

		case R.id.btnRetrievalList: {
			i = new Intent(this, RetrievalList.class);
			startActivityForResult(i, Util.REQ_RETRIEVAL);
			break;
		}

		case R.id.btnFulfillment: {
			i = new Intent(this, FulfillmentScreen.class);
			startActivityForResult(i, Util.REQ_FULFILLMENT);
			break;
		}

		case R.id.btnReportDiscrepancy: {
			i = new Intent(this, ReportDiscrepancy.class);
			startActivityForResult(i, Util.REQ_DISCREPANCY);
			break;
		}

		case R.id.btnUserSetting: {
			i = new Intent(this, MyPreferencesActivity.class);
			startActivityForResult(i, Util.REQ_USERSETTING);
			break;
		}
		}
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {

		switch (item.getItemId()) {
		case R.id.mnulogout:

			logout();
			finish();

			break;

		default:
			break;
		}

		return true;

	}

	public void logout() {
		
		String token = prefs.getString(Util.KEY_TOKEN, "");
		prefs.edit().putString(Util.KEY_TOKEN, "").commit();
		
		DefaultHttpClient httpclient = new DefaultHttpClient(
				new BasicHttpParams());
		HttpPost httppost = new HttpPost(Util.url);
		Log.i("URL: ", Util.url);

		List<NameValuePair> postParameters = new ArrayList<NameValuePair>();
		postParameters.add(new BasicNameValuePair("token", token));
		postParameters.add(new BasicNameValuePair("method", "logout"));

		httppost.addHeader("Content-type", "application/x-www-form-urlencoded");

		try {
			httppost.setEntity(new UrlEncodedFormEntity(postParameters));
			httpclient.execute(httppost);

		} catch (Exception e) {
			// Oops
		} 
	}
	
	@Override
	public void onDestroy() {
		
		logout();
		super.onStop();
	}
}
