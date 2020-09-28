package com.example.customerrating;

import android.app.Activity;
import android.graphics.Color;
import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Handler;
import android.os.Looper;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.View;

import android.view.Menu;
import android.view.MenuItem;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.ServerSocket;
import java.net.Socket;


public class MainActivity extends AppCompatActivity {
    Button btRathailong, btHailong, btKhonghailong,btYkienkhac;
    ScrollTextView txtViewTop, txtViewBot;
    LinearLayout lnButton, lnImage;
    TextView txtLabel;

    String IP = "10.27.0.46";
    int PORT = 9999;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);


        lnButton = (LinearLayout) findViewById((R.id.lnButton));
       // lnButton.setVisibility(View.INVISIBLE);
        lnImage = (LinearLayout) findViewById((R.id.lnImage));
        //lnImage.setVisibility(View.INVISIBLE);


        btRathailong = (Button) findViewById(R.id.btRatot);
        btHailong = (Button) findViewById(R.id.btTot);
        btKhonghailong = (Button) findViewById(R.id.btKhongtot);
        btYkienkhac = (Button) findViewById(R.id.btYkienkhac);

        txtLabel = (TextView) findViewById(R.id.txtLabel);
        txtLabel.setTextSize(35);

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
      //  serverConnect();

        //=============CHECK ACTIVE=============================
        btRathailong.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Toast.makeText(getApplicationContext(), "Quý khách rất hài lòng với dịch vụ của Agribank", Toast.LENGTH_SHORT).show();
                // Toast.makeText(getApplicationContext(), txtViewTop.getHeight()+"----"+btHeight+" ----"+txtViewBot.getHeight() , Toast.LENGTH_SHORT).show();

            }
        });

        btHailong.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Toast.makeText(getApplicationContext(), "Quý khách hài lòng với dịch vụ của Agribank", Toast.LENGTH_SHORT).show();
            }
        });


        btKhonghailong.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Toast.makeText(getApplicationContext(), "Quý khách không hài lòng với dịch vụ của Agribank", Toast.LENGTH_SHORT).show();

            }
        });
    }

    private void startServerSocket1() {
        Thread thread = new Thread(new Runnable() {
            Socket sk;

            @Override
            public void run() {

                while (true) {
                    try {
                        // InetAddress add = InetAddress.getByName(IP);
                        ServerSocket s = new ServerSocket(PORT, 0);
                        //  Toast.makeText(getApplicationContext(), "doi client ket noi" + s.getInetAddress(), Toast.LENGTH_LONG).show();
                        Log.i("==================", "doi client ket noi");
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

    private void serverConnect() {

        Thread thread = new Thread(new Runnable() {


            @Override
            public void run() {
                try {
                    Socket sk = new Socket(IP, PORT);
                    BufferedWriter output = new BufferedWriter(new OutputStreamWriter(sk.getOutputStream()));
                    output.write("question");
                    output.flush();
                    Log.i("==================", "send request for getting question");

                    BufferedReader input = new BufferedReader(new InputStreamReader(sk.getInputStream()));
                    final String st = input.readLine();
                    Handler refresh = new Handler(Looper.getMainLooper());
                    refresh.post(new Runnable() {
                        @Override
                        public void run() {
                            processData(st);
                        }
                    });
                } catch (IOException e) {
                }
            }
        });
        thread.start();
    }

    private void updateUI(final String stringData) {

        // Toast.makeText(getApplicationContext(),"Nhận được thông báo từ client",Toast.LENGTH_LONG);
    }

    private void processData(final String stringData) {
        Log.i("==================", "nhan du lieu: " + stringData);
        String[] st = stringData.split("\\(");
        txtLabel.setText(st[0] + "");
        if (st[0].length() <= 1) txtLabel.setText("AGRIBANK TỈNH THÁI NGUYÊN KÍNH CHÀO QUÝ KHÁCH!");
//        if (st[1].toString().trim().equals("0")) {
//            txtLabel.setTextSize(35);
//            txtLabel.setTextColor(Color.BLUE);
//        } else {
//
//            txtLabel.setText(st[0] + "\n" + st[1]);
//            if (st[0].length() <= 1) txtLabel.setText("0" + st[0] + "\n" + st[1]);
//            txtLabel.setTextSize(180);
//            txtLabel.setTextColor(Color.RED);
//        }
    }

    @Override
    public void onWindowFocusChanged(boolean hasFocus) {
        super.onWindowFocusChanged(hasFocus);
        //Here you can get the size!
        //    Toast.makeText(getApplicationContext(), txtViewTop.getHeight()+"----"+" ----"+txtViewBot.getHeight() , Toast.LENGTH_LONG).show();

//
//        DisplayMetrics displayMetrics = new DisplayMetrics();
//        ((Activity) this).getWindowManager()
//                .getDefaultDisplay()
//                .getMetrics(displayMetrics);
//        int statusBarHeight = (int) Math.ceil(25 * getApplicationContext().getResources().getDisplayMetrics().density);
//        final int height = displayMetrics.heightPixels;
//        final int width = displayMetrics.widthPixels;
//        final int btHeight = height - txtViewTop.getHeight() - txtViewBot.getHeight() - statusBarHeight - 10;
//        btKhonghailong.setHeight(btHeight);
//        btRathailong.setHeight(btHeight);
//        btHailong.setHeight(btHeight);
//
//        btRathailong.setWidth(width / 3);
//        btHailong.setWidth(width / 3);
//        btKhonghailong.setWidth(width / 3);

       // txtLabel.setHeight(btHeight);
        //  txtKhach.setHeight(btHeight/2);
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