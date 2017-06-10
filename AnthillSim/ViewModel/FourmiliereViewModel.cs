using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMetier;
using LibAbstraite;
using System.Collections.ObjectModel;

namespace AnthillSim
{

    public class FourmilierViewModel : ViewModelBase
    {
        public string NomApplication { get; set; }
        public List<PersonnageAbstrait> ListFourmis { get; set; }
        public List<ObjetAbstrait> ListObjet { get; set; }
        public Fourmi FourmisSelect { get; set; }
        public int DimensionX { get; set; }
        public int DimensionY { get; set; }
        public int NbrObjet { get; set; }
        public int NbrFourmi { get; set; }
        public int VitesseExecution { get; set; }

        public Fourmiliere Fourmiliere { get; set; }
        public FabriqueAbstraite Fabrique { get; set; }

        private bool EnCours = false;
        private bool continu = true;
        private int a = 1;

        public FourmilierViewModel()
        {
            NomApplication = "Fourmiliere";
            DimensionX = 3;
            DimensionY = 3;
            NbrObjet = 5;
            NbrFourmi = 3;
            VitesseExecution = 500;

            InitFourmiliere();

        }

        public void Refresh()
        {

            ListObjet = Fourmiliere.getObjets();
            ListFourmis = Fourmiliere.getPersonnages();
        }

        public void InitFourmiliere()
        {
            Fabrique = FabriqueFourmilliere.Instance();

            Fourmiliere = (Fourmiliere)Fabrique.CreerEnvironnement();
            CreationZonesAcces();
            CreationPersonnages();
            CreationObjets();
            Refresh();

        }

