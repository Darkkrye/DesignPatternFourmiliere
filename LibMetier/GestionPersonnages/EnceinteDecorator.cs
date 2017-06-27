using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite;
using System.Collections.ObjectModel;

namespace LibMetier
{
    public class EnceinteDecorator : PersonnageAbstrait
    {
        public PersonnageAbstrait reine { get; set; }

        public int nbJourPregnant { get; set; }

        public override ZoneAbstraite PreviousPosition { get; set; }


        public int X { get; set; }
        public int Y { get; set; }

        public override string Nom { get; set; }

        public override int Vie { get; set; }

        public override ZoneAbstraite Position { get; set; }

        public override TypePersonnage Type { get; set; }

        public override EtatFourmiAbstrait EtatCourant { get; set; }

        public override void ChangementEtat(EtatFourmiAbstrait etatCourant) { }

        public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList)
        {
            // TODO
            return null;
        }

        protected EnceinteDecorator(PersonnageAbstrait personnage)
        {
            this.
                Type = TypePersonnage.Reine;
                this.Nom = personnage.Nom;
                this.Position = personnage.Position;
                this.Vie = 100;

                this.nbJourPregnant = 0;
            this.reine = personnage;
            //    this.oeuf = new Oeuf("oeuf",2,Position);
            
        }
      
        


        public bool isAntBorn()
        {
            // une fourmi accouche au bout de 10 jours
            if (nbJourPregnant >= 2)
            {
                nbJourPregnant = 0;
                return true;
            }
            else
            {
                // si la fourmi n'a pas enceinte, j'augmente le nb de jour enceinte de la fourmi
                nbJourPregnant++;
                return false;
            }

        }

        public void setPregnant() {
            nbJourPregnant = 2;
        }


    }
}
