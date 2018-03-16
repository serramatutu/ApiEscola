using ApiEscola.Models.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ApiEscola.Controllers
{
    public class ProjetoController : ApiController
    {
        DatabaseProjetoRepository repo = new DatabaseProjetoRepository();

        [Route("getbyname")]
        public IHttpActionResult GetProjetos([FromUri]string nome)
        {
            return Ok(repo.GetProjetoByName(nome));
        }
    }
}