using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiEscola.Models
{
    public class Aluno
    {
        public Aluno()
        { }

        public Aluno(string ra)
        {
            RA = ra;
        }

        [StringLength(5, MinimumLength = 5, ErrorMessage = "RA deve conter 5 caracteres")]
        public string RA { get; set; }

        [StringLength(50, ErrorMessage = "Nome muito grande")]
        public string Nome { get; set; }

        [StringLength(50, ErrorMessage = "E-mail muito grande")]
        public string Email { get; set; }

        public override string ToString() => RA + " - " + Nome;
    }
}