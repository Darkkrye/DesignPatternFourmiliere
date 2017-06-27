using System;
using System.Collections.Generic;
using LibAbstraite;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using LibMetier;

namespace LibMetier
{
    public class Fourmiliere : EnvironnementAbstrait
    {
        internal List<AccesAbstrait> AccesAbstraitsList;
        internal List<ZoneAbstraite> ZoneAbstraiteList;
        internal List<ObjetAbstrait> ObjetAbstraitList;
        internal List<PersonnageAbstrait> PersonnageAbstraitList;
        public List<ObjetAbstrait> stock { get; set; }

        public temps time { get; set; }
        public ZoneAbstraite Position { get; set; }
        public Meteo Meteo { get; set; }

        public FabriqueFourmilliere fabrique;

        public Fourmiliere()
        {
            fabrique = FabriqueFourmilliere.Instance();
            AccesAbstraitsList = new List<AccesAbstrait>();
            ZoneAbstraiteList = new List<ZoneAbstraite>();
            ObjetAbstraitList = new List<ObjetAbstrait>();
            PersonnageAbstraitList = new List<PersonnageAbstrait>();
            stock = new List<ObjetAbstrait>();
            Meteo = new Meteo();
            time = new temps();

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
            Meteo.Subscribe(unPersonnage);
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
            unPersonnage.PreviousPosition = unPersonnage.Position;
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

            result += "\nObjet sur la map : \n";
            foreach (ObjetAbstrait a in ObjetAbstraitList)
            {
                result += "Nom = " + a.Nom + ", vie = " + a.Vie + ", type = " + a.Type + ", ";
                result += "Position = " + a.Position.X + ", " + a.Position.Y + "\n";
            }

            result += "\nNourriture en Stock : \n";
            foreach (ObjetAbstrait a in stock)
            {
                result += "Nom = " + a.Nom + ", vie = "+ a.Vie + ", type = " + a.Type + ", \n";
            }

            return result;
        }

        public List<ZoneAbstraite> getZoneAbstraiteList()
        {
            return ZoneAbstraiteList;
        }

        // gere la nombre de point de vie de la fourmi, ajoute des points si il y a de la nourriture en stock sinon enleve des points de vie
        // supprime une fourmi de la fourmiliere si plus de point de vie
        public void gereVieFourmi()
        {
            List<PersonnageAbstrait> toRemove = new List<PersonnageAbstrait>();

            foreach (PersonnageAbstrait o in PersonnageAbstraitList)
            {
                // check si nourriture en stock dans la fourmiliere + suppresion nourriture
                if (stock.Count > 0 && o.Vie < 100 && o.Position == this.Position)
                {
                    if (o.Position == this.Position)
                    {
                        // une nourriture est compose de 10 portions et une foumi mange 2 portions a chaque fois qu'elle se trouve dans la fourmiliere
                        // on verifie le nombre de portion restante de la 1er pheromone en stock :
                        // si il reste des portions, on enleve 2 portions a la pheromone + ajoute 10 de vie a la fourmi
                        // sinon 
                        if (stock.ElementAt(0).Vie > 0)
                        {
                            stock.ElementAt(0).Vie -= 2;
                            if (stock.ElementAt(0).Vie == 0)
                            {
                                stock.RemoveAt(0);
                            }
                        }
                        else
                        {
                            stock.RemoveAt(0);
                        }

                        o.Vie += 10;
                        if (o.Vie > 100)
                        {
                            o.Vie = 100;
                        }
                    }

                }
                else
                {
                    // -2 de vie par fourmi par tour si pas de nourriture
                    o.Vie -= 2;
                }
                // si une fourmi a plus de vie, on la rajoute ds une nouvelle liste des fourmi a supprimer
                if (o.Vie < 0)
                {
                    toRemove.Add(o);
                }
            }
            // on supprime de la liste la fourmi morte
            foreach (PersonnageAbstrait ro in toRemove)
            {
                this.PersonnageAbstraitList.Remove(ro);
            }
        }

        public void gerePheromoneVie()
        {
            List<ObjetAbstrait> toRemove = new List<ObjetAbstrait>();

            foreach (ObjetAbstrait o in ObjetAbstraitList)
            {
                if (o.Type == TypeObjet.Pheromone)
                {
                    // a chaque tour, si la pheromone a plus de 0, on lui enelve 10 de vie
                    if (o.Vie > 0)
                    {
                        o.Vie -= 10;
                    }
                    // on la supprime sinon
                    else
                    {
                        toRemove.Add(o);
                    }
                }

            }
            // la suppression de la pheromone se fait ici
            foreach (ObjetAbstrait ro in toRemove)
            {
                this.ObjetAbstraitList.Remove(ro);
            }
        }

