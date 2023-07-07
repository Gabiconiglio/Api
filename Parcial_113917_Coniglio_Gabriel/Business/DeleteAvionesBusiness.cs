using FluentValidation;
using MediatR;
using Parcial_113917_Coniglio_Gabriel.Models;
using Parcial_113917_Coniglio_Gabriel.Resultado.Aviones;
using System.Net;

namespace Parcial_113917_Coniglio_Gabriel.Business
{
    public class DeleteAvionesBusiness
    {
        public class deleteAvionComando : IRequest<ListaDeleteAvion>
        {
            public int Id { get; set; }
        }
        public class ejecutarValidacion : AbstractValidator<deleteAvionComando>
        {
            public ejecutarValidacion()
            { 
                RuleFor(x=>x.Id).NotEmpty().WithMessage("El ID es obligatorio");
            }
        }
        public class Manejador : IRequestHandler<deleteAvionComando, ListaDeleteAvion>
        {
            private readonly Prog3AerolineasContext _contexto;
            private readonly IValidator<deleteAvionComando> _validador;
            public Manejador(Prog3AerolineasContext contexto, IValidator<deleteAvionComando> validador)
            {
                _contexto = contexto;
                _validador = validador;
            }

            public async Task<ListaDeleteAvion> Handle(deleteAvionComando contexto, CancellationToken cancellation)
            {
                var resultado = new ListaDeleteAvion();
                var validacion = await _validador.ValidateAsync(contexto);
                if (!validacion.IsValid)
                {
                    var mensajeError = string.Join(Environment.NewLine, validacion.Errors);
                    resultado.SetMensajeError(mensajeError, HttpStatusCode.BadRequest);
                    return resultado;
                }
                else 
                {
                    var avion = new Avione
                    {
                        Id = contexto.Id
                    };

                    _contexto.Remove(avion);
                    await _contexto.SaveChangesAsync();

                    var itemDeleteAvion = new itemDeleteAvion
                    {
                        Id = contexto.Id
                    };
                    resultado.listaDeleteAvion.Add(itemDeleteAvion);
                    return resultado;
                }
            
            }
        }
    }
}
