using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiEscola.Models.Repos
{
    public interface IProjetoRepository
    {
        IEnumerable<Projeto> GetProjetoByName(string nome);

        void AddProjeto(Projeto p);

        void ChangeProjeto(Projeto p);

        void RemoveProjeto(Guid idProjeto);
    }
}