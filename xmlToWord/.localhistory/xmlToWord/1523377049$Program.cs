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

            System.IO.StreamReader str = new System.IO.StreamReader(filename);
            System.Xml.Serialization.XmlSerializer xSerializer = new System.Xml.Serialization.XmlSerializer(typeof(AdresseMap));
            AdresseMap res = (AdresseMap)xSerializer.Deserialize(str);
            //foreach (ResultSetResult r in res)
            //{
            //    Console.WriteLine(r.Title);
            //    Console.WriteLine(r.Summary);
            //    Console.WriteLine();
            //}
            //str.Close();

            Console.ReadLine();
        }
    }
}
