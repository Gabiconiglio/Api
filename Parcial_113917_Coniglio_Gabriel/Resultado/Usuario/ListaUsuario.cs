namespace Parcial_113917_Coniglio_Gabriel.Resultado.Usuario
{
    public class ListaUsuario: ResultadoBase
    {
        public List<ItemUsuario> listaUsuario { get; set; } = new List<ItemUsuario>();
    }

        public class ItemUsuario
        {
        public string NombreUsuario { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int IdRol { get; set; }
        }
}
