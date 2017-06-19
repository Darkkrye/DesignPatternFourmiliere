using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace AnthillSim
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {

        public static FourmilierViewModel Fourmiliere { get; set; }

        public App()
        {
            Fourmiliere = new FourmilierViewModel();
        }

    }
}
