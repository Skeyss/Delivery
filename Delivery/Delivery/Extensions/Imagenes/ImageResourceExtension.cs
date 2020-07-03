using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Delivery.Extensions.Imagenes
{
    [ContentProperty("Source")]//es para que cuando tengas un solo atributo no es necesario poner Source
    class ImageResourceExtension : IMarkupExtension<ImageSource>
    {
        public string Source { get; set; }

        public ImageSource ProvideValue(IServiceProvider serviceProvider)
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            string assemblyName = assembly.GetName().Name;
            var image = assemblyName + "." + Source;
            return ImageSource.FromResource(image, assembly);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {

            if (string.IsNullOrEmpty(Source))
            {
                IXmlLineInfoProvider lineInfoProvider =
                    serviceProvider.GetService(typeof(IXmlLineInfoProvider))
                    as IXmlLineInfoProvider;
                IXmlLineInfo lineInfo;

                if (lineInfoProvider != null)
                {
                    lineInfo = lineInfoProvider.XmlLineInfo;
                }
                else
                {
                    lineInfo = new XmlLineInfo();
                }

                throw new XamlParseException("No puede estar en blanco Source", lineInfo);
            }

            return (this as IMarkupExtension<ImageSource>).ProvideValue(serviceProvider);
        }
    }
}
