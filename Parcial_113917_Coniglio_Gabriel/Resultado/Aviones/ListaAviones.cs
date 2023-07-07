using Parcial_113917_Coniglio_Gabriel.Models;

namespace Parcial_113917_Coniglio_Gabriel.Resultado.Aviones
{
    public class ListaAviones:ResultadoBase
    {
        public List<itemAviones> listaAviones { get; set; } = new List<itemAviones>();

    }

    public class itemAviones
    {
        public int Id { get; set; }

        public int IdFabricante { get; set; }

        public int CantidadAsientos { get; set; }

        public string Modelo { get; set; } = null!;

        public int CantidadMotores { get; set; }

        public string? DatosVarios { get; set; }
        
        public string Marca { get; set; }

    }
}
