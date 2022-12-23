using File_Manager.Control;
using File_Manager.ViewModel;
using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace File_Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }

    public class DragDropCl : IDropTarget
    {

        private static IDropTarget _instance; // синглтон
        public static IDropTarget instance => _instance ??= new DragDropCl();
        public void DragOver(IDropInfo dropInfo)
        {
            if(dropInfo.TargetCollection is ObservableCollection<ObjectsVM> collection)
            {
                if (dropInfo.Data is ObjectsVM item)
                {
                    /*if (dropInfo.TargetItem == null && !collection.Contains(item)) // если не наведено на папку !collection.Contains(item) - папка назначения не содержит текущий элемент
                    {
                        dropInfo.Effects = DragDropEffects.Move;
                        dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                        dropInfo.EffectText = "Перемістити ";
                        dropInfo.DestinationText = $"{item.NAME}";
                    }*/
                    if (dropInfo.TargetItem is DirVM dir && dir != item) // dir != item не перемещаем саму в себя
                    {
                        dropInfo.Effects = DragDropEffects.Move;
                        dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                        dropInfo.EffectText = "Перемістити у";
                        dropInfo.DestinationText = $"{dir.NAME}";
                    }
                }
                else if (dropInfo.Data is ICollection<object> items)
                {

                }
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is DirVM sourcedir && dropInfo.TargetItem is DirVM destdir)
            {
                try
                {
                    Directory.Move(sourcedir.FULLNANE, Path.Combine(destdir.FULLNANE, sourcedir.NAME));
                }
                catch(System.Exception e)
                {

                }
            }
            else if (dropInfo.Data is FileVM sourcefile && dropInfo.TargetItem is DirVM destdir2)
            {
                try
                { 
                    Directory.Move(sourcefile.FULLNANE, Path.Combine(destdir2.FULLNANE, sourcefile.NAME));
                }
                catch (System.Exception e)
                {
                    
                }
            }
        }
    }
}
