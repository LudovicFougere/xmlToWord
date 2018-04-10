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
            //var xml = XDocument.Load(@"C:\Users\Ludovic\Documents\xmlToWord\Monsieur et Madame TROCHARD Gilles et Antoinette - Nouvelle étude.xml");

            string filename = @"..\..\..\Files\Monsieur et Madame TROCHARD Gilles et Antoinette - Nouvelle étude.xml";

            adresse a = new adresse();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(adresse));
            using (Stream stream = new FileStream(filename, FileMode.Open))
            {
                XmlWriter writer =
                new XmlTextWriter(stream, Encoding.Unicode);
                // Serialize using the XmlTextWriter.
                xmlSerializer.Serialize(writer, a);
                writer.Close();
            }


            //var query = xml.Descendants("projet").Descendants("Datas_Comp")
            //    .Select()
            Console.Read();
        }
    }
}
