namespace Parcial_113917_Coniglio_Gabriel.Comandos
{
    public class AvionComando
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
