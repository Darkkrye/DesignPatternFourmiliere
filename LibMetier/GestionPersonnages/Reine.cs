using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LibAbstraite;

namespace LibMetier
{
    public class Reine : PersonnageAbstrait
    {


        private Random rand;
        public int nbJourEnceinte { get; set; }

        public override ZoneAbstraite PreviousPosition { get; set; }
        public ObservableCollection<Etape> EtapesList { get; set; }
        public override EtatFourmiAbstrait EtatCourant { get; set; }


        public int X { get; set; }
        public int Y { get; set; }

        public override string Nom { get; set; }

        public override string urlImage { get; set; }

        public override int Vie { get; set; }

        public override ZoneAbstraite Position { get; set; }

        public override TypePersonnage Type { get; set; }

        public Reine(string nom, ZoneAbstraite position) : base(nom, position)
        {
            Type = TypePersonnage.Reine;
            EtapesList = new ObservableCollection<Etape>();
            EtapesList.Add(new Etape() { NumeroTour = 1, X = X, Y = Y });
            this.Vie = 100;
            urlImage = "Ressources/reine.png";
            rand = new Random();
            nbJourEnceinte = 0;
        }

        

        public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList)
        {
            // TODO
            return null;
        }
        public void changementEtat()
        {
            if (UpdateReineEnceinte())
            {
                
                new EtatReineEnceinte().ModifieEtat(this);
            }else
            {
                nbJourEnceinte = 0;
                new EtatFourmiRepos().ModifieEtat(this);
            }
        }
        public bool UpdateReineEnceinte()
        {
            
            if(EtatCourant is EtatFourmiRepos)
            {
                int res = rand.Next(0, 5);
                if(res == 0)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
            if(EtatCourant is EtatReineEnceinte)
            {
                nbJourEnceinte++;
                return true;
            }

            return true;
        }

        public void incJourEnceinte()
        {
            nbJourEnceinte++;
        }

        public override void ChangementEtat(EtatFourmiAbstrait etatCourant)
        {
            throw new NotImplementedException();
        }
    }
}