using System;
using System.Collections.Generic;
using LibAbstraite;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace LibMetier
{
    public class Fourmiliere : EnvironnementAbstrait
    {
        internal List<AccesAbstrait> AccesAbstraitsList;
        internal List<ZoneAbstraite> ZoneAbstraiteList;
        internal List<ObjetAbstrait> ObjetAbstraitList;
        internal List<PersonnageAbstrait> PersonnageAbstraitList;
        internal List<ObjetAbstrait> stock;
        public ZoneAbstraite Position { get; set; }


        public Fourmiliere()
        {

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
            foreach (ZoneAbstraite zone in zoneAbstraitsArray)
            {
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

        public List<ObjetAbstrait> getStock()
        {
            return stock;
        }

        public List<ObjetAbstrait> getObjets()
        {
            return ObjetAbstraitList;
        }

        public List<PersonnageAbstrait> getPersonnages()
        {
            return PersonnageAbstraitList;

        }


        public override string Statistiques()
        {
            string result = "";

            result += "\nZone : \n";
            foreach (ZoneAbstraite a in ZoneAbstraiteList)
            {
                result += "Nom = " + a.Nom + ", ";
                result += "Position = " + a.X + ", " + a.Y + "\n";

            }

            result += "Acces : \n";
            foreach (AccesAbstrait a in AccesAbstraitsList)
            {
                if (a != null)
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


        public void gerePheromoneVie()
        {
            foreach (ObjetAbstrait o in ObjetAbstraitList)
            {
                if (o.Type == TypeObjet.Pheromone)
                {
                    if (o.Vie > 0)
                    {
                        o.Vie--;

                    }
                    else
                    {
                        ObjetAbstraitList.Remove(o);

                    }
                }
            }
        }


        public void AnalyseSituation()
        {
            gerePheromoneVie();
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
                            if (!((p.Position.X == this.Position.X && p.Position.Y == this.Position.Y)))
                                AjouteObjet(new Pheromone("test" + zone.X + zone.Y, 10,zone));

                        }
                    }
                    else  // sinon recherche nouritture
                    {
                        var pos = rechercheNourriture(p);
                        if (pos != null)
                            DeplacerPersonnage(p, p.Position, pos);
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
            foreach (AccesAbstrait a in AccesAbstraitsList)
            {
                if (fourmi.Position == a.debut)
                {
                    // recherche d'un objet dans les zones autour de la fourmi
                    foreach (ObjetAbstrait o in ObjetAbstraitList)
                    {
                        cpt++;
                        zone.Add(a.fin);
                       if(o.Type == TypeObjet.Nourriture)
                        {
                            if (a.fin == o.Position)
                            {
                                fourmi.SetFood(true); // La fourmi récupère la nourriture
                                fourmi.currentFood = o;
                                ObjetAbstraitList.Remove(o); // Elle disparaît de la liste des nourriture.
                                AjouteObjet(new Pheromone("test" + "test" + a.fin.X + a.fin.Y,10, a.fin));
                                return a.fin;
                            }
                        }
                    }
                }
            }

            // si aucun objet trouve, la fourni prend un chemin au hasard
            p.SetFood(false);

            if (cpt > 0) // Il reste encore des objets, la fourmi cherche au hasard
            {
                resultat = random.Next(0, cpt);
                ZoneAbstraite za = zone.ElementAt(resultat);
                if (!za.isOccuped)
                {
                    foreach(ZoneAbstraite zz in ZoneAbstraiteList)
                    {
                        if (zz.X == p.Position.X && zz.Y == p.Position.Y) {
                            zz.isOccuped = false;
                        }
                    }

                    za.isOccuped = true;
                    return za;
                }
                else
                    return null;
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

            if (p.Position.X == this.Position.X && p.Position.Y == this.Position.Y) // La fourmi est à la maison
            {
                if (fourmi.GetFood())
                {
                    fourmi.SetFood(false);
                    this.stock.Add(fourmi.currentFood); // La nourriture est ajoutée aux stocks de la fourmilière.
                    fourmi.currentFood = null; // La fourmi se décharge de sa nourriture
                }

                if (ObjetAbstraitList.Count() == 0) // Il n'y a plus d'objets à chercher, la fourmi reste à la maison
                    return this.Position;
                else // Il reste des objets sur la carte, la fourmi repart
                    return this.rechercheNourriture(p);
            }

            foreach (ZoneAbstraite z in ZoneAbstraiteList)
            {
                // La fourmi se déplace en diagonale haute gauche
                if ((fourmi.Position.X > this.Position.X && fourmi.Position.Y > this.Position.Y) && (z.X == fourmi.Position.X - 1 && z.Y == fourmi.Position.Y - 1))
                    return z;
                // La fourmi se déplace en diagonale haute droite
                else if ((fourmi.Position.X < this.Position.X && fourmi.Position.Y > this.Position.Y) && (z.X == fourmi.Position.X + 1 && z.Y == fourmi.Position.Y - 1))
                    return z;
                // La fourmi se déplace en diagonale basse droite
                else if ((fourmi.Position.X < this.Position.X && fourmi.Position.Y < this.Position.Y) && (z.X == fourmi.Position.X + 1 && z.Y == fourmi.Position.Y + 1))
                    return z;
                // La fourmi se déplace en diagonale basse gauche
                else if ((fourmi.Position.X > this.Position.X && fourmi.Position.Y < this.Position.Y) && (z.X == fourmi.Position.X - 1 && z.Y == fourmi.Position.Y + 1))
                    return z;
                // La fourmi se déplace vers la gauche
                else if ((fourmi.Position.X > this.Position.X && fourmi.Position.Y == this.Position.Y) && (z.X == fourmi.Position.X - 1 && z.Y == fourmi.Position.Y))
                    return z;
                // La fourmi se déplace vers la droite
                else if ((fourmi.Position.X < this.Position.X && fourmi.Position.Y == this.Position.Y) && (z.X == fourmi.Position.X + 1 && z.Y == fourmi.Position.Y))
                    return z;
                // La fourmi se déplace vers le haut
                else if ((fourmi.Position.X == this.Position.X && fourmi.Position.Y > this.Position.Y) && (z.X == fourmi.Position.X && z.Y == fourmi.Position.Y - 1))
                    return z;
                // La fourmi se déplace vers le bas
                else if ((fourmi.Position.X == this.Position.X && fourmi.Position.Y < this.Position.Y) && (z.X == fourmi.Position.X && z.Y == fourmi.Position.Y +1))
                    return z;
            }

            return null;
        }

    }
}
