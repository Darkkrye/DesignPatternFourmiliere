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

		public override ObjetAbstrait CreerObjet(string nom)
		{
			//return new ObjetAbstrait();
			throw new NotImplementedException();
		}

		public override PersonnageAbstrait CreerPersonnage(string nom)
		{
            //wpf
			return new Fourmi("");
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
