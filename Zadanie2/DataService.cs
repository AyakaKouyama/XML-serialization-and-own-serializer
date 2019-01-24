using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Zadanie1
{
    public class DataService
    {
        private DataRepository _repository;

        public DataService(DataRepository repository)
        {
            if(repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }
            this._repository = repository;
            repository.GetAllWypozyczenie().CollectionChanged += ObslugaZdarzen;
        }

        public void Wyswietl<T>()
        {
            if (typeof(T) == typeof(Czytelnik))
            {
                foreach (var i in _repository.GetAllCzytelnicy())
                {
                    Console.WriteLine("Czytelnik: imie - " + i.Imie + "   nazwisko - " + i.Nazwisko + "   pesel - " + i.Pesel);
                }
            }
            else if (typeof(T) == typeof(Ksiazka))
            {
                foreach (var i in _repository.GetAllKsiazka())
                {
                    Console.WriteLine("Ksiazka: id - " + i.Value.Id + " autor - " + i.Value.Autor + "     tytul - " + i.Value.Tytul);
                }
            }
            else if (typeof(T) == typeof(OpisStanu))
            {
                foreach (var i in _repository.GetAllOpisStanu())
                {
                    Console.WriteLine("Stan: ksiazka-id - " + i.Ksiazka.Id + " czy wypozyczona - " + i.CzyWypozyczona + " wartosc - " + i.Wartosc);
                }
            }
            else if (typeof(T) == typeof(Wypozyczenie))
            {
                foreach (var i in _repository.GetAllWypozyczenie())
                {
                    Console.WriteLine("Wypozyczenie: czytelnik - " + i.Czytelnik.Imie + " " + i.Czytelnik.Nazwisko + " opis - " + i.Stan.CzyWypozyczona + ", " + i.Stan.Ksiazka.Id + ", " + i.Stan.Wartosc);
                }
            }
            else
            {
                throw new Exception("Błędny typ");
            }


        }

        public void WyswietlaniePowiazanychDanych()
        {
            foreach (var i in _repository.GetAllCzytelnicy())
            {
                Console.WriteLine("Czytelnik: imie - " + i.Imie + "   nazwisko - " + i.Nazwisko + "   pesel - " + i.Pesel);
                foreach (var j in _repository.GetAllWypozyczenie())
                {
                    if (j.Czytelnik == i)
                    {
                        Console.Write("  ==>   Wypozyczyl : tytul - " + j.Stan.Ksiazka.Tytul + "   id - " + j.Stan.Ksiazka.Id +
                            "\n  ==>   Stan: czy wypozyczona - " + j.Stan.CzyWypozyczona + "   wartosc - " + j.Stan.Wartosc + "\n");
                    }
                }
            }
        }

        public void DodajWypozyczenie(Wypozyczenie wypozyczenie)
        {
            _repository.CreateWypozyczenie(wypozyczenie);
        }
        public void DodajCzytelnika(Czytelnik czytelnik)
        {
            _repository.CreateCzytelnik(czytelnik);
        }
        public void DodajKsiazke(Ksiazka ksiazka)
        {
            _repository.CreateKsiazka(ksiazka);
        }
        public void DodajOpisStanu(OpisStanu opis)
        {
            _repository.CreateOpisStanu(opis);
        }


        public Wypozyczenie GetWypozyczenie(int id)
        {
            return _repository.GetWypozyczenie(id);
        }
        public Ksiazka GetKsiazka(int id)
        {
            return _repository.GetKsiazka(id);
        }
        public Czytelnik GetCzytelnik(int id)
        {
            return _repository.GetCzytelnik(id);
        }
        public OpisStanu GetOpisStanu(int id)
        {
            return _repository.GetOpisStanu(id);
        }


        public List<Czytelnik> GetAllCzytelnicy()
        {
            return _repository.GetAllCzytelnicy();
        }
        public List<OpisStanu> GetAllOpisStanu()
        {
            return _repository.GetAllOpisStanu();
        }
        public ObservableCollection<Wypozyczenie> GetAllWypozyczenie()
        {
            return _repository.GetAllWypozyczenie();
        }
        public Dictionary<int, Ksiazka> getAllKsiazka()
        {
            return _repository.GetAllKsiazka();
        }

        public void UsunCzytelnika(Czytelnik czytelnik)
        {
            _repository.DeleteCzytelnik(czytelnik);
        }
        public void UsunKsiazke(Ksiazka ksiazka)
        {
            _repository.DeleteKsiazka(ksiazka);
        }
        public void UsunWypozyczenie(Wypozyczenie wypozyczenie)
        {
            _repository.DeleteWypozyczenie(wypozyczenie);
        }
        public void UsunOpisSatnu(OpisStanu opis)
        {
            _repository.DeleteOpisStanu(opis);
        }

        public void UpdateKsiazka(int id, Ksiazka element)
        {
            _repository.UpdateKsiazka(id, element);
        }
        public void UpdateWypozyczenie(int id, Wypozyczenie element)
        {
            _repository.UpdateWypozyczenie(id, element);
        }
        public void UpdateCzytelnik(int id, Czytelnik element)
        {
            _repository.UpdateCzytelnik(id, element);
        }
        public void UpdateOpisStanu(int id, OpisStanu element)
        {
            _repository.UpdateOpisStanu(id, element);
        }

        public Dictionary<int, Ksiazka> SzukajKsiazekAutora(String autor)
        {
            Dictionary<int, Ksiazka> filtr = new Dictionary<int, Ksiazka>();

            foreach (var i in _repository.GetAllKsiazka())
            {
                if (i.Value.Autor == autor)
                {
                    filtr.Add(i.Key, i.Value);
                }
            }
            return filtr;
        }

        public List<OpisStanu> SzukajWypozyczen(bool czyWypozyczona)
        {
            List<OpisStanu> filtr = new List<OpisStanu>();

            foreach (var i in _repository.GetAllOpisStanu())
            {
                if (i.CzyWypozyczona == czyWypozyczona)
                {
                    filtr.Add(i);
                }

            }

            return filtr;
        }

        public Dictionary<int, Ksiazka> SzukajKsiazki(string tytul)
        {
            Dictionary<int, Ksiazka> filtr = new Dictionary<int, Ksiazka>();

            foreach (var i in _repository.GetAllKsiazka())
            {
                if (i.Value.Tytul == tytul)
                {
                    filtr.Add(i.Key, i.Value);
                }

            }

            return filtr;
        }

        public Dictionary<int, Ksiazka> SzukajKsiazkiPoId(int id)
        {
            Dictionary<int, Ksiazka> filtr = new Dictionary<int, Ksiazka>();

            foreach (var i in _repository.GetAllKsiazka())
            {
                if (i.Value.Id == id)
                {
                    filtr.Add(i.Key, i.Value);
                }

            }

            return filtr;
        }

        public List<Czytelnik> SzukajCzytelnikaPoNazwisku(string nazwisko)
        {
            List<Czytelnik> filtr = new List<Czytelnik>();

            foreach (var i in _repository.GetAllCzytelnicy())
            {
                if (i.Nazwisko == nazwisko)
                {
                    filtr.Add(i);
                }

            }

            return filtr;
        }

        public List<Czytelnik> SzukajCzytelnikaPoImieniu(string imie)
        {
            List<Czytelnik> filtr = new List<Czytelnik>();

            foreach (var i in _repository.GetAllCzytelnicy())
            {
                if (i.Imie == imie)
                {
                    filtr.Add(i);
                }

            }

            return filtr;
        }

        public List<Czytelnik> SzukajCzytelnikaPoImieniuiNazwisku(string imie, string nazwisko)
        {
            List<Czytelnik> filtr = new List<Czytelnik>();

            foreach (var i in _repository.GetAllCzytelnicy())
            {
                if (i.Imie == imie && i.Nazwisko == nazwisko)
                {
                    filtr.Add(i);
                }

            }

            return filtr;
        }

        public ObservableCollection<Wypozyczenie> SzukajWypozyczenCzytelnika(Czytelnik czytelnik)
        {
            ObservableCollection<Wypozyczenie> filtr = new ObservableCollection<Wypozyczenie>();

            foreach (var i in _repository.GetAllWypozyczenie())
            {
                if (i.Czytelnik == czytelnik)
                {
                    filtr.Add(i);
                }
            }
            return filtr;
        }

        public List<OpisStanu> SzukajOpisuStanu(Ksiazka ksiazka)
        {
            List<OpisStanu> filtr = new List<OpisStanu>();

            foreach (var i in _repository.GetAllOpisStanu())
            {
                if (i.Ksiazka == ksiazka)
                {
                    filtr.Add(i);
                }
            }
            return filtr;
        }

        public void Wypozycz(Czytelnik czytelnik, Ksiazka ksiazka)
        {
            int counter = 0, czytelnikId = 0;
            foreach (var i in _repository.GetAllCzytelnicy())
            {

                if (i == czytelnik)
                {
                    break;
                }
                czytelnikId++;
            }

            int stanId = 0;
            bool found = false;
            List<OpisStanu> opisy = SzukajOpisuStanu(ksiazka);
            foreach (var i in GetAllOpisStanu())
            {
                for (int j = 0; j < opisy.Count; j++)
                {

                    if (i == opisy[j] && opisy[j].CzyWypozyczona == false)
                    {
                        stanId = counter;
                        found = true;
                        break;
                    }
                    counter++;

                }
            }

            if (found == true)
            {
                DodajWypozyczenie(new Wypozyczenie(_repository.GetCzytelnik(czytelnikId), GetOpisStanu(stanId)));
                GetOpisStanu(stanId).CzyWypozyczona = true;
                Console.WriteLine(GetOpisStanu(stanId - 1).Ksiazka.Tytul);
            }
            else
            {
                Console.WriteLine("Nie znaleziono książki o tym tyltule, którą możnaby wypożyczyć");
                throw new Exception("Nie znaleziono książki o tym tyltule, którą możnaby wypożyczyć");
            }

        }

        private void ObslugaZdarzen(object seneder, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Console.WriteLine("Dodano wypozyczenie do kolekcji");
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                Console.WriteLine("Usunieto wypozyczenie z kolekcji");
            }
        }

        public DataContext getContext()
        {
            return _repository.getContext();
        }


    }
}
