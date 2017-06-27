using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMetier
{
    public class temps
    {
        public int nbJourDansAnne { get; set; }
        public int nbDeplacement { get; set; }

        public enum Saison { Hiver, Printemps, Ete, Automne };
        private Saison saisonActuelle { get; set; }

        public temps()
        {
            nbJourDansAnne = 0;
            nbDeplacement = 0;
            saisonActuelle = Saison.Hiver;
        }

        public int jourSuivant()
        {
            if (nbDeplacement < 25)
            {
                nbDeplacement++;
            }
            else
            {
                if (nbJourDansAnne < 365)
                {
                    nbJourDansAnne++;

                    if (nbJourDansAnne >= 60 && nbJourDansAnne < 150) // Printemps
                    {
                        saisonActuelle = Saison.Printemps;
                    }
                    else if (nbJourDansAnne >= 151 && nbJourDansAnne < 240) // Ete
                    {
                        saisonActuelle = Saison.Ete;
                    }
                    else if (nbJourDansAnne >= 241 && nbJourDansAnne < 330) // Automne
                    {
                        saisonActuelle = Saison.Automne;
                    }
                    else
                    {
                        saisonActuelle = Saison.Hiver;
                    }
                }
                else
                {
                    nbJourDansAnne = 0;
                }

            }
            return nbJourDansAnne;
        }
    }
}
