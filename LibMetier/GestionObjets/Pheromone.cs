using System;
using LibAbstraite;

namespace LibMetier
{
	public class Pheromone : ObjetAbstrait
	{
        public override TypeObjet Type { get; set; }

        public Pheromone(string unNom, ZoneAbstraite Position) : base(unNom, Position) {
            this.Vie = 100;
            Type = TypeObjet.Pheromone;
        }

        public override int Vie { get; set; }
        public override string Nom { get; set; }

		public override ZoneAbstraite Position { get; set; }

        public static bool Equals(Func<Type> getType)
        {
            throw new NotImplementedException();
        }
    }
}
