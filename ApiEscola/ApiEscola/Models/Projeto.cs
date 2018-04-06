using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiEscola.Models
{
    public class Projeto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(100, ErrorMessage = "E-mail muito grande")]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public int Ano { get; set; }

        private List<Aluno> _alunos = new List<Aluno>();

        public List<Aluno> Alunos
        {
            get
            {
                return _alunos;
            }
            set
            {
                if (value.Count > 3)
                    throw new ArgumentException("Um projeto pode ter no máximo 3 alunos");

                _alunos = new List<Aluno>(value);
            }
        }

        public Professor Professor { get; set; }
    }
}