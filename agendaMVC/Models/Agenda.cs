namespace agendaMVC.Models
{
    public class Agenda
    {
        public string documento { get; set; }
        public string fechaVisita { get; set; }

        public string horaVisita { get; set; }
        public string telefono { get; set; }
        public string usuario { get; set; }

        public List<Agenda> agendas { get; set; }   
    }
}
