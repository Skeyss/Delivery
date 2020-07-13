using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.HttpClientGeneral
{
    public class Post<T>
    {
        public async static Task<(bool Status, string Mensaje)> PostService(string urlApi, string authHeaderValue, T entidad)
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
                    string json = JsonConvert.SerializeObject(entidad);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(uri, content);
                    int statusCode = response.StatusCode.GetHashCode();

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Registro creado con éxito");
                    }
                    else if (statusCode == 400)
                    {
                        var resposeContent = await response.Content?.ReadAsStringAsync();
                        return (false, resposeContent);
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

        public async static Task<(bool Status, string Mensaje,R Entidad)> PostEntidadService<R>(string urlApi, string authHeaderValue,T entidad)
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
                    string json = JsonConvert.SerializeObject(entidad);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(uri, content);
                    int statusCode = response.StatusCode.GetHashCode();
    
                    if (response.IsSuccessStatusCode)
                    {                       
                        var resposeContsssent = await response.Content?.ReadAsStringAsync();
                        R result = JsonConvert.DeserializeObject<R>(resposeContsssent);

                        return (true, "Solicitud procesada con éxito", result);
                    }
                    else if(statusCode==400)
                    {
                        var resposeContent = await response.Content?.ReadAsStringAsync();
                        return (false, resposeContent,default);
                    }
                    else
                    {
                        //Guardar mensaje ex
                        var resposeContent = await response.Content?.ReadAsStringAsync();
                        return (false, "Internal server error",default);
                    }
                }

            }
            catch (Exception ex)
            {
                //Guardar mensaje ex
                return (false, "Error durante la conexión con el servidor",default);
            }

        }

        public async static Task<(bool Status, string Mensaje, string result)> PostStringService(string urlApi, string authHeaderValue, T entidad)
        {
            try
            {
                if (VerificarConexion.ExisteConexionInternet() == false)
                {
                    return (false, "Verifique su conexión a internet ", "");
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
                    string json = JsonConvert.SerializeObject(entidad);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(uri, content);
                    int statusCode = response.StatusCode.GetHashCode();

                    if (response.IsSuccessStatusCode)
                    {
                        var resposeContsssent = await response.Content?.ReadAsStringAsync();
                        return (true, "Solicitud procesada con éxito", resposeContsssent);
                    }
                    else if (statusCode == 400)
                    {
                        var resposeContent = await response.Content?.ReadAsStringAsync();
                        return (false, resposeContent, "");
                    }
                    else
                    {
                        //Guardar mensaje ex
                        var resposeContent = await response.Content?.ReadAsStringAsync();
                        return (false, "Internal server error", "");
                    }
                }

            }
            catch (Exception ex)
            {
                //Guardar mensaje ex
                return (false, "Error durante la conexión con el servidor", "");
            }

        }
    }
}
