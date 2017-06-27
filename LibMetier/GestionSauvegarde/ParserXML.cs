using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using LibAbstraite;
using Newtonsoft.Json;


namespace LibMetier
{
    public class ParserXML
    {
        private static JsonSerializer serializer;

        public static Fourmiliere Charger(string fileName)
        {
            var result = new Fourmiliere();

            return result;
        }

        public static void Sauvegarder(string fileName, Fourmiliere fourmiliere)
        {
            var content = JsonConvert.SerializeObject(fourmiliere);
            var stream = await file.OpenStreamForWriteAsync();


        }
    }

}
