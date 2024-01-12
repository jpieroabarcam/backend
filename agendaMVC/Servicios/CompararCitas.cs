using agendaMVC.Models;
using System.Diagnostics.Eventing.Reader;

namespace agendaMVC.Servicios
{
    public static class CompararCitas
    {
        public static bool mxCompararCitas(List<Agenda> agenda, string fecha, string hora)
        {

            bool horario = mxTurnosEmpleados(hora);
            if (!horario) { return false; }

            DateTime fechaCita = DateTime.Parse(fecha);
            TimeSpan horaCita = TimeSpan.Parse(hora);

            agenda = agenda.OrderBy(c => TimeSpan.Parse(c.horaVisita)).ToList();

            int tamanho = agenda.Count();

            for(int i = 0; i < tamanho; i++)
            {
                Agenda agenda1 = agenda[i];
                if ( (i + 1) == tamanho)
                    break;
                Agenda agenda2= agenda[i+1];

                if( !mxConflictoCitas(agenda1, agenda2, hora) )
                {
                    return false;
                }
            }

            return true;

        }

        public static bool mxTurnosEmpleados(string hora)
        {
            TimeSpan horaCita = TimeSpan.Parse(hora);

            TimeSpan horaInicio = TimeSpan.Parse("08:00");
            TimeSpan horaFin= TimeSpan.Parse("18:00");

            if( horaCita < horaFin && horaCita > horaInicio)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool mxConflictoCitas(Agenda agenda1, Agenda agenda2, string hora)
        {
            TimeSpan horaCita = TimeSpan.Parse(hora);

            TimeSpan horaInicio = TimeSpan.Parse(agenda1.horaVisita);
            TimeSpan horaFin = TimeSpan.Parse(agenda2.horaVisita);

            if(horaCita < horaFin && horaCita > horaInicio)
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        
    }
}
