using FugasDetectionSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FugasDetectionSystem.ApiSchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TecnicosController : ControllerBase
    {
        private readonly ITecnicoRepository _tecnicoRepository;

        public TecnicosController(ITecnicoRepository tecnicoRepository)
        {
            _tecnicoRepository = tecnicoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTecnicos()
        {
            var tecnicos = await _tecnicoRepository.GetAllTecnicosAsync();
            return Ok(tecnicos);
        }

        // GET api/<TecnicosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TecnicosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TecnicosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TecnicosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
