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

        public Fourmiliere()
        {

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

        public void gereVieFourmi()
        {
            List<PersonnageAbstrait> toRemove = new List<PersonnageAbstrait>();

            foreach (PersonnageAbstrait o in PersonnageAbstraitList)
            {
                // todo check si nourriture en stock dans la fourmiliere + suppresion nourriture
                if (stock.Count > 0 && o.Vie < 100 && o.Position == this.Position)
                {
                    if (o.Position == this.Position)
                    {
                        // on supprime une nourriture du stock + ajoute 7 de vie a la fourmi
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
                    // -5 de vie par fourmi par tour si pas de nourriture
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
                    if (o.Vie > 0)
                    {
                        o.Vie -= 10;
                    }
                    else
                    {
                        toRemove.Add(o);
                    }
                }

            }

            foreach (ObjetAbstrait ro in toRemove)
            {
                this.ObjetAbstraitList.Remove(ro);
            }
        }

        public void gereReineEnceinte()
        {
          
                    bool antBorn = false;
                    ReineEnceinte reineE = null;
                    PersonnageAbstrait reine = null;
                    ReineEnceinte queen = null;
                    List<PersonnageAbstrait> toRemove = new List<PersonnageAbstrait>();
                    Random random = new Random();
                    int resultat = random.Next(0, 5);
                    Fourmi newFourmi = null;
                    foreach (PersonnageAbstrait p in PersonnageAbstraitList)
                    {
                        if (p is ReineEnceinte)
                        {
                            queen = (ReineEnceinte)p;

                            if (queen.isAntBorn())
                            {
                                antBorn = true;
                                newFourmi = new Fourmi("Fourmi n" + PersonnageAbstraitList.Count, this.Position);
                                toRemove.Add(p);
                            }
                        }

                    }

                    if (queen != null && antBorn == true)
                    {
                        reine = queen.reine;
                        foreach (PersonnageAbstrait ro in toRemove)
                        {
                            this.PersonnageAbstraitList.Remove(ro);
                        }
                        this.AjoutePersonnage(reine);
                        this.AjoutePersonnage(newFourmi);
                    }
                    reine = null;

                    if (resultat == 0)
                    {
                        foreach (PersonnageAbstrait p in PersonnageAbstraitList)
                        {
                            if (p.Type == TypePersonnage.Reine && p is Reine)
                            {
                                reine = p;
                                toRemove.Add(p);
                            }

                        }
                        if (reine != null)
                        {
                            foreach (PersonnageAbstrait ro in toRemove)
                            {
                                this.PersonnageAbstraitList.Remove(ro);
                            }
                            reineE = new ReineEnceinte(reine);
                            this.AjoutePersonnage(reineE);
                        }

                    }
                    // verif doublons
                    bool checkReine = false;
                    bool checkReineEnceinte = false;
                    List<PersonnageAbstrait> toRemoveCheck = new List<PersonnageAbstrait>();
                    foreach (PersonnageAbstrait p in PersonnageAbstraitList)
                    {
                        if (p is Reine)
                        {
                            if (checkReine)
                            {
                                toRemoveCheck.Add(p);
                            }
                            checkReine = true;
                        }
                        if (p is ReineEnceinte)
                        {
                            if (checkReineEnceinte)
                            {
                                toRemoveCheck.Add(p);
                            }
                            checkReineEnceinte = true;
                        }
                    }
                    foreach (PersonnageAbstrait p in toRemoveCheck)
                    {
                        this.PersonnageAbstraitList.Remove(p);
                    }

                    
                }
















        public void AnalyseSituation()
        {
            gereReineEnceinte();
            gereVieFourmi();
            gerePheromoneVie();
            foreach (PersonnageAbstrait p in PersonnageAbstraitList)
            {
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

                    if (p.EtatCourant.GetType() == new EtatFourmiGoHome().GetType())
                    {
                        //retourne à la fourmilière
                        var zone = goHome(p);
                        if (zone != null)
                        {
                            if (p.Position != this.Position && zone != this.Position)
                                AjouteObjet(new Pheromone("test" + zone.X + zone.Y, zone));

                            DeplacerPersonnage(p, p.Position, zone);
                        }
                    }
                    else if (p.EtatCourant.GetType() == new EtatFourmiRechercheNourriture().GetType())
                    {
                        var pos = rechercheNourriture(p);
                        if (pos != null)
                            DeplacerPersonnage(p, p.Position, pos);
                    }
                }

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
                else if ((fourmi.Position.X == this.Position.X && fourmi.Position.Y < this.Position.Y) && (z.X == fourmi.Position.X && z.Y == fourmi.Position.Y + 1))
                    return z;
            }

            return null;
        }

    }
}
