using System;
using LibAbstraite;

namespace LibMetier
{
	public class Pheromone : ObjetAbstrait
	{
        public override TypeObjet Type { get; set; }
        public Pheromone(string unNom,int Vie, ZoneAbstraite Position) : base(unNom, Position) {
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
