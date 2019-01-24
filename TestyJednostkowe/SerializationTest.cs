using System;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie1;
using ConsoleApp1;
using System.IO;
using System.Collections.Generic;

namespace TestyJednostkowe
{
    [TestClass]
    public class SerializationTest
    {
        DataService service = new DataService(new DataRepository(new WypelnianieLosowo()));

        [TestMethod]
        public void SerializeXMLTest()
        {
            SerializationXML xml = new SerializationXML("toXmlTest.xml");
            xml.SerializeXML(service.getContext());

            DataContext result = (DataContext)xml.DeserializeXML();
            Assert.AreEqual(result.Wypozyczenia.Count, 100);
            Assert.AreEqual(result.Stan.Count, 499);
            Assert.AreEqual(result.Ksiazki.Count, 500);
            Assert.AreEqual(result.Czytelnicy.Count, 100);

            List<Czytelnik> resCzytelnicy = result.Czytelnicy;
            List<OpisStanu> resOpis = result.Stan;
            Dictionary<int, Ksiazka> resKsiazka = result.Ksiazki;
            ObservableCollection<Wypozyczenie> resWypozyczenie = result.Wypozyczenia;

            for (int i = 0; i < service.getContext().Czytelnicy.Count; i++)
            {
                Assert.IsTrue(resCzytelnicy[i].Equals(service.GetCzytelnik(i)));
            }

            for (int i = 0; i < service.getContext().Ksiazki.Count; i++)
            {
                Assert.IsTrue(resKsiazka[i].Equals(service.GetKsiazka(i)));
            }

            for (int i = 0; i < service.getContext().Stan.Count; i++)
            {
                Assert.IsTrue(resOpis[i].Equals(service.GetOpisStanu(i)));
            }

            for (int i = 0; i < service.getContext().Wypozyczenia.Count; i++)
            {
                Assert.IsTrue(resWypozyczenie[i].Equals(service.GetWypozyczenie(i)));
            }
        }

        [TestMethod]
        public void SerializeOwnTest()
        {
            DataContext data = new DataContext();
            StreamWriter stream = new StreamWriter("test.txt");

            OwnSeriallize ownSerialize = new OwnSeriallize();
            ownSerialize.Serialize(stream, service.getContext());

            StreamReader read = new StreamReader("test.txt");
            DataContext result = ownSerialize.Deserialize(read);

            Assert.AreEqual(result.Wypozyczenia.Count, 100);
            Assert.AreEqual(result.Stan.Count, 499);
           Assert.AreEqual(result.Ksiazki.Count, 500);
            Assert.AreEqual(result.Czytelnicy.Count, 100);


            List<Czytelnik> resCzytelnicy = result.Czytelnicy;
            List<OpisStanu> resOpis = result.Stan;
            Dictionary<int, Ksiazka> resKsiazka = result.Ksiazki;
            ObservableCollection<Wypozyczenie> resWypozyczenie = result.Wypozyczenia;

            for(int i = 0; i<service.getContext().Czytelnicy.Count; i++)
            {
                Assert.IsTrue(resCzytelnicy[i].Equals(service.GetCzytelnik(i)));
            }

            for (int i = 0; i < service.getContext().Ksiazki.Count; i++)
            {
                Assert.IsTrue(resKsiazka[i].Equals(service.GetKsiazka(i)));
            }

            for (int i = 0; i < service.getContext().Stan.Count; i++)
            {
               Assert.IsTrue(resOpis[i].Equals(service.GetOpisStanu(i)));
            }

            for(int i = 0; i<service.getContext().Wypozyczenia.Count; i++)
            {
                Assert.IsTrue(resWypozyczenie[i].Equals(service.GetWypozyczenie(i)));
            }
        }
    }
}
