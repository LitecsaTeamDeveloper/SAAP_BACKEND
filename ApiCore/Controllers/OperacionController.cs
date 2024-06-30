using ApiCore.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ApiCore.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class OperacionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public OperacionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("movimientotp")]
        public IActionResult MovimientoTp(OperacionMoverTp inventarioOperacion)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
                con.Open();

                foreach (var idInventarioItem in inventarioOperacion.IdInventario)
                {
                    SqlCommand cmd = new SqlCommand("dbo.SPMueveTP", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idtubo", System.Data.SqlDbType.VarChar, 30).Value = idInventarioItem.Id;
                    cmd.Parameters.Add("@idubicacionOri", System.Data.SqlDbType.VarChar, 30).Value = inventarioOperacion.IdUbicacionOri;
                    cmd.Parameters.Add("@idubicacionDes", System.Data.SqlDbType.VarChar, 30).Value = inventarioOperacion.IdUbicacionDest;
                    cmd.Parameters.Add("@idpozoOri", System.Data.SqlDbType.Int).Value = inventarioOperacion.IdPozoOri;
                    cmd.Parameters.Add("@idpozoDes", System.Data.SqlDbType.Int).Value = inventarioOperacion.IdPozoDest;

                    int i = cmd.ExecuteNonQuery();

                    if (i != -1)
                    {
                        con.Close();
                        return StatusCode(500, new { message = "No se pudo registrar la información" });
                    }
                }

                con.Close();
                return Ok(new { message = "Ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "No se pudo registrar la información. Detalles del error: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("listainventariodisponible")]

        public ActionResult<List<Inventario>> listarInventarioDisponible(OperacionInventarioDisponible inventarioDisponible)
        {
            try
            {
                List<Inventario> lista = new List<Inventario>();
                
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
                SqlCommand cmd = new SqlCommand("dbo.SPConsultaInventarioDisponible", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@idcompania", System.Data.SqlDbType.Int).Value = inventarioDisponible.idCompania;
                cmd.Parameters.Add("@idubicacion", System.Data.SqlDbType.Int).Value = inventarioDisponible.IdUbicacion;
                cmd.Parameters.Add("@idpozo", System.Data.SqlDbType.Int).Value = inventarioDisponible.IdPozo;
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
                        DiametroInteriorFraccion = dataReader["DiametroInteriorFraccion"].ToString(),
                        IdDiametroExterior = (int)dataReader["IdDiametroExterior"],
                        DiametroExteriorFraccion = dataReader["DiametroExteriorFraccion"].ToString(),
                        Longitud = (decimal)dataReader["Longitud"],
                        IdRango = (int)dataReader["IdRango"],
                        Rango = dataReader["Rango"].ToString(),
                        IdConexion = (int)dataReader["IdConexion"],
                        Conexion = dataReader["Conexion"].ToString(),
                        Libraje = (decimal)dataReader["Libraje"],
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
            catch (Exception ex)
            {
                return BadRequest("No se pudo listar el inventario. Detalles del error: " + ex.Message);
            }
        }
    }
}
