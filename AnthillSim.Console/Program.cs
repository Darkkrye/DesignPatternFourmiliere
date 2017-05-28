using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMetier;
using LibAbstraite;

namespace AnthillSim.Console
{
    class Program

    //quand fourmi recherche nourriture verifier si elle a nourriture, si elle a retourne a fourmiliere sinon continue de chercher
    //quand elle rentre elle dépose phéromone
    //rajouter constant dans program.cs pour que les fourmis sachent le chemin du retour de la fourmiliere fonction goHome dans fourmiliere

    {
        // taille de l'environnement
        public const int maxX = 3;
        public const int maxY = 3;
        // nombre objet + fourmi
        public const int nombreObjet = 3;
        public const int nombreFourmi = 2;

        static void Main(string[] args)
        {
            
            // creation singleton fabrique fourmiliere
            Fourmiliere f = null;
            FabriqueAbstraite f1;
            f1 = FabriqueFourmilliere.Instance();
			
            // initialisation
            Program initialisation = new Program();
            f = (Fourmiliere)f1.CreerEnvironnement();
            initialisation.CreationZonesAcces(f1, f);
            initialisation.CreationPersonnages(f1, f);
            initialisation.CreationObjets(f1, f);

            // tour de jeu
            bool continu = true;
            string choix;
            while (continu)
            {
                System.Console.WriteLine("Que voulez vous faire ?");
                System.Console.WriteLine("1. nouveau tour de jeu");
                System.Console.WriteLine("2. statistique tour");
                System.Console.WriteLine("3. Sauvegarder et quitter");
                choix = System.Console.ReadLine();
                if (choix == "1")
                {
                    f.Simuler();
                }
                else
                {
                    if (choix == "2")
                    {
                        System.Console.WriteLine(f.Statistiques());
                    }
                    else
                    {
                        if (choix == "3")
                        {
                            continu = false;
                        }
                        else
                        {
                            System.Console.WriteLine("Mauvaise reponse!");
                        }
                    }
                }
            }


            // affichage statistique
            System.Console.WriteLine("Fin du jeu!");
            System.Console.ReadKey();

        }

        void CreationZonesAcces(FabriqueAbstraite f1, Fourmiliere f)
        {
            // creation zones
            ZoneAbstraite[] boutTerrain = new ZoneAbstraite[maxX * maxY];
            List<ZoneAbstraite> terrainList = new List<ZoneAbstraite>();
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    terrainList.Add((BoutDeTerrain)f1.CreerZone("Terrain n" + i + j, i, j));
                    
                }
            }
            // add the list of zone to an array of zone
            for (int i = 0; i < terrainList.Count; i++)
            {
                boutTerrain[i] = terrainList.ElementAt(i);
            }
            // j'ajoute la liste de terrain a l'envirronement
            f.AjouteZoneAbstraits(boutTerrain);

