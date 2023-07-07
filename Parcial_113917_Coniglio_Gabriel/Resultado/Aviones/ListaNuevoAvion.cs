namespace Parcial_113917_Coniglio_Gabriel.Resultado.Aviones
{
        public class ListaNuevoAvion : ResultadoBase
        {
            public List<itemNuevoAvion> listaNuevoAvion { get; set; } = new List<itemNuevoAvion>();

        }

        public class itemNuevoAvion
    {

            public int IdFabricante { get; set; }

            public int CantidadAsientos { get; set; }

            public string Modelo { get; set; } = null!;

            public int CantidadMotores { get; set; }

            public string? DatosVarios { get; set; }

            public string Marca { get; set; }
    }
}
