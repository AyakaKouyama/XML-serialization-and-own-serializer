using System;
using Zadanie1;

namespace TestyJednostkowe
{
    public class WypelnianieLosowo : IWypelnianie
    {
        public void Fill(DataRepository repository)
        {
            Random randStirng = new Random();
            string strImie = null;
            string strNazwisko = null;
            string strPesel = null;
            string chars = "abcdefghijklmnoprstuwyz";


            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    strImie += chars[randStirng.Next(chars.Length)];
                    strNazwisko += chars[randStirng.Next(chars.Length)];
                    strPesel += randStirng.Next(0, 9).ToString();
                }
                repository.CreateCzytelnik(new Czytelnik(strImie, strNazwisko, strPesel));
                strImie = null;
                strNazwisko = null;
                strPesel = null;
            }

            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    strImie += chars[randStirng.Next(chars.Length)];
                    strNazwisko += chars[randStirng.Next(chars.Length)];
                }

                repository.CreateKsiazka(new Ksiazka(i, strImie, strNazwisko));
                strImie = null;
                strNazwisko = null;
                strPesel = null;
            }

            for (int i = 1; i < 500; i++)
            {
                repository.CreateOpisStanu(new OpisStanu(repository.GetKsiazka(i), false, randStirng.Next(150)));
            }

            Random intRand = new Random();
            for (int i = 0; i < 100; i++)
            {
                int nrCzytelnika = intRand.Next(repository.GetAllCzytelnicy().Count);
                int nrKsiazki = intRand.Next(repository.GetAllOpisStanu().Count);
                repository.CreateWypozyczenie(new Wypozyczenie(repository.GetCzytelnik(nrCzytelnika), repository.GetOpisStanu(nrKsiazki)));
                repository.GetOpisStanu(nrKsiazki).CzyWypozyczona = true;
            }

        }
    }
}