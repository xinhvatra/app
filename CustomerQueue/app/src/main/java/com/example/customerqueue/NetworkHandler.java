package com.example.customerqueue;

import android.app.IntentService;
import android.content.Intent;
import android.util.Log;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.Socket;

import androidx.annotation.Nullable;

public class NetworkHandler extends IntentService {
    /**
     * @param name
     * @deprecated
     */
    public NetworkHandler(String name) {
        super(name);
    }

    @Override
    protected void onHandleIntent(@Nullable Intent intent) {
        Thread thread = new Thread(new Runnable() {

            private String stringData = null;

            @Override
            public void run() {

                try {

                    Socket s = new Socket("10.0.2.2", 9998);
                    Log.d("==================","ket noi den server ok"+s.getRemoteSocketAddress());
                    //   OutputStream out = s.getOutputStream();
                    //   PrintWriter output = new PrintWriter(out);
                    //  output.println("");
                    //  output.flush();
                    // while (true) {
                    // InputStream input = s.getInputStream();
                    //  PrintWriter   output = new PrintWriter( new BufferedWriter( new OutputStreamWriter(s.getOutputStream())),true);
                    //  output.println("test");
                    while (true){
                        BufferedReader input = new BufferedReader(new InputStreamReader(s.getInputStream()));
                        final String st = input.readLine();
                        Log.d("==================", st);
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                    Log.d("==================","ket noi den server khong ok ty nao");
                }
            }

        });
        thread.start();
    }
}
