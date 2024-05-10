namespace ApiCore.Models
{
    public class Usuarios
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Usuario { get; set; }
        public string? Password { get; set; }
        public bool? EstadoUSuario { get; set; }
        public DateTime? FechaCreacionRegistro { get; set; }
        public bool? Valido { get; set; }

    }
}
