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
            // équivalent à if / else if / else
            Etat =  (r < 10) ? EtatMeteo.Orage :
                    (r > 65) ? EtatMeteo.Pluie :  EtatMeteo.Soleil;
            Notify();
        }
     
    }
}
