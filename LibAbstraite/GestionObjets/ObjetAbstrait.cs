using System;
namespace LibAbstraite
{
	public abstract class ObjetAbstrait
	{
		public abstract string Nom { get; set; }

		public abstract ZoneAbstraite Position { get; set; }

		protected ObjetAbstrait(String unNom)
		{
			Nom = unNom;
		}
	}
}
