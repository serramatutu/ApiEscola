package br.unicamp.cotuca.cliente2;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;

import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {
    ListView lvMonitores;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        lvMonitores = findViewById(R.id.lvMonitores);

        List<Monitor> monitores = new ArrayList<Monitor>();
        try {
            monitores.add(new Monitor("Felipe \"Pochete\" Martins", "16164", R.drawable._16164));
            monitores.add(new Monitor("Guilherme \"Bronze\" Brandt", "16173", R.drawable._16173));
            monitores.add(new Monitor("Igor Mandello", "16179",R.drawable._16179));
            monitores.add(new Monitor("Lucas \"Hideki\" Dinnouti", "16185", R.drawable._16185));
        }
        catch (Exception ex) { }

        //ArrayAdapter<Monitor> monitoresAdapter = new ArrayAdapter<Monitor>
        //        (this, android.R.layout.simple_list_item_1, monitores);
        CustomList monitoresAdapter = new CustomList(this, monitores);
        lvMonitores.setAdapter(monitoresAdapter);

        lvMonitores.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                Intent intent = new Intent(MainActivity.this, Main2Activity.class);
                Bundle params = new Bundle();
                String msgRa = ((Monitor)adapterView.getItemAtPosition(i)).getRa();

                //params.putString("ra", msgRa);
                intent.putExtra("ra", msgRa);

                intent.putExtras(intent);
                startActivity(intent);
            }
        });
    }
}
