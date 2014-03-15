package com.logicuniversity.viewcontrollers;
 
import com.logicuniversity.storesupervisor.R;

import android.annotation.TargetApi;
import android.app.ActionBar;
import android.app.Activity;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;

@TargetApi(Build.VERSION_CODES.HONEYCOMB)
public class SplashScreen extends Activity {

    // Splash screen timer
    private static int SPLASH_TIME_OUT = 3000;
 
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash);
        ActionBar actionBar=getActionBar();
        actionBar.hide();
        
        //Actual code
       // new ImageLoader().execute();
        
        //To Delay by 3 Seconds
        new Handler().postDelayed(new Runnable() {
            @Override
            public void run() {
                // This method will be executed once the timer is over
                // Start your app main activity
                Intent i = new Intent(SplashScreen.this, LoginActivity.class);
                startActivity(i);
 
                // close this activity
                finish();
            	
            	
            }
        }, SPLASH_TIME_OUT);
    }
}