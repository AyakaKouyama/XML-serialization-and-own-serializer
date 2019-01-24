using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Zadanie1
{
    [DataContract, KnownType(typeof(DataContext))]
    public class DataContext
    {
        [DataMember]
        private List<Czytelnik> czytelnicy;
        [DataMember]
        private Dictionary<int, Ksiazka> ksiazki;
        [DataMember]
        private ObservableCollection<Wypozyczenie> wypozyczenia;
        [DataMember]
        private List<OpisStanu> stan;

        public List<Czytelnik> Czytelnicy { get => czytelnicy; set => czytelnicy = value; }
        public Dictionary<int, Ksiazka> Ksiazki { get => ksiazki; set => ksiazki = value; }
        public ObservableCollection<Wypozyczenie> Wypozyczenia { get => wypozyczenia; set => wypozyczenia = value; }
        public List<OpisStanu> Stan { get => stan; set => stan = value; }


        public DataContext()
        {
            Czytelnicy = new List<Czytelnik>();
            Ksiazki = new Dictionary<int, Ksiazka>();
            Wypozyczenia = new ObservableCollection<Wypozyczenie>();
            Stan = new List<OpisStanu>();
        }


    }
}
