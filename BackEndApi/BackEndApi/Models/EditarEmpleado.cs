namespace BackEndApi.Models
{
    public class EditarEmpleado
    {
        public int IdEmpleado { get; set; }
        public string Fotografia { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int PuestoId { get; set; }
        public DateTime? FechaNacimineto { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public int EstadoId { get; set; }
    }
}
