using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Middleware.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery]string nome, [FromQuery]string cidade)
        {
            if (String.IsNullOrEmpty(nome) || String.IsNullOrEmpty(cidade))
            {
                return BadRequest();
            }
            return  Ok($"Você buscou por alunos com o nome {nome} na cidade {cidade}");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            return Ok($"Aluno {id}");
        }

        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return StatusCode((int)HttpStatusCode.Created, $"Criado objeto {value}");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok($"Alterado item com id {id} para valor {value}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            return StatusCode((int)HttpStatusCode.Accepted, $"Apagado objeto com id {id}");
        }
    }
}
