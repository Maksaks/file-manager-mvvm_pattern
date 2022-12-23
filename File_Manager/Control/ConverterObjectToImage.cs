using File_Manager.ViewModel;
using SharpVectors.Converters;
using SharpVectors.Renderers.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace File_Manager.Control
{
    class ConverterObjectToImage : IValueConverter
    {
        private WpfDrawingSettings settings;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            settings = new WpfDrawingSettings()
            {
                IncludeRuntime = true,
                TextAsGeometry = false
            };
            if (value is DirVM dir)
            {
                FileInfo imgpath;

                if (dir.FULLNANE.EndsWith(":\\"))
                {
                    imgpath = GetImage("disk");
                }
                else
                {
                    imgpath = GetImage("folder");
                }


                FileSvgReader reader = new FileSvgReader(settings);

                DrawingGroup draw = reader.Read(imgpath.FullName);

                if (draw != null)
                {
                    return new DrawingImage(draw);
                }
            }
            else if (value is FileVM file)
            {
                string ex = new FileInfo(file.FULLNANE).Extension;
                FileInfo imgpath = GetImage(ex);

                if (imgpath.Extension.ToLower() == ".svg")
                {
                    FileSvgReader reader = new FileSvgReader(settings);

                    DrawingGroup draw = reader.Read(imgpath.FullName);

                    if (draw != null)
                    {
                        return new DrawingImage(draw);
                    }
                }
                else
                {
                    return new BitmapImage(new Uri(imgpath.Extension));
                }
            }
            return new DrawingImage();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static FileInfo GetImage(string ex)
        {
            if (ex == "disk")
            {
                return new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icon", "harddisk.svg"));
            }
            else if (ex == "folder")
            {
                return new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icon", "folder.svg"));
            }
            else if (ex.ToLower() == ".doc" || ex.ToLower() == ".txt" || ex.ToLower() == ".docx")
            {
                return new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icon", "file-document.svg"));
            }
            else if (ex.ToLower() == ".pdf")
            {
                return new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icon", "file-pdf-box.svg"));
            }
            else if (ex.ToLower() == ".xml")
            {
                return new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icon", "file-xml-box.svg"));
            }
            else if (ex.ToLower() == ".zip" || ex.ToLower() == ".rar")
            {
                return new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icon", "folder-zip.svg"));
            }
            else if (ex.ToLower() == ".cs")
            {
                return new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icon", "language-csharp.svg"));
            }
            else if (ex.ToLower() == ".py")
            {
                return new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icon", "language-python.svg"));
            }
            else
            {
                return new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icon", "file.svg"));
            }
        }
    }
}
