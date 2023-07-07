using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parcial_113917_Coniglio_Gabriel.Business;
using Parcial_113917_Coniglio_Gabriel.Comandos;
using Parcial_113917_Coniglio_Gabriel.Models;
using Parcial_113917_Coniglio_Gabriel.Resultado.Usuario;

namespace Parcial_113917_Coniglio_Gabriel.Controllers
{
    [ApiController]
    public class UsuarioController : Controller
    {

        private readonly Prog3AerolineasContext _context;
        private readonly IMediator _mediator;
        public UsuarioController(Prog3AerolineasContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/Usuarios/PostUsuarios")]
        public async Task<ListaUsuario> ActualizarUsuario([FromBody] UsuarioComando comando)
        {
            var resultado = await _mediator.Send(new PostUsuarios.PostUsuarioComando
            {
                NombreUsuario = comando.NombreUsuario,
                Password = comando.Password,
                IdRol = comando.IdRol
            });
            return resultado;
        }
    }
}






















