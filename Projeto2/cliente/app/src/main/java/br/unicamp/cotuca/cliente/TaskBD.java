package br.unicamp.cotuca.cliente;

/**
 * Created by u16164 on 19/04/2018.
 */

public class TaskBD {
    protected String doInBackground (String... params) {
        String conteudo = HttpManager.getDados(params[0]);
        return conteudo;
    }
}
