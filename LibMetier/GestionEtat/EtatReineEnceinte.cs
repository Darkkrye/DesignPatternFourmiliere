using LibAbstraite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMetier
{
    class EtatReineEnceinte : EtatFourmiAbstrait
    {
        public override void ModifieEtat(PersonnageAbstrait personnage)
        {
            personnage.EtatCourant = this;
        }
    }
}
