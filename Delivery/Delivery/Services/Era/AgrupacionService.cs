using Delivery.Core;
using Delivery.Models;
using Delivery.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Delivery.Services
{
    public class AgrupacionService : IAgrupacion
    {
        public async Task<EstadoDeEjecucion> AddAgrupacionAsync(Agrupacion agrupacion)
        {
            throw new NotImplementedException();
        }

        public async Task<EstadoDeEjecucion> UpdateAgrupacionAsync(Agrupacion agrupacion)
        {
            throw new NotImplementedException();
        }

        public async Task<EstadoDeEjecucion> DeleteAgrupacionAsync(string codigo)
        {
            throw new NotImplementedException();
        }

        public async Task<EstadoDeConsulta> GetAgrupacionAsync(string codigo)
        {
            //Agrupacion
            throw new NotImplementedException();
        }

        public async Task<EstadoDeConsulta> GetAgrupacionesAsync(bool forceRefresh = false)
        {






            var httpClient = new System.Net.Http.HttpClient();
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/agrupacion");
            var retuern = JsonConvert.DeserializeObject<IEnumerable<Agrupacion>>(response);



            //IEnumerable<Agrupacion> _agrupaciones = agrupaciones.AsEnumerable();
            // Task.Delay(5000);

            EstadoDeConsulta edc = new EstadoDeConsulta();
            //Random rnd = new Random();
            //int asdasd= rnd.Next(0,2);

            //if (asdasd==0)
            //{
            //    edc.StatusProcesamiento = false;
            //    edc.Mensaje.MensajeGenerado = "Salio mal";
            //    edc.ValorObjeto = null;
            //}
            //else
            //{
            //    edc.StatusProcesamiento = true;
            //    edc.Mensaje.MensajeGenerado = "Todo bien";
            //    edc.ValorObjeto = _agrupaciones;
            //}


            return edc;

        }

     

        //HttpClient client;
        //public async Task<List<TodoItem>> RefreshDataAsync()
        //{
        //    Items = new List<TodoItem>();

        //    #if DEBUG
        //                client = new HttpClient(DependencyService.Get<IHttpClientHandlerService>().GetInsecureHandler());
        //    #else
        //                client = new HttpClient();
        //    #endif

        //    Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
        //    try
        //    {
        //        HttpResponseMessage response = await client.GetAsync(uri);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string content = await response.Content.ReadAsStringAsync();
        //            Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(@"\tERROR {0}", ex.Message);
        //    }

        //    return Items;
        //}

        //public static async Task<List<Category>> GetCategories()
        //{
        //    await TokenValidator.CheckTokenValidity();
        //    var httpClient = new HttpClient();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
        //    var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Categories");
        //    return JsonConvert.DeserializeObject<List<Category>>(response);
        //}

        //public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        //{
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");
        //    if (forceRefresh && IsConnected)
        //    {
        //        var json = await client.GetStringAsync($"api/item");
        //        items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));
        //    }

        //    return items;
        //}
    }


    public static class TokenValidator
    {
        public static async Task CheckTokenValidity()
        {
            var expirationTime = Preferences.Get("tokenExpirationTime", 0);
          //  Preferences.Set("currentTime", UnixTime.GetCurrentTime());
            var currentTime = Preferences.Get("currentTime", 0);
            if (expirationTime < currentTime)
            {
                var email = Preferences.Get("email", string.Empty);
                var password = Preferences.Get("password", string.Empty);
               // await ApiService.Login(email, password);
            }
        }
    }
}
