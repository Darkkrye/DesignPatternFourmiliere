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

		public override ObjetAbstrait CreerObjet(string nom, ObjetAbstrait.TypeObjet type)
		{
			if(type == ObjetAbstrait.TypeObjet.Nourriture){
				return new Nourriture(nom);
			}else if(type == ObjetAbstrait.TypeObjet.Oeuf){
				return new Oeuf(nom);
			}
			else if (type == ObjetAbstrait.TypeObjet.Pheromone)
			{
				return new Pheromone(nom);
			}else{
				throw new NotImplementedException();
			}
		}

		public override PersonnageAbstrait CreerPersonnage(string nom, PersonnageAbstrait.TypePersonnage type)
		{
			if (type == PersonnageAbstrait.TypePersonnage.Fourmi)
			{
				return new Fourmi(nom);
			}
			else
			{
				throw new NotImplementedException();
			}
		}

		public override ZoneAbstraite CreerZone(string nom)
		{
			return new BoutDeTerrain(nom);
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
