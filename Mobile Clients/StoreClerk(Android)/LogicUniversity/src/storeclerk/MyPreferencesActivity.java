package storeclerk;

import com.logicuniversity.R;
import com.logicuniversity.Util;
import com.logicuniversity.R.layout;
import com.logicuniversity.R.menu;

import android.os.Bundle;
import android.preference.PreferenceActivity;
import android.app.Activity;
import android.view.Menu;

public class MyPreferencesActivity extends PreferenceActivity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		addPreferencesFromResource(R.xml.preferences);
		setTitle(Util.TITLE_USERSETTING);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		//getMenuInflater().inflate(R.menu.my_preferences, menu);
		return true;
	}

}
