using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LibAbstraite;

namespace LibMetier
{
    public class Reine : PersonnageAbstrait
    {
        public Reine(string nom, ZoneAbstraite position) : base(nom, position)
        {
        }

        public override string Nom { get; set; }

        public override ZoneAbstraite Position { get; set; }

        public override TypePersonnage Type { get; set; }

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