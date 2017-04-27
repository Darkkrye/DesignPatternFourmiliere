using System;
using System.Collections.Generic;
using LibAbstraite;

namespace LibMetier
{
	public class Fourmi : PersonnageAbstrait
	{
		public override ZoneAbstraite Position { get; set; }
		public override string Nom { get; set; }

		public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList)
		{
			throw new NotImplementedException();
		}

		//public string ToString(){
			
		//}
	}
}
