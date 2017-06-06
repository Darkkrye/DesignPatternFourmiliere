using System;
using System.Collections.Generic;
using LibAbstraite;
using System.Collections.ObjectModel;

namespace LibMetier
{
    public class Fourmi : PersonnageAbstrait
    {
        private Random rand;
        public override TypePersonnage Type { get; set; }
        public override ZoneAbstraite Position { get; set; }
        public override string Nom { get; set; }
        public EtatFourmiAbstrait EtatCourant { get; set; }
        // ? Très chiant à parser ...
        public List<AccesAbstrait> pathToFood { get; set; }
        public ObjetAbstrait currentFood { get; set; }




        /// <summary>
        /// ajout depuis le cours WPF revérifier plus tard
        /// </summary>
        public ObservableCollection<Etape> EtapesList { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Affichage { get; set; }

        //vie ; 

        

        public Fourmi(string nom, ZoneAbstraite position) : base(nom, position)
        {
            Type = TypePersonnage.ChercheuseDeNourriture;
            //random non fnctionnel ...
            rand = new Random();
            //ajout depuis WPF
            EtapesList = new ObservableCollection<Etape>();
            //X = rand.Next(0,20);
            //Y = rand.Next(0,20);
            EtapesList.Add(new Etape() { NumeroTour = 1, X = X, Y = Y });
            //Nom = nom;
            //Affichage = Nom + X + Y;
        }

        public Fourmi(string nom, ZoneAbstraite position, TypePersonnage typeP) : base(nom, position)
		{
			Type = typeP;
			//random non fnctionnel ...
			rand = new Random();
			//ajout depuis WPF
			EtapesList = new ObservableCollection<Etape>();
			//X = rand.Next(0,20);
			//Y = rand.Next(0,20);
			EtapesList.Add(new Etape() { NumeroTour = 1, X = X, Y = Y });
			//Nom = nom;
			//Affichage = Nom + X + Y;
		}

        public Fourmi() : base("", null)
        {
        }

        public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList)
        {
            throw new NotImplementedException();
        }

        public override void ChangementEtat(EtatFourmiAbstrait etatCourant)
        {
            EtatCourant = etatCourant;
        }

        /// <summary>
        /// ajout depuis le cours WPF à mettre dans les design pattern
        /// </summary>
        public void Avance()
        { }



    }
}
