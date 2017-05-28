using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Threading;

namespace AnthillSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.Fourmiliere;
            Add.Click += Add_Click;
            Remove.Click += Remove_Click;
            //AproposBtn.Click += Apropos_Click;
            TourSuivant.Click += TourSuivant_Click;
            dessine();
            Plateau.Background = new SolidColorBrush(Colors.Green);
        }

        private void dessine()
        {
            Plateau.RowDefinitions.Clear();
            Plateau.ColumnDefinitions.Clear();
            Plateau.Children.Clear();

            for (int i = 0; i < App.Fourmiliere.DimensionX; i++)
            {
                Plateau.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < App.Fourmiliere.DimensionY; i++)
            {
                Plateau.RowDefinitions.Add(new RowDefinition());
            }

            dessineFourmi();
        }

        public void dessineFourmi()
        {
            foreach (var item in App.Fourmiliere.ListFourmis)
            {
                /*  Ellipse e = new Ellipse();
                  e.Fill = new SolidColorBrush(Colors.AliceBlue);
                  e.Margin = new Thickness(3);*/
                var e = new Image();
                e.Source = new BitmapImage(new Uri("Ressources/ant.png", UriKind.Relative));
                Plateau.Children.Add(e);
                Grid.SetColumn(e, item.X);
                Grid.SetRow(e, item.Y);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            App.Fourmiliere.AjouteFourmi();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            App.Fourmiliere.DeleteFourmi();
        }
/*
        private void Apropos_Click(object sender, RoutedEventArgs e)
        {
            var ap = new Apropos();
            ap.ShowDialog();
            // App.Fourmiliere.DeleteFourmi();
        }*/

        private void TourSuivant_Click(object sender, RoutedEventArgs e)
        {
            App.Fourmiliere.TourSuivant();

        }

        private void Avance_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(App.Fourmiliere.Avance);
            t.Start();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            App.Fourmiliere.Stop();
        }
    }
}
