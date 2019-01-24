using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie1;

namespace TestyJednostkowe
{
    [TestClass]
    public class WypelnianieLosoweTest
    {
        DataRepository dataRepository = new DataRepository(new WypelnianieLosowo());

        [TestMethod]
        public void FillTest()
        {
            Assert.AreEqual(dataRepository.GetAllCzytelnicy().Count, 100);
            Assert.AreEqual(dataRepository.GetAllKsiazka().Count, 500);
            Assert.AreEqual(dataRepository.GetAllWypozyczenie().Count, 100);
            Assert.AreEqual(dataRepository.GetAllOpisStanu().Count, 499);

            foreach (var i in dataRepository.GetAllCzytelnicy())
            {
                for (int j = 0; j < i.Imie.Length; j++)
                {
                    Assert.IsTrue(i.Imie[j] >= 'a' && i.Imie[j] <= 'z');
                }
                for (int j = 0; j < i.Nazwisko.Length; j++)
                {
                    Assert.IsTrue(i.Nazwisko[j] >= 'a' && i.Nazwisko[j] <= 'z');
                }
                for (int j = 0; j < i.Pesel.Length; j++)
                {
                    Assert.IsTrue(i.Pesel[j] >= '0' && i.Pesel[j] <= '9');
                }
            }

            foreach (var i in dataRepository.GetAllKsiazka())
            {
                for (int j = 0; j < i.Value.Autor.Length; j++)
                {
                    Assert.IsTrue(i.Value.Autor[j] >= 'a' && i.Value.Autor[j] <= 'z');
                }

                for (int j = 0; j < i.Value.Tytul.Length; j++)
                {
                    Assert.IsTrue(i.Value.Tytul[j] >= 'a' && i.Value.Tytul[j] <= 'z');
                }
            }

        }
    }
}