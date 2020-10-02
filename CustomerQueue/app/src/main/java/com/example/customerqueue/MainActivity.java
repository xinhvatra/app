package com.example.customerqueue;

import android.app.Activity;
import android.app.IntentService;
import android.content.Intent;
import android.graphics.Color;
import android.net.wifi.WifiManager;
import android.os.Bundle;

import com.google.android.material.floatingactionbutton.FloatingActionButton;
import com.google.android.material.snackbar.Snackbar;

import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;

import android.os.Handler;
import android.os.Looper;
import android.text.TextUtils;
import android.text.format.Formatter;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.View;

import android.view.Menu;
import android.view.MenuItem;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TabHost;
import android.widget.TextView;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.DataInputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;


public class MainActivity extends AppCompatActivity {
    Button btRathailong, btHailong, btKhonghailong;
    ScrollTextView txtViewTop, txtViewBot;
    LinearLayout lnButton, lnText;
    TextView txtCua, txtKhach;
    int PORT = 9998;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);

        lnButton = (LinearLayout) findViewById((R.id.lnButton));
        lnButton.setVisibility(View.INVISIBLE);
        btRathailong = (Button) findViewById(R.id.btRatot);
        btHailong = (Button) findViewById(R.id.btTot);
        btKhonghailong = (Button) findViewById(R.id.btKhongtot);

        txtCua = (TextView) findViewById(R.id.textCua);
        txtCua.setTextSize(320);

        txtViewTop = (ScrollTextView) findViewById(R.id.txtViewTop);
        txtViewTop.setText("AGRIBANK TỈNH THÁI NGUYÊN KÍNH CHÀO QUÝ KHÁCH!");
        txtViewTop.setTextColor(Color.BLUE);
        txtViewTop.setTextSize(30);
        txtViewTop.startScroll();

        txtViewBot = (ScrollTextView) findViewById(R.id.txtViewBot);
        txtViewBot.setText("XIN CẢM ƠN QUÝ KHÁCH ĐÃ SỬ DỤNG DỊCH VỤ CỦA AGRIBANK!");
        txtViewBot.setTextColor(Color.GREEN);
        txtViewBot.setTextSize(30);
        txtViewBot.startScroll();
//        WifiManager wm = (WifiManager) getApplicationContext().getSystemService(WIFI_SERVICE);
//        IP = Formatter.formatIpAddress(wm.getConnectionInfo().getIpAddress());
//        Toast.makeText(getApplicationContext(),"Ip address: "+IP,Toast.LENGTH_LONG).show();
       // Log.i("==================", IP+"iiiippppppp");
        startServerSocket1();
    }

    private void startServerSocket1() {
        Thread thread = new Thread(new Runnable() {
            private String stringData = null;
            Socket sk;
            @Override
            public void run() {

                while (true) {
                    try {
                       // InetAddress add = InetAddress.getByName(IP);
                        ServerSocket s = new ServerSocket(PORT, 0);
                        Toast.makeText(getApplicationContext(),"doi client ket noi"+s.getInetAddress(),Toast.LENGTH_LONG).show();
                        Log.i("==================","doi client ket noi");
                        sk = s.accept();
                        BufferedReader input = new BufferedReader(new InputStreamReader(sk.getInputStream()));
                        final String st = input.readLine();

                        Handler refresh = new Handler(Looper.getMainLooper());
                        refresh.post(new Runnable() {
                            @Override
                            public void run() {
                                processData(st);
                            }
                        });
                        input.close();
                        sk.close();
                        s.close();
                    } catch (IOException e) {
                            e.printStackTrace();

                    }


                }

            }

        });
        thread.start();
    }


    private void updateUI(final String stringData) {

        // Toast.makeText(getApplicationContext(),"Nhận được thông báo từ client",Toast.LENGTH_LONG);
    }

    private void processData(final String stringData) {
       Log.i("==================","nhan du lieu: "+stringData);
        String[] st = stringData.split(",");
        txtCua.setText(st[0] + "");
        if (st[0].length() <= 1) txtCua.setText("0" + st[0]);
        if (st[1].toString().trim().equals("0")) {
            txtCua.setTextSize(330);
            txtCua.setTextColor(Color.BLUE);
        } else {

            txtCua.setText(st[0] + "\n"+st[1]);
            if (st[0].length() <= 1) txtCua.setText("0" + st[0]+"\n"+st[1]);
            txtCua.setTextSize(180);
            txtCua.setTextColor(Color.RED);
        }
    }

    @Override
    public void onWindowFocusChanged(boolean hasFocus) {
        super.onWindowFocusChanged(hasFocus);

    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }
}