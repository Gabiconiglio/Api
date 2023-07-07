using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Parcial_113917_Coniglio_Gabriel.Models;
using Parcial_113917_Coniglio_Gabriel.Resultado.Aviones;
using System.Net;

namespace Parcial_113917_Coniglio_Gabriel.Business
{
    public class PutAviones
    {

        public class PutAvionesComando : IRequest<ListaAviones>
        {
            public int Id { get; set; }

            public int IdFabricante { get; set; }

            public int CantidadAsientos { get; set; }

            public string Modelo { get; set; } = null!;

            public int CantidadMotores { get; set; }

            public string? DatosVarios { get; set; }

            public string Marca { get; set; }
        }

        public class EjecutarValidacion : AbstractValidator<PutAvionesComando>
        {
            public EjecutarValidacion()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("El campo Id es obligatorio");
                RuleFor(x => x.IdFabricante).NotEmpty().WithMessage("El campo ID del Fabricante es obligatorio");
                RuleFor(x => x.CantidadAsientos).NotEmpty().WithMessage("La cantidad de asientos es obligatorio");
                RuleFor(x => x.Modelo).NotEmpty().WithMessage("La cantidad de asientos es obligatorio");
                RuleFor(x => x.CantidadMotores).NotEmpty().WithMessage("La cantidad de motores es obligatorio");
                RuleFor(x => x.DatosVarios).NotEmpty().WithMessage("Los datos complementarios son obligatorios");
            }
        }
        public class Manejador : IRequestHandler<PutAvionesComando, ListaAviones>
        {
            private readonly Prog3AerolineasContext _contexto;
            private readonly IValidator<PutAvionesComando> _validador;

            public Manejador(Prog3AerolineasContext contexto, IValidator<PutAvionesComando> validador)
            { 
                _contexto=contexto;
                _validador=validador;
            }

            public async Task<ListaAviones> Handle(PutAvionesComando request, CancellationToken cancellation)
            {
                var resultado = new ListaAviones();
                var validacion= await _validador.ValidateAsync(request);
                if (!validacion.IsValid)
                {
                    var mensajeError = string.Join(Environment.NewLine, validacion.Errors);
                    resultado.SetMensajeError(mensajeError, HttpStatusCode.BadRequest);
                    return resultado;
                }
                else
                {
                    var avion = await _contexto.Aviones.Include(d => d.IdFabricanteNavigation).Where(a => a.CantidadAsientos <= 5 && a.CantidadMotores == 1 && a.IdFabricanteNavigation.Nombre.Equals("Boeing")).FirstOrDefaultAsync();
                        
                    if (avion == null)
                    {
                        var mensajeError = "No se pudo encontrar el avion";
                        resultado.SetMensajeError(mensajeError, HttpStatusCode.BadRequest);
                        return resultado;
                    }
                    else
                    {
                        avion.Id = request.Id;
                        avion.IdFabricante = request.IdFabricante;
                        avion.CantidadAsientos = request.CantidadAsientos;
                        avion.Modelo = request.Modelo;
                        avion.CantidadMotores = request.CantidadMotores;
                        //avion.IdFabricanteNavigation.Nombre = request.Marca;
                        avion.DatosVarios = request.DatosVarios;

                        _contexto.Update(avion);
                        await _contexto.SaveChangesAsync();

                        var itemAvion = new itemAviones
                        {
                            Id = request.Id,
                            IdFabricante = request.IdFabricante,
                            CantidadAsientos = request.CantidadAsientos,
                            Modelo = request.Modelo,
                            CantidadMotores = request.CantidadMotores,
                            DatosVarios = request.DatosVarios,
                            Marca=avion.IdFabricanteNavigation.Nombre
                        };
                        resultado.listaAviones.Add(itemAvion);
                        return resultado;
                    }
                }
            }
        }
    }
}
