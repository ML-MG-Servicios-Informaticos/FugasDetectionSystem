namespace FugasDetectionSystem.Domain.Entities
{
    public class DisponibilidadTecnico
    {
        public int DisponibilidadID { get; set; }
        public int TecnicoID { get; set; }
        public string Fecha { get; set; } = string.Empty;
        public string HoraInicio { get; set; } = string.Empty;
        public string HoraFin { get; set; } = string.Empty;
    }
}
