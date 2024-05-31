using ApiCore.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ApiCore.Controllers
{
    [EnableCors("ReglasCors")]
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
        [Route("listainventario/{idCompania}")]

        public ActionResult<List<Inventario>> listarInventario(int idCompania)
        {
            try
            {
                List<Inventario> lista = new List<Inventario>();

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
            SqlCommand cmd = new SqlCommand("dbo.SPConsultaInventario", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@idcompania", System.Data.SqlDbType.Int).Value = idCompania;
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

        [HttpPost]
        [Route("registrainventario")]
        public IActionResult RegistrarInventario(Inventario inventario)
        {
            try 
            { 

            Inventario datosinventario = new Inventario();

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
            SqlCommand cmd = new SqlCommand("dbo.SPNuevoEditaCatTubo", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@IdTubo", System.Data.SqlDbType.VarChar, 30).Value = inventario.IdInventario;
            cmd.Parameters.Add("@RFID", System.Data.SqlDbType.VarChar, 30).Value = inventario.Rfid;
            cmd.Parameters.Add("@IdNumeroParte", System.Data.SqlDbType.Int).Value = inventario.IdNumeroParte;
            cmd.Parameters.Add("@IdCompania", System.Data.SqlDbType.Int).Value = inventario.idCompania;
            cmd.Parameters.Add("@DescripcionTubo", System.Data.SqlDbType.VarChar, 200).Value = inventario.Descripcion;
            cmd.Parameters.Add("@IdDiametroInte", System.Data.SqlDbType.Int).Value = inventario.IdDiametroInterior;
            cmd.Parameters.Add("@IdDiametroExte", System.Data.SqlDbType.Int).Value = inventario.IdDiametroExterior;
            cmd.Parameters.Add("@Longitud", System.Data.SqlDbType.Decimal).Value = inventario.Longitud;
            cmd.Parameters.Add("@Bending", System.Data.SqlDbType.Decimal).Value = inventario.Bending;
            cmd.Parameters.Add("@IdRango", System.Data.SqlDbType.Int).Value = inventario.IdRango;
            cmd.Parameters.Add("@FechaIngreso", System.Data.SqlDbType.DateTime).Value = inventario.FechaIngreso;
            cmd.Parameters.Add("@Esnuevo", System.Data.SqlDbType.Bit).Value = inventario.EsNuevo;
            cmd.Parameters.Add("@Libraje", System.Data.SqlDbType.Decimal).Value = inventario.Libraje;
            cmd.Parameters.Add("@IdConexion", System.Data.SqlDbType.Int).Value = inventario.IdConexion;
            cmd.Parameters.Add("@IdGrado", System.Data.SqlDbType.Int).Value = inventario.IdGrado;
            cmd.Parameters.Add("@IdEstatus", System.Data.SqlDbType.Int).Value = inventario.IdEstatus;
            cmd.Parameters.Add("@NuevoEdita", System.Data.SqlDbType.Char,1).Value = inventario.TipoRegistro;
                //var returnValueParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
                //cmd.Parameters.Add(returnValueParameter);

            con.Open();
             int i = 0;
             i = cmd.ExecuteNonQuery();
            con.Close();
                //int valorRetorno = (int)returnValueParameter.Value;

            if (i == -1)
            {
                    return Ok(new { message = "Ok" }); 
            }
            else
            {
                    return StatusCode(500, new { message = "No se pudo registrar la información" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "No se pudo registrar la información. Detalles del error: " + ex.Message });
                //return "Error en el registro del tramo. Detalles del error: " + ex.Message;
            }
        }

        [HttpGet]
        [Route("obtieneinventario/{idInventario}")]

        public ActionResult<Inventario> obtenerInventario(int idInventario)
        {
            try
            {
                Inventario inventa = new Inventario();

                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
                SqlCommand cmd = new SqlCommand("dbo.SPConsultaInventarioIndividual", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdTubo", System.Data.SqlDbType.Int).Value = idInventario;
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();



                if (dataReader != null && dataReader.Read())
                {
                    inventa.IdInventario = (int)dataReader["IdTubo"];
                    inventa.Rfid = dataReader["RFID"].ToString();
                    inventa.IdNumeroParte = (int)dataReader["IdNumeroParte"];
                    inventa.idCompania = (int)dataReader["IdCompania"];
                    inventa.Descripcion = dataReader["Descripcion"].ToString();
                    inventa.IdDiametroInterior = (int)dataReader["IdDiametroInterior"];
                    inventa.IdDiametroExterior = (int)dataReader["IdDiametroExterior"];
                    inventa.Longitud = (decimal)dataReader["Longitud"];
                    inventa.IdRango = (int)dataReader["IdRango"];
                    inventa.IdGrado = (int)dataReader["IdGrado"];
                    inventa.IdConexion = (int)dataReader["IdConexion"];
                    inventa.Libraje = (decimal)dataReader["Libraje"];
                    inventa.EsNuevo = (bool)dataReader["esNuevo"];
                    inventa.Bending = (decimal)dataReader["Bending"];
                    inventa.IdEstatus = (int)dataReader["idEstatus"];
                    inventa.FechaIngreso = (DateTime)dataReader["FechaIngreso"];

               }



                con.Close();
                return inventa;
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo obtener el inventario. Detalles del error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("eliminainventario")]
        public IActionResult EliminarInventario(Inventario inventario)
        {
            try
            {

               

                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
                SqlCommand cmd = new SqlCommand("dbo.SPNuevoEditaCatTubo", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdTubo", System.Data.SqlDbType.VarChar, 30).Value = inventario.IdInventario;
                cmd.Parameters.Add("@NuevoEdita", System.Data.SqlDbType.Char, 1).Value = inventario.TipoRegistro;
                cmd.Parameters.Add("@FechaIngreso", System.Data.SqlDbType.DateTime).Value = inventario.FechaIngreso;

                con.Open();
                int i = 0;
                i = cmd.ExecuteNonQuery();
                con.Close();

                if (i == -1)
                {
                    return Ok(new { message = "Ok" });
                }
                else
                {
                    return StatusCode(500, new { message = "No se pudo eliminar la información" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "No se pudo eliminar la información. Detalles del error: " + ex.Message });

            }
        }

    }
}
