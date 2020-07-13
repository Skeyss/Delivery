using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.HttpClientGeneral
{
    public class Get<T>
    {
        /// <summary>
        /// Este metodo devuelve
        /// 200 sie sta bien
        /// 400 algo fallo
        /// 0 fallo de progrmacion
        /// </summary>
        /// <param name="urlApi"></param>
        /// <returns></returns>
        public async Task<(int StatusCode, ObservableCollection<T>)> GetService(string urlApi,string authHeaderValue)
        {
            int statusCode = 0;
            try
            {
                if (VerificarConexion.ExisteConexionInternet() == false)
                {
                    statusCode = 1;
                    return (statusCode, null);
                }
                else
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(urlApi);
                    if (authHeaderValue!=null)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
                    }
                    Uri uri = new Uri(string.Format(urlApi, string.Empty));
                    HttpResponseMessage response = await client.GetAsync(uri);
                    statusCode = response.StatusCode.GetHashCode();



                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return (statusCode, JsonConvert.DeserializeObject<ObservableCollection<T>>(content));
                    }
                    else
                    {
                        return (statusCode, null);
                    }
                }


            }
            catch (Exception ex)
            {
                return (statusCode, null);
            }

        }

    }
}
