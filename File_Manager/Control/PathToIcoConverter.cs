using SharpVectors.Converters;
using SharpVectors.Renderers.Wpf;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace File_Manager.Control
{
    internal class PathToIcoConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Image ico = new Image();
            WpfDrawingSettings settings = new WpfDrawingSettings()
            {
                IncludeRuntime = true,
                TextAsGeometry = false
            };
            if (value is string icopath)
            {
                FileInfo icofile = new FileInfo(icopath);
                if (icofile.Extension.ToLower() == ".svg")
                {
                    FileSvgReader reader = new FileSvgReader(settings);

                    DrawingGroup draw = reader.Read(icofile.FullName);

                    if (draw != null)
                    {
                        ico.Source = new DrawingImage(draw);
                    }
                }
                else
                {
                    ico.Source = new BitmapImage(new Uri(icofile.Extension));
                }
            }
            return ico;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
