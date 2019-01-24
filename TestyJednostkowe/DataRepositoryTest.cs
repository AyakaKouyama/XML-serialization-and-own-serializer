using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie1;
using TestyJednostkowe;

namespace TestyJedniostkowe
{
    [TestClass]
    public class DataRepositoryTest
    {
        DataRepository dataRepository = new DataRepository(new WypelnianieLosowo());

        [TestMethod]
        public void CreateKsiazkaTest()
        {
            Ksiazka ksi = new Ksiazka(1000, "Mistrz i Małgorzata", "Michaił Bułhakow");
            dataRepository.CreateKsiazka(ksi);
            Assert.AreEqual(dataRepository.GetKsiazka(1000).Id, 1000);
            Assert.AreEqual(dataRepository.GetKsiazka(1000).Tytul, "Mistrz i Małgorzata");
            Assert.AreEqual(dataRepository.GetKsiazka(1000).Autor, "Michaił Bułhakow");
        }

        [TestMethod]
        public void CreateWypozyczenieTest()
        {
            Czytelnik czyt = new Czytelnik("Janina", "Kowal", "94121304040");
            Ksiazka ksiazka = new Ksiazka(1000, "Opium w Rosole", "Małgorzata Musierowicz");
            dataRepository.CreateCzytelnik(czyt);
            OpisStanu opisStanu = new OpisStanu(ksiazka, true, 30);
            dataRepository.CreateOpisStanu(opisStanu);
            Wypozyczenie wypozyczenie = new Wypozyczenie(dataRepository.GetCzytelnik(dataRepository.GetAllCzytelnicy().Count - 1), dataRepository.GetOpisStanu(dataRepository.GetAllOpisStanu().Count - 1));
            dataRepository.CreateWypozyczenie(wypozyczenie);
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Czytelnik.Imie, "Janina");
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Czytelnik.Nazwisko, "Kowal");
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Czytelnik.Pesel, "94121304040");
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Stan.Ksiazka.Id, 1000);
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Stan.CzyWypozyczona, true);
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Stan.Wartosc, 30);
        }

        [TestMethod]
        public void CreateCzytelnikTest()
        {
            Czytelnik czyt = new Czytelnik("Janina", "Kowal", "94121304040");
            dataRepository.CreateCzytelnik(czyt);
            Assert.AreEqual(dataRepository.GetCzytelnik(dataRepository.GetAllCzytelnicy().Count - 1).Imie, "Janina");
            Assert.AreEqual(dataRepository.GetCzytelnik(dataRepository.GetAllCzytelnicy().Count - 1).Nazwisko, "Kowal");
            Assert.AreEqual(dataRepository.GetCzytelnik(dataRepository.GetAllCzytelnicy().Count - 1).Pesel, "94121304040");
        }

        [TestMethod]
        public void CreateOpisStanuTest()
        {
            Ksiazka ksiazka = new Ksiazka(1000, "Opium w Rosole", "Małgorzata Musierowicz");
            OpisStanu opisStanu = new OpisStanu(ksiazka, false, 30);
            dataRepository.CreateOpisStanu(opisStanu);
            Assert.AreEqual(dataRepository.GetOpisStanu(dataRepository.GetAllOpisStanu().Count - 1).Ksiazka.Id, 1000);
            Assert.AreEqual(dataRepository.GetOpisStanu(dataRepository.GetAllOpisStanu().Count - 1).CzyWypozyczona, false);
            Assert.AreEqual(dataRepository.GetOpisStanu(dataRepository.GetAllOpisStanu().Count - 1).Wartosc, 30);
        }

        [TestMethod]
        public void GetKsiazkaTest()
        {
            Ksiazka ksi = new Ksiazka(1000, "Mistrz i Małgorzata", "Michaił Bułhakow");
            dataRepository.CreateKsiazka(ksi);

            Assert.AreEqual(dataRepository.GetKsiazka(1000).Id, 1000);
            Assert.AreEqual(dataRepository.GetKsiazka(1000).Tytul, "Mistrz i Małgorzata");
            Assert.AreEqual(dataRepository.GetKsiazka(1000).Autor, "Michaił Bułhakow");
        }

        [TestMethod]
        public void GetWypozyczenieTest()
        {
            Czytelnik czytelnik = new Czytelnik("Jan", "Kowalski", "123456789");
            OpisStanu opis = new OpisStanu(new Ksiazka(2000, "Jakas", "Autor"), true, 100);
            Wypozyczenie wyp = new Wypozyczenie(czytelnik, opis);
            dataRepository.CreateWypozyczenie(wyp);
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Czytelnik.Imie, "Jan");
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Czytelnik.Nazwisko, "Kowalski");
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Czytelnik.Pesel, "123456789");
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Stan.CzyWypozyczona, true);
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Stan.Ksiazka.Id, 2000);
        }

        [TestMethod]
        public void GetCzytelnikTest()
        {
            Czytelnik czytelnik = new Czytelnik("Jan", "Kowalski", "123456789");
            dataRepository.CreateCzytelnik(czytelnik);
            Assert.AreEqual(dataRepository.GetCzytelnik(dataRepository.GetAllCzytelnicy().Count - 1).Imie, "Jan");
            Assert.AreEqual(dataRepository.GetCzytelnik(dataRepository.GetAllCzytelnicy().Count - 1).Nazwisko, "Kowalski");
            Assert.AreEqual(dataRepository.GetCzytelnik(dataRepository.GetAllCzytelnicy().Count - 1).Pesel, "123456789");
        }

        [TestMethod]
        public void GetOpisStanuTest()
        {
            Ksiazka ksiazka = new Ksiazka(1000, "Jakaś Książka", "Autor nieznany");
            OpisStanu opis = new OpisStanu(ksiazka, true, 10);
            dataRepository.CreateOpisStanu(opis);
            Assert.AreEqual(dataRepository.GetOpisStanu(dataRepository.GetAllOpisStanu().Count - 1).Ksiazka.Id, 1000);
            Assert.AreEqual(dataRepository.GetOpisStanu(dataRepository.GetAllOpisStanu().Count - 1).CzyWypozyczona, true);
            Assert.AreEqual(dataRepository.GetOpisStanu(dataRepository.GetAllOpisStanu().Count - 1).Wartosc, 10);

        }

        [TestMethod]
        public void GetAllKsiazkaTest()
        {
            Assert.AreEqual(dataRepository.GetAllKsiazka().Count, 500);
        }

        [TestMethod]
        public void GetAllWypozyczenieTest()
        {
            Assert.AreEqual(dataRepository.GetAllWypozyczenie().Count, 100);
        }

        [TestMethod]
        public void GetAllCzytelnicyTest()
        {
            Assert.AreEqual(dataRepository.GetAllCzytelnicy().Count, 100);
        }

        [TestMethod]
        public void GetAllOpisStanuTest()
        {
            Assert.AreEqual(dataRepository.GetAllOpisStanu().Count, 499);
        }

        [TestMethod]
        public void DeleteKsiazkaTest()
        {
            Ksiazka ksi = new Ksiazka(1000, "Mistrz i Małgorzata", "Michaił Bułhakow");
            dataRepository.CreateKsiazka(ksi);
            int size = dataRepository.GetAllKsiazka().Count;
            dataRepository.DeleteKsiazka(ksi);
            Assert.AreEqual(size - 1, dataRepository.GetAllKsiazka().Count);
        }

        [TestMethod]
        public void DeleteWypozyczenieTest()
        {
            Wypozyczenie wyp = new Wypozyczenie(dataRepository.GetCzytelnik(0), dataRepository.GetOpisStanu(0));
            dataRepository.CreateWypozyczenie(wyp);
            int size = dataRepository.GetAllWypozyczenie().Count;
            dataRepository.DeleteWypozyczenie(wyp);
            Assert.AreEqual(size - 1, dataRepository.GetAllWypozyczenie().Count);

        }

        [TestMethod]
        public void DeleteCzytelnikTest()
        {
            Czytelnik czyt = new Czytelnik("Janina", "Kowal", "94121304040");
            dataRepository.CreateCzytelnik(czyt);
            int size = dataRepository.GetAllCzytelnicy().Count;
            dataRepository.DeleteCzytelnik(czyt);
            Assert.AreEqual(size - 1, dataRepository.GetAllCzytelnicy().Count);
        }

        [TestMethod]
        public void DeleteOpisStanuTest()
        {
            OpisStanu opis = new OpisStanu(dataRepository.GetKsiazka(1), true, 30);
            dataRepository.CreateOpisStanu(opis);
            int size = dataRepository.GetAllOpisStanu().Count;
            dataRepository.DeleteOpisStanu(opis);
            Assert.AreEqual(size - 1, dataRepository.GetAllOpisStanu().Count);
        }

        [TestMethod]
        public void UpdateKsiazkaTest()
        {
            Ksiazka staraKsiazka = new Ksiazka(2000, "Jakas", "Autor");
            Ksiazka ksi = new Ksiazka(2000, "Mistrz i Małgorzata", "Michaił Bułhakow");
            dataRepository.CreateKsiazka(staraKsiazka);
            Assert.AreEqual(dataRepository.GetKsiazka(2000).Id, 2000);
            Assert.AreEqual(dataRepository.GetKsiazka(2000).Tytul, "Jakas");
            Assert.AreEqual(dataRepository.GetKsiazka(2000).Autor, "Autor");

            dataRepository.UpdateKsiazka(2000, ksi);

            Assert.AreEqual(dataRepository.GetKsiazka(2000).Id, 2000);
            Assert.AreEqual(dataRepository.GetKsiazka(2000).Tytul, "Mistrz i Małgorzata");
            Assert.AreEqual(dataRepository.GetKsiazka(2000).Autor, "Michaił Bułhakow");
        }

        [TestMethod]
        public void UpdateWypozyczenieTest()
        {
            OpisStanu opis1 = new OpisStanu(new Ksiazka(1000, "Jakas", "Autor"), true, 200);
            OpisStanu opis2 = new OpisStanu(new Ksiazka(2000, "Inna", "Ksiazka"), false, 100);
            Wypozyczenie stareWypozyczenie = new Wypozyczenie(new Czytelnik("Jas", "Nowak", "321"), opis1);
            Wypozyczenie wyp = new Wypozyczenie(new Czytelnik("Zosia", "Samosia", "123"), opis2);

            dataRepository.CreateWypozyczenie(stareWypozyczenie);

            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Czytelnik.Imie, "Jas");
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Czytelnik.Nazwisko, "Nowak");
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Czytelnik.Pesel, "321");
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Stan.CzyWypozyczona, true);
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Stan.Ksiazka.Id, 1000);
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Stan.Wartosc, 200);

            dataRepository.UpdateWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1, wyp);
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Czytelnik.Imie, "Zosia");
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Czytelnik.Nazwisko, "Samosia");
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Czytelnik.Pesel, "123");
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Stan.CzyWypozyczona, false);
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Stan.Ksiazka.Id, 2000);
            Assert.AreEqual(dataRepository.GetWypozyczenie(dataRepository.GetAllWypozyczenie().Count - 1).Stan.Wartosc, 100);
        }

        [TestMethod]
        public void UpdateCzytelnikTest()
        {
            Czytelnik staryCzytelnik = new Czytelnik("Zosia", "Samosia", "1223");
            Czytelnik czyt = new Czytelnik("Janina", "Kowal", "94121304040");
            dataRepository.CreateCzytelnik(staryCzytelnik);
            Assert.AreEqual(dataRepository.GetCzytelnik(dataRepository.GetAllCzytelnicy().Count - 1).Imie, "Zosia");
            Assert.AreEqual(dataRepository.GetCzytelnik(dataRepository.GetAllCzytelnicy().Count - 1).Nazwisko, "Samosia");
            Assert.AreEqual(dataRepository.GetCzytelnik(dataRepository.GetAllCzytelnicy().Count - 1).Pesel, "1223");

            dataRepository.UpdateCzytelnik(dataRepository.GetAllCzytelnicy().Count - 1, czyt);

            Assert.AreEqual(dataRepository.GetCzytelnik(dataRepository.GetAllCzytelnicy().Count - 1).Imie, "Janina");
            Assert.AreEqual(dataRepository.GetCzytelnik(dataRepository.GetAllCzytelnicy().Count - 1).Nazwisko, "Kowal");
            Assert.AreEqual(dataRepository.GetCzytelnik(dataRepository.GetAllCzytelnicy().Count - 1).Pesel, "94121304040");
        }

        [TestMethod]
        public void UpdateOpisStanuTest()
        {
            OpisStanu staryOpis = new OpisStanu(new Ksiazka(1000, "Jakas", "Autor"), true, 200);
            OpisStanu opis = new OpisStanu(new Ksiazka(2000, "Inna", "Ksiazka"), false, 100);

            dataRepository.CreateOpisStanu(staryOpis);

            Assert.AreEqual(dataRepository.GetOpisStanu(dataRepository.GetAllOpisStanu().Count - 1).Ksiazka.Id, 1000);
            Assert.AreEqual(dataRepository.GetOpisStanu(dataRepository.GetAllOpisStanu().Count - 1).CzyWypozyczona, true);
            Assert.AreEqual(dataRepository.GetOpisStanu(dataRepository.GetAllOpisStanu().Count - 1).Wartosc, 200);

            dataRepository.UpdateOpisStanu(dataRepository.GetAllOpisStanu().Count - 1, opis);

            Assert.AreEqual(dataRepository.GetOpisStanu(dataRepository.GetAllOpisStanu().Count - 1).Ksiazka.Id, 2000);
            Assert.AreEqual(dataRepository.GetOpisStanu(dataRepository.GetAllOpisStanu().Count - 1).CzyWypozyczona, false);
            Assert.AreEqual(dataRepository.GetOpisStanu(dataRepository.GetAllOpisStanu().Count - 1).Wartosc, 100);

        }

    }
}