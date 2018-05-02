package br.unicamp.cotuca.cliente;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;

import java.util.List;

public class MainActivity extends AppCompatActivity {
    ListView lvMonitores;
    Button btnBuscar;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        lvMonitores = findViewById(R.id.lvMonitores);
        btnBuscar = findViewById(R.id.btnBuscar);

        if (isOnline())
            buscarMonitores();

    }

    private boolean isOnline () {
        ConnectivityManager cm = (ConnectivityManager)getSystemService
                (Context.CONNECTIVITY_SERVICE);
        NetworkInfo nw = cm.getActiveNetworkInfo();

        if (nw != null && nw.isConnectedOrConnecting())
            return true;

        return false;
    }

    private void buscarMonitores () {
        TaskBD task = new TaskBD();
        task.execute("http://localhost:3000/monitoria");
    }
}
