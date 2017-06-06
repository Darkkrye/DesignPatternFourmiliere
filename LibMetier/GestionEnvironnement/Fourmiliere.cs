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
        internal List<ObjetAbstrait> stock;


		public Fourmiliere(){
			 AccesAbstraitsList = new List<AccesAbstrait>();
			ZoneAbstraiteList = new List<ZoneAbstraite>();
			ObjetAbstraitList = new List<ObjetAbstrait>();
			PersonnageAbstraitList = new List<PersonnageAbstrait>();
            stock = new List<ObjetAbstrait>();
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

        public List<ObjetAbstrait> getStock() {
            return stock;
        }

        public List<ObjetAbstrait> getObjets() {
            return ObjetAbstraitList;
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
                Fourmi fourmi = (Fourmi)a;
                string nourriture = "Aucune nourriture";
                if (fourmi.currentFood != null)
                    nourriture = fourmi.currentFood.Nom; 

                result += "Nom = " + a.Nom + ", Type = " + a.Type.ToString() + ", Transporte = " + nourriture + ", ";
				result += "Position = " + a.Position.X + ", " + a.Position.Y + "\n";
			}

			result += "\nStock : \n";
            foreach (ObjetAbstrait a in stock)
			{
				result += "Nom = " + a.Nom + ", \n";
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
                if (p.Type == TypePersonnage.ChercheuseDeNourriture)
                {
					// if fourmi a de la nourriture 
					if (p.GetFood())
					{
						//retourne à la fourmilière
						var zone = goHome(p);
						if (zone != null)
						{
							DeplacerPersonnage(p, p.Position, zone);
						}
					}
					else  // sinon recherche nouritture
					{
						DeplacerPersonnage(p, p.Position, rechercheNourriture(p));

					}
                }

            }
            
        }
        
        public ZoneAbstraite rechercheNourriture(PersonnageAbstrait p)
        {
            Fourmi fourmi = (Fourmi)p;

            Random random = new Random();
            int resultat;
            // liste de chemin disponible
            List<ZoneAbstraite> zone = new List<ZoneAbstraite>();
            // compteur de chemin disponible
            int cpt = 0;
            foreach(AccesAbstrait a in AccesAbstraitsList)
            {
                if (fourmi.Position == a.debut)
                {
                    // recherche d'un objet dans les zones autour de la fourmi
                    foreach (ObjetAbstrait o in ObjetAbstraitList)
                    {
                        cpt++;
                        zone.Add(a.fin);
                        if (a.fin == o.Position)
                        {
                            fourmi.SetFood(true); // La fourmi récupère la nourriture
                            fourmi.currentFood = o;
                            ObjetAbstraitList.Remove(o); // Elle disparaît de la liste des nourriture.
                            return a.fin;
                        }
                    }
                }
            }

			// si aucun objet trouve, la fourni prend un chemin au hasard
			p.SetFood(false);

            if (cpt > 0) // Il reste encore des objets, la fourmi cherche au hasard
            {
				resultat = random.Next(0, cpt);
				return zone.ElementAt(resultat);
            }
            else // Il ne reste plus d'objets, la fourmi rentre à la fourmilière
            {
				var z = goHome(p);
                if (z != null)
                {
                    return z;
                }
            }

            return null;
        }


        //Fonction à finir, créer une variable pour savoir comment les fourmis peuvent rentrer
        public ZoneAbstraite goHome(PersonnageAbstrait p)
        {
            Fourmi fourmi = (Fourmi)p;

            if (p.Position.X == 0 && p.Position.Y == 0) // La fourmi est à la maison
            {
                if (fourmi.GetFood())
                {
					fourmi.SetFood(false);
					this.stock.Add(fourmi.currentFood); // La nourriture est ajoutée aux stocks de la fourmilière.
					fourmi.currentFood = null; // La fourmi se décharge de sa nourriture
				}

                if (ObjetAbstraitList.Count() == 0) // Il n'y a plus d'objets à chercher, la fourmi reste à la maison
                {
                    foreach(ZoneAbstraite z in ZoneAbstraiteList)
                    {
                        if (z.X == 0 && z.Y == 0)
                        {
                            return z;
                        }
                    }
                }
                else // Il reste des objets sur la carte, la fourmi repart
                   return this.rechercheNourriture(p);
            }

            foreach(ZoneAbstraite z in ZoneAbstraiteList)
            {
                if ((fourmi.Position.X > 0 && fourmi.Position.Y > 0) && (z.X == fourmi.Position.X - 1 && z.Y == fourmi.Position.Y - 1)) // La fourmi se déplace en diagonale
				{
					return z;
				}
                else if ((fourmi.Position.X > 0 && fourmi.Position.Y == 0) && (z.X == fourmi.Position.X - 1 && z.Y == fourmi.Position.Y)) // La fourmi se déplace vers la droite
				{
                    return z;
				}
                else if ((fourmi.Position.X == 0 && fourmi.Position.Y > 0) && (z.X == fourmi.Position.X && z.Y == fourmi.Position.Y - 1)) // La fourmi se déplace vers le haut
				{
                    return z;
				}
            }

            return null;
        }

    }
}
