package br.unicamp.cotuca.cliente2;

/**
 * Created by u16164 on 19/04/2018.
 */

public class Monitor {
    private String nome;
    private String ra;
    private int imagem;

    public Monitor (String nome, String ra, int imagem) throws Exception
    {
        this.setNome(nome);
        this.setRa(ra);
        this.setImagem(imagem);
    }

    public String getRa() {
        return ra;
    }

    public void setRa(String ra) throws Exception{
        if (ra == null || ra.length() != 5)
            throw new Exception("RA nulo ou inválido");
        this.ra = ra;
    }

    public String getNome() {
        return nome;
    }

    public void setNome(String nome) throws Exception{
        if (nome == null || nome.length() > 50)
            throw new Exception("Nome nulo ou grande demais");
        this.nome = nome;
    }

    public int getImagem() {
        return imagem;
    }

    public void setImagem(int imagem) {
        this.imagem = imagem;
    }

    @Override
    public String toString() {
        return nome + " - " + ra;
    }
}
