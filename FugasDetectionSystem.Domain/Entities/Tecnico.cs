using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
