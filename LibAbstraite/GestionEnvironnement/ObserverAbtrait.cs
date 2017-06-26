using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstraite
{
    public abstract class ObserverAbstrait
    {
        public abstract EtatMeteo Etat { get; set; }

        public abstract List<PersonnageAbstrait> observers { get; set; }

        public abstract void Subscribe(PersonnageAbstrait personnage);

        public abstract void Unsubscribe(PersonnageAbstrait personnage);

        public abstract void Notify();



    }
}
