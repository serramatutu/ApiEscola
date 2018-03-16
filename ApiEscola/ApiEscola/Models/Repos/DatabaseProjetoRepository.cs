using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ApiEscola.Models.Repos
{
    public class DatabaseProjetoRepository : IProjetoRepository
    {
        protected static SqlConnection GetConnection()
        {
            return new SqlConnection(WebConfigurationManager.ConnectionStrings["regulus.BD16164.dbo"].ConnectionString);
        }

        public void AddProjeto(Projeto p)
        {
            SqlConnection conn = GetConnection();

            // Insere o projeto
            SqlCommand cp = new SqlCommand(
                "INSERT ApiProjeto(Id, Nome, Descricao, Ano, idProfessor) VALUES(@id, @nome, @desc, @ano, @prof)",
            conn);
            cp.Parameters.AddWithValue("@id", p.Id);
            cp.Parameters.AddWithValue("@nome", p.Nome);
            cp.Parameters.AddWithValue("@desc", p.Descricao);
            cp.Parameters.AddWithValue("@ano", p.Ano);
            cp.Parameters.AddWithValue("@prof", p.Professor);
            cp.ExecuteNonQuery();

            // Insere o aluno
            foreach (string ra in p.Alunos)
            {
                // Cria cada um dos alunos no banco
                SqlCommand ca = new SqlCommand(
                    "UPDATE ApiAluno SET idProjeto=@proj WHERE RA=@ra",
                conn);
                ca.Parameters.AddWithValue("@ra", ra);
                ca.Parameters.AddWithValue("@idProjeto", p.Id);

                ca.ExecuteNonQuery();
            }

            conn.Close();
        }

        public void ChangeProjeto(Projeto p)
        {
            SqlConnection conn = GetConnection();

            SqlCommand cp = new SqlCommand(
                "UPDATE ApiProjeto SET Nome=@nome, Descricao=@desc, Ano=@ano, idProfessor=@prof WHERE Id=@id",
            conn);
            // Modifica o projeto em si
            cp.Parameters.AddWithValue("@nome", p.Nome);
            cp.Parameters.AddWithValue("@desc", p.Descricao);
            cp.Parameters.AddWithValue("@ano", p.Ano);

            // Modifica o professor
            cp.Parameters.AddWithValue("@prof", p.Professor);
            cp.Parameters.AddWithValue("@prof", p.Id);
            cp.ExecuteNonQuery();

            // Modifica os alunos
            foreach (string ra in p.Alunos)
            {
                SqlCommand ca = new SqlCommand(
                    "UPDATE ApiAluno SET idProjeto=@proj WHERE ra=@ra",
                conn);
                ca.Parameters.AddWithValue("@proj", p.Id);
                ca.Parameters.AddWithValue("@ra", ra);
                ca.ExecuteNonQuery();
            }

            conn.Close();
        }

        public IEnumerable<Projeto> GetProjetoByName(string nome)
        {
            SqlConnection conn = GetConnection();

            SqlCommand comm = new SqlCommand(
                "SELECT Nome, Descricao, Ano, idProfessor FROM ApiProjeto WHERE Nome=@nome", 
            conn);
            comm.Parameters.AddWithValue("@nome", nome);

            List<Projeto> l = new List<Projeto>();
            using (SqlDataReader r = comm.ExecuteReader())
            {
                while(r.Read())
                {
                    l.Add(new Projeto()
                    {
                        Nome = r.GetString(0),
                        Descricao = r.GetString(1),
                        Ano = r.GetInt32(2),
                        Professor = r.GetGuid(3)
                    });
                }
            }

            foreach (Projeto p in l)
            {
                comm = new SqlCommand(
                    "SELECT ra FROM ApiAluno WHERE idProjeto=@proj",
                conn);
                comm.Parameters.AddWithValue("@proj", p.Id);

                using (SqlDataReader r = comm.ExecuteReader())
                {
                    while(r.Read())
                        p.Alunos.Add(r.GetString(0));
                }
            }

            conn.Close();

            return l;
        }

        public void RemoveProjeto(Guid idProjeto)
        {
            SqlConnection conn = GetConnection();

            SqlCommand comm = new SqlCommand(
                "DELETE ApiProjeto WHERE idProjeto=@id",
            conn);
            comm.Parameters.AddWithValue("@id", idProjeto);
            comm.ExecuteNonQuery();

            conn.Close();
        }
    }
}