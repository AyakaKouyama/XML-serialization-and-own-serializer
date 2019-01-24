using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Zadanie1;
using System.Collections.ObjectModel;
using System.Xml;

namespace ConsoleApp1
{
    public class SerializationXML
    {
        private DataContractSerializer _serializer;
        private string _fileName;

        public SerializationXML(string fileName)
        {
            _serializer = new DataContractSerializer(typeof(Wypozyczenie), new Type[] { typeof(DataContext)}, int.MaxValue, false, true, null, null);
            _fileName = fileName;
        }
        public void SerializeXML(object graph)
        {
            using (XmlWriter stream = XmlWriter.Create(_fileName))
            {
                try
                {
                    _serializer.WriteObject(stream, graph);
                }
                catch (SerializationException exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }

        public object DeserializeXML()
        {
            FileStream file = new FileStream(_fileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            using (XmlReader read = XmlReader.Create(file))
            {
                try
                {
                    return _serializer.ReadObject(read);
                }
                catch (SerializationException exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }

            return null;
        }

    }
}
