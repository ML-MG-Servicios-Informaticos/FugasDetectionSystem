using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json; // Importante para trabajar con JSON

namespace FugasDetectionSystem.Domain.Entities
{
    [Table("Tecnicos", Schema = "dbo")]
    public class Tecnico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TecnicoID { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Nombre { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(100)")]
        public string Apellido { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(100)")]
        public string Especialidad { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(255)")]
        public string CorreoElectronico { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(20)")]
        public string Telefono { get; set; } = string.Empty;

        public string DisponibilidadJson { get; set; } = string.Empty;

        // Propiedad de conveniencia para manipular la disponibilidad como un objeto
        [NotMapped] // Indica que esta propiedad no se mapea a una columna de la base de datos
        public Disponibilidad? Disponibilidad
        {
            get => string.IsNullOrEmpty(DisponibilidadJson) ? null : JsonSerializer.Deserialize<Disponibilidad>(DisponibilidadJson);
            set => DisponibilidadJson = value == null ? string.Empty : JsonSerializer.Serialize(value);
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
        public string Inicio { get; set; } = "00:00"; // Formato "HH:mm"
        public string Termino { get; set; } = "23:59"; // Formato "HH:mm"
    }
}
