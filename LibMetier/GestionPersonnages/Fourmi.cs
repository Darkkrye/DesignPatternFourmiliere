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
        public override ZoneAbstraite PreviousPosition { get; set; }// utilise pour ne pas retourner sur sa derniere position
        public override string Nom { get; set; }
        public EtatFourmiAbstrait EtatCourant { get; set; }
        
        public List<AccesAbstrait> pathToFood { get; set; }
        public ObjetAbstrait currentFood { get; set; }
        public override int Vie { get; set; }



        /// <summary>
        /// ajout depuis le cours WPF revérifier plus tard
        /// </summary>
        public ObservableCollection<Etape> EtapesList { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Affichage { get; set; }

        public override List<ObserverAbstrait> observers
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }


        //vie ; 



        public Fourmi(string nom, ZoneAbstraite position) : base(nom, position)
        {
            Type = TypePersonnage.ChercheuseDeNourriture;
            rand = new Random();
            EtapesList = new ObservableCollection<Etape>();
            EtapesList.Add(new Etape() { NumeroTour = 1, X = X, Y = Y });
            Nom = nom;
            this.Vie = 100;
        }

    //    public Fourmi(string nom, ZoneAbstraite position, TypePersonnage typeP) : base(nom, position)
	//	{
	//		Type = typeP;
	//		rand = new Random();
	//		EtapesList = new ObservableCollection<Etape>();
	//		EtapesList.Add(new Etape() { NumeroTour = 1, X = X, Y = Y });
	//		//Nom = nom;
	//		//Affichage = Nom + X + Y;
	//	}

        

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

        public override void Update(EtatMeteo Etat){
            switch (Etat) { 
                case EtatMeteo.Soleil:
                    //continue ta vie
                    break;
                case EtatMeteo.Orage:
                    // meurt ?
                    break;
                case EtatMeteo.Pluie:
                    //goHome
                    break;
                default:
                    break;
            }


        }



    }
}
