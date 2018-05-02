package br.unicamp.cotuca.cliente2;

import java.sql.Time;

/**
 * Created by u16164 on 27/04/2018.
 */

public class HorarioMonitoria {
    private String ra;

    public HorarioMonitoria(String ra, String diaDaSemana, Time horario) throws Exception{
        this.setRa(ra);
        this.setDiaDaSemana(diaDaSemana);
        this.setHorario(horario);
    }

    private String diaDaSemana;
    private Time horario;

    public String getRa() {
        return ra;
    }

    public void setRa(String ra) throws Exception{
        if (ra == null || ra.length() != 5)
            throw new Exception("RA nulo ou inválido");

        this.ra = ra;
    }

    public String getDiaDaSemana() {
        return diaDaSemana;
    }

    public void setDiaDaSemana(String diaDaSemana) throws Exception {
        if (diaDaSemana == null || diaDaSemana == "" || diaDaSemana.length() > 7)
            throw new Exception("Dia da semana nulo ou inválido");

        this.diaDaSemana = diaDaSemana;
    }

    public Time getHorario() {
        return horario;
    }

    public void setHorario(Time horario) throws Exception {
        if (horario == null)
            throw new Exception("Horário nulo");

        this.horario = horario;
    }
}
