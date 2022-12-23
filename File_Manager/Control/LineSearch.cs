using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace File_Manager.Control
{
    /// <summary>
    /// Логика взаимодействия для LineSearch.xaml
    /// </summary>
    public class LineSearch : TextBox
    {
        public static readonly DependencyProperty ContentButtonsProperty = DependencyProperty.Register(
            nameof(ContentButtons), typeof(UIElement), typeof(LineSearch), new PropertyMetadata(default(UIElement)));
        public UIElement ContentButtons
        {
            get => (UIElement)GetValue(ContentButtonsProperty);
            set => SetValue(ContentButtonsProperty, value);
        }
        static LineSearch() // настройка поиска стиля по умолчанию
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LineSearch),
                new FrameworkPropertyMetadata(typeof(LineSearch)));
        }
    }
}
