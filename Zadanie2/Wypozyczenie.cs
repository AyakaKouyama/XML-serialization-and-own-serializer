using System;
using System.Runtime.Serialization;

namespace Zadanie1
{
    [KnownType(typeof(Czytelnik)), KnownType(typeof(OpisStanu)), Serializable]
    public class Wypozyczenie : ISerializable
    {
        private Czytelnik _czytelnik;
        private OpisStanu _stan;

        public Czytelnik Czytelnik { get => _czytelnik; set => _czytelnik = value; }
        public OpisStanu Stan { get => _stan; set => _stan = value; }


        public Wypozyczenie(Czytelnik czytelnik, OpisStanu stan)
        {
            _czytelnik = czytelnik;
            _stan = stan;
        }

        public Wypozyczenie(SerializationInfo info, StreamingContext context)
        {
            _czytelnik = (Czytelnik)info.GetValue("czytelnik", typeof(Czytelnik));
            _stan = (OpisStanu)info.GetValue("stan", typeof(OpisStanu));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("czytelnik",  _czytelnik, typeof(Czytelnik));
            info.AddValue("stan", _stan, typeof(OpisStanu));
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

            Wypozyczenie wypozyczenie = (Wypozyczenie)obj;
            if (_czytelnik.Equals(wypozyczenie.Czytelnik) && _stan.Equals(wypozyczenie.Stan))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int result = 0;
            result = (_czytelnik.GetHashCode() * 500) ^ result;
            result = (_stan.GetHashCode() * 500) ^ result;

            return result;
        }
    }
}




