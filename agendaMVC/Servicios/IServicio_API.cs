using agendaMVC.Models;
namespace agendaMVC.Servicios

{
    public interface IServicio_API
    {
        Task<List<Agenda>> ObtenerUsuario(String Usuario);

        Task<bool> GuardarUsuario(Agenda agenda);



    }
}
