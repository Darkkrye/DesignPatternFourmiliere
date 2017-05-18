using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMetier;
using LibAbstraite;

namespace AnthillSim.Console
{
    class Program
    {
        static void Main(string[] args)
        {
			FabriqueAbstraite f1 = FabriqueFourmilliere.Instance();
			Fourmi fourmi;
			Nourriture n;
			Fourmiliere f;
			fourmi = (LibMetier.Fourmi)f1.CreerPersonnage("romain", PersonnageAbstrait.TypePersonnage.Fourmi);
			n = (LibMetier.Nourriture)f1.CreerObjet("carotte", ObjetAbstrait.TypeObjet.Nourriture);
			f = (LibMetier.Fourmiliere)f1.CreerEnvironnement();
			f.AjoutePersonnage(fourmi);
			f.AjouteObjet(n);
			System.Console.WriteLine(f.Statistiques());
			System.Console.ReadKey();

        }
    }
}
