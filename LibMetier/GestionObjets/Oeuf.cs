using System;
using LibAbstraite;

namespace LibMetier
{
	public class Oeuf : ObjetAbstrait
	{
		public Oeuf(string unNom) : base(unNom) { }

		public override string Nom { get; set; }
		public override ZoneAbstraite Position { get; set; }
	}
}
