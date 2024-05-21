using ApiCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios(Usuario,Password) VALUES ('" + usuario.Usuario + "','" + usuario.Password + "')", con);
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
                    Id = (int)dataReader["IdUsuario"],
                    Nombre = dataReader["Nombre"].ToString(),
                    ApellidoPaterno = dataReader["ApellidoPaterno"].ToString(),
                    ApellidoMaterno = dataReader["ApellidoMaterno"].ToString(),
                    Usuario = dataReader["Usuario"].ToString(),
                    Password = dataReader["Password"].ToString(),
                    EstadoUSuario = (bool)dataReader["EstadoUSuario"],
                    FechaCreacionRegistro = (DateTime)dataReader["FechaCreacionRegistro"]

                });
            }
            con.Close();
            return lista;
        }

        [HttpPost]
        [Route("validausuario")]
        public Usuarios validausuario(Usuarios usuario)
        {
            Usuarios valida = new Usuarios();

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Saap").ToString());
            SqlCommand cmd = new SqlCommand("dbo.SPConsultaUsuarios", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@usuario", System.Data.SqlDbType.VarChar, 25).Value = usuario.Usuario;
            cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 25).Value = usuario.Password;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                valida.Valido = (bool)reader["valido"];
                valida.Nombre = reader["nombre"].ToString();
                valida.ApellidoPaterno = reader["apaterno"].ToString();
                valida.ApellidoMaterno = reader["amaterno"].ToString();
                valida.IdCompania = (int)reader["idCompania"];
            }

            con.Close();
            return valida;
        }


    }
}
