using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstraite
{
    public abstract class ObserverAbstrait
    {
        public abstract string Etat { get; set; }
        public abstract List<PersonnageAbstrait> observers { get; set; }

        public void Subscribe(PersonnageAbstrait personnage)
        {
            observers.Add(personnage);
        }

        public void Unsubscribe(PersonnageAbstrait personnage)
        {
            observers.Remove(personnage);
        }

        public void Notify()
        {
            foreach (var personnage in observers)
            {
                personnage.Update();
            }
        }
        

    }
}
