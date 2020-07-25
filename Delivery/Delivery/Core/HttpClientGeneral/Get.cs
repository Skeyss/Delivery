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
        public async static Task<(bool Status, string Mensaje)> GetService(string urlApi, string authHeaderValue)
        {
            try
            {
                if (VerificarConexion.ExisteConexionInternet() == false)
                {
                    return (false, "Verifique su conexión a internet ");
                }
                else
                {

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(urlApi);
                    if (authHeaderValue != null)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authHeaderValue);
                    }
                    Uri uri = new Uri(string.Format(urlApi, string.Empty));
                    HttpResponseMessage response = await client.GetAsync(uri);
                    int statusCode = response.StatusCode.GetHashCode();

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Solicitud procesada con éxito");
                    }
                    else if (statusCode == 400)
                    {
                        var resposeContent = await response.Content?.ReadAsStringAsync();
                        return (false, resposeContent);
                    }
                    else if (statusCode == 403)
                    {
                        return (false, "Acceso denegado ");
                    }
                    else
                    {
                        //Guardar mensaje ex
                        var resposeContent = await response.Content?.ReadAsStringAsync();
                        return (false, "Internal server error");
                    }
                }

            }
            catch (Exception ex)
            {
                //Guardar mensaje ex
                return (false, "Error durante la conexión con el servidor");
            }

        }

        public async static Task<(bool Status,string Mensaje, T Result)> GetServiceIEnumerable(string urlApi,string authHeaderValue)
        {

            try
            {
                if (VerificarConexion.ExisteConexionInternet() == false)
                {
                    return (false, "Verifique su conexión a internet ", default);
                }
                else
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(urlApi);
                    if (authHeaderValue != null)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authHeaderValue);
                    }

                    Uri uri = new Uri(string.Format(urlApi, string.Empty));
                    HttpResponseMessage response = await client.GetAsync(uri);
                    int statusCode = response.StatusCode.GetHashCode();
                   // response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        var resposeContsssent = await response.Content?.ReadAsStringAsync();
                        T result = JsonConvert.DeserializeObject<T>(resposeContsssent);

                        return (true, "Solicitud procesada con éxito", result);
                    }
                    else if (statusCode == 400)
                    {
                        var resposeContent = await response.Content?.ReadAsStringAsync();
                        return (false, resposeContent, default);
                    }
                    else if (statusCode == 403)
                    {
                        return (false, "Acceso denegado ", default);
                    }
                    else
                    {
                        //Guardar mensaje ex
                        var resposeContent = await response.Content?.ReadAsStringAsync();
                        return (false, "Internal server error", default);
                    }
                }

            }
            catch (Exception ex)
            {
                //Guardar mensaje ex
                return (false, "Error durante la conexión con el servidor", default);
            }

        }

    }
}
