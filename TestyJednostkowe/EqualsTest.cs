using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie1;

namespace TestyJednostkowe
{
    [TestClass]
    public class EqualsTest
    {
        [TestMethod]
        public void KsiazkaEqualsTest()
        {
            Ksiazka ksiazka = new Ksiazka(1, "Jakis", "Tam");
            Ksiazka ksiazka2 = new Ksiazka(1, "Jakis", "Tam");
            Ksiazka ksiazka3 = new Ksiazka(1, "Jakis", "Inna");
            Ksiazka ksiazka4 = new Ksiazka(2, "Jakis", "Inna");
            Ksiazka ksiazka5 = new Ksiazka(1, "Ala", "Inna");

            Assert.AreEqual(ksiazka.Equals(ksiazka2), true);
            Assert.AreEqual(ksiazka.Equals(ksiazka), true);
            Assert.AreEqual(ksiazka.Equals(null), false);

            Assert.AreEqual(ksiazka.Equals(ksiazka3), false);
            Assert.AreEqual(ksiazka.Equals(ksiazka4), false);
            Assert.AreEqual(ksiazka.Equals(ksiazka5), false);
        }

        [TestMethod]
        public void CzytelnikEqualsTest()
        {
            Czytelnik czytelnik = new Czytelnik("Jan", "Nowak", "123");
            Czytelnik czytelnik2 = new Czytelnik("Jan", "Nowak", "124");
            Czytelnik czytelnik3 = new Czytelnik("Jan", "Kowalski", "123");
            Czytelnik czytelnik4 = new Czytelnik("Michał", "Nowak", "123");
            Czytelnik czytelnik5 = new Czytelnik("Jan", "Nowak", "123");

            Assert.AreEqual(czytelnik.Equals(czytelnik), true);
            Assert.AreEqual(czytelnik.Equals(czytelnik5), true);
            Assert.AreEqual(czytelnik.Equals(null), false);

            Assert.AreEqual(czytelnik.Equals(czytelnik2), false);
            Assert.AreEqual(czytelnik.Equals(czytelnik3), false);
            Assert.AreEqual(czytelnik.Equals(czytelnik4), false);
        }

        [TestMethod]
        public void OpisStanuEqualsTest()
        {
            Ksiazka ksiazka = new Ksiazka(1, "Jakis", "Tam");
            Ksiazka ksiazka2 = new Ksiazka(1, "Jakis", "Tam");
            Ksiazka ksiazka3 = new Ksiazka(1, "Jakis", "Inna");

            OpisStanu opis = new OpisStanu(ksiazka, true, 100);
            OpisStanu opis2 = new OpisStanu(ksiazka2, true, 100);
            OpisStanu opis3 = new OpisStanu(ksiazka3, true, 100);
            OpisStanu opis4 = new OpisStanu(ksiazka, false, 100);

            Assert.AreEqual(opis.Equals(opis2), true);
            Assert.AreEqual(opis.Equals(opis), true);
            Assert.AreEqual(opis.Equals(null), false);
            Assert.AreEqual(opis.Equals(opis3), false);
            Assert.AreEqual(opis.Equals(opis4), false);
        }

        [TestMethod]
        public void WypozyczenieEqualsTest()
        {
            Czytelnik czytelnik = new Czytelnik("Jan", "Nowak", "123");
            Czytelnik czytelnik2 = new Czytelnik("Jan", "Nowak", "124");
            Czytelnik czytelnik3 = new Czytelnik("Jan", "Kowalski", "123");
            Ksiazka ksiazka = new Ksiazka(1, "Jakis", "Tam");
            Ksiazka ksiazka2 = new Ksiazka(1, "Jais", "Tam");
            OpisStanu opis = new OpisStanu(ksiazka, true, 100);
            OpisStanu opis2 = new OpisStanu(ksiazka2, true, 100);

            Wypozyczenie wypozyczenie = new Wypozyczenie(czytelnik, opis);
            Wypozyczenie wypozyczenie2 = new Wypozyczenie(czytelnik, opis);
            Wypozyczenie wypozyczenie3 = new Wypozyczenie(czytelnik, opis2);
            Wypozyczenie wypozyczenie4 = new Wypozyczenie(czytelnik3, opis);

            Assert.AreEqual(wypozyczenie.Equals(wypozyczenie), true);
            Assert.AreEqual(wypozyczenie.Equals(wypozyczenie2), true);
            Assert.AreEqual(wypozyczenie.Equals(null), false);
            Assert.AreEqual(wypozyczenie.Equals(wypozyczenie3), false);
            Assert.AreEqual(wypozyczenie.Equals(wypozyczenie4), false);
        }
    }
}
