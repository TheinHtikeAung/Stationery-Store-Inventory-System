package com.example.logicuniversityemployee;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicHeader;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.protocol.HTTP;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import android.os.Bundle;
import android.os.StrictMode;
import android.preference.PreferenceManager;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.AutoCompleteTextView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class MainActivity extends Activity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        StrictMode.ThreadPolicy policy = new StrictMode.
        ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy); 
        ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,android.R.layout.simple_dropdown_item_1line, getAllStationary());       
        final AutoCompleteTextView textView = (AutoCompleteTextView)findViewById(R.id.stationary_search);
        textView.setThreshold(1);
        textView.setAdapter(adapter);
        final EditText quantityEditText = (EditText)findViewById(R.id.quantity);
        Button button = (Button)findViewById(R.id.checkStationaryId);
        SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
		final String access_token = preferences.getString("access_token", null);
		
        button.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
            	int errorCode = -1;
            	String qtyString = quantityEditText.getText().toString();
            	String stationaryName = textView.getText().toString();
            	try{
            		int quantity ;
            		String errorMsg="";
            		if(qtyString.isEmpty() || stationaryName.isEmpty()||(qtyString==""||(stationaryName==""))){
            			errorCode=0;
            			errorMsg = "Quantity and Stationary Name cannot be empty";
            			
            		    Toast toast = Toast.makeText(getApplicationContext(),errorMsg, Toast.LENGTH_LONG);
                		toast.show();
            		}
            		try{
            			quantity = Integer.parseInt(qtyString);
            		}catch(Exception e){
            			errorCode = 1;
            			errorMsg = "Please enter only numeric quantity.";
            			
            		    Toast toast = Toast.makeText(getApplicationContext(),errorMsg, Toast.LENGTH_LONG);
                		toast.show();
            		}
            		if(errorCode==-1){
            		quantity = Integer.parseInt(qtyString);
            		makeRequisition(access_token,stationaryName,quantity);
            		textView.setText("");
            		quantityEditText.setText("");
            		Toast toast = Toast.makeText(getApplicationContext(), "Requisition is successfully sent to Department Head", Toast.LENGTH_LONG);
            		toast.show();}
            		
            		//getStationaryId(textView.getText().toString()); 
            	}catch(Exception e){
            		System.out.println("Exc occured while sending json");
            		e.printStackTrace();
            	}
            }
        }); 
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }
    
    
    public boolean onOptionsItemSelected(MenuItem item) {
		   Intent i;
		   switch (item.getItemId()) {
		   case R.id.pending_requisitions :{
			   i = new Intent(this,PendingRequisitionActivity.class);				  
				  startActivity(i);
			      return true;
		   }
		   case R.id.approved_requisitions :{
			   i = new Intent(this,ApprovedRequisitionActivity.class);				  
				  startActivity(i);
			      return true;
		   }
		   case R.id.rejected_requisitions :{
			   i = new Intent(this,RejectedRequisitionActivity.class);				  
				  startActivity(i);
			      return true;
		   }
		   case R.id.processing_requisitions :{
			   i = new Intent(this,ProcessingRequisitionActivity.class);				  
				  startActivity(i);
			      return true;
		   }
		   case R.id.distributed_requisitions :{
			   i = new Intent(this,DistributedRequisitionActivity.class);				  
				  startActivity(i);
			      return true;
		   }
		   case R.id.logout :{
			   //call logout method
			   logout();
			   i = new Intent(this,LoginActivity.class);				  
				  startActivity(i);
			      return true;
		   }
		   default:
			      return super.onOptionsItemSelected(item);
		   
		   }
     }
    
    
    public void logout(){
    	SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(this);
    	String access_token = preferences.getString("access_token", null);
		 List<NameValuePair> params = new ArrayList<NameValuePair>();
			params.add(new BasicNameValuePair("access_token", access_token));
			params.add(new BasicNameValuePair("method", "logout"));
			 try {
				JSONObject json = new JSONObject(Helper.loadJSONString(params));
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			preferences.edit().remove("access_token").commit();
    }
    
    public ArrayList<String> getAllStationary(){
    	//ArrayList<Stationary> stationaries = new ArrayList<Stationary>();
    	ArrayList<String> stationaries = new ArrayList<String>();
    	try{
    		DefaultHttpClient httpClient = new DefaultHttpClient();
    		
    		
      	  HttpGet httpGet = new HttpGet(Configs.api_root_url+"MRequisition.aspx?method=loadAllStationaries");
      	  HttpResponse response = httpClient.execute(httpGet);
      	  BufferedReader in = new BufferedReader(new InputStreamReader(response.getEntity().getContent()));
      	  StringBuffer sb = new StringBuffer("");
            String line = "";
            String NL = System.getProperty("line.separator");
            while ((line = in.readLine()) != null) {                    
                sb.append(line + NL);
            }
            in.close();
            String result = sb.toString();
            JSONArray jsonArray = new JSONArray(result);
            for(int i=0;i<jsonArray.length();i++){
          	  JSONObject jsonObject = new JSONObject(jsonArray.getString(i));  
          	  String stationaryName = jsonObject.getString("stationery_name");
          	  stationaries.add(stationaryName);
            }//for
    	   }catch(Exception e){
    		   System.out.println("exception occured");
    		   e.printStackTrace();
    	   }
    	return stationaries;
    }
    
    
    
    
    
    
    //public int getStationaryId(JSONObject stationaryName){
    	 public int getStationaryId(String stationaryName){
    	try{
    		DefaultHttpClient httpClient = new DefaultHttpClient();
    		String url = Configs.api_root_url+"MRequisition.aspx?method=getStationaryId&stName="+removeWhiteSpaces(stationaryName);       	 
    		//String url = "http://10.10.2.130/LogicU/Mobile/MRequisition.aspx?method=getStationaryId&stName=";  
        	  HttpPost httpPost = new HttpPost(url);
        	  StringEntity se = new StringEntity(stationaryName.toString());
        	  se.setContentType("application/json;charset=UTF-8");
        	  se.setContentEncoding(new BasicHeader(HTTP.CONTENT_TYPE, "application/json;charset=UTF-8"));
        	  httpPost.setEntity(se);
        	  HttpResponse httpresponse = httpClient.execute(httpPost);
        	  
    	}catch(Exception e){
    		System.out.println("Exception occured in getStationryId");
    		e.printStackTrace();
    	}//catch
    	
    	
    	return 0;
    }//getStationaryId
    	 
    	 
    public String makeRequisition(String accessToken,String stationaryName,int qty){
    	try{   		
    		DefaultHttpClient httpClient = new DefaultHttpClient();
    		String url = Configs.api_root_url+"MRequisition.aspx?method=makeRequisition&accessToken="+accessToken+"&stName="+removeWhiteSpaces(stationaryName)+"&qty="+qty;       	 
        	  HttpPost httpPost = new HttpPost(url);
        	  StringEntity se = new StringEntity(stationaryName.toString());
        	  se.setContentType("application/json;charset=UTF-8");
        	  se.setContentEncoding(new BasicHeader(HTTP.CONTENT_TYPE, "application/json;charset=UTF-8"));
        	  httpPost.setEntity(se);
        	  HttpResponse httpresponse = httpClient.execute(httpPost);
        	  
    	}catch(Exception e){
    		System.out.println("Exception occured in makeRequisition");
    		e.printStackTrace();
    	}//catch
    	
    	return null;
    }	// 
    	 
    
    public String removeWhiteSpaces(String text){   	
    	//text.replace(" ", "_");
    	text = text.replaceAll("\\s","_");
    	System.out.println("RWS:"+text);
    	return text;
    }//removeWhiteSpaces
    
}
