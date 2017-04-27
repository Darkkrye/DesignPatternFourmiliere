using System;
using LibAbstraite;

namespace LibMetier
{
	public class Pheromone : ObjetAbstrait
	{
		public Pheromone(string unNom) : base(unNom) { }

		public override string Nom { get; set; }
		public override ZoneAbstraite Position { get; set; }
	}
}
