using System;
using LibAbstraite;

namespace LibMetier
{
	public class Nourriture : ObjetAbstrait
	{
		public override string Nom { get; set; }
		public override ZoneAbstraite Position { get; set; }

		public Nourriture(string unNom) : base(unNom) { }


	}
}
