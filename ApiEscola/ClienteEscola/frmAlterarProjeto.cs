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
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void frmAlterarProjeto_Load(object sender, EventArgs e)
        {
            cbxAluno1.Items.Add("Nulo");
            cbxAluno2.Items.Add("Nulo");
            cbxAluno3.Items.Add("Nulo");


            txtNome.Text = p.Nome;
            txtDescricao.Text = p.Descricao;
            txtAno.Text = p.Ano.ToString();
            cbxOrientador.SelectedItem = p.Professor;
            if (p.Alunos.Count >= 1)
                cbxAluno1.SelectedItem = p.Alunos[0];
            if (p.Alunos.Count >= 2)
                cbxAluno1.SelectedItem = p.Alunos[1];
            if (p.Alunos.Count >= 3)
                cbxAluno1.SelectedItem = p.Alunos[2];
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
