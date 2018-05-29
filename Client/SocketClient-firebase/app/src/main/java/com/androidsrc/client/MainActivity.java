package com.androidsrc.client;

import android.content.Intent;
import android.os.Bundle;
import android.app.Activity;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

public class MainActivity extends Activity {

	TextView response, txtSlogan;
	Button butnSignup, butnSignin;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);

		response = (TextView) findViewById(R.id.responseTextView);
		txtSlogan= (TextView) findViewById(R.id.txtSlogan);
		butnSignin= (Button) findViewById(R.id.btnSignin);
		butnSignup= (Button) findViewById(R.id.btnSignup);


		//Client myClient = new Client("192.168.1.246", 49001, response);
		//myClient.execute();
		String test = String.valueOf(response.getText());
		//response.setText("");
		//Toast.makeText(this,test,Toast.LENGTH_LONG).show();

		butnSignin.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {

			}
		});

		butnSignin.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				Intent log= new Intent(MainActivity.this,Signin.class);
				startActivity(log);
			}
		});

		butnSignup.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				Intent log= new Intent(MainActivity.this,Signup.class);
				startActivity(log);
			}
		});

	}

}
