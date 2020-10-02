package com.example.customerrating;

import android.app.Activity;
import android.graphics.Color;
import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Handler;
import android.os.Looper;
import android.util.DisplayMetrics;
import android.util.Log;
import android.util.TypedValue;
import android.util.Xml;
import android.view.View;

import android.view.Menu;
import android.view.MenuItem;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Base64;
import java.util.function.BinaryOperator;
import java.util.stream.Stream;


public class MainActivity extends AppCompatActivity {
    Button btRathailong, btHailong, btKhonghailong,btYkienkhac;
    ScrollTextView txtViewTop, txtViewBot;
    LinearLayout lnButton, body;
    TextView txtLabel;

    String IP = "192.168.1.34";
    int PORT = 9999;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);


        lnButton = (LinearLayout) findViewById((R.id.lnButton));
        body = (LinearLayout) findViewById((R.id.body));



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


        final ViewGroup.LayoutParams lp = body.getLayoutParams();
      //  int height = (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, <HEIGHT>, getResources().getDisplayMetrics());
        lp.height=630;
        serverConnect();


        //=============CHECK ACTIVE=============================
        btRathailong.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
              //  Toast.makeText(getApplicationContext(), "Quý khách rất hài lòng với dịch vụ của Agribank", Toast.LENGTH_SHORT).show();
              //   Toast.makeText(getApplicationContext(), statusBarHeight+"----"+txtViewTop.getHeight()+"----"+height+"----"+lp.height+"-----"+txtViewBot.getHeight() , Toast.LENGTH_SHORT).show();

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

//    private void startServerSocket1() {
//        Thread thread = new Thread(new Runnable() {
//            Socket sk;
//
//            @Override
//            public void run() {
//
//                while (true) {
//                    try {
//                        // InetAddress add = InetAddress.getByName(IP);
//                        ServerSocket s = new ServerSocket(PORT, 0);
//                        //  Toast.makeText(getApplicationContext(), "doi client ket noi" + s.getInetAddress(), Toast.LENGTH_LONG).show();
//                        Log.i("==================", "doi client ket noi");
//                        sk = s.accept();
//                        BufferedReader input = new BufferedReader(new InputStreamReader(sk.getInputStream()));
//                        final String st = input.readLine();
//
//                        Handler refresh = new Handler(Looper.getMainLooper());
//                        refresh.post(new Runnable() {
//                            @Override
//                            public void run() {
//                                processData(st);
//                            }
//                        });
//                        input.close();
//                        sk.close();
//                        s.close();
//                    } catch (IOException e) {
//                        e.printStackTrace();
//                        Toast.makeText(getApplicationContext(), "Không kết nối được server!", Toast.LENGTH_SHORT).show();
//                    }
//
//
//                }
//
//            }
//
//        });
//        thread.start();
//    }

    private void serverConnect() {

        Thread thread = new Thread(new Runnable() {


            @Override
            public void run() {
                try {
                    Socket sk = new Socket(IP, PORT);
                    BufferedWriter output = new BufferedWriter(new OutputStreamWriter(sk.getOutputStream()));
                    output.write("question|");
                    output.flush();

                    byte[] inputByte = new byte[1024];
                    BufferedInputStream input = new BufferedInputStream(sk.getInputStream());
                   int read = input.read(inputByte);
                   final String stInput = new String(inputByte,0,read);

                    Handler refresh = new Handler(Looper.getMainLooper());
                    refresh.post(new Runnable() {
                        @Override
                        public void run() {
                            processData(stInput);
                        }
                    });
                } catch (IOException e) {
                    e.printStackTrace();
                    Toast.makeText(getApplicationContext(), "Không kết nối được server!", Toast.LENGTH_SHORT).show();
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
        String[] stQuestion = stringData.split("\\|");
        Log.i("==================", "câu hỏi: " + stQuestion[6]);
        String[] st = stQuestion[6].split("\\{\\{");
        txtLabel.setText(st[0] + "");
        if (st[0].length() <= 1) txtLabel.setText("AGRIBANK TỈNH THÁI NGUYÊN KÍNH CHÀO QUÝ KHÁCH!");
    }

    @Override
    public void onWindowFocusChanged(boolean hasFocus) {
        super.onWindowFocusChanged(hasFocus);
        //Here you can get the size!
        //    Toast.makeText(getApplicationContext(), txtViewTop.getHeight()+"----"+" ----"+txtViewBot.getHeight() , Toast.LENGTH_LONG).show();

//
        DisplayMetrics displayMetrics = new DisplayMetrics();
        ((Activity) this).getWindowManager()
                .getDefaultDisplay()
                .getMetrics(displayMetrics);
        int statusBarHeight = (int) Math.ceil(25 * getApplicationContext().getResources().getDisplayMetrics().density);
        final int height = displayMetrics.heightPixels;
        final int width = displayMetrics.widthPixels;
        final ViewGroup.LayoutParams lp = body.getLayoutParams();
        //  int height = (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, <HEIGHT>, getResources().getDisplayMetrics());
       // lp.height=height-txtViewTop.getHeight()*2-statusBarHeight*2;

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