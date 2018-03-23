using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiEscola.Models
{
    public class Professor
    {
        public Professor()
        { }

        public Professor(Guid id)
        {
            Id = id;
        }

        public Professor(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(50, ErrorMessage = "Nome muito grande")]
        public string Nome { get; set; }

        [StringLength(50, ErrorMessage = "E-mail muito grande")]
        public string Email { get; set; }
    }
}