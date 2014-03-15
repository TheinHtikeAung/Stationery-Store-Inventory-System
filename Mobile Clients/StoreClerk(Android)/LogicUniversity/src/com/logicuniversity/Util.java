package com.logicuniversity;

import java.util.List;

import android.app.Application;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;

import entity.MFulfillment_View;

public class Util extends Application{
	
	static SharedPreferences prefs;

	public static String protocol="http://";
	public static String ip="1.1.1.1";
	public static String sPath="";
	public static String fPath="/api/mfulfillment.ashx";
	
	public static String url=protocol+ip+sPath+fPath;
	
	public static String KEY_DEPTID="id";
	public static String KEY_DEPTNAME="departmentName";
	
	public static String KEY_USERNAME="username";
	public static String KEY_PASSWORD="password";
	public static String KEY_KEEPME="keepme";
	public static String KEY_TOKEN="token";
	public static String KEY_DOMAINIP="domainIP";
	public static String VAL_TOKEN="";
	
	public static MFulfillment_View mf;
	
	public static String REFRESH_NEED="REFRESH_NEED";
	public static String STATIONERY_ID="stationeryId";
	
	public static int REQ_DIALOG=1;
	public static int REQ_ITEMDETAIL=2;
	public static int REQ_HOME=3;
	public static int REQ_FULFILLMENT=3;
	public static int REQ_DISCREPANCY=4;
	public static int REQ_RETRIEVAL=5;
	public static int REQ_USERSETTING=6;
	public static int REQ_APPSETTING=7;
	
	public static String TITLE_APPNAME="Logic University";
	public static String TITLE_USERSETTING="User Setting";
	public static String TITLE_APPSETTING="App Setting";
	public static String TITLE_FULFILLMENT="Requisition Fulfillment";
	public static String TITLE_RETRIEVAL="Retrieval List";
	
	public static String alignText(int i)
	{
		String result=null;
		
		String str=String.valueOf(i);
		if(str.length()==1)
			result="    "+str;
		else if(str.length()==2)
			result="   "+str;
		else if(str.length()==3)
			result="  "+str;
		else
			return str;
			
		return result;
	}
	
	public static void prefSetUp(Context context)
	{
		prefs = PreferenceManager.getDefaultSharedPreferences(context);
		
		Util.VAL_TOKEN = prefs.getString(Util.KEY_TOKEN, "");
		
		String ip = prefs.getString(Util.KEY_DOMAINIP, "");
		if(!ip.equals(""))
		{
			Util.ip=ip;
			Util.url=Util.protocol+Util.ip+Util.sPath+Util.fPath;
		}
	}
}
