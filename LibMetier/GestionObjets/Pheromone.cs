using System;
using LibAbstraite;

namespace LibMetier
{
	public class Pheromone : ObjetAbstrait
	{
		public Pheromone(string unNom, ZoneAbstraite Position) : base(unNom, Position) { }

		public override string Nom { get; set; }
		public override ZoneAbstraite Position { get; set; }
	}
}
