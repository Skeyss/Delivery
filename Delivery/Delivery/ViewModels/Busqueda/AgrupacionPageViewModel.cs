using Delivery.Core;
using Delivery.Models;
using Delivery.Services.Manager;
using Delivery.Views.Busqueda;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Delivery.ViewModels.Busqueda
{
    public class AgrupacionPageViewModel : BaseViewModel
    {
        private ObservableCollection<Agrupacion> agrupaciones;
        private Agrupacion selectAgrupacion;


        public INavigation Navigation;
        public ObservableCollection<Agrupacion> Agrupaciones
        {
            get => agrupaciones; set
            {
                agrupaciones = value;
                OnPropertyChanged();
            }
        }
        public ICommand CommandActualizar { get ; set; }
        public ICommand AgrupacionChangedCommand { get; set; }
        public Agrupacion SelectAgrupacion
        {
            get => selectAgrupacion; set
            {
                selectAgrupacion = value;
            }
        }


        public AgrupacionPageViewModel(INavigation _Navigation) 
        {
            Navigation = _Navigation;
            CargarAgrupaciones();

            CommandActualizar = 
                new Command(async () => 
                { 
                    IsBusy = true;
                    CargarAgrupaciones();
                    IsBusy = false; 
                });
         
            
            AgrupacionChangedCommand = new Command((a) =>
            {
                // var agrupacion = a;
                Agrupacion agrupacion = selectAgrupacion;

       
               
            }
            );

        }

        private async void CargarAgrupaciones()
        {
            AgrupacionManager agrupacionManager = new AgrupacionManager();

            EstadoDeConsulta edc_resultadoDeBuscar = new EstadoDeConsulta();
            edc_resultadoDeBuscar=await  agrupacionManager.GetTasksAsync(true);

            if (edc_resultadoDeBuscar.StatusProcesamiento)
            {
                var asdas =  (IEnumerable<Agrupacion>) edc_resultadoDeBuscar.ValorObjeto ;
                Agrupaciones =  new ObservableCollection<Agrupacion>(asdas);

              // Agrupaciones =  new ObservableCollection<Agrupacion>( Agrupaciones.Where(c => c.MenuPrincipal == true));
               


            }
            else
            {
                Agrupaciones = new ObservableCollection<Agrupacion>();             
            }

        }
    }
}
