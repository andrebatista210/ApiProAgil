using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.Api.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        public readonly ProAgilContext _context;

        public ValuesController (ProAgilContext context) {
            _context = context;

        }

        // GET api/values
        [HttpGet]
        // public ActionResult<IEnumerable<Evento>> Get([FromServices] DataContext context)
        // {
        //     return context.Eventos.ToList();
        // }

        public  async Task<IActionResult> Get ([FromServices] ProAgilContext context) {
            try {
                var tests = context.Eventos.ToList();
                var results = await context.Eventos.ToListAsync ();
                return Ok (results);
            } catch (System.Exception) {
                return this.StatusCode (StatusCodes.Status500InternalServerError, "Banco de Dados Falhou ");
            }

        }

        // GET api/values/5
        [HttpGet ("{id}")]
        public async Task<ActionResult<Evento>> Get ([FromServices] ProAgilContext context, int id) {
            
            try
            {
                var results = await context.Eventos.FirstOrDefaultAsync(x => x.Id == id);
                return results;
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Problema com Banco de dados");    
                
            }
            
        }

        // POST api/values
        [HttpPost]
        public void Post ([FromBody] string value) { }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public void Delete (int id) { }
    }
}