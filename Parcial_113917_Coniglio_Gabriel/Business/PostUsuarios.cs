using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Parcial_113917_Coniglio_Gabriel.Models;
using Parcial_113917_Coniglio_Gabriel.Resultado.Usuario;
using System.Net;

namespace Parcial_113917_Coniglio_Gabriel.Business
{
    public class PostUsuarios
    {
        public class PostUsuarioComando : IRequest<ListaUsuario>
        {
            public string NombreUsuario { get; set; } = null!;
            public string Password { get; set; } = null!;
            public int IdRol { get; set; }
        }
        public class EjecutaValidacion : AbstractValidator<PostUsuarioComando>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.NombreUsuario).NotEmpty().WithMessage("Error en el nombre de usuario");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Error la password");
                RuleFor(x => x.IdRol).NotEmpty().WithMessage("Error en el rol");

            }
        }
        public class Manejador : IRequestHandler<PostUsuarioComando, ListaUsuario>
        {

            private readonly Prog3AerolineasContext _contextDb;

            private readonly IValidator<PostUsuarioComando> _validator;
            public Manejador(Prog3AerolineasContext contextDb, IValidator<PostUsuarioComando> validator)
            {
                _contextDb = contextDb;
                _validator = validator;
            }

            public async Task<ListaUsuario> Handle(PostUsuarioComando request, CancellationToken cancellationToken)
            {
             
                var lista = new ListaUsuario();
                var validacion = await _validator.ValidateAsync(request);
                if (!validacion.IsValid)
                {
                    var mensajeError = string.Join(Environment.NewLine, validacion.Errors);
                    lista.SetMensajeError(mensajeError, HttpStatusCode.BadRequest);
                    return lista;
                }
                else
                {
                    //buscamos coincidencias con la base de datos
                    var usuario = await _contextDb.Usuarios.FirstOrDefaultAsync(u =>
                    u.NombreUsuario == request.NombreUsuario &&
                    u.Password == request.Password &&
                    u.IdRol == request.IdRol);


                    if (usuario != null)
                    {
                        // El usuario fue encontrado, crear el objeto ItemUsuario
                        var usuarioItem = new ItemUsuario
                        {
                            NombreUsuario = usuario.NombreUsuario,
                            Password = usuario.Password,
                            IdRol = (int)usuario.IdRol
                        };

                        lista.listaUsuario.Add(usuarioItem);
                        return lista;
                    }
                    else
                    {
                        string mensajeError = "No se encontró ningún usuario con los datos proporcionados";
                        lista.SetMensajeError(mensajeError, HttpStatusCode.NotFound);
                        return lista;
                    }
                }
            }
        }
    }
}
