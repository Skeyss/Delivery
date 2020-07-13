using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Delivery
{
    public class Class1 : INotifyDataErrorInfo
    {
        public bool HasErrors => throw new NotImplementedException();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
