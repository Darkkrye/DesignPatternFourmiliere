using System;
using LibAbstraite;

namespace LibMetier
{
	public class Oeuf : ObjetAbstrait
	{
		public Oeuf(string unNom, ZoneAbstraite Position) : base(unNom, Position) { }

		public override string Nom { get; set; }
		public override ZoneAbstraite Position { get; set; }
	}
}
