using System;
using System.Collections.Generic;

namespace LibAbstraite
{
	public abstract class ZoneAbstraite
	{
        private string unNom;

        public abstract string Nom { get; set; }
        public abstract int X { get; set; }
        public abstract int Y { get; set; }

        protected ZoneAbstraite(string unNom, int x, int y)
		{
			this.Nom = unNom;
            this.X = x;
            this.Y = y;
		}

        public ZoneAbstraite(string unNom)
        {
            this.unNom = unNom;
        }

        public abstract void AjouteAcces(AccesAbstrait acces);
		public abstract void AjouteObjet(ObjetAbstrait objet);
		public abstract void AjoutePersonnage(PersonnageAbstrait unPersonnage);
		public abstract void RetirerPersonnage(PersonnageAbstrait unPersonnage);
	}
}
