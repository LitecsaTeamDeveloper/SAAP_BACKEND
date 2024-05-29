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
    public class CatalogosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CatalogosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("listanumeroparte")]

        public ActionResult<List<CatNumeroParte>>  listarNumeroParte()
        {
            try
            {
                List<CatNumeroParte> lista = new List<CatNumeroParte>();

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
                SqlCommand cmd = new SqlCommand("dbo.SPConsultaCatNumeroParte", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                lista.Add(new CatNumeroParte
                {
                    Id = (int)dataReader["IdNumeroParte"],
                    NumeroParte = dataReader["NumParte"].ToString(),

                });
            }
            con.Close();
            return lista;
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo listar el catalogo de Numero de Parte. Detalles del error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("listadiametros")]

        public ActionResult<List<CatDiametros>> listarDiametros()
        {
            try
            {
                List<CatDiametros> lista = new List<CatDiametros>();

                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
                SqlCommand cmd = new SqlCommand("dbo.SPConsultaCatDiametro", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    lista.Add(new CatDiametros
                    {
                        Id = (int)dataReader["IdDiametro"],
                        DiametroFraccion = dataReader["Diametro"].ToString(),

                    });
                }
                con.Close();
                return lista;
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo listar el catalogo de diametros. Detalles del error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("listaubicacion")]

        public ActionResult<List<CatUbicacion>> listarUbicacion()
        {
            try
            {
                List<CatUbicacion> lista = new List<CatUbicacion>();

                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
                SqlCommand cmd = new SqlCommand("dbo.SPConsultaCatTUbicacion", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    lista.Add(new CatUbicacion
                    {
                        Id = (int)dataReader["IdUbicacion"],
                        Ubicacion = dataReader["Ubicacion"].ToString(),

                    });
                }
                con.Close();
                return lista;
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo listar el catalogo de ubicaciones. Detalles del error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("listaestatus")]

        public ActionResult<List<CatEstatus>> listarEstatus()
        {
            try
            {
                List<CatEstatus> lista = new List<CatEstatus>();

                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
                SqlCommand cmd = new SqlCommand("dbo.SPConsultaCatEstatus", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    lista.Add(new CatEstatus
                    {
                        Id = (int)dataReader["IdEstatus"],
                        Estatus = dataReader["Estatus"].ToString(),

                    });
                }
                con.Close();
                return lista;
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo listar el catalogo de estatus. Detalles del error: " + ex.Message);
            }
        }
        [HttpGet]
        [Route("listarango")]

        public ActionResult<List<CatRango>> listarRango()
        {
            try
            {
                List<CatRango> lista = new List<CatRango>();

                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
                SqlCommand cmd = new SqlCommand("dbo.SPConsultaCatRango", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    lista.Add(new CatRango
                    {
                        Id = (int)dataReader["IdRango"],
                        Rango = dataReader["Rango"].ToString(),

                    });
                }
                con.Close();
                return lista;
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo listar el catalogo de rangos. Detalles del error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("listagrado")]

        public ActionResult<List<CatGrado>> listarGrado()
        {
            try
            {
                List<CatGrado> lista = new List<CatGrado>();

                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
                SqlCommand cmd = new SqlCommand("dbo.SPConsultaCatGrado", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    lista.Add(new CatGrado
                    {
                        Id = (int)dataReader["IdGrado"],
                        Grado = dataReader["Grado"].ToString(),

                    });
                }
                con.Close();
                return lista;
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo listar el catalogo de grados. Detalles del error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("listaconexion")]

        public ActionResult<List<CatConexion>> listarConexion()
        {
            try
            {
                List<CatConexion> lista = new List<CatConexion>();

                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
                SqlCommand cmd = new SqlCommand("dbo.SPConsultaCatConexion", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    lista.Add(new CatConexion
                    {
                        Id = (int)dataReader["IdConexion"],
                        Conexion = dataReader["Conexion"].ToString(),

                    });
                }
                con.Close();
                return lista;
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo listar el catalogo de conexiones. Detalles del error: " + ex.Message);
            }
        }

    }
}
