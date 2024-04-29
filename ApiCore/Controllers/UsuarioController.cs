using ApiCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("usuario")]
        public string usuario(Usuarios usuario)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios(NombreUsuario,Password) VALUES ('" + usuario.NombreUsuario + "','" + usuario.Password + "')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "Ok";
            }
            else
            {
                return "Error";
            }


        }

        [HttpGet]
        [Route("listausuario")]

        public List<Usuarios> listarUsuarios()
        {
            List<Usuarios> lista = new List<Usuarios>();

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
            SqlCommand cmd = new SqlCommand("SELECT * FROM Usuarios", con);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                lista.Add(new Usuarios
                {
                    Id = (int)dataReader["id"],
                    NombreUsuario = dataReader["NombreUsuario"].ToString(),
                    Password = dataReader["Password"].ToString(),
                    EsActivo = (bool)dataReader["EsActivo"],
                    FechaCreacion = (DateTime)dataReader["FechaCreacion"]

                });
            }
            con.Close();
            return lista;
        }

    }
}
