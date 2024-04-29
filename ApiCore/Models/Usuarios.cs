namespace ApiCore.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public bool EsActivo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
