using System;
using LibAbstraite;

namespace LibMetier
{
	public class FabriqueFourmilliere : FabriqueAbstraite
	{
		// instanciation singleton
		private static FabriqueFourmilliere instanceFourmilliere;

        	public override string Titre { get; }

		public override AccesAbstrait CreerAcces(ZoneAbstraite zdebut, ZoneAbstraite zfin)
		{
			return new Chemin(zdebut, zfin);
		}

		public override EnvironnementAbstrait CreerEnvironnement()
		{
			return new Fourmiliere();
		}

		public override ObjetAbstrait CreerObjet(string nom, ObjetAbstrait.TypeObjet type, ZoneAbstraite position)
		{
			if(type == ObjetAbstrait.TypeObjet.Nourriture){
				return new Nourriture(nom, position);
			}else if(type == ObjetAbstrait.TypeObjet.Oeuf){
				return new Oeuf(nom, position);
			}
			else if (type == ObjetAbstrait.TypeObjet.Pheromone)
			{
				return new Pheromone(nom, position);
			}else{
				throw new NotImplementedException();
			}
		}

		public override PersonnageAbstrait CreerPersonnage(string nom, PersonnageAbstrait.TypePersonnage type, ZoneAbstraite position)
		{
			if (type == PersonnageAbstrait.TypePersonnage.Fourmi)
			{
				return new Fourmi(nom, position);
			}
			else
			{
				throw new NotImplementedException();
			}
		}

		public override ZoneAbstraite CreerZone(string nom, int x, int y)
		{
			return new BoutDeTerrain(nom, x, y);
		}


		protected FabriqueFourmilliere() { }
		// Singleton
		public static FabriqueFourmilliere Instance(){
			if (instanceFourmilliere == null){
				instanceFourmilliere = new FabriqueFourmilliere();
			}
			return instanceFourmilliere;
		}
	}
}
