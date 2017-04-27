using System;
using System.Collections.Generic;
using LibAbstraite;

namespace LibMetier
{
	public class Fourmiliere : EnvironnementAbstrait
	{
		List<AccesAbstrait> AccesAbstraitsList;
		List<ZoneAbstraite> ZoneAbstraiteList;
		List<ObjetAbstrait> ObjetAbstraitList;
		List<PersonnageAbstrait> PersonnageAbstraitList;


		public Fourmiliere(){
			AccesAbstraitsList = new List<AccesAbstrait>();
			ZoneAbstraiteList = new List<ZoneAbstraite>();
			ObjetAbstraitList = new List<ObjetAbstrait>();
			PersonnageAbstraitList = new List<PersonnageAbstrait>();
		}
		public override void AjouteChemins(FabriqueAbstraite fabrique, params AccesAbstrait[] accesArray)
		{
			foreach (AccesAbstrait acces in accesArray)
			{
				AccesAbstraitsList.Add(acces);
			}
		}

		public override void AjouteObjet(ObjetAbstrait unObjet)
		{
			ObjetAbstraitList.Add(unObjet);
		}

		public override void AjoutePersonnage(PersonnageAbstrait unPersonnage)
		{
			PersonnageAbstraitList.Add(unPersonnage);
		}

		public override void AjouteZoneAbstraits(params ZoneAbstraite[] zoneAbstraitsArray)
		{
			foreach(ZoneAbstraite zone in zoneAbstraitsArray){
				ZoneAbstraiteList.Add(zone);
			}
		}

		public override void ChargerEnvironnement(FabriqueAbstraite fabrique)
		{
			throw new NotImplementedException();
		}

		public override void ChargerObjets(FabriqueAbstraite fabrique)
		{
			throw new NotImplementedException();
		}

		public override void ChargerPersonnages(FabriqueAbstraite fabrique)
		{
			throw new NotImplementedException();
		}

		public override void DeplacerPersonnage(PersonnageAbstrait unPersonnage, ZoneAbstraite zoneSource, ZoneAbstraite zoneFin)
		{
			throw new NotImplementedException();
		}

		public override void Simuler()
		{
			throw new NotImplementedException();
		}

		public override string Statistiques()
		{
			throw new NotImplementedException();
		}
	}
}
