using agendaMVC.Models;

namespace agendaMVC.Servicios
{
    public static class CompararCitas
    {
        public static bool mxCompararCitas(List<Agenda> agenda, string fecha, string hora)
        {
            bool resultado = false;

            DateTime fechaCita = DateTime.Parse(fecha);
            TimeSpan horaCita = TimeSpan.Parse(hora);

            return agenda.Any(c => DateTime.Parse(c.fechaVisita) == fechaCita && TimeSpan.Parse(c.horaVisita) == horaCita);

        }
    }
}
