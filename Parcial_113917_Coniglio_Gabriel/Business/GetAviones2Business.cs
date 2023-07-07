using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Parcial_113917_Coniglio_Gabriel.Models;
using Parcial_113917_Coniglio_Gabriel.Resultado.Aviones;
using System.Net;

namespace Parcial_113917_Coniglio_Gabriel.Business
{
    public class GetAviones2Business
    {
        public class GetAvionComando : IRequest<ListaAviones>
        { 
        }

        public class EjecutaValidacion : AbstractValidator<GetAvionComando>
        {
            public EjecutaValidacion()
            { 
            
            }
        }

        public class Manejador : IRequestHandler<GetAvionComando, ListaAviones>
        {
            private readonly Prog3AerolineasContext _contexto;
            private readonly IValidator<GetAvionComando> _validador;

            public Manejador(Prog3AerolineasContext contexto, IValidator<GetAvionComando> validador)
            {
                _contexto = contexto;
                _validador = validador;
            }

            public async Task<ListaAviones> Handle(GetAvionComando comando, CancellationToken cancellation)
            {
                var resultado = new ListaAviones();
                var validacion = await _validador.ValidateAsync(comando);
                if (!validacion.IsValid)
                {
                    var mensajeError = string.Join(Environment.NewLine, validacion.Errors);
                    resultado.SetMensajeError(mensajeError, HttpStatusCode.BadRequest);
                    return resultado;
                }

                var avion = await _contexto.Aviones.Include(d => d.IdFabricanteNavigation).Where(a => a.CantidadAsientos <= 5 && a.CantidadMotores == 1 && a.IdFabricanteNavigation.Nombre.Equals("Boeing")).FirstOrDefaultAsync();
                if (avion == null)
                {
                    var mensajeError = "No se pudo encontrar el avión";
                    resultado.SetMensajeError(mensajeError, HttpStatusCode.BadRequest);
                    return resultado;
                }
                else
                {
                    var itemAvion = new itemAviones
                    {
                        Id = avion.Id,
                        IdFabricante = avion.IdFabricante,
                        CantidadAsientos = avion.CantidadAsientos,
                        Modelo = avion.Modelo,
                        CantidadMotores = avion.CantidadMotores,
                        DatosVarios = avion.DatosVarios,
                        Marca = avion.IdFabricanteNavigation.Nombre
                    };
                    resultado.listaAviones.Add(itemAvion);
                    return resultado;                   
                }
            }
        }
    }
}
