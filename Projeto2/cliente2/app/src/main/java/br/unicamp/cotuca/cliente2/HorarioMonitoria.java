package br.unicamp.cotuca.cliente2;

import java.sql.Time;

/**
 * Created by u16164 on 27/04/2018.
 */

public class HorarioMonitoria {
    private String ra;
    private String diaDaSemana;
    private Time horario;

    private String lab;

    public HorarioMonitoria(String ra, String diaDaSemana, Time horario, String lab) throws Exception{
        this.setRa(ra);
        this.setDiaDaSemana(diaDaSemana);
        this.setHorario(horario);
        this.setLab(lab);
    }

    public HorarioMonitoria(String ra, String diaDaSemana, String horario, String lab) throws Exception{
        this.setRa(ra);
        this.setDiaDaSemana(diaDaSemana);
        this.setHorario(horario);
        this.setLab(lab);
    }

    public HorarioMonitoria() {}

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

    public void setHorario(String horario) throws Exception {
        if (horario == null || horario == "")
            throw new Exception("Horário nulo");

        this.horario = new Time(Integer.parseInt(horario.substring(0,2)), Integer.parseInt(horario.substring(3)), 0);
    }

    public String getLab() {
        return lab;
    }

    public void setLab(String lab) throws Exception{
        if (lab == null || lab == "")
            throw new Exception("Laboratório nulo");

        this.lab = lab;
    }

    public String toString ()
    {
        return diaDaSemana + " - "  + horario.toString() + " - " + lab;
    }
}
