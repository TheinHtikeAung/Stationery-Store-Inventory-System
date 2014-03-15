package storeclerk;

import android.annotation.TargetApi;
import android.app.Activity;
import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.drawable.ColorDrawable;
import android.os.Build;
import android.os.Bundle;
import android.os.StrictMode;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Window;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.AutoCompleteTextView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.logicuniversity.R;
import com.logicuniversity.Util;

@TargetApi(Build.VERSION_CODES.GINGERBREAD)
public class ReportDiscrepancy_Dialog extends Activity implements TextWatcher, OnItemClickListener, OnClickListener{
	
	private ReportDiscrepancy_Helper helper;
	
	private AutoCompleteTextView myAutoComplete;
	private EditText txtQty;
	private EditText txtRemark;
	private TextView lblError;
	private Button btnSave;
	
	int stationeryId;
	String refreshNeed="false";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		//setContentView(R.layout.report_discrepancy_activity);
		
		StrictMode.ThreadPolicy policy = new StrictMode.
		ThreadPolicy.Builder().permitAll().build();
		StrictMode.setThreadPolicy(policy);
		
		Util.prefSetUp(this);
		
		helper=new ReportDiscrepancy_Helper();
		helper.JSONtoMStationery();
		stationeryId =Integer.parseInt(getIntent().getStringExtra(Util.STATIONERY_ID));

		
		// Dialog view from Distribute process
		
				Dialog dialog = new Dialog(this);
				
				//Remove title bar
				//this.requestWindowFeature(Window.FEATURE_NO_TITLE);

				//Remove notification bar
				//this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
				
			    dialog.setContentView(R.layout.report_discrepancy_activity);
			    dialog.setTitle("Create new discrepancy item");
			    dialog.setCancelable(true);
			    dialog.setCanceledOnTouchOutside(true);
			    //TextView text = (TextView) dialog.findViewById(R.id.text1);
			    //text.setText("Message");
			    
			    //Transperent Dialog
			    //dialog.getWindow().setBackgroundDrawable(new ColorDrawable(android.graphics.Color.TRANSPARENT));
			    dialog.getWindow().setBackgroundDrawableResource(R.color.midnight_blue);
				
				txtQty=(EditText) dialog.findViewById(R.id.txtQty);
				txtRemark=(EditText) dialog.findViewById(R.id.txtRemark);
				btnSave=(Button) dialog.findViewById(R.id.btnSave);
				btnSave.setOnClickListener(this);
				lblError=(TextView) dialog.findViewById(R.id.lblError);
				lblError.setVisibility(View.GONE);
				
				myAutoComplete = (AutoCompleteTextView)dialog.findViewById(R.id.autoCompleteTextView1);

				myAutoComplete.addTextChangedListener(this);
				myAutoComplete.setOnItemClickListener(this);
				
				myAutoComplete.setAdapter(new ArrayAdapter<String>(this, android.R.layout.simple_dropdown_item_1line, helper.nameArray));

			    dialog.show();
			    //
			   dialog.setOnCancelListener(new DialogInterface.OnCancelListener() {

			    public void onCancel(DialogInterface arg0) {
			    	Intent i = getIntent();
			    	i.putExtra(Util.REFRESH_NEED, refreshNeed);
			    	setResult(RESULT_OK, i);
			        finish();
			    }

			   });
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		//getMenuInflater().inflate(R.menu.report_discrepancy, menu);
		return true;
	}

	@Override
	public void afterTextChanged(Editable arg0) {
		// TODO Auto-generated method stub
		Log.i("after text changed","testing");
		
	}

	@Override
	public void beforeTextChanged(CharSequence arg0, int arg1, int arg2,
			int arg3) {
		Log.i("before text changed","testing");
	}

	@Override
	public void onTextChanged(CharSequence arg0, int arg1, int arg2, int arg3) {
		Log.i("text changed","testing");
	}

	@Override
	public void onItemClick(AdapterView<?> arg0, View arg1, int arg2, long arg3) {
		String s = ((TextView) arg1).getText().toString();
		
		helper.getArrayIndex(s);
        Log.e("Test", s + " position: " + helper.getArrayIndex(s));
	}

	@Override
	public void onClick(View v) {
		final int id = v.getId();
	    switch (id) {
		    case R.id.btnSave:
		    {	
		    	if(!txtQty.getText().toString().equals("") && helper.selectedId>0)
		    	{
		    		String result = helper.createNewDiscrepancy(txtQty.getText().toString(), txtRemark.getText().toString());
		    		
		    		txtQty.setText("");
		    		myAutoComplete.setText("");
		    		txtRemark.setText("");
		    		lblError.setText("Successfully recorded as discrepancy item.");
		    		lblError.setVisibility(View.VISIBLE);
		    		
		    		if(stationeryId==helper.selectedId)
		    		{
		    			refreshNeed="true";
		    			helper.selectedId=-1;
		    		}
		    		
		    	}else{
		    		lblError.setText("* Please fill stationery name and quantity.");
		    		lblError.setVisibility(View.VISIBLE);
		    	}
		    	
		        break;
		    }
	    }
	}
}
