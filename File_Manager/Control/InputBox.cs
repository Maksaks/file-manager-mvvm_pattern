using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace File_Manager.Control
{
    public class InputBox
    {

        Window inputBox = new Window();//window for the inputbox
        FontFamily font = new FontFamily("Times New Roman");//font for the whole inputbox
        int FontSize = 20;//fontsize for the input
        StackPanel elemcontent = new StackPanel();// items container
        string title = "FileManager - Форма для введення";
        string message;
        string submitbutton = "OK";
        Brush BoxBackgroundColor = Brushes.DimGray;
        Brush InputBackgroundColor = Brushes.LightGray;
        TextBox input = new TextBox();
        Button ok = new Button();


        public InputBox(string message)
        {
            this.message = message;
            InitInputBox();
        }
        private void InitInputBox()
        {
            inputBox.Height = 250;// Box Height
            inputBox.Width = 400;// Box Width
            inputBox.Background = BoxBackgroundColor;
            inputBox.Title = title;
            inputBox.Content = elemcontent;
            TextBlock content = new TextBlock();
            content.TextWrapping = TextWrapping.Wrap;
            content.Background = null;
            content.HorizontalAlignment = HorizontalAlignment.Center;
            content.Text = message;
            content.FontFamily = font;
            content.Margin = new Thickness(10);
            content.FontSize = FontSize;
            elemcontent.Children.Add(content);

            input.Background = InputBackgroundColor;
            input.FontFamily = font;
            input.FontSize = FontSize;
            input.HorizontalAlignment = HorizontalAlignment.Center;
            input.MinWidth = 200;
            input.MaxWidth = 200;
            input.Margin = new Thickness(30);
            elemcontent.Children.Add(input);
            ok.Width = 70;
            ok.Height = 30;
            ok.Click += ok_Click;
            ok.Content = submitbutton;
            ok.HorizontalAlignment = HorizontalAlignment.Center;
            elemcontent.Children.Add(ok);

        }

        void ok_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Close();
        }

        public string ShowDialog()
        {
            inputBox.ShowDialog();
            return input.Text;
        }
    }

    public class SeacrhBox
    {

        Window inputBox = new Window();//window for the inputbox
        FontFamily font = new FontFamily("Times New Roman");//font for the whole inputbox
        int FontSize = 20;//fontsize for the input
        StackPanel elemcontent = new StackPanel();// items container
        string title = "FileManager - Форма для пошуку";
        string message = "Введіть пошуковий запит";
        string submitbutton = "Здійснити пошук";
        Brush BoxBackgroundColor = Brushes.DimGray;
        Brush InputBackgroundColor = Brushes.LightGray;
        TextBox input = new TextBox();
        Button ok = new Button();


        public SeacrhBox()
        {
            InitInputBox();
        }
        private void InitInputBox()
        {
            inputBox.Height = 250;// Box Height
            inputBox.Width = 400;// Box Width
            inputBox.Background = BoxBackgroundColor;
            inputBox.Title = title;
            inputBox.Content = elemcontent;
            TextBlock content = new TextBlock();
            content.TextWrapping = TextWrapping.Wrap;
            content.Background = null;
            content.HorizontalAlignment = HorizontalAlignment.Center;
            content.Text = message;
            content.FontFamily = font;
            content.Margin = new Thickness(10);
            content.FontSize = FontSize;
            elemcontent.Children.Add(content);

            input.Background = InputBackgroundColor;
            input.FontFamily = font;
            input.FontSize = FontSize;
            input.HorizontalAlignment = HorizontalAlignment.Center;
            input.MinWidth = 200;
            input.MaxWidth = 200;
            input.Margin = new Thickness(30);
            elemcontent.Children.Add(input);
            ok.Width = 150;
            ok.Height = 40;
            ok.Click += ok_Click;
            ok.Content = submitbutton;
            ok.HorizontalAlignment = HorizontalAlignment.Center;
            elemcontent.Children.Add(ok);

        }

        void ok_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Close();
        }

        public string ShowDialog()
        {
            inputBox.ShowDialog();
            
            return input.Text;
        }
    }
}
