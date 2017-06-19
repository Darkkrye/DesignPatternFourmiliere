using System;
using LibAbstraite;

namespace LibMetier
{
	public class Oeuf : ObjetAbstrait
	{
        public override TypeObjet Type { get; set; }
        public override int Vie { get; set; }


        public Oeuf(string unNom, int Vie, ZoneAbstraite Position) : base(unNom, Position) {
            Type = TypeObjet.Oeuf;
        }

		public override string Nom { get; set; }
		public override ZoneAbstraite Position { get; set; }

       
	}
}
