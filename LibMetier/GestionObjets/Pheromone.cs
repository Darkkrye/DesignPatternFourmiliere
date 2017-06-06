using System;
using LibAbstraite;

namespace LibMetier
{
	public class Pheromone : ObjetAbstrait
	{
        public override TypeObjet Type { get; set; }
        public Pheromone(string unNom, ZoneAbstraite Position) : base(unNom, Position) {
            Type = TypeObjet.Pheromone;
        }

		public override string Nom { get; set; }
		public override ZoneAbstraite Position { get; set; }
	}
}
