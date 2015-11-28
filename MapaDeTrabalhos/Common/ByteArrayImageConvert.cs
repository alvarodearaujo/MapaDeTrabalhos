using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace MapaDeTrabalhos.Common
{
    public class ByteArrayImageConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || !(value is byte[]))
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/default.gif"));
            }
            else
            {
                return Functions.ConvertArrayBytesToImage((byte[])value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
