using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApiEscola.Models;

namespace ClienteEscola
{
    public partial class frmAlterarProjeto : Form
    {
        Projeto p;

        public frmAlterarProjeto(Projeto alterar)
        {
            InitializeComponent();
            p = alterar;
            if (alterar == null)
            {
                MessageBox.Show("Favor escolher um projeto antes de alterar");
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void frmAlterarProjeto_Load(object sender, EventArgs e)
        {
            cbxOrientador.Items.Add(
                                new Professor()
                                {
                                    Id = new System.Guid("816D7F7E-8FFE-43C5-B912-4CC6EB33AAA4"),
                                    Nome = "Andréia Cristina de Souza",
                                    Email = "andreia@cotuca.unicamp.br"
                                });
            cbxOrientador.Items.Add(
                                new Professor()
                                {
                                    Id = new System.Guid("D6C7BBD5-754E-4B25-B7CC-4F938EBE7C5E"),
                                    Nome = "André Luís dos Reis Gomes de Carvalho",
                                    Email = "andre@cotuca.unicamp.br"
                                });
            cbxOrientador.Items.Add(
                                new Professor()
                                {
                                    Id = new System.Guid("215F2BE1-DCCA-4765-91ED-7522C483EBF4"),
                                    Nome = "Simone Pierini Facini Rocha",
                                    Email = "simone@cotuca.unicamp.br"
                                });
            cbxOrientador.Items.Add(
                                new Professor()
                                {
                                    Id = new System.Guid("2DA0759A-2856-418B-BE69-AAB683A5DFAA"),
                                    Nome = "Francisco da Fonseca Rodrigues",
                                    Email = "chico@cotuca.unicamp.br"
                                });
            cbxOrientador.Items.Add(
                                new Professor()
                                {
                                    Id = new System.Guid("C18B5641-19DB-4248-B65E-DCA51785BF50"),
                                    Nome = "Patrícia Gagliardo de Campos",
                                    Email = "patricia@cotuca.unicamp.br"
                                });
            cbxOrientador.Items.Add(
                                new Professor()
                                {
                                    Id = new System.Guid("D9BFACEA-F897-4AA4-A74D-E22CC32D5313"),
                                    Nome = "Márcia Maria Tognetti Corrêa",
                                    Email = "marcia@cotuca.unicamp.br"
                                });
            cbxOrientador.Items.Add(
                                new Professor()
                                {
                                    Id = new System.Guid("1A785680-58D1-4460-8377-EDFFDCAB2406"),
                                    Nome = "Sérgio Luiz Moral Marques",
                                    Email = "sergio@cotuca.unicamp.br"
                                });
            cbxOrientador.Items.Add(
                                new Professor()
                                {
                                    Id = new System.Guid("13977462-0A12-4729-AA42-F46623D890BA"),
                                    Nome = "Samuel Antônio de Oliveira",
                                    Email = "samuel@cotuca.unicamp.br"
                                });
            cbxOrientador.SelectedIndex = 0;

            cbxAluno1.Items.Add("Nulo");
            cbxAluno1.Items.Add(
                            new Aluno()
                            {
                                RA = "16159",
                                Nome = "Alexandre",
                                Email = "ale@gmail.com"
                            });
            cbxAluno1.Items.Add(
                            new Aluno()
                            {
                                RA = "16160",
                                Nome = "Bárbara",
                                Email = "barbara@gmail.com"
                            });
            cbxAluno1.Items.Add(
                            new Aluno()
                            {
                                RA = "16162",
                                Nome = "Bruna",
                                Email = "bruhreal@gmail.com"
                            });
            cbxAluno1.Items.Add(
                            new Aluno()
                            {
                                RA = "16163",
                                Nome = "Davi",
                                Email = "davi@gmail.com"
                            });
            cbxAluno1.Items.Add(
                            new Aluno()
                            {
                                RA = "16164",
                                Nome = "Felipe",
                                Email = "thefemr@gmail.com"
                            });
            cbxAluno1.Items.Add(
                            new Aluno()
                            {
                                RA = "16165",
                                Nome = "Fernanda",
                                Email = "fer@gmail.com"
                            });
            cbxAluno1.Items.Add(
                            new Aluno()
                            {
                                RA = "16166",
                                Nome = "Gabriel Frônio",
                                Email = "frodo@gmail.com"
                            });
            cbxAluno1.Items.Add(
                            new Aluno()
                            {
                                RA = "16167",
                                Nome = "Gabriel Siqueira",
                                Email = "tucao@gmail.com"
                            });
            cbxAluno1.Items.Add(
                            new Aluno()
                            {
                                RA = "16168",
                                Nome = "Gabriel Palotta",
                                Email = "palotera@gmail.com"
                            });
            cbxAluno1.Items.Add(
                            new Aluno()
                            {
                                RA = "16173",
                                Nome = "Guilherme",
                                Email = "guibrandt@gmail.com"
                            });
            cbxAluno1.Items.Add(
                            new Aluno()
                            {
                                RA = "16187",
                                Nome = "Lucas Valente",
                                Email = "lucasvvop@hotmail.com"
                            });
            cbxAluno1.Items.Add(
                            new Aluno()
                            {
                                RA = "16194",
                                Nome = "Vinícius Waki",
                                Email = "wakit@gmail.com"
                            });
            cbxAluno1.SelectedIndex = 0;

            cbxAluno2.Items.Add("Nulo");
            cbxAluno2.Items.Add(
                            new Aluno()
                            {
                                RA = "16159",
                                Nome = "Alexandre",
                                Email = "ale@gmail.com"
                            });
            cbxAluno2.Items.Add(
                            new Aluno()
                            {
                                RA = "16160",
                                Nome = "Bárbara",
                                Email = "barbara@gmail.com"
                            });
            cbxAluno2.Items.Add(
                            new Aluno()
                            {
                                RA = "16162",
                                Nome = "Bruna",
                                Email = "bruhreal@gmail.com"
                            });
            cbxAluno2.Items.Add(
                            new Aluno()
                            {
                                RA = "16163",
                                Nome = "Davi",
                                Email = "davi@gmail.com"
                            });
            cbxAluno2.Items.Add(
                            new Aluno()
                            {
                                RA = "16164",
                                Nome = "Felipe",
                                Email = "thefemr@gmail.com"
                            });
            cbxAluno2.Items.Add(
                            new Aluno()
                            {
                                RA = "16165",
                                Nome = "Fernanda",
                                Email = "fer@gmail.com"
                            });
            cbxAluno2.Items.Add(
                            new Aluno()
                            {
                                RA = "16166",
                                Nome = "Gabriel Frônio",
                                Email = "frodo@gmail.com"
                            });
            cbxAluno2.Items.Add(
                            new Aluno()
                            {
                                RA = "16167",
                                Nome = "Gabriel Siqueira",
                                Email = "tucao@gmail.com"
                            });
            cbxAluno2.Items.Add(
                            new Aluno()
                            {
                                RA = "16168",
                                Nome = "Gabriel Palotta",
                                Email = "palotera@gmail.com"
                            });
            cbxAluno2.Items.Add(
                            new Aluno()
                            {
                                RA = "16173",
                                Nome = "Guilherme",
                                Email = "guibrandt@gmail.com"
                            });
            cbxAluno2.Items.Add(
                            new Aluno()
                            {
                                RA = "16187",
                                Nome = "Lucas Valente",
                                Email = "lucasvvop@hotmail.com"
                            });
            cbxAluno2.Items.Add(
                            new Aluno()
                            {
                                RA = "16194",
                                Nome = "Vinícius Waki",
                                Email = "wakit@gmail.com"
                            });
            cbxAluno2.SelectedIndex = 0;

            cbxAluno3.Items.Add("Nulo");
            cbxAluno3.Items.Add(
                            new Aluno()
                            {
                                RA = "16159",
                                Nome = "Alexandre",
                                Email = "ale@gmail.com"
                            });
            cbxAluno3.Items.Add(
                            new Aluno()
                            {
                                RA = "16160",
                                Nome = "Bárbara",
                                Email = "barbara@gmail.com"
                            });
            cbxAluno3.Items.Add(
                            new Aluno()
                            {
                                RA = "16162",
                                Nome = "Bruna",
                                Email = "bruhreal@gmail.com"
                            });
            cbxAluno3.Items.Add(
                            new Aluno()
                            {
                                RA = "16163",
                                Nome = "Davi",
                                Email = "davi@gmail.com"
                            });
            cbxAluno3.Items.Add(
                            new Aluno()
                            {
                                RA = "16164",
                                Nome = "Felipe",
                                Email = "thefemr@gmail.com"
                            });
            cbxAluno3.Items.Add(
                            new Aluno()
                            {
                                RA = "16165",
                                Nome = "Fernanda",
                                Email = "fer@gmail.com"
                            });
            cbxAluno3.Items.Add(
                            new Aluno()
                            {
                                RA = "16166",
                                Nome = "Gabriel Frônio",
                                Email = "frodo@gmail.com"
                            });
            cbxAluno3.Items.Add(
                            new Aluno()
                            {
                                RA = "16167",
                                Nome = "Gabriel Siqueira",
                                Email = "tucao@gmail.com"
                            });
            cbxAluno3.Items.Add(
                            new Aluno()
                            {
                                RA = "16168",
                                Nome = "Gabriel Palotta",
                                Email = "palotera@gmail.com"
                            });
            cbxAluno3.Items.Add(
                            new Aluno()
                            {
                                RA = "16173",
                                Nome = "Guilherme",
                                Email = "guibrandt@gmail.com"
                            });
            cbxAluno3.Items.Add(
                            new Aluno()
                            {
                                RA = "16187",
                                Nome = "Lucas Valente",
                                Email = "lucasvvop@hotmail.com"
                            });
            cbxAluno3.Items.Add(
                            new Aluno()
                            {
                                RA = "16194",
                                Nome = "Vinícius Waki",
                                Email = "wakit@gmail.com"
                            });
            cbxAluno3.SelectedIndex = 0;


            txtNome.Text = p.Nome;
            txtDescricao.Text = p.Descricao;
            txtAno.Text = p.Ano.ToString();
            cbxOrientador.SelectedIndex = cbxOrientador.FindString(p.Professor.ToString());
            if (p.Alunos.Count >= 1)
                cbxAluno1.SelectedIndex = cbxAluno1.FindString(p.Alunos[0].ToString());
            if (p.Alunos.Count >= 2)
                cbxAluno1.SelectedIndex = cbxAluno2.FindString(p.Alunos[1].ToString());
            if (p.Alunos.Count >= 3)
                cbxAluno1.SelectedIndex = cbxAluno3.FindString(p.Alunos[2].ToString());
        }

        public Projeto ProjetoAlterar
        {
            get
            {
                List<Aluno> alunos = new List<Aluno>();
                if (cbxAluno1.SelectedIndex != 0)
                    alunos.Add((Aluno)cbxAluno1.SelectedItem);
                if (cbxAluno2.SelectedIndex != 0)
                    alunos.Add((Aluno)cbxAluno2.SelectedItem);
                if (cbxAluno3.SelectedIndex != 0)
                    alunos.Add((Aluno)cbxAluno3.SelectedItem);

                return new Projeto()
                {
                    Nome = txtNome.Text,
                    Descricao = txtDescricao.Text,
                    Ano = int.Parse(txtAno.Text),
                    Professor = (Professor)cbxOrientador.SelectedItem,
                    Alunos = alunos
                };
            }
        }
    }
}
