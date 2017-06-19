using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite;


namespace LibMetier
{
    public class OberverMeteo : ObserverAbstrait
    {
        private const string PLUIE = "Pluie";
        private const string SOLEIL = "Soleil";

        public override string Etat { get; set; }

        public override void Update()
        {
            if (Etat == PLUIE)
                Etat = SOLEIL;
            else if (Etat == SOLEIL)
                Etat = PLUIE;
            else
                Etat = SOLEIL;

        }
    }
}
