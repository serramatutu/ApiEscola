package br.unicamp.cotuca.cliente2;

import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.RadioGroup;
import android.widget.TextView;
import android.widget.Toast;

import org.w3c.dom.Text;

import java.util.List;

public class Main2Activity extends AppCompatActivity {
    TextView textView;
    TextView tvHorarios;
    ImageView imgMonitor;
    RadioGroup radioGroup;
    List<HorarioMonitoria> horarios;
    String ra;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main2);

        textView = findViewById(R.id.tvMonitor);
        tvHorarios = findViewById(R.id.tvHorarios);
        imgMonitor = findViewById(R.id.imgMonitor);
        radioGroup = findViewById(R.id.radioGroup);

        Intent intent = getIntent();
        Bundle params = intent.getExtras();
        String msgRa = intent.getStringExtra("ra");
        ra = msgRa;

        imgMonitor.setImageResource(getResources().getIdentifier("_" + ra, "drawable", getPackageName()));
        textView.setText(intent.getStringExtra("nome"));

        radioGroup.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(RadioGroup radioGroup, int i) {
                exibirDados(i);
            }
        });

        if (isOnline()) {
            buscarDados(msgRa);
            exibirDados(0);
        }
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
        MyTask buscaHorarios = new MyTask();

        buscaHorarios.execute();
    }

    private void exibirDados (int i)
    {
        tvHorarios.setText("");

        if (horarios == null)
            return;

        switch (i) {
            case R.id.rdbtnTodos:
                for (int j = 0; j < horarios.size(); j++) {
                    HorarioMonitoria atual = horarios.get(j);
                    if (atual.getRa().equals(ra))
                        tvHorarios.append(atual.toString() + "\n");
                }
                break;
            case R.id.rdbtnSeg:
                for (int j = 0; j < horarios.size(); j++) {
                    HorarioMonitoria atual = horarios.get(j);
                    if (atual.getRa().equals(ra) && atual.getDiaDaSemana().equals("Segunda"))
                        tvHorarios.append(atual.toString() + "\n");
                }
                break;
            case R.id.rdbtnTer:
                for (int j = 0; j < horarios.size(); j++) {
                    HorarioMonitoria atual = horarios.get(j);
                    if (atual.getRa().equals(ra) && atual.getDiaDaSemana().equals("Terça"))
                        tvHorarios.append(atual.toString() + "\n");
                }
                break;
            case R.id.rdbtnQua:
                for (int j = 0; j < horarios.size(); j++) {
                    HorarioMonitoria atual = horarios.get(j);
                    if (atual.getRa().equals(ra) && atual.getDiaDaSemana().equals("Quarta"))
                        tvHorarios.append(atual.toString() + "\n");
                }
                break;
            case R.id.rdbtnQui:
                for (int j = 0; j < horarios.size(); j++) {
                    HorarioMonitoria atual = horarios.get(j);
                    if (atual.getRa().equals(ra) && atual.getDiaDaSemana().equals("Quinta"))
                        tvHorarios.append(atual.toString() + "\n");
                }
                break;
            case R.id.rdbtnSex:
                for (int j = 0; j < horarios.size(); j++) {
                    HorarioMonitoria atual = horarios.get(j);
                    if (atual.getRa().equals(ra) && atual.getDiaDaSemana().equals("Sexta"))
                        tvHorarios.append(atual.toString() + "\n");
                }
                break;
            case R.id.rdbtnSab:
                for (int j = 0; j < horarios.size(); j++) {
                    HorarioMonitoria atual = horarios.get(j);
                    if (atual.getRa().equals(ra) && atual.getDiaDaSemana() .equals("Sábado"))
                        tvHorarios.append(atual.toString() + "\n");
                }
                break;
        }
    }

    private class MyTask extends AsyncTask<String, Void, List<HorarioMonitoria>> {
        @Override
        public List<HorarioMonitoria> doInBackground (String... strings)
        {
            String conteudo = HttpManager.getDados("http://177.220.18.57:3000/monitoria/horarios");

            return HorarioMonitoriaJSONParser.parseDados(conteudo);
        }

        @Override
        public void onPostExecute (List<HorarioMonitoria> result)
        {
            horarios = result;
        }
    }
}
