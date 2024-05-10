﻿namespace ApiCore.Models
{
    public class Inventario
    {
        public int? IdInventario { get; set; }
        public string? Rfid { get; set; }
        public int? IdNumeroParte { get; set; }
        public string? NumeroParte { get; set; }
        public int? idCompania { get; set; }
        public string? Compania { get; set; }
        public string? Descripcion { get; set; }
        public int? IdDiametroInterior { get; set; }
        public decimal? DiametroInterior { get; set; }
        public int? IdDiametroExterior { get; set; }
        public decimal? DiametroExterior { get; set; }
        public decimal? Longitud { get; set; }
        public int? IdUbicacion { get; set; }
        public string? Ubicacion { get; set; }
        public int? IdRango { get; set; }
        public string? Rango { get; set; }
        public bool? EsNuevo { get; set; }
        public decimal? Bending { get; set; }
        public int? IdEstatus { get; set; }
        public string? Estatus { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaCreacionRegistro { get; set; }

    }
}