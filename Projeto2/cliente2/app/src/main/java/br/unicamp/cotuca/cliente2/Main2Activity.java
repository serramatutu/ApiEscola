package br.unicamp.cotuca.cliente2;

import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.TextView;
import android.widget.Toast;

import org.w3c.dom.Text;

public class Main2Activity extends AppCompatActivity {
    TextView textView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R    .layout.activity_main2);

        textView = findViewById(R.id.textView);

        Intent intent = getIntent();
        Bundle params = intent.getExtras();
        String msgRa = intent.getStringExtra("ra");

        if (isOnline())
            buscarDados(msgRa);
        else
            Toast.makeText(this,"Rede não está disponível",Toast.LENGTH_LONG).show();
    }

    private boolean isOnline() {
        ConnectivityManager cm = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo networkInfo = cm.getActiveNetworkInfo();
        if (networkInfo != null && networkInfo.isConnectedOrConnecting()) {
            return true;
        } else {
            return false;
        }
    }

    private void buscarDados (String msgRa)
    {
        textView.setText(msgRa);
    }
}
