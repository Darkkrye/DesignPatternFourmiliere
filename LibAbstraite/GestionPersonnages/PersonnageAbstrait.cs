using System;
using System.Collections.Generic;

namespace LibAbstraite
{
	public abstract class PersonnageAbstrait
	{
		protected Random Hasard;

		public abstract string Nom { get; set; }

		public abstract ZoneAbstraite Position { get; set; }

		public abstract ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList);

        public abstract void ChangementEtat(EtatFourmiAbstrait etatCourant);

		//public PersonnageAbstrait();
	}
}
