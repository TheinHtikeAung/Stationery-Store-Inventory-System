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

import android.app.DownloadManager.Request;
import android.util.Log;

import com.logicuniversity.Util;

import entity.MFulfillment_View;
import entity.MRetrievalFormDetails_View;
import entity.MRetrievalFormMaster_View;

public class RetrievalList_Helper {
	// Get JSON string from server
	public String getJSONByRetrievalMaster()
	{
		DefaultHttpClient   httpclient = new DefaultHttpClient(new BasicHttpParams());
		HttpPost httppost = new HttpPost(Util.url);
		Log.i("URL: ",Util.url);
		
		List<NameValuePair> postParameters = new ArrayList<NameValuePair>();
		postParameters.add(new BasicNameValuePair("token", Util.VAL_TOKEN));
		postParameters.add(new BasicNameValuePair("method", "getJSONByRetrievalMaster"));
		
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
		       sb.append(line + "\n");
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
	
	public List<MRetrievalFormMaster_View> JSONtoMRetrievalFormMaster_View()
	{
		String JSON = getJSONByRetrievalMaster();
		
		List<MRetrievalFormMaster_View> mList=new ArrayList<MRetrievalFormMaster_View>();
		
		try {
			JSONArray JFList = new JSONArray(JSON);
			
			for (int i = 0; i < JFList.length(); i++) {
				
				String  obj=JFList.getString(i);
				JSONObject mObj=new JSONObject(obj);
				
				MRetrievalFormMaster_View master=new MRetrievalFormMaster_View(
						mObj.getInt("id"), mObj.getString("stationery_name"), 
						mObj.getInt("Needed"), mObj.getInt("Retrieved"));
				
				mList.add(master);
				
			}
		} catch (JSONException e) {
			e.printStackTrace();
		} finally{ return mList;}
	}
	
	// Get JSON string from server
		public String getJSONByRetrievalDetail(int stationeryId)
		{
			DefaultHttpClient   httpclient = new DefaultHttpClient(new BasicHttpParams());
			HttpPost httppost = new HttpPost(Util.url);
			Log.i("URL: ",Util.url);
			
			List<NameValuePair> postParameters = new ArrayList<NameValuePair>();
			postParameters.add(new BasicNameValuePair("token", Util.VAL_TOKEN));
			postParameters.add(new BasicNameValuePair("method", "getJSONByRetrievalDetail"));
			postParameters.add(new BasicNameValuePair("stationeryId", String.valueOf(stationeryId)));
			
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
			       sb.append(line + "\n");
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
		
		public List<MRetrievalFormDetails_View> JSONtoMRetrievalFormDetails_View(int stationeryId)
		{
			String JSON = getJSONByRetrievalDetail(stationeryId);
			
			List<MRetrievalFormDetails_View> mList=new ArrayList<MRetrievalFormDetails_View>();
			
			try {
				JSONArray JFList = new JSONArray(JSON);
				
				for (int i = 0; i < JFList.length(); i++) {
					
					String  obj=JFList.getString(i);
					JSONObject mObj=new JSONObject(obj);
					
					MRetrievalFormDetails_View details=new MRetrievalFormDetails_View(
							mObj.getInt("id"), mObj.getString("department_name"), mObj.getInt("Needed"), 
							mObj.getInt("Actual"), mObj.getInt("stationery"));
					
					mList.add(details);
					
				}
			} catch (JSONException e) {
				e.printStackTrace();
			} finally{ return mList;}
		}
}
