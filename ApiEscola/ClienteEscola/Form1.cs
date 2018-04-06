using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApiEscola.Models;
using Newtonsoft.Json;

namespace ClienteEscola
{
    public partial class frmCliente : Form
    {
        private string URI = "http://localhost:49442/api/projetos/";
        HttpClient client = new HttpClient();
        HttpResponseMessage response = new HttpResponseMessage();
        Projeto proj = null;

        public frmCliente()
        {
            InitializeComponent();
        }

        public async void GetByName(string nome)
        {
            dgvProjeto.Rows.Clear();
            response = await client.GetAsync(URI + $"getbyname/{nome}");
            if (response.IsSuccessStatusCode)
            {
                var ProjetoJsonString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<Projeto>>(ProjetoJsonString);
                for (int i = 0; i < result.Count; i++)
                {
                    Projeto p = result[i];
                    dgvProjeto.Rows.Add(p.Id, p.Nome, p.Descricao, p.Ano, p.Professor.Nome);
                    for (int j = 0; j < p.Alunos.Count; j++)
                        dgvProjeto.Rows[i].Cells[5 + j].Value = p.Alunos[j].Nome;
                }

            }
            else
                MessageBox.Show($"Não foi possível obter o projeto com o nome {nome}");
        }

        private async void GetById (string guid)
        {
            dgvProjeto.Rows.Clear();
            response = await client.GetAsync(URI + $"getbyid/{guid}");
            if (response.IsSuccessStatusCode)
            {
                var ProjetoJsonString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<Projeto>(ProjetoJsonString);
                proj = result;
                dgvProjeto.Rows.Add(proj.Id, proj.Nome, proj.Descricao, proj.Ano, proj.Professor.Nome);
                for (int j = 0; j < proj.Alunos.Count; j++)
                    dgvProjeto.Rows[0].Cells[5 + j].Value = proj.Alunos[j].Nome;

            }
            else
                MessageBox.Show($"Não foi possível obter o projeto com o código {guid}");
        }

        private async void DeleteProject(string guid)
        {
            response = await client.DeleteAsync(URI + $"delete/{guid}");
            if (response.IsSuccessStatusCode)
            {
                proj = null;
                MessageBox.Show("Projeto deletado com sucesso");
            }
            else
                MessageBox.Show("Não foi possível deletar o projeto");
        }

        private async void InsertProject (Projeto p)
        {
            var serializedProjeto = JsonConvert.SerializeObject(p);
            //A classe StringContent adiciona o conteúdo json em um objeto HTTP
            var content = new StringContent(serializedProjeto, Encoding.UTF8, "application/json");

            response = await client.PostAsync(URI + "insert", content);

            if (response.IsSuccessStatusCode)
                MessageBox.Show("Projeto inserido com sucesso");
            else
                MessageBox.Show("Não foi possível inserir o projeto");
        }

        private async void ChangeProject (Projeto p)
        {
            var serializedProjeto = JsonConvert.SerializeObject(p);
            //A classe StringContent adiciona o conteúdo json em um objeto HTTP
            var content = new StringContent(serializedProjeto, Encoding.UTF8, "application/json");

            response = await client.PutAsync(URI + "change", content);

            if (response.IsSuccessStatusCode)
                MessageBox.Show("Projeto alterado com sucesso");
            else
                MessageBox.Show("Não foi possível alterar o projeto");
        }

        private async void GetAll()
        {
            dgvProjeto.Rows.Clear();
            response = await client.GetAsync(URI + "getall");

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<Projeto>>(await response.Content.ReadAsStringAsync());

                for (int i = 0; i < result.Count; i++)
                {
                    Projeto p = result[i];
                    dgvProjeto.Rows.Add(p.Id, p.Nome, p.Descricao, p.Ano, p.Professor.Nome);
                    for (int j = 0; j < p.Alunos.Count; j++)
                        dgvProjeto.Rows[i].Cells[5 + j].Value = p.Alunos[j].Nome;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetByName(txtNome.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetById(txtCodigo.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteProject(txtCodigo.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmInserirProjeto Form2 = new frmInserirProjeto();

            try
            {
                if (Form2.ShowDialog(this) == DialogResult.OK)
                    InsertProject(Form2.NovoProjeto);
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao inserir o projeto, por favor tente novamente");
            }

            Form2.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmAlterarProjeto Form2 = new frmAlterarProjeto(proj);

            try
            {
                if (Form2.ShowDialog(this) == DialogResult.OK)
                    ChangeProject(Form2.ProjetoAlterar);
                proj = Form2.ProjetoAlterar;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao alterar o projeto, por favor tente novamente");
            }

            Form2.Close();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            GetAll();
        }
    }
}
