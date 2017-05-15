using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstraite
{
    public abstract class EtatFourmiAbstrait
    {
        // à voir : faire un Etat abstrait supplémentaire ? (si besoin d'état autre que les fourmis)
        PersonnageAbstrait Fourmi { get; set; }

        //  TODO : Méthode abstraite d'action
    }
}
