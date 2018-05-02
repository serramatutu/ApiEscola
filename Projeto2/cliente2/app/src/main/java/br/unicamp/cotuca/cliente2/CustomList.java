package br.unicamp.cotuca.cliente2;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.List;

/**
 * Created by u16164 on 20/04/2018.
 */

public class CustomList extends ArrayAdapter<Monitor>{

    //Declara os atributos da classe
    private Context context;
    private List<Monitor> monitores = null;

    public CustomList (Context context, List<Monitor> monitores) {
        /* invoca o constructor da superclasse, durante o constructor da subclasse, passando os
        parâmetros contexto, 0 e carros*/
        super(context, 0, monitores);
        this.monitores = monitores;
        this.context = context;
    }

    //Sobrescreve o método getView()
    @Override
    public View getView(int position, View view, ViewGroup parent) {
        //Posição do item em meio aos dados do Adapter
        Monitor monitor = monitores.get(position);
        //Infla o xml lista_itens.xml
        if (view == null)
            view = LayoutInflater.from(context).inflate(R.layout.list_single, null);
        //Cria uma view de imagem
        ImageView imageViewMonitor = (ImageView) view.findViewById(R.id.img);
        imageViewMonitor.setImageResource(monitor.getImagem());
        //Cria uma view de texto
        TextView textViewModeloMonitor = (TextView) view.findViewById(R.id.txt);
        textViewModeloMonitor.setText("     " + monitor.getNome());
        //Retorna o objeto view
        return view;
    }
}
