using Delivery.Core.HttpClientGeneral;
using Delivery.Models.Inicio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services
{
    public class BienvenidaService
    {
        //public static async Task<ObservableCollection<Bienvenida>> ConseguirPantallaDeBienvenida()
        //{
        //    //Get<Bienvenida> get = new Get<Bienvenida>();
        //    //var resultado= await get.GetService(AppSettings.ApiUrl + "api/Pantalladebienvenida/Mostrar",nu);

        //}

        public async static Task<(bool Status, string mensaje, List<Pantalladebienvenida> ListBienvenida)> ConseguirPantallaDeBienvenida()
        {
            string url = AppSettings.ApiUrl + "api/IncioDeSesion/PantallaDeBienvenida";
            var result= await Get<IEnumerable<Pantalladebienvenida>>.GetServiceIEnumerable(url, null);
            if (result.Status)
            {
                List<Pantalladebienvenida> list = result.Result.ToList();
                return (result.Status, result.Mensaje, list);
            }
            else
            {
                return (result.Status, result.Mensaje, null);
            }
           
            
        }
    }
}
