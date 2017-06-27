using LibAbstraite;
using LibMetier;
using LibMetier.GestionTemps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthillSim
{
    public class AproposViewModel : ViewModelBase
    {

        public List<PersonnageAbstrait> ListFourmis { get; set; }
        public List<ObjetAbstrait> ListObjet { get; set; }
        public Meteo Meteo { get; set; }
        public Fourmiliere Fourmiliere { get; set; }
        public List<ObjetAbstrait> stock { get; set; }
        public temps time { get; set; }

        public AproposViewModel(Fourmiliere Four)
        {
            stock = Four.stock;
            Fourmiliere = Four;
            Meteo = Four.Meteo;
            time = Four.time;
            Refresh();
        }

        public void Refresh()
        {
            ListObjet = new List<ObjetAbstrait>();
            foreach (var a in Fourmiliere.getObjets())
            {
                if(a.Type == TypeObjet.Nourriture)
                {
                    ListObjet.Add(a);
                }
            }
            
            ListFourmis = Fourmiliere.getPersonnages();
            Meteo.observers = ListFourmis;
            stock = Fourmiliere.stock;
            time = Fourmiliere.time;
        }
    }
}
