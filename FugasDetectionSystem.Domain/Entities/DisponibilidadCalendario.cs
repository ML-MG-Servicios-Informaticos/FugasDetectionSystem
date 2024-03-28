namespace FugasDetectionSystem.Domain.Entities
{
    // Clase para representar la disponibilidad, ajusta según tus necesidades
    public class DisponibilidadCalendario
    {
        public DisponibilidadCalendario(IEnumerable<DisponibilidadTecnico> disponibilidades)
        {
            foreach(var disponibilidad in disponibilidades)
            {
                var inicio = disponibilidad.HoraInicio.ToString();
                var termino = disponibilidad.HoraFin.ToString();
                switch (disponibilidad.Fecha)
                {
                    case "Lunes":
                        Lunes.Add(new BloqueHorario(inicio, termino));
                        break;
                    case "Martes":
                        Martes.Add(new BloqueHorario(inicio, termino));
                        break;
                    case "Miercoles":
                        Miercoles.Add(new BloqueHorario(inicio, termino));
                        break;
                    case "Jueves":
                        Jueves.Add(new BloqueHorario(inicio, termino));
                        break;
                    case "Viernes":
                        Viernes.Add(new BloqueHorario(inicio, termino));
                        break;
                    case "Sabado":
                        Sabado.Add(new BloqueHorario(inicio, termino));
                        break;
                    case "Domingo":
                        Domingo.Add(new BloqueHorario(inicio, termino));
                        break;
                }
            }
        }

        public List<BloqueHorario> Lunes { get; set; } = [];
        public List<BloqueHorario> Martes { get; set; } = [];
        public List<BloqueHorario> Miercoles { get; set; } = [];
        public List<BloqueHorario> Jueves { get; set; } = [];
        public List<BloqueHorario> Viernes { get; set; } = [];
        public List<BloqueHorario> Sabado { get; set; } = [];
        public List<BloqueHorario> Domingo { get; set; } = [];
    }
}
