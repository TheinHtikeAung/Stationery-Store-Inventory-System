package storeclerk;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.lang.reflect.Array;
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

import android.util.Log;

import com.logicuniversity.Util;

import entity.MRetrievalFormMaster_View;
import entity.MStationery;

public class ReportDiscrepancy_Helper {
	
	public String[] idArray;
	public String[] nameArray;
	public int selectedId;
	
	public final int getArrayIndex(String name) {
	    
	    for (int i = 0; i < nameArray.length; i++) {
	    	
	        if (nameArray[i].equals(name)) {
	        	
	        	selectedId=Integer.parseInt(idArray[i]);
	            return (selectedId);
	        }
	    }
	    return (-1);// didn't find what I was looking for
	}
	
	// Get JSON string from server
		public String getJSONByStationery()
		{
			DefaultHttpClient   httpclient = new DefaultHttpClient(new BasicHttpParams());
			HttpPost httppost = new HttpPost(Util.url);
			Log.i("URL: ",Util.url);
			
			List<NameValuePair> postParameters = new ArrayList<NameValuePair>();
			postParameters.add(new BasicNameValuePair("token", Util.VAL_TOKEN));
			postParameters.add(new BasicNameValuePair("method", "getJSONByStationery"));
			
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
		
		public void JSONtoMStationery()
		{
			String JSON = getJSONByStationery();
			
			List<MStationery> mList=new ArrayList<MStationery>();
			
			try {
				JSONArray JFList = new JSONArray(JSON);
				idArray=new String[JFList.length()];
				nameArray=new String[JFList.length()];
				
				for (int i = 0; i < JFList.length(); i++) {
					
					String  obj=JFList.getString(i);
					JSONObject mObj=new JSONObject(obj);
					
					idArray[i]=String.valueOf(mObj.getInt("id"));
					nameArray[i]=String.valueOf(mObj.getString("stationery_name"));
					
					//MStationery s=new MStationery(mObj.getInt("id"), mObj.getString("stationery_name"));
					//mList.add(s);
					
				}
			} catch (JSONException e) {
				e.printStackTrace();
			} finally{ //return mList;
				
			}
		}
		
		public String createNewDiscrepancy(String quantity, String remark)
		{
			DefaultHttpClient   httpclient = new DefaultHttpClient(new BasicHttpParams());
			HttpPost httppost = new HttpPost(Util.url);
			Log.i("URL: ",Util.url);
			
			List<NameValuePair> postParameters = new ArrayList<NameValuePair>();
			postParameters.add(new BasicNameValuePair("token", Util.VAL_TOKEN));
			postParameters.add(new BasicNameValuePair("method", "createNewDiscrepancy"));
			postParameters.add(new BasicNameValuePair("stationeryId", String.valueOf(selectedId)));
			postParameters.add(new BasicNameValuePair("quantity", quantity));
			postParameters.add(new BasicNameValuePair("remark", remark));
			
			httppost.addHeader("Content-type", "application/x-www-form-urlencoded");

			InputStream inputStream = null;
			String result = null;
			try {
				httppost.setEntity(new UrlEncodedFormEntity(postParameters));
			    HttpResponse response = httpclient.execute(httppost);
			    
			    Log.w("OUTPUT",response.toString());
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
		
}
