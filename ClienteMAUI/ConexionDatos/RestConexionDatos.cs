using ClienteMAUI.Models;
using Java.Net;
using Org.Apache.Http.Conn;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClienteMAUI.ConexionDatos
{
    public class RestConexionDatos : IRestConexionDatos // al implmentarse como interfaz genera codigo con excepsiones que se deben revisar y ordenar 1get 2 update 3 delete
    {
        private readonly HttpClient httpClient;
        private readonly string dominio;
        private readonly string url;
        private readonly JsonSerializerOptions opcionesJson;
        public RestConexionDatos()
        {
            httpClient = new HttpClient();
            dominio = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5231" : "http://localhost:5231";
            url = $"{dominio}/api";
            opcionesJson = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        public async Task<List<Plato>> GetPlatosAsync()
        {
            List<Plato> platos = new List<Plato>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            { //para ver si no hay conexion a internet y mostrara la lista inicial
                Debug.WriteLine("[RED] Sin acceso a internet. ");
                return platos;
            }
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"{url}/plato");
                if (response.IsSuccessStatusCode)
                {
                    //deserializar
                    var contenido = await response.Content.ReadAsStringAsync();
                    platos = JsonSerializer.Deserialize<List<Plato>>(contenido, opcionesJson);
                }
                else
                {
                    Debug.WriteLine("[RED] Sin respuesta exitosa HTTP (2XX) ");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"[ERROR] {e.Message} ");
            }
            return platos;
        }
        public async Task AddPlatoAsync(Plato plato)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            { //para ver si no hay conexion a internet y mostrara la lista inicial
                Debug.WriteLine("[RED] Sin acceso a internet. ");
                return;
            }
            try //podrian causar excepsiones or eso se emplea el try
            {
                string platoSer = JsonSerializer.Serialize<Plato>(plato, opcionesJson);
                StringContent contenido = new StringContent(platoSer, Encoding.UTF8, "application/json");// el encoding es el idioma
                HttpResponseMessage response = await httpClient.PostAsync($"{url}/plato", contenido); //$"" es la api si se tiene alguna api externa se de ingresar aqui
                if (response.IsSuccessStatusCode)
                {//deserializar
                    Debug.WriteLine("[RED se registro correctamente. ");
                }
                else
                {
                    Debug.WriteLine("[RED] Sin respuesta exitosa HTTP (2XX) ");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"[ERROR] {e.Message} ");
            }
        }

        public async Task UpdatePlatoASync(Plato plato)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            { //para ver si no hay conexion a internet y mostrara la lista inicial
                Debug.WriteLine("[RED] Sin acceso a internet. ");
                return;
            }
            try //podrian causar excepsiones or eso se emplea el try
            {
                string platoSer = JsonSerializer.Serialize<Plato>(plato, opcionesJson);
                StringContent contenido = new StringContent(platoSer, Encoding.UTF8, "application/json");// el encoding es el idioma
                HttpResponseMessage response = await httpClient.PutAsync($"{url}/plato/{plato.Id}", contenido); //$"" es la api si se tiene alguna api externa se de ingresar aqui
                if (response.IsSuccessStatusCode)
                {//deserializar
                    Debug.WriteLine("[RED se actualizo correctamente. ");
                }
                else
                {
                    Debug.WriteLine("[RED] Sin respuesta exitosa HTTP (2XX) ");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"[ERROR] {e.Message} ");
            }
        }
        public async Task DeletePlatoAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            { //para ver si no hay conexion a internet y mostrara la lista inicial
                Debug.WriteLine("[RED] Sin acceso a internet. ");
                return;
            }
            try //podrian causar excepsiones or eso se emplea el try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"{url}/plato{id}");
                if (response.IsSuccessStatusCode)
                {
                    //deserializar
                    Debug.WriteLine("[RED se actualizo correctamente. ");
                }
                else
                {
                    Debug.WriteLine("[RED] Sin respuesta exitosa HTTP (2XX) ");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"[ERROR] {e.Message} ");
            }
        }

    }
}

