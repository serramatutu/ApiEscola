using ApiEscola.Models;
using ApiEscola.Models.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ApiEscola.Controllers
{
    [RoutePrefix("api/projetos")]
    public class ProjetoController : ApiController
    {
        DatabaseProjetoRepository repo = new DatabaseProjetoRepository();

        [Route("getbyname/{nome}")]
        [HttpGet]
        public IEnumerable<Projeto> GetProjetos([FromUri]string nome)
        {
            return repo.GetProjetoByName(nome);
        }
    }
}