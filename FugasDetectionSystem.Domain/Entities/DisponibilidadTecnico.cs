using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FugasDetectionSystem.Domain.Entities
{
    [Table("DisponibilidadTecnico", Schema = "dbo")]
    public class DisponibilidadTecnico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DisponibilidadID { get; set; }
        
        [Column(TypeName = "int")]
        public int TecnicoID { get; set; }
        
        [Column(TypeName = "date")]
        public string Fecha { get; set; } = string.Empty;

        [Column(TypeName = "time")]
        public string HoraInicio { get; set; } = string.Empty;

        [Column(TypeName = "time")]
        public string HoraFin { get; set; } = string.Empty;
    }
}
