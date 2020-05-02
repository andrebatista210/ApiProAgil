using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PalestranteController : ControllerBase
    {
        private readonly IProAgilRepository _repo;

        public PalestranteController(IProAgilRepository repo)
        {
            _repo = repo;
        }

        // [HttpGet]
        // public async Task<IActionResult> Get ([FromServices] IProAgilRepository repo) {
        //     try {
        //         var results = await repo.GetAllPalestranteoAsync (true);
        //         return Ok (results);
        //     } catch (System.Exception) {
        //         return this.StatusCode (StatusCodes.Status500InternalServerError, "Banco de Dados Falhou no Get ");
        //     }

        // }

        [HttpGet ("{PalestranteId}")]
        public async Task<IActionResult> Get (int PalestranteId, [FromServices] IProAgilRepository repo) {
            try {
                var results = await repo.GetPalestrantesAsyncById (PalestranteId, true);
                return Ok (results);
            } catch (System.Exception) {
                return this.StatusCode (StatusCodes.Status500InternalServerError, "Banco de Dados Falhou ");
            }

        }

        [HttpGet ("getByNome/{nome}")]
        public async Task<IActionResult> Get (string nome, [FromServices] IProAgilRepository repo) {
            try {
                var results = await repo.GetPalestrantesAsyncByName (nome, true);
                return Ok (results);
            } catch (System.Exception) {
                return this.StatusCode (StatusCodes.Status500InternalServerError, "Banco de Dados Falhou nome ");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post (Palestrante model) {
            try {
                _repo.Add (model);

                if (await _repo.SaveChangesAsync ()) {
                    return Created ($"/api/palestrante/{model.Id}", model);
                }
            } catch (System.Exception) {
                return this.StatusCode (StatusCodes.Status500InternalServerError, "Banco de Dados Falhou post");
            }

            return BadRequest ("Deu ruim!");
        }

         [HttpPut]
        public async Task<IActionResult> Put (int PalestranteId, Palestrante model) {
            try {

                var palestrante = _repo.GetPalestrantesAsyncById (PalestranteId, false);

                if (palestrante == null) return NotFound ();

                _repo.Update (model);

                if (await _repo.SaveChangesAsync ()) {
                    return Created ($"/api/palestrante/{model.Id}", model);
                }
            } catch (System.Exception) {
                return this.StatusCode (StatusCodes.Status500InternalServerError, "Banco de Dados Falhou put ");
            }

            return BadRequest ("Deu ruim!");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete (int PalestranteId) {
            try {

                var palestrante = _repo.GetPalestrantesAsyncById (PalestranteId, false);

                if (palestrante == null) return NotFound ();

                _repo.Delete (palestrante);

                if (await _repo.SaveChangesAsync ()) {
                    return Ok ();
                }
            } catch (System.Exception) {
                return this.StatusCode (StatusCodes.Status500InternalServerError, "Banco de Dados Falhou Delete");
            }

            return BadRequest ("Deu ruim!");
        }

    }
}