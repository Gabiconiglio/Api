namespace Parcial_113917_Coniglio_Gabriel.Resultado.Aviones
{
    public class ListaDeleteAvion:ResultadoBase
    {
        public List<itemDeleteAvion> listaDeleteAvion { get; set; } = new List<itemDeleteAvion>();

    }

    public class itemDeleteAvion
    {

        public int Id { get; set; }
    }
}
