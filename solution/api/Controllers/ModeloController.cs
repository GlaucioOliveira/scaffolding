using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pecacompativel.db.Services;
using pecacompativel.db.Models;

namespace pecacompativel.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModeloController : ControllerBase
    {
        private readonly ModeloService _modeloService;

        public ModeloController(ModeloService modeloService)
        {
            _modeloService = modeloService;
        }

        [HttpGet]
        public ActionResult<List<Modelo>> Get() =>
         _modeloService.Get();

        [HttpGet("{id:length(24)}", Name = "GetModelo")]
        public ActionResult<Modelo> Get(string id)
        {
            var modelo = _modeloService.Get(id);

            if (modelo == null)
            {
                return NotFound();
            }

            return modelo;
        }

        //[HttpGet("ListaPopulares")]
        //public ActionResult<List<Marca>> ListaPopulares()
        //{
        //    List<string> MarcasPopulares = new List<string>() { "HONDA", "DAFRA", "YAMAHA", "SUZUKI", "KAWASAKI", "BMW" };

        //    return _modeloService.Get().Where(x => MarcasPopulares.Contains(x.Nome)).ToList();
        //}

        //[HttpPost]
        //public ActionResult<Modelo> Create(Marca marca)
        //{
        //    _modeloService.Create(marca);
        //    return CreatedAtRoute("GetMarca", new { id = marca.Id.ToString() }, marca);
        //}

        //[HttpDelete("{id:length(24)}")]
        //public IActionResult Delete(string id)
        //{
        //    var peca = _modeloService.Get(id);

        //    if (peca == null)
        //    {
        //        return NotFound();
        //    }

        //    _modeloService.Remove(peca.Id);

        //    return NoContent();
        //}
    }
}