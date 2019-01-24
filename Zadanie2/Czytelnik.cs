using System;
using System.Runtime.Serialization;

namespace Zadanie1
{

    [Serializable]
    public class Czytelnik  : ISerializable
    {

        private string _imie;
        private string _nazwisko;
        private string _pesel;

        public string Imie { get => _imie; set => _imie = value; }
        public string Nazwisko { get => _nazwisko; set => _nazwisko = value; }
        public string Pesel { get => _pesel; set => _pesel = value; }

        public Czytelnik(string _imie, string _nazwisko, string _pesel)
        {
            this._imie = _imie;
            this._nazwisko = _nazwisko;
            this._pesel = _pesel;
        }

        public Czytelnik(SerializationInfo info, StreamingContext context)
        {
            _imie = (string)info.GetValue("imie", typeof(string));
            _nazwisko = (string)info.GetValue("nazwisko", typeof(string));
            _pesel = (string)info.GetValue("pesel", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("imie", _imie, typeof(string));
            info.AddValue("nazwisko", _nazwisko, typeof(string));
            info.AddValue("pesel", _pesel, typeof(string));
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

            Czytelnik czytelnik = (Czytelnik)obj;
            if (_imie == czytelnik.Imie && _nazwisko == czytelnik.Nazwisko && _pesel == czytelnik.Pesel)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int result = 0;
            result = (_imie.Length * 500) ^ result;
            result = (_nazwisko.Length * 500) ^ result;
            result = (_pesel.Length * 500)  ^ result;

            return result;
        }
    }
}
