using LibMetier;
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
using System.Windows.Shapes;


namespace AnthillSim
{
    /// <summary>
    /// Interaction logic for Apropos.xaml
    /// </summary>
    public partial class Apropos : Window
    {
        public Apropos(Fourmiliere Four)
        {
            InitializeComponent();
            DataContext = new AproposViewModel(Four);
        }
    }
}
