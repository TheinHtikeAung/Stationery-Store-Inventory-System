package com.example.logicuniversityemployee;
import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.List;
import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.params.BasicHttpParams;
import android.os.StrictMode;
import android.os.StrictMode.ThreadPolicy;
import android.util.Log;
public class Helper {
	public static String loadJSONString(List<NameValuePair> params){
		StrictMode.setThreadPolicy(ThreadPolicy.LAX);
		DefaultHttpClient   httpclient = new DefaultHttpClient(new BasicHttpParams());
		//String url = 
		HttpPost httppost = new HttpPost(Configs.api_root_url+"MDepartmentEmployee.ashx");
		
		httppost.addHeader("Content-type", "application/x-www-form-urlencoded");
		InputStream inputStream = null;
		String result = null;
		try {
			httppost.setEntity(new UrlEncodedFormEntity(params));
		    HttpResponse response = httpclient.execute(httppost);
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
		    Log.e("error ==> ", e.toString());
		}
		finally {
		    try{if(inputStream != null)inputStream.close();}catch(Exception squish){}
		}
		Log.i("jsonResult => ",result.toString());
		return result;
	}
	
}
