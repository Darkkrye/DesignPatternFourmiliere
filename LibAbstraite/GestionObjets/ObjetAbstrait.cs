using System;
namespace LibAbstraite
{
	public abstract class ObjetAbstrait
	{
		public enum TypeObjet { Nourriture, Oeuf, Pheromone };

		public abstract string Nom { get; set; }

		public abstract ZoneAbstraite Position { get; set; }

		protected ObjetAbstrait(String unNom, ZoneAbstraite Position)
		{
			Nom = unNom;
            this.Position = Position;
		}


	}
}
