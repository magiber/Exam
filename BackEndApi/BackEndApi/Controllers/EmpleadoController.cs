using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BackEndApi.Models;

using System.Data;
using System.Data.SqlClient;

namespace BackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        public readonly string cadenaSql;

        public EmpleadoController(IConfiguration config)
        {
            cadenaSql = config.GetConnectionString("CadenaSQL");
        }


        [HttpGet]
        [Route("Listar")]
        public IActionResult Listar()
        {
            List<Empleados> lista = new List<Empleados>();

            try
            {
                using (var conexion = new SqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ObtenerEmpleado", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Empleados
                            {
                                IdEmpleado = Convert.ToInt32(rd["IdEmpleado"]),
                                Fotografia = rd["Fotografia"].ToString(),
                                Nombre = rd["Nombre"].ToString(),
                                Apellidos = rd["Apellidos"].ToString(),
                                Puesto = rd["Puesto"].ToString(),
                                FechaNacimineto = Convert.ToDateTime(rd["FechaNacimineto"]),
                                FechaContraracion = Convert.ToDateTime(rd["FechaContraracion"]),
                                Direccion = rd["Direccion"].ToString(),
                                Telefono = rd["Telefono"].ToString(),
                                CorreoElectronico = rd["CorreoElectronico"].ToString(),
                                Estado = rd["Estado"].ToString(),
                            });
                        }
                    }
                }
                //return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
                return Ok(lista);
            }
            catch(Exception error) 
            {
                //return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        [Route("Buscar/{id:int}")]
        public IActionResult Buscar(int id)
        {
            List<Empleados> lista = new List<Empleados>();
            
            Empleados empleado = new Empleados();

            try
            {
                using (var conexion = new SqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ObtenerEmpleado", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Empleados
                            {
                                IdEmpleado = Convert.ToInt32(rd["IdEmpleado"]),
                                Fotografia = rd["Fotografia"].ToString(),
                                Nombre = rd["Nombre"].ToString(),
                                Apellidos = rd["Apellidos"].ToString(),
                                Puesto = rd["Puesto"].ToString(),
                                FechaNacimineto = Convert.ToDateTime(rd["FechaNacimineto"]),
                                FechaContraracion = Convert.ToDateTime(rd["FechaContraracion"]),
                                Direccion = rd["Direccion"].ToString(),
                                Telefono = rd["Telefono"].ToString(),
                                CorreoElectronico = rd["CorreoElectronico"].ToString(),
                                Estado = rd["Estado"].ToString(),
                                PuestoId = Convert.ToInt32(rd["PuestoId"]),
                                EstadoId = Convert.ToInt32(rd["EstadoId"]),
                            });
                        }
                    }
                }
                empleado = lista.Where(item => item.IdEmpleado == id).FirstOrDefault();

                //return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = empleado });
                return Ok(empleado);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = empleado });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] InsertarEmpleado objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("InsertarEmpleado", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@Fotografia", objeto.Fotografia);
                    cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("@Apellidos", objeto.Apellidos);
                    cmd.Parameters.AddWithValue("@PuestoId", objeto.PuestoId);
                    cmd.Parameters.AddWithValue("@FechaNacimineto", objeto.FechaNacimineto);
                    cmd.Parameters.AddWithValue("@Direccion", objeto.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono", objeto.Telefono);
                    cmd.Parameters.AddWithValue("@CorreoElectronico", objeto.CorreoElectronico);
                    cmd.Parameters.AddWithValue("@EstadoId", objeto.EstadoId);
                    
                    cmd.ExecuteNonQuery();
                }
                return Ok();
                //return StatusCode(StatusCodes.Status200OK, new { mensaje = "Inserccion ok" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message});
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] EditarEmpleado objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ActualizarEmpleado", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdEmpleado", objeto.IdEmpleado == 0 ? DBNull.Value : objeto.IdEmpleado);
                    cmd.Parameters.AddWithValue("@Fotografia", objeto.Fotografia is null ? DBNull.Value : objeto.Fotografia);
                    cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre is null ? DBNull.Value : objeto.Nombre);
                    cmd.Parameters.AddWithValue("@Apellidos", objeto.Apellidos is null ? DBNull.Value : objeto.Apellidos);
                    cmd.Parameters.AddWithValue("@PuestoId", objeto.PuestoId == 0 ? DBNull.Value : objeto.PuestoId);
                    cmd.Parameters.AddWithValue("@FechaNacimineto", objeto.FechaNacimineto.HasValue ? objeto.FechaNacimineto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Direccion", objeto.Direccion is null ? DBNull.Value : objeto.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono", objeto.Telefono is null ? DBNull.Value : objeto.Telefono);
                    cmd.Parameters.AddWithValue("@CorreoElectronico", objeto.CorreoElectronico is null ? DBNull.Value : objeto.CorreoElectronico);
                    cmd.Parameters.AddWithValue("@EstadoId", objeto.EstadoId == 0 ? DBNull.Value : objeto.EstadoId);
                    
                    cmd.ExecuteNonQuery();
                }
                return Ok();
                //return StatusCode(StatusCodes.Status200OK, new { mensaje = "Actualizacion ok" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{Id:int}")]
        public IActionResult Eliminar(int Id)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EliminarEmpleado", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdEmpleado", Id);

                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Eliminacion ok" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }


    }
}
