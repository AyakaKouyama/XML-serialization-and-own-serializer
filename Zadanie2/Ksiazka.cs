using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Zadanie1
{
    [Serializable]
    public class Ksiazka : ISerializable
    {
        private int _id;
        private string _tytul;
        private string _autor;

        public int Id { get => _id; set => _id = value; }
        public string Tytul { get => _tytul; set => _tytul = value; }
        public string Autor { get => _autor; set => _autor = value; }

        public Ksiazka(int _id, string _tytul, string _autor)
        {
            this._id = _id;
            this._tytul = _tytul;
            this._autor = _autor;
        }

        public Ksiazka(SerializationInfo info, StreamingContext context)
        {
            _id = (int)info.GetValue("id", typeof(int));
            _tytul = (string)info.GetValue("tytul", typeof(string));
            _autor = (string)info.GetValue("autor", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("id", _id, typeof(int));
            info.AddValue("tytul", _tytul, typeof(string));
            info.AddValue("autor", _autor, typeof(string));
        }

        public override bool Equals(object obj)
        {
            if(this == obj)
            {
                return true;
            }
            if(obj == null)
            {
                return false;
            }
            Ksiazka ksiazka = (Ksiazka)obj;
            if (_id == ksiazka.Id && _tytul == ksiazka.Tytul && _autor == ksiazka.Autor)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int result = 0;
            result = (_id * 500) ^ result;
            result = (_tytul.Length * 500) ^ result;
            result = (_autor.Length * 500) ^ result;

            return result;
        }
    }
}
