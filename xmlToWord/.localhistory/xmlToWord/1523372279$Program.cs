using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace xmlToWord
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start parsing....");

            string filename = @"..\..\..\Files\Monsieur et Madame TROCHARD Gilles et Antoinette - Nouvelle étude.xml";

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(adresse));
            using (var reader = XmlReader.Create(filename))
            {
                var wrapper = (adresse)xmlSerializer.Deserialize(reader);

            }



            Console.Read();
        }
    }
}
