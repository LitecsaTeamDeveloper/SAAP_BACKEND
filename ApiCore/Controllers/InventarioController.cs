using ApiCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public InventarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("listainventario")]

        public List<Inventario> listarInventario()
        {
            List<Inventario> lista = new List<Inventario>();

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
            SqlCommand cmd = new SqlCommand("dbo.SPConsultaInventario", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                lista.Add(new Inventario
                {
                    IdInventario = (int)dataReader["IdTubo"],
                    Rfid = dataReader["RFID"].ToString(),
                    IdNumeroParte = (int)dataReader["IdNumeroParte"],
                    NumeroParte = dataReader["NumeroParte"].ToString(),
                    idCompania = (int)dataReader["IdCompania"],
                    Compania = dataReader["Compania"].ToString(),
                    Descripcion = dataReader["Descripcion"].ToString(),
                    IdDiametroInterior = (int)dataReader["IdDiametroInterior"],
                    DiametroInterior = (decimal)dataReader["DiametroInteriorDecimal"],
                    IdDiametroExterior = (int)dataReader["IdDiametroExterior"],
                    DiametroExterior = (decimal)dataReader["DiametroExteriorDecimal"],
                    Longitud = (decimal)dataReader["Longitud"],
                    IdUbicacion = (int)dataReader["IdUbicacion"],
                    Ubicacion = dataReader["Ubicacion"].ToString(),
                    IdRango = (int)dataReader["IdRango"],
                    Rango = dataReader["Rango"].ToString(),
                    EsNuevo = (bool)dataReader["esNuevo"],
                    Bending = (decimal)dataReader["Bending"],
                    IdEstatus = (int)dataReader["idEstatus"],
                    Estatus = dataReader["Estatus"].ToString(),
                    FechaIngreso = (DateTime)dataReader["FechaIngreso"]

                });
            }
            con.Close();
            return lista;
        }
    }
}
