using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Zadanie1
{
    public class DataRepository
    {
        private IWypelnianie _filler;
        private DataContext _data;

        public DataRepository(IWypelnianie filler)
        {
            if (filler == null)
            {
                throw new ArgumentNullException(nameof(filler));
            }
            this._data = new DataContext();
            this._filler = filler;
            filler.Fill(this);
        }

        public void CreateKsiazka(Ksiazka ksiazka)
        {
            _data.Ksiazki.Add(ksiazka.Id, ksiazka);
        }
        public void CreateWypozyczenie(Wypozyczenie wypozyczenie)
        {
            _data.Wypozyczenia.Add(wypozyczenie);
        }
        public void CreateCzytelnik(Czytelnik czytelnik)
        {
            _data.Czytelnicy.Add(czytelnik);
        }
        public void CreateOpisStanu(OpisStanu opis)
        {
            _data.Stan.Add(opis);
        }


        public Ksiazka GetKsiazka(int id)
        {
            return _data.Ksiazki[id];
        }
        public Wypozyczenie GetWypozyczenie(int id)
        {
            return _data.Wypozyczenia[id];
        }
        public Czytelnik GetCzytelnik(int id)
        {
            return _data.Czytelnicy[id];
        }
        public OpisStanu GetOpisStanu(int id)
        {
            return _data.Stan[id];
        }


        public Dictionary<int, Ksiazka> GetAllKsiazka()
        {
            return _data.Ksiazki;
        }
        public ObservableCollection<Wypozyczenie> GetAllWypozyczenie()
        {
            return _data.Wypozyczenia;
        }
        public List<Czytelnik> GetAllCzytelnicy()
        {
            return _data.Czytelnicy;
        }
        public List<OpisStanu> GetAllOpisStanu()
        {
            return _data.Stan;
        }


        public void DeleteKsiazka(Ksiazka element)
        {
            _data.Ksiazki.Remove(element.Id);
        }
        public void DeleteWypozyczenie(Wypozyczenie element)
        {
            _data.Wypozyczenia.Remove(element);
        }
        public void DeleteCzytelnik(Czytelnik element)
        {
            _data.Czytelnicy.Remove(element);
        }
        public void DeleteOpisStanu(OpisStanu element)
        {
            _data.Stan.Remove(element);
        }


        public void UpdateKsiazka(int id, Ksiazka element)
        {
            _data.Ksiazki[id] = element;
        }
        public void UpdateWypozyczenie(int id, Wypozyczenie element)
        {
            _data.Wypozyczenia[id] = element;
        }
        public void UpdateCzytelnik(int id, Czytelnik element)
        {
            _data.Czytelnicy[id] = element;
        }
        public void UpdateOpisStanu(int id, OpisStanu element)
        {
            _data.Stan[id] = element;
        }

        public DataContext getContext()
        {
            return _data;
        }
    }
}


