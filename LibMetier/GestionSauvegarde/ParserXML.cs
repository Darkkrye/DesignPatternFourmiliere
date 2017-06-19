using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using LibAbstraite;


namespace LibMetier
{
    public class ParserXML
    {
        private static XmlWriter writer;
        public static Fourmiliere Charger(string fileName)
        {
            var result = new Fourmiliere();

            return result;
        }

        public static void Sauvegarder(string fileName, Fourmiliere fourmiliere)
        {
            writer = XmlWriter.Create(new StringBuilder(fileName));
            
            writer.WriteStartDocument();

            WriteAcces(fourmiliere.AccesAbstraitsList);
            WriteZone(fourmiliere.ZoneAbstraiteList);
            WriteObjet(fourmiliere.ObjetAbstraitList);
            WritePersonnage(fourmiliere.PersonnageAbstraitList);

            writer.WriteEndDocument();

            

        }

      
        private static void WritePersonnage(List<PersonnageAbstrait> personnages)
        {
            writer.WriteStartElement("Personnages");
            foreach (var fourmi in personnages)
            {
                writer.WriteStartElement(fourmi.Type.ToString());

                writer.WriteStartElement("Position");
                writer.WriteAttributeString("Nom", fourmi.Position.Nom);

                writer.WriteStartElement("X");
                writer.WriteString(fourmi.Position.X.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("Y");
                writer.WriteString(fourmi.Position.Y.ToString());
                writer.WriteEndElement();
                writer.WriteEndElement();
                switch (fourmi.Type)
                {
                    case TypePersonnage.ChercheuseDeNourriture:
                        var f = (Fourmi)fourmi;
                        WriteEtat(f.EtatCourant);
                        WriteAcces( f.pathToFood);
                        break;
                    case TypePersonnage.Reine:
                        var r = (Reine)fourmi;
                        // TODO : all
                        break;
                    default:
                        break;
                }
                writer.WriteEndElement();
            }
           
        }

        private static void WriteAcces(List<AccesAbstrait> acces)
        {
            writer.WriteStartElement("AccesAbstraits");
            foreach (var acce in acces)
            {
                writer.WriteStartElement("Acces");
                // TODO : all
                writer.WriteEndElement();
            }
            writer.WriteEndElement();


        }

        private static void WriteEtat(EtatFourmiAbstrait acces)
        {
            writer.WriteStartElement("Etat");
            // TODO : finir EtatFourmi
            writer.WriteEndElement();
            // TODO : all

        }

        private static void WriteZone(List<ZoneAbstraite> personnages) {

        }

        private static void WriteObjet(List<ObjetAbstrait> objets)
        {
            writer.WriteStartElement("ObjetAbstrait");
            foreach (var obj in objets)
            {
                writer.WriteStartElement("Objet");
                switch (obj.Type)
                {/*
                    case TypeObjet:
                        var f = (Fourmi)fourmi;
                        WriteEtat(f.EtatCourant);
                        WriteAcces(f.pathToFood);
                        break;
                    case TypePersonnage.Reine:
                        var r = (Reine)fourmi;
                        // TODO : all
                        break;
                    default:
                        break;*/
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

        }

        private static List<PersonnageAbstrait> ReadPersonnage()
        {
            var result = new List<PersonnageAbstrait>();

            return result;
        }

        private static List<AccesAbstrait> ReadAcces()
        {
            var result = new List<AccesAbstrait>();

            return result;

        }

        private static EtatFourmiAbstrait ReadEtat()
        {
            EtatFourmiAbstrait result = null;
            // TODO switch case by etat
            return result;
        }

        private static List<ZoneAbstraite> ReadZone()
        {
            var result = new List<ZoneAbstraite>();

            return result;
        }

        private static List<ObjetAbstrait> ReadObjet( )
        {
            var result = new List<ObjetAbstrait>();

            return result;
        }



    }
}