        public void AnalyseSituation()
        {
            // gere la nombre de point de vie de la fourmi, ajoute des points si il y a de la nourriture en stock sinon enleve des points de vie
            // supprime une fourmi de la fourmiliere si plus de point de vie
            gereVieFourmi();
            gerePheromoneVie();
            bool enceinte = false;
            Reine r = null;
            // je parcours la liste des fourmi pour donner l'ordre de :
            //  tomber enceinte et donner naissance pour la reine
            // chercher de la nourriture ou ramener de la nourriture pour les autres fourmis
            foreach (PersonnageAbstrait p in PersonnageAbstraitList)
            {
                if(p.Type == TypePersonnage.Reine)
                {
                    r = (Reine)p;
                    r.changementEtat();
                    if(r.nbJourEnceinte > 4)
                    {
                        enceinte = true;
                        
                    }
                }
                if (p.Type == TypePersonnage.ChercheuseDeNourriture)
                {
                    if (p.Position == this.Position)
                    {
                        p.PreviousPosition = this.Position;
                    }

                    // if fourmi a de la nourriture 
                    if (p.GetFood())
                    {
                        new EtatFourmiGoHome().ModifieEtat(p);
                    }
                    else  // sinon recherche nouritture
                    {
                        if (Meteo.Etat != EtatMeteo.Soleil)
                        {
                            new EtatFourmiGoHome().ModifieEtat(p);
                        }
                        else
                        {
                            new EtatFourmiRechercheNourriture().ModifieEtat(p);
                        }

                    }

                    if (p.EtatCourant is EtatFourmiGoHome)
                    {
                        //retourne à la fourmilière
                        var zone = goHome(p);
                        if (zone != null)
                        {
                            if (p.Position != this.Position && zone != this.Position)
                                AjouteObjet(new Pheromone("pheromone", zone));

                            DeplacerPersonnage(p, p.Position, zone);
                        }
                    }
                    else if (p.EtatCourant is EtatFourmiRechercheNourriture)
                    {
                        var pos = rechercheNourriture(p);
                        if (pos != null)
                            DeplacerPersonnage(p, p.Position, pos);
                    }
                }

            }
            if (enceinte)
            {
                //var f = fabrique.CreerPersonnage("Fourmi " + PersonnageAbstraitList.Count, TypePersonnage.ChercheuseDeNourriture, this.Position);
                //this.AjoutePersonnage(f);
                r.nbJourEnceinte = 0;
                new EtatFourmiRepos().ModifieEtat(r);
            }
            time.jourSuivant();

        }

        public ZoneAbstraite rechercheNourriture(PersonnageAbstrait p)
        {
            Fourmi fourmi = (Fourmi)p;
            Random random = new Random();
            int resultat;
            Boolean foodAroundMe = false;
            // liste de chemin disponible
            List<ZoneAbstraite> zone = new List<ZoneAbstraite>();
            // liste des objets autour de soi disponible
            List<ObjetAbstrait> objetsDispo = new List<ObjetAbstrait>();

            // compteur de chemin disponible
            int cpt = 0;
            foreach (AccesAbstrait a in AccesAbstraitsList)
            {
                if (fourmi.Position == a.debut)
                {

                    // recherche si nourriture autour de soi et je regarde si il y a de la nourriture
                    foreach (ObjetAbstrait o in ObjetAbstraitList)
                    {
                        // j'ajoute un acces à la liste des chemins disponible l'acces ne correspondant pas à celui de la fourmiliere 
                        // ou si il ne correspond pas a lancienne position de la fourmi
                        if (a.fin != fourmi.PreviousPosition && a.fin != this.Position)
                        {
                            cpt++;
                            zone.Add(a.fin);
                        }

                        if (o.Type == TypeObjet.Nourriture)
                        {

                            if (a.fin == o.Position && fourmi.PreviousPosition != o.Position)
                            {
                                foodAroundMe = true;
                                fourmi.SetFood(true); // La fourmi récupère la nourriture
                                fourmi.currentFood = o;
                                if (o.Vie > 2) // ici je check le nombre de portion restante a la nourriture
                                {
                                    o.Vie -= 2; // avec 2, il faut 5 tour a la fourmi pour recuperer tout l'objet nourriture
                                }
                                else
                                {
                                    ObjetAbstraitList.Remove(o); // Elle disparaît de la liste des nourriture.
                                }
                                AjouteObjet(new Pheromone("test" + "test" + a.fin.X + a.fin.Y, a.fin));
                                return a.fin;
                            }
                        }
                    }
                    // si pas de nourriture autour de moi, je suis les pheromones
                    if (foodAroundMe == false)
                    {
                        foreach (ObjetAbstrait o in ObjetAbstraitList)
                        {
                            if (o.Type == TypeObjet.Pheromone)
                            {
                                if (a.fin == o.Position && fourmi.PreviousPosition != o.Position)
                                {
                                    return a.fin;
                                }
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
                    foreach (ZoneAbstraite zz in ZoneAbstraiteList)
                    {
                        if (zz.X == p.Position.X && zz.Y == p.Position.Y)
                        {
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
                    Nourriture n = new Nourriture(fourmi.currentFood.Nom, fourmi.currentFood.Position);
                    this.stock.Add(n); // La nourriture est ajoutée aux stocks de la fourmilière.
                    fourmi.currentFood = null; // La fourmi se décharge de sa nourriture
                }

                if (ObjetAbstraitList.Count() == 0) // Il n'y a plus d'objets à chercher, la fourmi reste à la maison
                    return this.Position;
                else
                {
                    if (fourmi.EtatCourant is EtatFourmiRechercheNourriture)
                    {
                        // Il reste des objets sur la carte, la fourmi repart
                        return this.rechercheNourriture(p);
                    }
                    
                }
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
                else if ((fourmi.Position.X == this.Position.X && fourmi.Position.Y < this.Position.Y) && (z.X == fourmi.Position.X && z.Y == fourmi.Position.Y + 1))
                    return z;
            }

            return null;
        }

    }
}
