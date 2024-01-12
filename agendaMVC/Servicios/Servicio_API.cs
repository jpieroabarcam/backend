using agendaMVC.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;

namespace agendaMVC.Servicios
{

    public class Servicio_API : IServicio_API

    {
        private static string _baseurl;

        public Servicio_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsetting.json").Build();

            _baseurl = builder.GetSection("ApiSetting:baseUrl").Value;
        }

        public async Task<List<Agenda>> Agenda(string codigoUsuario)
        {
            List<Agenda> agenda = new List<Agenda>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"api/agenda/{codigoUsuario}");

            if (response.IsSuccessStatusCode) { 
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Agenda>(json_respuesta);
                agenda = resultado.agendas;
            }
            return agenda;
        }


        public async Task<bool> GuardarUsuario(Agenda agenda)
        {
            List<Agenda> agendas = await ObtenerUsuario("0");

            if (Servicios.CompararCitas.mxCompararCitas(agendas, agenda.fechaVisita, agenda.horaVisita))
            {
                Console.WriteLine("No se puede registrar una fecha y hora repetida");
                return false;
            }
            else
            {
                bool respuesta = false;
                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(_baseurl);

                var content = new StringContent(JsonConvert.SerializeObject(agenda), Encoding.UTF8, "application/json");

                var response = await cliente.PostAsync($"api/agenda/registrar", content);

                if (response.IsSuccessStatusCode)
                {
                    respuesta = true;
                }
                return respuesta;

            }
        }

        public async Task<List<Agenda>> ObtenerUsuario(string codigoUsuario)
        {
            List<Agenda> agenda = new List<Agenda>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"api/agenda/{codigoUsuario}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Agenda>(json_respuesta);
                agenda = resultado.agendas;
            }
            return agenda;
        }
    }
}
