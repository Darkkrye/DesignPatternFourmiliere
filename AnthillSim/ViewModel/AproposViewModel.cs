using LibAbstraite;
using LibMetier;
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

        public AproposViewModel(Fourmiliere Four)
        {
            Fourmiliere = Four;
            Meteo = Four.Meteo;
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
        }
    }
}
