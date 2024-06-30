namespace ApiCore.Models
{
    public class OperacionMoverTp
    {
        public List<IdInventarioItem>? IdInventario { get; set; }
        public int? IdUbicacionOri { get; set; }
        public int? IdUbicacionDest { get; set; }
        public int? IdPozoOri { get; set; }
        public int? IdPozoDest { get; set; }

    }

    public class IdInventarioItem
    {
        public int? Id { get; set; }
    }
}
