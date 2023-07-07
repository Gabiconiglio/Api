using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial_113917_Coniglio_Gabriel.Models;
using Parcial_113917_Coniglio_Gabriel.Resultado.Aviones;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Parcial_113917_Coniglio_Gabriel.Business
{
    public class GetAvionesBusiness
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
                private readonly Prog3AerolineasContext _contextDb;
                private readonly IValidator<GetAvionComando> _validator;
                public Manejador(Prog3AerolineasContext contextDb, IValidator<GetAvionComando> validator)
                {
                    _contextDb = contextDb;
                    _validator = validator;
                }

                public async Task<ListaAviones> Handle(GetAvionComando comando, CancellationToken cancellation)
                {
                    var resultado = new ListaAviones();
                    var validacion = await _validator.ValidateAsync(comando);
                    if (!validacion.IsValid)
                    {
                        var errores = string.Join(Environment.NewLine, validacion.Errors);
                        resultado.SetMensajeError(errores, HttpStatusCode.InternalServerError);
                        return resultado;
                    }
                        var avion = await _contextDb.Aviones
                        .Include(d => d.IdFabricanteNavigation)
                        .Where(c => c.CantidadAsientos >= 150 && c.IdFabricanteNavigation.Nombre.Contains("Boeing")).FirstOrDefaultAsync();

                    if (avion != null)
                    {

                    var itemavion = new itemAviones
                    {
                        Id = avion.Id,
                        IdFabricante = avion.IdFabricante,
                        CantidadAsientos = avion.CantidadAsientos,
                        Modelo = avion.Modelo,
                        CantidadMotores = avion.CantidadMotores,
                        DatosVarios = avion.DatosVarios,
                        Marca = avion.IdFabricanteNavigation.Nombre,
                        //IdFabricanteNavigation=avion.IdFabricanteNavigation
                    };
                        resultado.listaAviones.Add(itemavion);
                        return resultado;
                    }
                    var mensajeError = "Aviones no encontrados";
                    resultado.SetMensajeError(mensajeError, HttpStatusCode.NotFound);
                    return resultado;
                }
            }
        }
}
