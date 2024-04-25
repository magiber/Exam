namespace BackEndApi.Models
{
    public class Empleados
    {
        public int IdEmpleado { get; set; }
        public string Fotografia { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Puesto { get; set; }
        public DateTime FechaNacimineto { get; set; }
        public DateTime FechaContraracion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Estado { get; set; }
        public int PuestoId { get; set; }
        public int EstadoId { get; set; }
    }
}
