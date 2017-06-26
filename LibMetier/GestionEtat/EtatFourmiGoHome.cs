using LibAbstraite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMetier
{
    public class EtatFourmiGoHome : EtatFourmiAbstrait
    {
        public override void ModifieEtat(PersonnageAbstrait personnage)
        {
            personnage.EtatCourant = this;
        }

    }
}
