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
        public ObservableCollection<PersonnageAbstrait> ListFourmis { get; set; }
        public Fourmi FourmisSelect { get; set; }

        public int DimensionX { get; set; }
        public int DimensionY { get; set; }
        public int NbrObjet { get; set; }
        public int NbrFourmi { get; set; }
        public int VitesseExecution { get; set; }

        public Fourmiliere Fourmiliere { get; set; }
        public FabriqueAbstraite Fabrique { get; set; }

        private bool EnCours = false;
        private bool continu = false;

        public FourmilierViewModel()
        {
            

            NomApplication = "Fourmiliere";
            DimensionX = 3;
            DimensionY = 3;
            NbrObjet = 3;
            NbrFourmi = 2;
            VitesseExecution = 500;
            
            InitFourmiliere();

        }

        public void InitFourmiliere()
        {
            Fabrique = FabriqueFourmilliere.Instance();

            Fourmiliere = (Fourmiliere)Fabrique.CreerEnvironnement();
            CreationZonesAcces();
            CreationPersonnages();
            CreationObjets();

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


        }

        public void CreationPersonnages()
        {
            ListFourmis = new ObservableCollection<PersonnageAbstrait>();
            for (int i = 0; i < NbrFourmi; i++)
            {
                PersonnageAbstrait f = Fabrique.CreerPersonnage("Fourmi n" + i, TypePersonnage.ChercheuseDeNourriture,
                                            getZoneFromPosition(0, 0)
                                            );
                ListFourmis.Add(f);

                Fourmiliere.AjoutePersonnage(f);


            }

        }

        public void CreationObjets()
        {
            // creation objet
            Random random = new Random();
            int randomObjet, randomX, randomY;

            // les type d'objets sont cree aleatoirement
            for (int i = 0; i < NbrObjet; i++)
            {
                randomObjet = random.Next(0, 3);
                randomX = random.Next(0, DimensionX);
                randomY = random.Next(0, DimensionY);

                Fourmiliere.AjouteObjet(Fabrique.CreerObjet("Nourriture n" + i, TypeObjet.Nourriture, getZoneFromPosition(randomX, randomY)));
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



        public void AjouteFourmi()
        {
            //   ListFourmis.Add(new Fourmi("Fourmis " + ListFourmis.Count()));
        }

        public void DeleteFourmi()
        {
            ListFourmis.Remove(FourmisSelect);
        }



        public void TourSuivant()
        {
            if (Fourmiliere.getStock().Count == NbrObjet &&
                Fourmiliere.getObjets().Count == 0)
                continu = false;
            else
                Fourmiliere.Simuler();

            /*
            foreach (var item in ListFourmis)
            {
                item.Avance();
            }*/
        }

        public void Stop()
        {
            EnCours = false;
        }

        public void Avance()
        {
            EnCours = true;
            while (EnCours)
            {
                TourSuivant();

            }

        }



    }
}