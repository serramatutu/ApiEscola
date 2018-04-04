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

        public frmCliente()
        {
            InitializeComponent();
        }

        public async void getByName(string nome)
        {
            dgvProjeto.Rows.Clear();
            /*
            * Método HttpClient.GetAsync (Uri)
            *
            * Envia uma requisição GET para o URI especificado
            * com uma operação assíncrona.
            */
            //O objeto response(HttpResponseMessage) recebe a resposta do envio de requisição ao endereço URI
            response = await client.GetAsync(URI + $"getbyname/{nome}");
            //Se o envio de requisição fdor atendido
            if (response.IsSuccessStatusCode)
            {
                /*
                * Propriedade HttpResponseMessage.Content
                * Obtém ou define o conteúdo de uma mensagem de resposta HTTP.
                *
                * Para criar código assíncrono usamos as palavras chaves async
                * e await onde por padrão um método modificado por uma palavra-chave
                * async contém pelo menos uma expressão await.
                */
                //A variável AlunoJsonString recebe o resultado da requisição
                var ProjetoJsonString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<Projeto>>(ProjetoJsonString);
                for (int i = 0; i < result.Count; i++)
                {
                    Projeto p = result[i];
                    dgvProjeto.Rows.Add(p.Id, p.Nome, p.Descricao, p.Ano, p.Professor.Nome);
                    for (int j = 0; j < p.Alunos.Count; j++)
                        dgvProjeto.CurrentRow.Cells[5 + j].Value = p.Alunos[j].Nome;
                }

            }
            else
                MessageBox.Show($"Não foi possível obter o projeto com o nome {nome}");
        }

        private async void getById (string guid)
        {
            dgvProjeto.Rows.Clear();
            /*
            * Método HttpClient.GetAsync (Uri)
            *
            * Envia uma requisição GET para o URI especificado
            * com uma operação assíncrona.
            */
            //O objeto response(HttpResponseMessage) recebe a resposta do envio de requisição ao endereço URI
            response = await client.GetAsync(URI + $"getbyid/{guid}");
            //Se o envio de requisição fdor atendido
            if (response.IsSuccessStatusCode)
            {
                /*
                * Propriedade HttpResponseMessage.Content
                * Obtém ou define o conteúdo de uma mensagem de resposta HTTP.
                *
                * Para criar código assíncrono usamos as palavras chaves async
                * e await onde por padrão um método modificado por uma palavra-chave
                * async contém pelo menos uma expressão await.
                */
                //A variável AlunoJsonString recebe o resultado da requisição
                var ProjetoJsonString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<Projeto>(ProjetoJsonString);
                Projeto p = result;
                dgvProjeto.Rows.Add(p.Id, p.Nome, p.Descricao, p.Ano, p.Professor.Nome);
                for (int j = 0; j < p.Alunos.Count; j++)
                    dgvProjeto.CurrentRow.Cells[5 + j].Value = p.Alunos[j].Nome;

            }
            else
                MessageBox.Show($"Não foi possível obter o projeto com o código {guid}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getByName(txtNome.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getById(txtCodigo.Text);
        }
    }
}
