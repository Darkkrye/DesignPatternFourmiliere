﻿using System;
using System.Collections.Generic;

namespace LibAbstraite
{
    public abstract class PersonnageAbstrait
    {
        private bool food;
        protected Random Hasard;
        public virtual int Vie { get; set; }
        public virtual string urlImage { get; set; }

        public abstract string Nom { get; set; }
        public abstract TypePersonnage Type { get; set; }
        public abstract ZoneAbstraite Position { get; set; }
        public abstract ZoneAbstraite PreviousPosition { get; set; }
        
        public abstract ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList);
        public abstract EtatFourmiAbstrait EtatCourant { get; set; }

        public abstract void ChangementEtat(EtatFourmiAbstrait etatCourant);

        public virtual void Update(EtatMeteo Etat) { }

        public bool GetFood(){return food;}

        public void SetFood(bool value){food = value;}

        public PersonnageAbstrait(string nom, ZoneAbstraite position)
        {
            Nom = nom;
            Position = position;
        }

        public PersonnageAbstrait(PersonnageAbstrait personnage)
        {

        }
        public PersonnageAbstrait()
        {

        }

    }
}
