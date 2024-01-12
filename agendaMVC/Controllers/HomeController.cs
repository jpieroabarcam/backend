using agendaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using agendaMVC.Servicios;

namespace agendaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicio_API _servicioApi;

        public HomeController(IServicio_API servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> ObtenerAgenda(string codigoUsuario)
        {
            List<Agenda> agenda = await _servicioApi.ObtenerUsuario(codigoUsuario);

            return View(agenda);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(Agenda agendaUsuario)
        {
            bool respuesta = false;
            if(!agendaUsuario.usuario.Equals("0"))
            {
                respuesta = await _servicioApi.GuardarUsuario(agendaUsuario);
            }

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else return NoContent();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}