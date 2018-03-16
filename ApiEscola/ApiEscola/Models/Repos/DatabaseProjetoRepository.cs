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
            foreach (Aluno a in p.Alunos)
            {
                // Cria cada um dos alunos no banco
                SqlCommand ca = new SqlCommand(
                    "UPDATE ApiAluno SET idProjeto=@proj WHERE RA=@ra",
                conn);
                ca.Parameters.AddWithValue("@ra", a.RA);
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
            cp.Parameters.AddWithValue("@prof", p.Professor.Id);
            cp.Parameters.AddWithValue("@prof", p.Id);
            cp.ExecuteNonQuery();

            // Modifica os alunos
            foreach (Aluno a in p.Alunos)
            {
                SqlCommand ca = new SqlCommand(
                    "UPDATE ApiAluno SET idProjeto=@proj WHERE ra=@ra",
                conn);
                ca.Parameters.AddWithValue("@proj", p.Id);
                ca.Parameters.AddWithValue("@ra", a.RA);
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
                    Projeto p = new Projeto()
                    {
                        Nome = r.GetString(0),
                        Descricao = r.GetString(1),
                        Ano = r.GetInt32(2),
                    };

                    // Lê o professor
                    Guid idProfessor = r.GetGuid(3);
                    SqlCommand cprof = new SqlCommand(
                        "SELECT Nome, Email, FROM ApiProfessor WHERE idProfessor=@id",
                    conn);
                    cprof.Parameters.AddWithValue("@id", idProfessor);
                    using (SqlDataReader r2 = cprof.ExecuteReader())
                    {
                        r2.Read();

                        p.Professor = new Professor(r2.GetString(0), r2.GetString(1));
                    }

                    // Lê o aluno
                    SqlCommand cal = new SqlCommand(
                        "SELECT RA, Nome, Email FROM ApiAluno WHERE idProjeto=@id",
                    conn);
                    cal.Parameters.AddWithValue("@id", p.Id);
                    
                    using (SqlDataReader r2 = cal.ExecuteReader())
                    {
                        while (r2.Read())
                        {
                            p.Alunos.Add(new Aluno()
                            {
                                RA = r2.GetString(0),
                                Nome = r2.GetString(1),
                                Email = r2.GetString(2)
                            });
                        }
                    }

                    l.Add(p);
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