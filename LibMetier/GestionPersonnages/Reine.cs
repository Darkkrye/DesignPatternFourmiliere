using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LibAbstraite;

namespace LibMetier
{
    public class Reine : PersonnageAbstrait
    {

        public override ZoneAbstraite PreviousPosition { get; set; }
        public ObservableCollection<Etape> EtapesList { get; set; }
        public override EtatFourmiAbstrait EtatCourant { get; set; }


        public int X { get; set; }
        public int Y { get; set; }

        public override string Nom { get; set; }

        public override int Vie { get; set; }

        public override ZoneAbstraite Position { get; set; }

        public override TypePersonnage Type { get; set; }

        public Reine(string nom, ZoneAbstraite position) : base(nom, position)
        {

            EtapesList = new ObservableCollection<Etape>();
            EtapesList.Add(new Etape() { NumeroTour = 1, X = X, Y = Y });
            this.Vie = 100;
        }

        public override void ChangementEtat(EtatFourmiAbstrait etatCourant)
        {
           
        }

        public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList)
        {
            // TODO
            return null;
        }

        
    }
}