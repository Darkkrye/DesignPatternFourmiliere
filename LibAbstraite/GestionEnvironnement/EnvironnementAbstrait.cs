using System;
using System.Collections.Generic;

namespace LibAbstraite
{
	public abstract class EnvironnementAbstrait
	{
		public abstract void AjouteChemins(FabriqueAbstraite fabrique, params AccesAbstrait[] accesArray);
		public abstract void AjouteObjet(ObjetAbstrait unObjet);
		public abstract void AjoutePersonnage(PersonnageAbstrait unPersonnage);
		public abstract void AjouteZoneAbstraits(params ZoneAbstraite[] zoneAbstraitsArray);
		public abstract void ChargerEnvironnement(FabriqueAbstraite fabrique);
		public abstract void ChargerObjets(FabriqueAbstraite fabrique);
		public abstract void ChargerPersonnages(FabriqueAbstraite fabrique);
		public abstract void DeplacerPersonnage(PersonnageAbstrait unPersonnage, ZoneAbstraite zoneSource, ZoneAbstraite zoneFin);
		public abstract void Simuler();
		public abstract string Statistiques();
	}
}
