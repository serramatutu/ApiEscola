﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ApiEscola.Models.Repos
{
    public class DatabaseProjetoRepository : IProjetoRepository
    {
        protected static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["BD16164"].ConnectionString);
            conn.Open();
            return conn;
        }

        public void AddProjeto(Projeto p)
        {
            SqlConnection conn = GetConnection();

            // Insere o projeto
            SqlCommand cp = new SqlCommand(
                "INSERT ApiProjeto(idProjeto, Nome, Descricao, Ano, idProfessor) VALUES(@id, @nome, @desc, @ano, @prof)",
            conn);
            cp.Parameters.AddWithValue("@id", p.Id);
            cp.Parameters.AddWithValue("@nome", p.Nome);
            cp.Parameters.AddWithValue("@desc", p.Descricao);
            cp.Parameters.AddWithValue("@ano", p.Ano);
            cp.Parameters.AddWithValue("@prof", p.Professor.Id);
            cp.ExecuteNonQuery();

            // Insere o aluno
            foreach (Aluno a in p.Alunos)
            {
                // Cria cada um dos alunos no banco
                SqlCommand ca = new SqlCommand(
                    "UPDATE ApiAluno SET idProjeto=@proj WHERE RA=@ra",
                conn);
                ca.Parameters.AddWithValue("@ra", a.RA);
                ca.Parameters.AddWithValue("@proj", p.Id);

                ca.ExecuteNonQuery();
            }

            conn.Close();
        }

        public void ChangeProjeto(Projeto p)
        {
            SqlConnection conn = GetConnection();

            SqlCommand cp = new SqlCommand(
                "UPDATE ApiProjeto SET Nome=@nome, Descricao=@desc, Ano=@ano, idProfessor=@prof WHERE idProjeto=@id",
            conn);
            // Modifica o projeto em si
            cp.Parameters.AddWithValue("@nome", p.Nome);
            cp.Parameters.AddWithValue("@desc", p.Descricao);
            cp.Parameters.AddWithValue("@ano", p.Ano);

            // Modifica o professor
            cp.Parameters.AddWithValue("@prof", p.Professor.Id);
            cp.Parameters.AddWithValue("@id", p.Id);
            cp.ExecuteNonQuery();

            // Remove todas as relaçoes
            SqlCommand update = new SqlCommand(
                "UPDATE ApiAluno SET idProjeto=NULL WHERE idProjeto=@id",
            conn);
            update.Parameters.AddWithValue("@id", p.Id);
            update.ExecuteNonQuery();

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

        public Projeto GetProjetoById(Guid id)
        {
            return GetProjetoById(id, GetConnection());
        }

        protected Projeto GetProjetoById(Guid id, SqlConnection conn)
        {
            SqlCommand comm = new SqlCommand(
                "SELECT Nome, Descricao, Ano, idProfessor FROM ApiProjeto WHERE idProjeto=@id",
            conn);
            comm.Parameters.AddWithValue("@id", id);

            Projeto p = null;
            Guid idProfessor;
            using (SqlDataReader r = comm.ExecuteReader())
            {
                r.Read();
                p = new Projeto()
                {
                    Id = id,
                    Nome = r.GetString(0),
                    Descricao = r.GetString(1),
                    Ano = r.GetInt32(2)
                };

                idProfessor = r.GetGuid(3);
            }

            if (p == null)
                return null;

            SqlCommand cprof = new SqlCommand(
                "SELECT Nome, Email FROM ApiProfessor WHERE idProfessor=@id",
            conn);
            cprof.Parameters.AddWithValue("@id", idProfessor);
            using (SqlDataReader r2 = cprof.ExecuteReader())
            {
                r2.Read();

                p.Professor = new Professor()
                {
                    Nome = r2.GetString(0),
                    Email = r2.GetString(1),
                    Id = idProfessor
                };
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

            return p;
        }

        public IEnumerable<Projeto> GetProjetoByName(string nome)
        {
            SqlConnection conn = GetConnection();

            SqlCommand comm = new SqlCommand(
                "SELECT idProjeto FROM ApiProjeto WHERE Nome like @nome", 
            conn);
            comm.Parameters.AddWithValue("@nome", "%" + nome + "%");

            List<Guid> idProjetos = new List<Guid>();
            using (SqlDataReader r = comm.ExecuteReader())
            {
                while (r.Read())
                    idProjetos.Add(r.GetGuid(0));
            }

            List<Projeto> projetos = idProjetos.ConvertAll(x => GetProjetoById(x, conn));
            conn.Close();

            return projetos;
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

        public IEnumerable<Projeto> GetAll()
        {
            SqlConnection conn = GetConnection();

            SqlCommand comm = new SqlCommand( // prêmio string do ano :D
                @"SELECT proj.IdProjeto as IdProjeto, proj.nome as NomeProj, proj.descricao as DescricaoProj, 
	                    prof.idProfessor as IdProfessor, prof.nome as NomeProfessor, prof.email as EmailProfessor
                 FROM ApiProjeto proj 
                 INNER JOIN ApiProfessor prof ON proj.IdProfessor = prof.IdProfessor",
            conn);

            List<Projeto> l = new List<Projeto>();
            using (SqlDataReader r = comm.ExecuteReader())
            {
                while (r.Read())
                {
                    Projeto p = new Projeto()
                    {
                        Id = (Guid)r["IdProjeto"],
                        Nome = (string)r["NomeProj"],
                        Descricao = (string)r["DescricaoProj"]
                    };
                    p.Professor = new Professor()
                    {
                        Id = (Guid)r["IdProfessor"],
                        Nome = (string)r["NomeProfessor"],
                        Email = (string)r["EmailProfessor"]
                    };

                    l.Add(p);
                }
            }

            // Seleciona os alunos
            foreach (Projeto p in l)
            {
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
            }

            conn.Close();
            return l;
        }
    }
}