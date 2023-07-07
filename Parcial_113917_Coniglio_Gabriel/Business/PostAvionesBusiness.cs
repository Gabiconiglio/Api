using FluentValidation;
using MediatR;
using Parcial_113917_Coniglio_Gabriel.Models;
using Parcial_113917_Coniglio_Gabriel.Resultado.Aviones;
using System.Net;

namespace Parcial_113917_Coniglio_Gabriel.Business
{
    public class PostAvionesBusiness
    {
        public class postAvionesComando : IRequest<ListaNuevoAvion>
        {
            public int IdFabricante { get; set; }

            public int CantidadAsientos { get; set; }

            public string Modelo { get; set; } = null!;

            public int CantidadMotores { get; set; }

            public string? DatosVarios { get; set; }
        }

        public class ejecutarValicadion : AbstractValidator<postAvionesComando>
        {
            public ejecutarValicadion()
            {
                RuleFor(x => x.IdFabricante).NotEmpty().WithMessage("El Fabricante es obligatorio");
                RuleFor(x => x.CantidadAsientos).NotEmpty().WithMessage("La cantidad de asientos es obligatorio");
                RuleFor(x => x.Modelo).NotEmpty().WithMessage("El modelo es obligatorio");
                RuleFor(x => x.CantidadMotores).NotEmpty().WithMessage("La cantidad de asientos es obligatorio");
                RuleFor(x => x.DatosVarios).NotEmpty().WithMessage("Los datos adicionales son obligatorios");
            }
        }

        public class Manejador : IRequestHandler<postAvionesComando, ListaNuevoAvion>
        {
            private readonly Prog3AerolineasContext _contexto;
            private readonly IValidator<postAvionesComando> _validador;

            public Manejador(Prog3AerolineasContext contexto, IValidator<postAvionesComando> validador)
            {
                _contexto = contexto;
                _validador = validador;
            }
            public async Task<ListaNuevoAvion> Handle(postAvionesComando request, CancellationToken cancellation)
            {
                var resultado = new ListaNuevoAvion();
                var validacion = await _validador.ValidateAsync(request);
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
                        IdFabricante = request.IdFabricante,
                        CantidadAsientos = request.CantidadAsientos,
                        Modelo = request.Modelo,
                        CantidadMotores = request.CantidadMotores,
                        DatosVarios = request.DatosVarios
                    };

                    _contexto.Add(avion);
                    await _contexto.SaveChangesAsync();

                    var itemAvion = new itemNuevoAvion
                    {
                        IdFabricante = request.IdFabricante,
                        CantidadAsientos = request.CantidadAsientos,
                        Modelo = request.Modelo,
                        CantidadMotores = request.CantidadMotores,
                        DatosVarios = request.DatosVarios
                    };
                    resultado.listaNuevoAvion.Add(itemAvion);
                    return resultado;
                }            
            }
        }
    }
}