            // creation chemins
            List<AccesAbstrait> cheminList = new List<AccesAbstrait>();
            Random random = new Random();
            //int randomNumber;
            // je cree les combinaisons de chemins aleatoirement a une liste
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    if(i > 0 && j > 0 && i < maxX - 1 && j < maxY - 1)
                    {
                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i + 1, j - 1, f)));
                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i + 1, j, f)));
                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i + 1, j + 1, f)));
                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i, j - 1, f)));
                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i, j + 1, f)));
                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i - 1, j - 1, f)));
                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i - 1, j, f)));
                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i - 1, j +1, f)));
                    }
                    else
                    {
                        if(i == 0 && j > 0 && j < maxY - 1)
                        {
                            cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i + 1, j - 1, f)));
                            cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i + 1, j, f)));
                            cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i + 1, j + 1, f)));
                            cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i, j - 1, f)));
                            cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i, j + 1, f)));
                        }
                        else
                        {
                            if (i > 0 && j == 0 && i < maxX - 1)
                            {
                                cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i + 1, j, f)));
                                cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i + 1, j + 1, f)));
                                cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i, j + 1, f)));
                                cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i - 1, j, f)));
                                cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i - 1, j + 1, f)));
                            }
                            else
                            {
                                if (i == maxX - 1 && j == maxY - 1)
                                {
                                    cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i, j - 1, f)));
                                    cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i - 1, j - 1, f)));
                                    cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i - 1, j, f)));
                                }
                                else
                                {
                                    if (i == maxX - 1 && j < maxY - 1 && j > 0)
                                    {
                                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i, j - 1, f)));
                                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i, j + 1, f)));
                                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i - 1, j - 1, f)));
                                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i - 1, j, f)));
                                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i - 1, j + 1, f)));
                                    }
                                    else
                                    {
                                        if (i < maxX - 1 && j == maxY - 1 && i > 0)
                                        {
                                            cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i + 1, j - 1, f)));
                                            cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i + 1, j, f)));
                                            cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i, j - 1, f)));
                                            cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i - 1, j - 1, f)));
                                            cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i - 1, j, f)));
                                        }
                                        else
                                        {
                                            if (i == 0 && j == 0)
                                            {
                                                cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i + 1, j, f)));
                                                cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i + 1, j + 1, f)));
                                                cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i, j + 1, f)));
                                            }
                                            else
                                            {
                                                if (i == 0 && j == maxY - 1)
                                                {
                                                    cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i + 1, j - 1, f)));
                                                    cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i + 1, j, f)));
                                                    cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i, j - 1, f)));
                                                }
                                                else
                                                {
                                                    if (j == 0 && i == maxX - 1)
                                                    {
                                                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i, j + 1, f)));
                                                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i - 1, j, f)));
                                                        cheminList.Add(f1.CreerAcces(getZoneFromPosition(i, j, f), getZoneFromPosition(i - 1, j + 1, f)));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                        /*randomNumber = random.Next(0, 10);
                        if (randomNumber != 0)
                        {
                            
                        }*/
                    
                }
            }
            // add the list of access to an array of access
            AccesAbstrait[] chemins = new AccesAbstrait[cheminList.Count];
            for (int i = 0; i < cheminList.Count; i++)
            {
                chemins[i] = cheminList.ElementAt(i);
            }
            // j'ajoute la liste de chemin a l'envirronement
            f.AjouteChemins(f1,chemins);
        }

        void CreationPersonnages(FabriqueAbstraite f1, Fourmiliere f)
        {
            // creation fourmi
            Random random = new Random();
            int randomX, randomY;
            
            for (int i = 0; i < nombreFourmi; i++)
            {
                randomX = random.Next(0, maxX);
                randomY = random.Next(0, maxY);
                f.AjoutePersonnage(f1.CreerPersonnage("Fourmi n" + i, PersonnageAbstrait.TypePersonnage.Fourmi, getZoneFromPosition(randomX, randomY, f)));
            }
        }

        void CreationObjets(FabriqueAbstraite f1, Fourmiliere f)
        {
            // creation objet
            Random random = new Random();
            int randomObjet, randomX, randomY;

            // les type d'objets sont cree aleatoirement
            for (int i = 0; i < nombreObjet; i++)
            {
                randomObjet = random.Next(0, 3);
                randomX = random.Next(0, maxX);
                randomY = random.Next(0, maxY);
                
                if (randomObjet == 0)
                {
                    f.AjouteObjet(f1.CreerObjet("Nourriture n" + i, ObjetAbstrait.TypeObjet.Nourriture, getZoneFromPosition(randomX, randomY, f)));
                }
                else
                {
                    if (randomObjet == 1)
                    {
                        f.AjouteObjet(f1.CreerObjet("Oeuf n" + i, ObjetAbstrait.TypeObjet.Oeuf, getZoneFromPosition(randomX, randomY, f)));
                    }else
                    {
                        f.AjouteObjet(f1.CreerObjet("Pheromone n" + i, ObjetAbstrait.TypeObjet.Pheromone, getZoneFromPosition(randomX, randomY, f)));
                    }
                }
                
            }
        }

        ZoneAbstraite getZoneFromPosition(int x, int y, Fourmiliere f)
        {
            foreach (ZoneAbstraite z in f.getZoneAbstraiteList())
            {
                if(z.X == x && z.Y == y)
                {
                    return z;
                }
            }
            return null;
        }
    }
}
