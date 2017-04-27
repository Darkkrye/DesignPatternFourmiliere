using System;
using System.Collections.Generic;

namespace LibAbstraite
{
	public abstract class ZoneAbstraite
	{
		public abstract string Nom { get; set; }

		protected ZoneAbstraite(String unNom)
		{
			this.Nom = unNom;

		}

		public abstract void AjouteAcces(AccesAbstrait acces);
		public abstract void AjouteObjet(ObjetAbstrait objet);
		public abstract void AjoutePersonnage(PersonnageAbstrait unPersonnage);
		public abstract void RetirerPersonnage(PersonnageAbstrait unPersonnage);
	}
}
