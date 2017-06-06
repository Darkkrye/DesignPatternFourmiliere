﻿using System;
using LibAbstraite;

namespace LibMetier
{
	public class Nourriture : ObjetAbstrait
	{
        public override TypeObjet Type { get; set; }
        public override string Nom { get; set; }
		public override ZoneAbstraite Position { get; set; }

        public Nourriture(string unNom, ZoneAbstraite Position) : base(unNom, Position) {
            Type = TypeObjet.Nourriture;
        }



	}
}
