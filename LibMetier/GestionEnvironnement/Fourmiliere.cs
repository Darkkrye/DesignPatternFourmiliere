using System;
using System.Collections.Generic;
using LibAbstraite;
using System.Linq;

namespace LibMetier
{
	public class Fourmiliere : EnvironnementAbstrait
	{
		internal List<AccesAbstrait> AccesAbstraitsList;
		internal List<ZoneAbstraite> ZoneAbstraiteList;
		internal List<ObjetAbstrait> ObjetAbstraitList;
		internal List<PersonnageAbstrait> PersonnageAbstraitList;


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
			unPersonnage.Position = zoneFin;
		}

		public override void Simuler()
		{
            AnalyseSituation();
        }

		public override string Statistiques()
		{
			string result = "";

            result += "\nZone : \n";
            foreach (ZoneAbstraite a in ZoneAbstraiteList)
            {
                result += "Nom = " + a.Nom + ", ";
                result += "Position = " + a.X + ", "  + a.Y + "\n";

            }

            result += "Acces : \n";
			foreach(AccesAbstrait a in AccesAbstraitsList){
                if(a != null)
                {
                    result += "Début = " + a.debut.Nom + ", ";
                    result += "Fin = " + a.fin.Nom + "\n";
                }
			}

			result += "\nObjet : \n";
			foreach (ObjetAbstrait a in ObjetAbstraitList)
			{
				result += "Nom = " + a.Nom + ", ";
				result += "Position = " + a.Position.X + ", " + a.Position.Y + "\n";
			}

			result += "\nPersonnage : \n";
			foreach (PersonnageAbstrait a in PersonnageAbstraitList)
			{
				result += "Nom = " + a.Nom + ", ";
				result += "Position = " + a.Position.X + ", " + a.Position.Y + "\n";
			}
			return result;
		}

        public List<ZoneAbstraite> getZoneAbstraiteList()
        {
            return ZoneAbstraiteList;
        }

        public void AnalyseSituation()
        {
            foreach (PersonnageAbstrait p in PersonnageAbstraitList)
            {
                // if fourmi a de la nourriture 
                if (p.GetFood())
                {
                    //retourne à la fourmilière
                    DeplacerPersonnage(p, p.Position, goHome(p));
                }
                else  // sinon recherche nouritture
                {
                    DeplacerPersonnage(p, p.Position, rechercheNourriture(p));

                }

            }
            
        }
        
        public ZoneAbstraite rechercheNourriture(PersonnageAbstrait p)
        {
            Random random = new Random();
            int resultat;
            // liste de chemin disponible
            List<ZoneAbstraite> zone = new List<ZoneAbstraite>();
            // compteur de chemin disponible
            int cpt = 0;
            foreach(AccesAbstrait a in AccesAbstraitsList)
            {
                if (p.Position == a.debut)
                {
                    // recherche d'un objet dans les zones autour de la fourmi
                    foreach (ObjetAbstrait o in ObjetAbstraitList)
                    {
                        cpt++;
                        zone.Add(a.fin);
                        if (a.fin == o.Position)
                        {
                            p.SetFood(true);
                            return a.fin;
                        }
                    }
                }
            }
            // si aucun objet trouve, la fourni prend un chemin au hasard
            p.SetFood(false);

            resultat = random.Next(0, cpt);
            return zone.ElementAt(resultat);
        }


        //Fonction à finir, créer une variable pour savoir comment les fourmis peuvent rentrer
        public ZoneAbstraite goHome(PersonnageAbstrait p)
        {
            Random random = new Random();
            int resultat;
            // liste de chemin disponible
            List<ZoneAbstraite> zone = new List<ZoneAbstraite>();
            //la fourni prend un chemin au hasard
            p.SetFood(false);

            resultat = random.Next(0, 1);
            return zone.ElementAt(resultat);
        }
    }
}
