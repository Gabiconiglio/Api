using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parcial_113917_Coniglio_Gabriel.Business;
using Parcial_113917_Coniglio_Gabriel.Comandos;
using Parcial_113917_Coniglio_Gabriel.Models;
using Parcial_113917_Coniglio_Gabriel.Resultado.Aviones;

namespace Parcial_113917_Coniglio_Gabriel.Controllers
{
    [ApiController]
    public class AvionesController : Controller
    {

        private readonly Prog3AerolineasContext _context;
        private readonly IMediator _mediator;
        public AvionesController(Prog3AerolineasContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/aviones/GetAviones")]
        public async Task<ListaAviones> GetAviones()
        {
            var resultado = await _mediator.Send(new GetAvionesBusiness.GetAvionComando());
            return resultado;
        }

        [HttpGet]
        [Route("api/aviones/GetAvionesParcial2")]
        public async Task<ListaAviones> GetAvionesParcial2()
        {
            var resultado = await _mediator.Send(new GetAviones2Business.GetAvionComando());
            return resultado;
        }

        [HttpPut]
        [Route("api/Aviones/PutAviones")]
        public async Task<ListaAviones> ActualizarAviones([FromBody] AvionComando comando)
        {
            var resultado = await _mediator.Send(new PutAviones.PutAvionesComando
            {
                Id = comando.Id,
                IdFabricante = comando.IdFabricante,
                CantidadAsientos = comando.CantidadAsientos,
                CantidadMotores = comando.CantidadMotores,
                Modelo = comando.Modelo,
                //Marca=comando.Marca,
                DatosVarios = comando.DatosVarios,
            });
            return resultado;
        }
        [HttpPost]
        [Route("api/Aviones/PostAviones")]
        public async Task<ListaNuevoAvion> InsertarAvion([FromBody] AvionComando comando)
        {
            var resultado = await _mediator.Send(new PostAvionesBusiness.postAvionesComando
            {

                IdFabricante = comando.IdFabricante,
                CantidadAsientos = comando.CantidadAsientos,
                CantidadMotores = comando.CantidadMotores,
                Modelo = comando.Modelo,
                DatosVarios = comando.DatosVarios
            });
            return resultado;
        }
        [HttpDelete]
        [Route("api/Aviones/DeleteAviones/{id}")]
        public async Task<ListaDeleteAvion> DeleteAvion(int id)
        {
            var resultado = await _mediator.Send(new DeleteAvionesBusiness.deleteAvionComando
            {
                Id = id
            });
            return resultado;
        }
    }
 }

