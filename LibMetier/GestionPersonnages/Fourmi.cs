using System;
using System.Collections.Generic;
using LibAbstraite;

namespace LibMetier
{
	public class Fourmi : PersonnageAbstrait
	{
		public override ZoneAbstraite Position { get; set; }
		public override string Nom { get; set; }
        public EtatFourmiAbstrait EtatCourant { get; set; }

		public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList)
		{
			throw new NotImplementedException();
		}

        public override void ChangementEtat(EtatFourmiAbstrait etatCourant)
        {
            EtatCourant = etatCourant;
        }


        //public string ToString(){

        //}



    }
}
