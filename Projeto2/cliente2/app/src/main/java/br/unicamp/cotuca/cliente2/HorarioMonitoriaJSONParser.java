package br.unicamp.cotuca.cliente2;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by u16164 on 02/05/2018.
 */

public class HorarioMonitoriaJSONParser {
    public static List<HorarioMonitoria> parseDados(String content) {
        try {
            JSONArray jsonArray = new JSONArray(content);
            List<HorarioMonitoria> horarioMonitoriaList = new ArrayList<>();
            for (int i = 0; i < jsonArray.length(); i++) {
                JSONObject jsonObject = jsonArray.getJSONObject(i);
                HorarioMonitoria horarioMonitoria = new HorarioMonitoria();
                try {
                    horarioMonitoria.setRa(jsonObject.getString("RA"));
                    horarioMonitoria.setLab(jsonObject.getString("Lab"));
                    horarioMonitoria.setDiaDaSemana(jsonObject.getString("Dia"));
                    horarioMonitoria.setHorario(jsonObject.getString("Horario").substring(11,16));
                }
                catch (Exception ex) {}
                horarioMonitoriaList.add(horarioMonitoria);
            }
            return horarioMonitoriaList;
        } catch (JSONException e) {
            e.printStackTrace();
            return null;
        }
    }
}
