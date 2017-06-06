using System;
namespace LibAbstraite
{
	public abstract class ObjetAbstrait
	{
		public abstract TypeObjet Type { get; set; }

		public abstract string Nom { get; set; }

		public abstract ZoneAbstraite Position { get; set; }

		protected ObjetAbstrait(String unNom, ZoneAbstraite Position)
		{
			Nom = unNom;
            this.Position = Position;
		}


	}
}
