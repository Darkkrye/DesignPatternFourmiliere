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

        public abstract void Update();

    }
}
