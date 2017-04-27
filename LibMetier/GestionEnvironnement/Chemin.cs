using System;
using LibAbstraite;

namespace LibMetier
{
	public class Chemin : AccesAbstrait
	{
		public override ZoneAbstraite debut { get; set; }
		public override ZoneAbstraite fin { get; set; }

		public Chemin(ZoneAbstraite debut, ZoneAbstraite fin) : base(debut, fin) { }
	}
}
