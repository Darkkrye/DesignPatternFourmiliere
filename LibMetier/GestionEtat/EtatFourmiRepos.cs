﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite;

namespace LibMetier
{
    public class EtatFourmiRepos :  EtatFourmiAbstrait
    {
        public override void ModifieEtat(PersonnageAbstrait personnage)
        {
            personnage.EtatCourant = this;
        }

    }
}
