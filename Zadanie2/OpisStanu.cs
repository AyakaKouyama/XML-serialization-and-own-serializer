using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Zadanie1
{
    [KnownType(typeof(Ksiazka)), Serializable]
    public class OpisStanu : ISerializable
    {
        private Ksiazka _ksiazka;
        private bool _czyWypozyczona;
        private int _warotsc;

        public int Wartosc { get => _warotsc; set => _warotsc = value; }
        public bool CzyWypozyczona { get => _czyWypozyczona; set => _czyWypozyczona = value; }
        public Ksiazka Ksiazka { get => _ksiazka; set => _ksiazka = value; }

        public OpisStanu(Ksiazka ksiazka, bool czyWypozyczona, int wartosc)
        {
            _ksiazka = ksiazka;
            _czyWypozyczona = czyWypozyczona;
            _warotsc = wartosc;
        }

        public OpisStanu(SerializationInfo info, StreamingContext context)
        {
            _warotsc = (int)info.GetValue("wartosc", typeof(int));
            _czyWypozyczona = (bool)info.GetValue("czy", typeof(bool));
            _ksiazka = (Ksiazka)info.GetValue("ksiazka", typeof(Ksiazka));
           
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("wartosc", _warotsc, typeof(int));
            info.AddValue("czy", _czyWypozyczona, typeof(bool));
            info.AddValue("ksiazka", _ksiazka, typeof(Ksiazka));
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }

            OpisStanu opis = (OpisStanu)obj;
            if (_ksiazka.Equals(opis.Ksiazka) && _czyWypozyczona == opis.CzyWypozyczona && _warotsc == opis.Wartosc)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int result = 0;
            result = (_ksiazka.GetHashCode() * 500) ^ result;
            result = (_warotsc * 500) ^ result;

            return result; 
        }
    }
}
