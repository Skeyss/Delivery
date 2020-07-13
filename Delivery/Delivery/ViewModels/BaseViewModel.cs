using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using Delivery.Models;
using Delivery.Services;
using System.Collections;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Delivery.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        public bool IsBusy
        {
            get => isBusy; set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        //bool isBusy = false;
        //public bool IsBusy
        //{
        //    get { return isBusy; }
        //    set { SetProperty(ref isBusy, value); OnPropertyChanged(); }
        //}

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    var changed = PropertyChanged;
        //    if (changed == null)
        //        return;

        //    changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        public event PropertyChangedEventHandler PropertyChanged;



        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion

        #region INotifyDataErrorInfo

        public IDictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private bool isBusy;

        public bool HasErrors
        {
            get
            {
                return _errors?.Any(x => x.Value?.Any() == true) == true;
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName) && _errors[propertyName].Any())
            {
                return _errors[propertyName];
            }
            else
            {
                return new List<string>();
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void Validacion(object value, Func<bool> rule, string error, [CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return;
            }

            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
            }

            if (rule() == false)
            {
                _errors.Add(propertyName, new List<string> { error });
            }

            #region dataqanotacion

            var validationContext = new ValidationContext(this)
            {
                MemberName = propertyName
            };

             var listValidationResult = new List<ValidationResult>();

            Validator.TryValidateProperty(value, validationContext, listValidationResult);

            if (listValidationResult.Count > 0)
            {
                foreach (ValidationResult item in listValidationResult)
                {
                    if (_errors.ContainsKey(propertyName))
                    {
                        _errors[propertyName].Add(item.ErrorMessage);
                    }
                    else
                    {
                        _errors.Add(propertyName, new List<string> { item.ErrorMessage });
                    }
                }

            }
            #endregion


            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }



        #endregion


    }
}
