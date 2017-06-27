using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Microsoft.Win32;
using LibMetier;
using LibAbstraite;
using System.Windows;
using System.Windows.Forms;

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
            WindowState = WindowState.Maximized;
            DataContext = App.Fourmiliere;
            Add.Click += Add_Click;
            Remove.Click += Remove_Click;
            Apropos.Click += Apropos_Click;
            TourSuivant.Click += TourSuivant_Click;
            dessine();

        }

        private void dessine()
        {
            App.Fourmiliere.Refresh();
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

            dessineFourmiliere();
            dessineObjet();
            dessineFourmi();
        }

        public void dessineFourmi()
        {
            foreach (var item in App.Fourmiliere.ListFourmis)
            {
                if (!(item.Position.X == App.Fourmiliere.Fourmiliere.Position.X &&
                    item.Position.Y == App.Fourmiliere.Fourmiliere.Position.Y))
                {
                    
                    var e = new Image();
                    if (item.GetFood())
                        e.Source = new BitmapImage(new Uri("Ressources/antWithSalade.png", UriKind.Relative));
                    else
                        e.Source = new BitmapImage(new Uri("Ressources/ant.png", UriKind.Relative));

                    Plateau.Children.Add(e);
                    Grid.SetColumn(e, item.Position.X);
                    Grid.SetRow(e, item.Position.Y);
                }
            }
        }

        public void dessineObjet()
        {
            foreach (var item in App.Fourmiliere.ListObjet)
            {

                if (item.Type == LibAbstraite.TypeObjet.Nourriture)
                {
                    var e = new Image();
                    e.Source = new BitmapImage(new Uri("Ressources/salade.png", UriKind.Relative));
                    e.Opacity = Convert.ToSingle(item.Vie) / 10;
                    Plateau.Children.Add(e);
                    Grid.SetColumn(e, item.Position.X);
                    Grid.SetRow(e, item.Position.Y);
                }
                else if (item.Type == TypeObjet.Pheromone)
                {
                    var e = new Image();
                    e.Source = new BitmapImage(new Uri("Ressources/pheromone.png", UriKind.Relative));
                    e.Opacity = Convert.ToSingle(item.Vie) / 100;
                    Plateau.Children.Add(e);
                    Grid.SetColumn(e, item.Position.X);
                    Grid.SetRow(e, item.Position.Y);
                }

            }
        }

        public void dessineFourmiliere()
        {
            var color = (App.Fourmiliere.Fourmiliere.Meteo.Etat == EtatMeteo.Soleil) ? new SolidColorBrush(Colors.SaddleBrown) :
                                 (App.Fourmiliere.Fourmiliere.Meteo.Etat == EtatMeteo.Orage) ? new SolidColorBrush(Colors.Gray) :
                                 new SolidColorBrush(Colors.Blue);
            Plateau.Background = color;
            var e = new Image();
            e.Source = new BitmapImage(new Uri("Ressources/fourmiliere.png", UriKind.Relative));
            Plateau.Children.Add(e);
            Grid.SetColumn(e, App.Fourmiliere.Fourmiliere.Position.X);
            Grid.SetRow(e, App.Fourmiliere.Fourmiliere.Position.Y);

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            App.Fourmiliere.AjouteFourmi();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            App.Fourmiliere.DeleteFourmi();
        }

        private void Apropos_Click(object sender, RoutedEventArgs e)
        {
            var ap = new Apropos(App.Fourmiliere.Fourmiliere);
            ap.ShowDialog();
            
        }

        private void TourSuivant_Click(object sender, RoutedEventArgs e)
        {
            App.Fourmiliere.TourSuivant();
            dessine();

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

        private void Sauvegarder_Click(object sender, RoutedEventArgs e)
        {

            string folderName = "";
            FolderBrowserDialog browse = new FolderBrowserDialog();

            DialogResult result = browse.ShowDialog();
            if (result.ToString() == "OK")
            {
                folderName = browse.SelectedPath;



                int i = 0;
                bool etat = false;
                var fileName = folderName + "/save";

                do
                {
                    i++;
                    etat = (!File.Exists(fileName + i + ".json"));
                    if (etat)
                        fileName = fileName + i + ".json";
                } while (!etat);

                
                File.WriteAllText(fileName, ParserXML.Sauvegarder(App.Fourmiliere.Fourmiliere));
            }

        }

        private void Charger_Click(object sender, RoutedEventArgs e)
        {
            string filename = "";
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = "Charger une Fourmiliere";
            openFileDialog.Filter = "Fichier JSON (*.json)|*.json|Tous les fichiers (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                filename = openFileDialog.FileName;

            var res = ParserXML.Charger(File.ReadAllText(filename));


            }

        }
    }
}
