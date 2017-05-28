using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMetier;
using LibAbstraite;
using System.Collections.ObjectModel;

namespace AnthillSim
{

    public class FourmilierViewModel : ViewModelBase
    {
        public string NomApplication { get; set; }
        public ObservableCollection<Fourmi> ListFourmis { get; set; }
        public Fourmi FourmisSelect { get; set; }

        public int DimensionX { get; set; }
        public int DimensionY { get; set; }
        public int VitesseExecution { get; set; }

        private bool EnCours = false;

        public FourmilierViewModel()
        {
            NomApplication = "Fourmiliere";

            ListFourmis = new ObservableCollection<Fourmi>();
            ListFourmis.Add(new Fourmi("Fourmis 0"));
            ListFourmis.Add(new Fourmi("Fourmis 1"));
            ListFourmis.Add(new Fourmi("Fourmis 2"));
            ListFourmis.Add(new Fourmi("Fourmis 3"));
            ListFourmis.Add(new Fourmi("Fourmis 4"));
            ListFourmis.Add(new Fourmi("Fourmis 5"));

            FourmisSelect = new Fourmi("");
            DimensionX = 20;
            DimensionY = 30;
            VitesseExecution = 500;

        }

        public void AjouteFourmi()
        {
            ListFourmis.Add(new Fourmi("Fourmis " + ListFourmis.Count()));
        }

        public void DeleteFourmi()
        {
            ListFourmis.Remove(FourmisSelect);
        }

        public void TourSuivant()
        {
            foreach (var item in ListFourmis)
            {
                item.Avance();
            }
        }

        public void Stop()
        {
            EnCours = false;
        }

        public void Avance()
        {
            EnCours = true;
            while (EnCours)
            {
                TourSuivant();

            }

        }


    }
}