using System;
using System.Collections.Generic;

namespace LibAbstraite
{
	public abstract class PersonnageAbstrait
	{
		protected Random Hasard;

		public enum TypePersonnage { Fourmi };

		public abstract string Nom { get; set; }
		private TypePersonnage TypeP;

		public abstract ZoneAbstraite Position { get; set; }

		public abstract ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList);

        	public abstract void ChangementEtat(EtatFourmiAbstrait etatCourant);

		public PersonnageAbstrait(string nom, TypePersonnage type){
			Nom = nom;
			TypeP = type;
		}
	}
}
