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
        private static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        public static Fourmiliere Charger(string content)
        {

            var tmp = JsonConvert.DeserializeObject<Fourmiliere>(content, settings);

            return tmp;
        }

        public static string  Sauvegarder(Fourmiliere fourmiliere)
        {
            var tmp =  JsonConvert.SerializeObject(fourmiliere, Formatting.Indented, settings);

            return tmp.ToString();

        }
    }

}
