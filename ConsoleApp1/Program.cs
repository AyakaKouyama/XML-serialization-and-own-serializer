using System;
using System.IO;
using Zadanie1;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DataService service = new DataService(new DataRepository(new WypelnianieStalymi()));

            SerializationXML xml = new SerializationXML("toXml.xml");
            xml.SerializeXML(service.getContext());
            DataContext xmlContext = (DataContext)xml.DeserializeXML();


            StreamWriter stream = new StreamWriter("test.txt");
            OwnSeriallize ownSerialize = new OwnSeriallize();
            ownSerialize.Serialize(stream, service.getContext());
            StreamReader read = new StreamReader("test.txt");
            DataContext con = ownSerialize.Deserialize(read);

            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
