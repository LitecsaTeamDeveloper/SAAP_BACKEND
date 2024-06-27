namespace ApiCore.Models
{
    public class OperacionMoverTp
    {
        public List<IdInventarioItem>? IdInventario { get; set; }
        public int? IdUbicacion { get; set; }
        public int? IdPozo { get; set; }

    }

    public class IdInventarioItem
    {
        public int? Id { get; set; }
    }
}