        public void CreationZonesAcces()
        {
            // creation zones
            ZoneAbstraite[] boutTerrain = new ZoneAbstraite[DimensionX * DimensionY];
            List<ZoneAbstraite> terrainList = new List<ZoneAbstraite>();
            for (int i = 0; i < DimensionX; i++)
            {
                for (int j = 0; j < DimensionY; j++)
                {
                    terrainList.Add((BoutDeTerrain)Fabrique.CreerZone("Terrain n" + i + j, i, j));
                }
            }
            // add the list of zone to an array of zone
            for (int i = 0; i < terrainList.Count; i++)
            {
                boutTerrain[i] = terrainList.ElementAt(i);
            }
            // j'ajoute la liste de terrain a l'envirronement
            Fourmiliere.AjouteZoneAbstraits(boutTerrain);

            // creation chemins
            List<AccesAbstrait> cheminList = new List<AccesAbstrait>();
            Random random = new Random();
            //int randomNumber;
            // je cree les combinaisons de chemins aleatoirement a une liste
            for (int i = 0; i < DimensionX; i++)
            {
                for (int j = 0; j < DimensionY; j++)
                {
                    if (i > 0 && j > 0 && i < DimensionX - 1 && j < DimensionY - 1) // Chemins à partir d'une case qui ne touche aucun bord
                    {
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i + 1, j - 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i + 1, j)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i + 1, j + 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i, j - 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i, j + 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i - 1, j - 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i - 1, j)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i - 1, j + 1)));
                    }
                    else if (i == 0 && j > 0 && j < DimensionY - 1) // Chemins à partir d'une case qui touche le bord haut
                    {
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i + 1, j - 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i + 1, j)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i + 1, j + 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i, j - 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i, j + 1)));
                    }
                    else if (i > 0 && j == 0 && i < DimensionX - 1) // Chemins à partir d'une case qui touche le bord droit
                    {
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i + 1, j)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i + 1, j + 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i, j + 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i - 1, j)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i - 1, j + 1)));
                    }
                    else if (i == DimensionX - 1 && j == DimensionY - 1) // Chemins à partir de la case DimensionX.DimensionY
                    {
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i, j - 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i - 1, j - 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i - 1, j)));
                    }
                    else if (i == DimensionX - 1 && j < DimensionY - 1 && j > 0) // Chemins à partir d'une case qui touche le bord bas
                    {
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i, j - 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i, j + 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i - 1, j - 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i - 1, j)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i - 1, j + 1)));
                    }
                    else if (i < DimensionX - 1 && j == DimensionY - 1 && i > 0) // Chemins à partir d'une case qui touche le bord droit
                    {
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i + 1, j - 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i + 1, j)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i, j - 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i - 1, j - 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i - 1, j)));
                    }
                    else if (i == 0 && j == 0) // Chemins à partir de la case 0.0
                    {
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i + 1, j)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i + 1, j + 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i, j + 1)));
                    }
                    else if (i == 0 && j == DimensionY - 1) // Chemins à partir de la case 0.DimensionY
                    {
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i + 1, j - 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i + 1, j)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i, j - 1)));
                    }
                    else if (j == 0 && i == DimensionX - 1) // Chemins à partir de la case DimensionX.0
                    {
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i, j + 1)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i - 1, j)));
                        cheminList.Add(Fabrique.CreerAcces(getZoneFromPosition(i, j), getZoneFromPosition(i - 1, j + 1)));
                    }
                    else
                    {
                        System.Console.WriteLine("Rien ne s'est passé");
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
            Fourmiliere.AjouteChemins(Fabrique, chemins);

            //Random position Fourmiliere

            Fourmiliere.Position = getZoneFromPosition(random.Next(0, DimensionX), random.Next(0, DimensionY));
            //  Fourmiliere.Position.Y = random.Next(0, DimensionY);
        }

        public void CreationPersonnages()
        {
            ListFourmis = new List<PersonnageAbstrait>();
            for (int i = 0; i < NbrFourmi; i++)
            {
                AjouteFourmi(i);
            }
        }

        public void CreationObjets()
        {
            // creation objet
            Random random = new Random();
            int randomObjet, randomX, randomY;
            ListObjet = new List<ObjetAbstrait>();

            // les type d'objets sont cree aleatoirement
            for (int i = 0; i < NbrObjet; i++)
            {
                randomObjet = random.Next(0, 3);
                randomX = random.Next(0, DimensionX);
                randomY = random.Next(0, DimensionY);

                ZoneAbstraite za = getZoneFromPosition(randomX, randomY);

                while (za == Fourmiliere.Position)
                {
                    randomX = random.Next(0, DimensionX);
                    randomY = random.Next(0, DimensionY);
                    za = getZoneFromPosition(randomX, randomY);
                }   

                ObjetAbstrait obj = Fabrique.CreerObjet("Nourriture n" + i, TypeObjet.Nourriture, za);

                ListObjet.Add(obj);
                Fourmiliere.AjouteObjet(obj);
            }

        }

        public ZoneAbstraite getZoneFromPosition(int x, int y)
        {

            foreach (ZoneAbstraite z in Fourmiliere.getZoneAbstraiteList())
            {
                if (z.X == x && z.Y == y)
                {
                    return z;
                }
            }
            return null;
        }



        public void AjouteFourmi(int i = -1)
        {
            i = (i != -1) ? i : ListFourmis.Count + 1;
            PersonnageAbstrait f = Fabrique.CreerPersonnage("Fourmi n" + i, TypePersonnage.ChercheuseDeNourriture,
                                         Fourmiliere.Position

                                         );

            Fourmiliere.AjoutePersonnage(f);

        }

        public void DeleteFourmi()
        {
            ListFourmis.Remove(FourmisSelect);
        }



        public void TourSuivant()
        {
            if (Fourmiliere.getStock().Count == (NbrObjet * a) && Fourmiliere.getObjets().Count == 0)
                continu = false;
            else
                Fourmiliere.Simuler();

            if (!continu)
            {
                a++;
                this.CreationObjets();
                continu = true;
            }

            Refresh();

            /*
            foreach (var item in ListFourmis)
            {
                item.Avance();
            }*/
        }

        public void Stop()
        {
            
        }

        public void Avance()
        {
            TourSuivant();
        }



    }
}