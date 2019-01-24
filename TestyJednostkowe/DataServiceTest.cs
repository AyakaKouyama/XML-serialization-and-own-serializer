using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie1;

namespace TestyJednostkowe
{
    [TestClass]
    public class DataServiceTest
    {
        DataService service = new DataService(new DataRepository(new WypelnianieLosowo()));

        [TestMethod]
        public void SzukajKsiazekAutoraTest()
        {
            Dictionary<int, Ksiazka> ksiazki = service.SzukajKsiazekAutora("Henryk Sienkiewicz");
            foreach (var i in ksiazki)
            {
                Assert.AreEqual(i.Value.Autor, "Henryk Sienkiewicz");
            }

        }
        [TestMethod]
        public void SzuajWypozyczenTest()
        {
            List<OpisStanu> wypozyczenia = service.SzukajWypozyczen(true);
            foreach (var i in wypozyczenia)
            {
                Assert.AreEqual(i.CzyWypozyczona, true);
            }

            List<OpisStanu> wypozyczenia2 = service.SzukajWypozyczen(false);
            foreach (var i in wypozyczenia2)
            {
                Assert.AreEqual(i.CzyWypozyczona, false);
            }
        }

        [TestMethod]
        public void SzukajKsiazkiTest()
        {
            service.DodajKsiazke(new Ksiazka(2000, "Ogniem i mieczem", "Henryk Sienkiewicz"));
            service.DodajKsiazke(new Ksiazka(3000, "Ogniem i mieczem", "Henryk Sienkiewicz"));
            Dictionary<int, Ksiazka> ksiazka = service.SzukajKsiazki("Ogniem i mieczem");
            foreach (var i in ksiazka)
            {
                Assert.AreEqual(i.Value.Tytul, "Ogniem i mieczem");
            }
        }

        [TestMethod]
        public void SzukajKsiazkiPoIDTest()
        {
            Dictionary<int, Ksiazka> ksiazki = service.SzukajKsiazkiPoId(5);
            foreach (var i in ksiazki)
            {
                Assert.AreEqual(i.Key, 5);
            }

        }

        [TestMethod]
        public void SzukajCzytelnikaPoNazwiskuTest()
        {
            service.DodajCzytelnika(new Czytelnik("Józef", "Nowak", "123"));
            List<Czytelnik> czytelnicy = service.SzukajCzytelnikaPoNazwisku("Nowak");
            foreach (var i in czytelnicy)
            {
                Assert.AreEqual(i.Nazwisko, "Nowak");
            }

        }

        [TestMethod]
        public void SzukajCzytelnikaPoImieniuTest()
        {
            List<Czytelnik> czytelnicy = service.SzukajCzytelnikaPoImieniu("Jan");
            foreach (var i in czytelnicy)
            {
                Assert.AreEqual(i.Imie, "Jan");
            }
        }

        [TestMethod]
        public void SzukajCzytelnikaPoImieniuINazwiskuTest()
        {
            List<Czytelnik> czytelnicy = service.SzukajCzytelnikaPoImieniuiNazwisku("Jan", "Nowak");
            foreach (var i in czytelnicy)
            {
                Assert.AreEqual(i.Imie, "Jan");
                Assert.AreEqual(i.Nazwisko, "Nowak");
            }
        }

        [TestMethod]
        public void SzukaWypozyczenCzytelnika()
        {
            Czytelnik czyt = new Czytelnik("Zofia", "Mazurkiewicz", "321");
            Ksiazka ksiazka = new Ksiazka(1000, "Jakaś Książka", "Autor nieznany");
            Ksiazka ksiazka2 = new Ksiazka(2000, "Jakaś Książka cz. 2", "Autor nieznany");
            OpisStanu opis = new OpisStanu(ksiazka, false, 20);
            OpisStanu opis2 = new OpisStanu(ksiazka2, false, 25);
            service.DodajWypozyczenie(new Wypozyczenie(czyt, opis));
            service.DodajWypozyczenie(new Wypozyczenie(czyt, opis2));
            ObservableCollection<Wypozyczenie> wypozyczenia = service.SzukajWypozyczenCzytelnika(czyt);

            Assert.AreEqual(wypozyczenia.Count, 2);
            Assert.AreEqual(wypozyczenia[0].Czytelnik, czyt);
            Assert.AreEqual(wypozyczenia[0].Stan, opis);
            Assert.AreEqual(wypozyczenia[1].Czytelnik, czyt);
            Assert.AreEqual(wypozyczenia[1].Stan, opis2);

        }

        [TestMethod]
        public void WypozyczTest()
        {
            Czytelnik czyt = new Czytelnik("Zofia", "Mazurkiewicz", "123");
            Ksiazka ksi = new Ksiazka(1000, "Jakaś Książka", "Autor nieznany");
            OpisStanu opis = new OpisStanu(ksi, false, 200);
            service.DodajOpisStanu(opis);
            service.DodajCzytelnika(czyt);

            ObservableCollection<Wypozyczenie> wypozyczenia = service.SzukajWypozyczenCzytelnika(czyt);
            Assert.AreEqual(wypozyczenia.Count, 0);

            service.Wypozycz(czyt, ksi);

            ObservableCollection<Wypozyczenie> wypozyczenia2 = service.SzukajWypozyczenCzytelnika(czyt);
            Assert.AreEqual(wypozyczenia2[0].Stan.CzyWypozyczona, true);
            Assert.AreEqual(wypozyczenia2.Count, 1);
        }


        [TestMethod]
        public void WypozyczTestWyjatekKsiazkaJestJuzWypozyczona()
        {
            try
            {
                Czytelnik czyt = new Czytelnik("Zofia", "Mazurkiewicz", "123");
                Ksiazka ksi = new Ksiazka(1000, "Jakaś Książka", "Autor nieznany");
                OpisStanu opis = new OpisStanu(ksi, true, 200);
                service.DodajOpisStanu(opis);
                service.DodajCzytelnika(czyt);

                ObservableCollection<Wypozyczenie> wypozyczenia = service.SzukajWypozyczenCzytelnika(czyt);
                Assert.AreEqual(wypozyczenia.Count, 0);
                service.Wypozycz(czyt, ksi);

            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Nie znaleziono książki o tym tyltule, którą możnaby wypożyczyć");
            }
        }

        [TestMethod]
        public void WyswietlTestWyjatek()
        {
            try
            {
                service.Wyswietl<DataRepository>();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Błędny typ");
            }

        }

        [TestMethod]
        public void WyswietlTest()
        {
            service.Wyswietl<Czytelnik>();
            service.Wyswietl<Ksiazka>();
            service.Wyswietl<Wypozyczenie>();
            service.Wyswietl<OpisStanu>();
        }
    }
}