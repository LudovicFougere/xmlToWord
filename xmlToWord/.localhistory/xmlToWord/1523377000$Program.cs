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

          
            // Read a purchase order.
            DeserializeObject(filename);


            Console.Read();
        }

        private static void DeserializeObject(string filename)
        {
            Console.WriteLine("Reading with XmlReader");

            // Create an instance of the XmlSerializer specifying type and namespace.
           
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "projet";
            // xRoot.Namespace = "http://www.cpandl.com";
            xRoot.IsNullable = true;


            XmlSerializer serializer = new
           XmlSerializer(typeof(AdresseMap), xRoot);
            // A FileStream is needed to read the XML document.
            FileStream fs = new FileStream(filename, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);

            // Declare an object variable of the type to be deserialized.
            AdresseMap i;

            // Use the Deserialize method to restore the object's state.
            i = (AdresseMap)serializer.Deserialize(reader);
            fs.Close();

            // Write out the properties of the object.
            //Console.Write(
            //i.ItemName + "\t" +
            //i.Description + "\t" +
            //i.UnitPrice + "\t" +
            //i.Quantity + "\t" +
            //i.LineTotal);
        }
    }
}
