namespace FugasDetectionSystem.Domain.Entities
{
    public class Tecnico
    {
        public int TecnicoID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public IEnumerable<DisponibilidadTecnico>? Disponibilidad { get; set; }
        public DisponibilidadCalendario? Calendario { get; }
        
    }

}
