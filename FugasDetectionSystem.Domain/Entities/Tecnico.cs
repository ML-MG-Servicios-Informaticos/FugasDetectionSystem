using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json; // Importante para trabajar con JSON

namespace FugasDetectionSystem.Domain.Entities
{
    [Table("tecnicos")]
    public class Tecnico
    {
        [Column("idtecnico")]
        public int IdTecnico { get; set; }

        [Column("nombres")]
        public string Nombres { get; set; } = string.Empty;

        [Column("apellidos")]
        public string Apellidos { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;
                
        [Column("telefono")]
        public string Telefono { get; set; } = string.Empty;

        [Column("disponibilidad")]
        public string DisponibilidadJson { get; set; } = string.Empty;

        // Propiedad de conveniencia para manipular la disponibilidad como un objeto
        [NotMapped] // Indica que esta propiedad no se mapea a una columna de la base de datos
        public Disponibilidad Disponibilidad
        {
            get => string.IsNullOrEmpty(DisponibilidadJson) ? null : JsonSerializer.Deserialize<Disponibilidad>(DisponibilidadJson);
            set => DisponibilidadJson = JsonSerializer.Serialize(value);
        }
    }

    // Clase para representar la disponibilidad, ajusta según tus necesidades
    public class Disponibilidad
    {
        public List<BloqueHorario> Lunes { get; set; } = new List<BloqueHorario>();
        public List<BloqueHorario> Martes { get; set; } = new List<BloqueHorario>();
        public List<BloqueHorario> Miercoles { get; set; } = new List<BloqueHorario>();
        public List<BloqueHorario> Jueves { get; set; } = new List<BloqueHorario>();
        public List<BloqueHorario> Viernes { get; set; } = new List<BloqueHorario>();
        public List<BloqueHorario> Sabado { get; set; } = new List<BloqueHorario>();
        public List<BloqueHorario> Domingo { get; set; } = new List<BloqueHorario>();
    }

    // Clase para representar los bloques horarios en cada día
    public class BloqueHorario
    {
        public string Inicio { get; set; } // Formato "HH:mm"
        public string Termino { get; set; } // Formato "HH:mm"
    }
}
