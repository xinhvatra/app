package com.example.customerqueue;

import android.app.Activity;
import android.app.IntentService;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;

import com.google.android.material.floatingactionbutton.FloatingActionButton;
import com.google.android.material.snackbar.Snackbar;

import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;

import android.os.Handler;
import android.os.Looper;
import android.text.TextUtils;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.View;

import android.view.Menu;
import android.view.MenuItem;
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

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
//        Toolbar toolbar = findViewById(R.id.toolbar);
//        setSupportActionBar(toolbar);
//
//        FloatingActionButton fab = findViewById(R.id.fab);
//        fab.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
//                        .setAction("Action", null).show();
//            }
//        });

        lnButton = (LinearLayout) findViewById((R.id.lnButton));
        lnButton.setVisibility(View.INVISIBLE);
        btRathailong = (Button) findViewById(R.id.btRatot);
        btHailong = (Button) findViewById(R.id.btTot);
        btKhonghailong = (Button) findViewById(R.id.btKhongtot);

        txtCua = (TextView) findViewById(R.id.textCua);
        txtCua.setTextSize(330);

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

        startServerSocket1();

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
                //   Intent intent = new Intent(getApplicationContext(), LodeProcessing.class);
                // startActivity(intent);
                //   overridePendingTransition(R.anim.slide_in_right,R.anim.slide_out_left);
                Toast.makeText(getApplicationContext(), "Quý khách không hài lòng với dịch vụ của Agribank", Toast.LENGTH_SHORT).show();

            }
        });
    }

    private void startServerSocket1() {
        Thread thread = new Thread(new Runnable() {
            private String stringData = null;
            Socket sk;

            @Override
            public void run() {


                while (true) {
                    try {
                        InetAddress ip = InetAddress.getByName("192.168.1.53");
                        ServerSocket s = new ServerSocket(9998, 0, ip);
                        // Log.d("==================","doi client ket noi"+s.getInetAddress());
                        sk = s.accept();
                        BufferedReader input = new BufferedReader(new InputStreamReader(sk.getInputStream()));
                        final String st = input.readLine();
                        // Log.d("==================", st);
                        Handler refresh = new Handler(Looper.getMainLooper());
                        refresh.post(new Runnable() {
                            @Override
                            public void run() {
                                processData(st);
                            }
                        });
                    } catch (IOException e) {
                        try {
                            BufferedReader input = new BufferedReader(new InputStreamReader(sk.getInputStream()));
                            final String st = input.readLine();
                            // Log.d("==================", st);
                            Handler refresh = new Handler(Looper.getMainLooper());
                            refresh.post(new Runnable() {
                                @Override
                                public void run() {
                                    processData(st);
                                }
                            });
                        } catch (IOException i) {

                            //  Log.d("==================", "ket noi den server khong ok ty nao");
                        }
                    }


                }

            }

        });
        thread.start();
    }

    private void startServerSocket() {
        Thread thread = new Thread(new Runnable() {
            private String stringData = null;

            @Override
            public void run() {

                try {
                    while (true) {
                        Socket s = new Socket("192.168.1.35", 9998);
                        //  Log.d("==================","ket noi den server ok"+s.getRemoteSocketAddress());
                        //   OutputStream out = s.getOutputStream();
                        //   PrintWriter output = new PrintWriter(out);
                        //  output.println("");
                        //  output.flush();
                        // while (true) {
                        // InputStream input = s.getInputStream();
                        //  PrintWriter output = new PrintWriter( new BufferedWriter( new OutputStreamWriter(s.getOutputStream())),true);
                        //  output.println("test");
                        // Log.d("==================","ket noi den server ok"+s.getRemoteSocketAddress());

                        // try {
                        BufferedReader input = new BufferedReader(new InputStreamReader(s.getInputStream()));
                        // DataInputStream dt = new DataInputStream((s.getInputStream()));
                        final String st = input.readLine();
                        // input.close();
                       // Log.d("==================", st);
                        Handler refresh = new Handler(Looper.getMainLooper());
                        refresh.post(new Runnable() {
                            @Override
                            public void run() {
                                processData(st);
                            }
                        });

                        // }catch(IOException w){

                        // }
                    }
                } catch (IOException e) {
                    // e.printStackTrace();
                    Handler refresh = new Handler((Looper.getMainLooper()));
                    refresh.post(new Runnable() {
                        @Override
                        public void run() {
                            txtCua.setText("ĐANG BẬN");
                            txtCua.setTextSize(170);
                            txtCua.setTextColor(Color.RED);
                        }
                    });
                }
            }

        });
        thread.start();
    }

    private void updateUI(final String stringData) {

        // Toast.makeText(getApplicationContext(),"Nhận được thông báo từ client",Toast.LENGTH_LONG);
    }

    private void processData(final String stringData) {

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
        //Here you can get the size!
        //    Toast.makeText(getApplicationContext(), txtViewTop.getHeight()+"----"+" ----"+txtViewBot.getHeight() , Toast.LENGTH_LONG).show();


        DisplayMetrics displayMetrics = new DisplayMetrics();
        ((Activity) this).getWindowManager()
                .getDefaultDisplay()
                .getMetrics(displayMetrics);
        int statusBarHeight = (int) Math.ceil(25 * getApplicationContext().getResources().getDisplayMetrics().density);
        final int height = displayMetrics.heightPixels;
        final int width = displayMetrics.widthPixels;
        final int btHeight = height - txtViewTop.getHeight() - txtViewBot.getHeight() - statusBarHeight - 10;
        btKhonghailong.setHeight(btHeight);
        btRathailong.setHeight(btHeight);
        btHailong.setHeight(btHeight);

        btRathailong.setWidth(width / 3);
        btHailong.setWidth(width / 3);
        btKhonghailong.setWidth(width / 3);

        txtCua.setHeight(btHeight);
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