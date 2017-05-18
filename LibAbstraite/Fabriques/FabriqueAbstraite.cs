using System;
namespace LibAbstraite
{
	public abstract class FabriqueAbstraite
	{
		public abstract string Titre { get; }
		public abstract AccesAbstrait CreerAcces(ZoneAbstraite zdebut, ZoneAbstraite zfin);
		public abstract EnvironnementAbstrait CreerEnvironnement();
		public abstract ObjetAbstrait CreerObjet(string nom, ObjetAbstrait.TypeObjet type);
		public abstract PersonnageAbstrait CreerPersonnage(string nom, PersonnageAbstrait.TypePersonnage type);
		public abstract ZoneAbstraite CreerZone(string nom);
	}
}
