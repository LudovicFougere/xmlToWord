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

            XmlDocument _Doc = new XmlDocument();
            _Doc.Load(filename);
            var ser = new XmlSerializer(typeof(adresse));
            var wrapper = (adresse)ser.Deserialize(new StringReader(_Doc.OuterXml));


            Console.Read();
        }
    }
}
