using System;
using System.Collections.Generic;
using LibAbstraite;

namespace LibMetier
{
	public class BoutDeTerrain : ZoneAbstraite
	{
		public override string Nom { get; set; }
		List<ObjetAbstrait> ObjetList;
		List<PersonnageAbstrait> PersonnageList;
		List<AccesAbstrait> AccesAbstraiteList;
        public override int X { get; set; }
        public override int Y { get; set; }
        public override bool isOccuped { get; set; }

        public BoutDeTerrain(string unNom, int x, int y) : base(unNom, x, y)
		{
			ObjetList = new List<ObjetAbstrait>();
			PersonnageList = new List<PersonnageAbstrait>();
			AccesAbstraiteList = new List<AccesAbstrait>();
		}

		public override void AjouteAcces(AccesAbstrait acces)
		{
			AccesAbstraiteList.Add(acces);
		}

		public override void AjouteObjet(ObjetAbstrait objet)
		{
			ObjetList.Add(objet);
		}

		public override void AjoutePersonnage(PersonnageAbstrait unPersonnage)
		{
			PersonnageList.Add(unPersonnage);
		}

		public override void RetirerPersonnage(PersonnageAbstrait unPersonnage)
		{
			PersonnageList.Remove(unPersonnage);
		}
	}
}
