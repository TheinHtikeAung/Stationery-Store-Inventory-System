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

import com.logicuniversity.R;
import com.logicuniversity.Util;
import com.logicuniversity.R.layout;
import com.logicuniversity.R.menu;

import android.os.Build;
import android.os.Bundle;
import android.os.StrictMode;
import android.annotation.TargetApi;
import android.app.Activity;
import android.util.Log;
import android.util.SparseArray;
import android.view.Menu;
import android.widget.ExpandableListView;
import entity.MRetrievalFormMaster_View;
import entity.MRetrievalFormDetails_View;

@TargetApi(Build.VERSION_CODES.GINGERBREAD)
public class RetrievalList extends Activity {
	
	SparseArray<Group> groups = new SparseArray<Group>();
	
	List<MRetrievalFormMaster_View> masterList;
	List<MRetrievalFormDetails_View> detailList;
	RetrievalList_Helper helper;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		StrictMode.ThreadPolicy policy = new StrictMode.
		ThreadPolicy.Builder().permitAll().build();
		StrictMode.setThreadPolicy(policy);
		
		Util.prefSetUp(RetrievalList.this.getApplicationContext());
		
		setContentView(R.layout.retrieval_list_activity);
		setTitle(Util.TITLE_RETRIEVAL);
		
		helper=new RetrievalList_Helper();
		createData();
		
		 ExpandableListView listView = (ExpandableListView) findViewById(R.id.expandableListView1);
		 MyExpandableListAdapter adapter = new MyExpandableListAdapter(this, groups);
		 listView.setAdapter(adapter);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		//getMenuInflater().inflate(R.menu.retrieval_list, menu);
		return true;
	}

	public void createData(){
		
		masterList = helper.JSONtoMRetrievalFormMaster_View();
		
		int i=0;
		for (MRetrievalFormMaster_View master : masterList) {
			Group group = new Group(master);
			List<MRetrievalFormDetails_View> details=helper.JSONtoMRetrievalFormDetails_View(master.id);
			for (MRetrievalFormDetails_View d : details) {
				group.details.add(d);
			}
			
			groups.append(i, group);
			i++;
		}
		
	}
	
	
}
