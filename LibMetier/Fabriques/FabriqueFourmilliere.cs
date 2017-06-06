using System;
using LibAbstraite;

namespace LibMetier
{
    public class FabriqueFourmilliere : FabriqueAbstraite
    {
        // instanciation singleton
        private static FabriqueFourmilliere instanceFourmilliere;

        public override string Titre { get; }

        public override AccesAbstrait CreerAcces(ZoneAbstraite zdebut, ZoneAbstraite zfin)
        {
            return new Chemin(zdebut, zfin);
        }

        public override EnvironnementAbstrait CreerEnvironnement()
        {
            return new Fourmiliere();
        }

        public override ObjetAbstrait CreerObjet(string nom, TypeObjet type, ZoneAbstraite position)
        {
            if (type == TypeObjet.Nourriture)
            {
                return new Nourriture(nom, position);
            }
            else if (type == TypeObjet.Oeuf)
            {
                return new Oeuf(nom, position);
            }
            else if (type == TypeObjet.Pheromone)
            {
                return new Pheromone(nom, position);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override PersonnageAbstrait CreerPersonnage(string nom, TypePersonnage type, ZoneAbstraite position)
        {
            if (type == TypePersonnage.Fourmi)
            {
                return new Fourmi(nom, position);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override ZoneAbstraite CreerZone(string nom, int x, int y)
        {
            return new BoutDeTerrain(nom, x, y);
        }


        protected FabriqueFourmilliere() { }
        // Singleton
        public static FabriqueFourmilliere Instance()
        {
            if (instanceFourmilliere == null)
            {
                instanceFourmilliere = new FabriqueFourmilliere();
            }
            return instanceFourmilliere;
        }


    }
}
