using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite;


namespace LibMetier
{
    public class Meteo : ObserverAbstrait
    {
        public override EtatMeteo Etat { get; set; }

        public override List<PersonnageAbstrait> observers { get; set; }

        private int compteur = 0;

        private Random rand;

        public Meteo()
        {
            Etat = EtatMeteo.Soleil;
            observers = new List<PersonnageAbstrait>();
            rand = new Random();
        }
       

        public override void Subscribe(PersonnageAbstrait personnage)
        {
            observers.Add(personnage);
        }

        public override void Unsubscribe(PersonnageAbstrait personnage)
        {
            observers.Remove(personnage);
        }

        public override void Notify()
        {
            foreach (var personnage in observers)
            {
                personnage.Update(Etat);
            }
        }


        public void ChangementMeteo() {
            var r = rand.Next(0, 100);

            switch (Etat) {
                case EtatMeteo.Soleil:
                    Etat =  (r < 5 ) ? EtatMeteo.Orage :
                            (r > 85) ? EtatMeteo.Pluie : EtatMeteo.Soleil;
                    break;
                case EtatMeteo.Pluie:
                    VerifCompteur(5);
                    break;
                case EtatMeteo.Orage:
                    VerifCompteur(10);
                    break;
            }
           
           
           
            Notify();
        }
        private void VerifCompteur(int max) {
            if (compteur < max) {
                compteur++;
            }
            else
            {
                compteur = 0;
                Etat = EtatMeteo.Soleil;
            }
        }
     
    }
}
