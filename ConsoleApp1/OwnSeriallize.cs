using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Zadanie1;

namespace ConsoleApp1
{
    public class OwnSeriallize
    {
        private ObjectIDGenerator idGenerator;
        private string textFile;
        private List<int> ksiazkiIds;
        private List<int> stanIds;
        private List<int> wypozyczeniaIds;
        private List<int> czytelnicyIds;

        public OwnSeriallize()
        {
            ksiazkiIds = new List<int>();
            stanIds = new List<int>();
            wypozyczeniaIds = new List<int>();
            czytelnicyIds = new List<int>();
        }


        public void Serialize(StreamWriter stream, DataContext dataContext)
        {
            idGenerator = new ObjectIDGenerator();
            using (stream)
            {
                stream.WriteLine("<" + typeof(DataContext).Name + ">\n");
                SerializeCzytelnicy(stream, dataContext.Czytelnicy);
                SerializeKsiazki(stream, dataContext.Ksiazki);
                SerializeStan(stream, dataContext.Stan);
                SerializeWypozyczenia(stream, dataContext.Wypozyczenia);
                stream.WriteLine("</" + typeof(DataContext).Name + ">\n");
            }

        }

        public DataContext Deserialize(StreamReader stream)
        {
            idGenerator = new ObjectIDGenerator();
            textFile = stream.ReadToEnd();
            DataContext dataContex = new DataContext();

            dataContex.Czytelnicy = DeserializeCzytelnicy();
            dataContex.Ksiazki = DeserializeKsiazki();
            dataContex.Stan = DesarializeOpisStanu(dataContex.Ksiazki);
            dataContex.Wypozyczenia = DeserializeWypozyczenia(dataContex.Stan, dataContex.Czytelnicy);

            return dataContex;
        }

        private void SerializeCzytelnicy(StreamWriter stream, List<Czytelnik> czytelnicy)
        {
            stream.WriteLine("<" + typeof(List<Czytelnik>).Name + " " + typeof(Czytelnik).Name + ">\n");
            StringBuilder wyp = new StringBuilder();
            string name;
            object value;
            Type type;

            foreach (var i in czytelnicy)
            {
                SerializationInfo info = new SerializationInfo(typeof(Czytelnik), new FormatterConverter());
                StreamingContext context = new StreamingContext();
                i.GetObjectData(info, context);
                wyp.Append("(" + typeof(Czytelnik).Name + ")\n");
                wyp.Append("|ID|" + idGenerator.GetId(i, out bool first) + "|/ID|\n");
                foreach (SerializationEntry k in info)
                {
                    name = k.Name;
                    value = k.Value;
                    wyp.Append("|" + name + "|" + value + "|/" + name + "|\n");
                }
                foreach (SerializationEntry k in info)
                {
                    name = k.Name;
                    type = k.ObjectType;
                    wyp.Append("|" + name + "|" + type.Name + "|/" + name + "|\n");
                }
                wyp.Append("(/" + typeof(Czytelnik).Name + ")\n");
                stream.WriteLine(wyp);
                wyp.Clear();

            }
            stream.WriteLine("</" + typeof(List<Czytelnik>).Name + " " + typeof(Czytelnik).Name + ">");

        }

        private void SerializeKsiazki(StreamWriter stream, Dictionary<int, Ksiazka> ksiazki)
        {
            stream.WriteLine("<" + typeof(Dictionary<int, Ksiazka>).Name + ">\n");
            StringBuilder wyp = new StringBuilder();
            bool first;
            string name;
            Type type;
            object value;

            foreach (var i in ksiazki)
            {
                SerializationInfo info = new SerializationInfo(typeof(Ksiazka), new FormatterConverter());
                StreamingContext context = new StreamingContext();
                i.Value.GetObjectData(info, context);
                wyp.Append("(" + typeof(Ksiazka).Name + ")\n");
                wyp.Append("|ID|" + idGenerator.GetId(i.Value, out first) + "|/ID|\n");
                foreach (SerializationEntry k in info)
                {
                    name = k.Name;
                    type = k.ObjectType;
                    value = k.Value;
                    wyp.Append("|" + name + "|" + value + "|/" + name + "|\n");
                }
                wyp.Append("|key|" + i.Key + "|/key|\n");

                foreach (SerializationEntry k in info)
                {
                    name = k.Name;
                    type = k.ObjectType;
                    wyp.Append("|" + name + "|" + type.Name + "|/" + name + "|\n");
                }

                wyp.Append("(/" + typeof(Ksiazka).Name + ")\n");
                stream.WriteLine(wyp);
                wyp.Clear();

            }

            stream.WriteLine("</" + typeof(Dictionary<int, Ksiazka>).Name + ">");
        }


        private void SerializeWypozyczenia(StreamWriter stream, ObservableCollection<Wypozyczenie> wypozyczenia)
        {
            stream.WriteLine("<" + typeof(ObservableCollection<Wypozyczenie>).Name + ">\n");
            StringBuilder wyp = new StringBuilder();
            bool first;
            string name;
            Type type;
            object value;

            foreach (var i in wypozyczenia)
            {
                SerializationInfo info = new SerializationInfo(typeof(Wypozyczenie), new FormatterConverter());
                StreamingContext context = new StreamingContext();
                i.GetObjectData(info, context);

                wyp.Append("(" + typeof(Wypozyczenie).Name + ")\n");
                wyp.Append("|ID|" + idGenerator.GetId(i, out first) + "|/ID|\n");
                foreach (SerializationEntry k in info)
                {
                    name = k.Name;
                    type = k.ObjectType;
                    value = k.Value;


                    if (name.Equals("czytelnik") == true)
                    {
                        wyp.Append("|" + name + "|" + idGenerator.GetId(i.Czytelnik, out first) + "|/" + name + "|\n");
                    }
                    else if (name.Equals("stan") == true)
                    {
                        wyp.Append("|" + name + "|" + idGenerator.GetId(i.Stan, out first) + "|/" + name + "|\n");
                    }
                }
                foreach (SerializationEntry k in info)
                {
                    name = k.Name;
                    type = k.ObjectType;
                    wyp.Append("|" + name + "|" + type.Name + "|/" + name + "|\n");
                }
                wyp.Append("(/" + typeof(Wypozyczenie).Name + ")\n");
                stream.WriteLine(wyp);
                wyp.Clear();
            }
            stream.WriteLine("</" + typeof(ObservableCollection<Wypozyczenie>).Name + ">");
        }

        private void SerializeStan(StreamWriter stream, List<OpisStanu> stan)
        {
            stream.WriteLine("<" + typeof(List<OpisStanu>).Name + " " + typeof(OpisStanu).Name + ">\n");
            StringBuilder wyp = new StringBuilder();
            string name;
            object value;
            Type type;

            foreach (var i in stan)
            {
                SerializationInfo info = new SerializationInfo(typeof(Czytelnik), new FormatterConverter());
                StreamingContext context = new StreamingContext();
                i.GetObjectData(info, context);
                wyp.Append("(" + typeof(OpisStanu).Name + ")\n");
                wyp.Append("|ID|" + idGenerator.GetId(i, out bool first) + "|/ID|\n");
                foreach (SerializationEntry k in info)
                {
                    name = k.Name;
                    value = k.Value;
                    if (name == "ksiazka")
                    {
                        wyp.Append("|" + name + "|" + idGenerator.GetId(i.Ksiazka, out first) + "|/" + name + "|\n");
                    }
                    else
                    {
                        wyp.Append("|" + name + "|" + value + "|/" + name + "|\n");
                    }

                }

                foreach (SerializationEntry k in info)
                {
                    name = k.Name;
                    type = k.ObjectType;
                    wyp.Append("|" + name + "|" + type.Name + "|/" + name + "|\n");
                }

                wyp.Append("(/" + typeof(OpisStanu).Name + ")\n");
                stream.WriteLine(wyp);
                wyp.Clear();

            }

            stream.WriteLine("</" + typeof(List<OpisStanu>).Name + " " + typeof(OpisStanu).Name + ">");

        }

        public List<Czytelnik> DeserializeCzytelnicy()
        {
            List<Czytelnik> result = new List<Czytelnik>();
            string startTag = "<" + typeof(List<Czytelnik>).Name + " " + typeof(Czytelnik).Name + ">";
            string endTag = "</" + typeof(List<Czytelnik>).Name + " " + typeof(Czytelnik).Name + ">";
            int startIndex = textFile.IndexOf(startTag);
            int endIndex = textFile.IndexOf(endTag);
            string czytelnicyText = textFile.Substring(startIndex, endIndex - startIndex);

            int prev = 0;
            int endI = 0;
            do
            {
                SerializationInfo info = new SerializationInfo(typeof(Czytelnik), new FormatterConverter());
                StreamingContext context = new StreamingContext();

                string czytelnik = null;
                string begin = "(Czytelnik)";
                string end = "(/Czytelnik)";
                int beginI = czytelnicyText.IndexOf(begin, prev);
                endI = czytelnicyText.IndexOf(end, prev);

                if (endI != -1)
                {
                    czytelnik = czytelnicyText.Substring(beginI, endI - beginI);
                    string[] cut = czytelnik.Split('|');
                    czytelnicyIds.Add(Int32.Parse(cut[2]));

                    info.AddValue(cut[5], cut[6], ConvertToType(cut[18]));
                    info.AddValue(cut[9], cut[10], ConvertToType(cut[22]));
                    info.AddValue(cut[13], cut[14], ConvertToType(cut[26]));

                    result.Add(new Czytelnik(info, context));
                }

                prev = endI + 1;
            } while (endI != -1);

            return result;
        }

        public Dictionary<int, Ksiazka> DeserializeKsiazki()
        {
            Dictionary<int, Ksiazka> result = new Dictionary<int, Ksiazka>();
            string startTag = "<" + typeof(Dictionary<int, Ksiazka>).Name + ">";
            string endTag = "</" + typeof(Dictionary<int, Ksiazka>).Name + ">";
            int startIndex = textFile.IndexOf(startTag);
            int endIndex = textFile.IndexOf(endTag);
            string czytelnicyText = textFile.Substring(startIndex, endIndex - startIndex);
            int prev = 0;
            int endI = 0;
            do
            {
                SerializationInfo info = new SerializationInfo(typeof(Ksiazka), new FormatterConverter());
                StreamingContext context = new StreamingContext();

                string ksiazka = null;
                string begin = "(Ksiazka)";
                string end = "(/Ksiazka)";
                int beginI = czytelnicyText.IndexOf(begin, prev);
                endI = czytelnicyText.IndexOf(end, prev);
                if (endI != -1)
                {
                    ksiazka = czytelnicyText.Substring(beginI, endI - beginI);
                    string[] cut = ksiazka.Split('|');

                    ksiazkiIds.Add(Int32.Parse(cut[2]));
                    int bookId = Int32.Parse(cut[6]);

                    info.AddValue(cut[5], bookId, ConvertToType(cut[22]));
                    info.AddValue(cut[9], cut[10], ConvertToType(cut[26]));
                    info.AddValue(cut[13], cut[14], ConvertToType(cut[30]));

                    int key = Int32.Parse(cut[18]);
                    result.Add(key, new Ksiazka(info, context));
                }

                prev = endI + 1;
            } while (endI != -1);


            return result;
        }

        public List<OpisStanu> DesarializeOpisStanu(Dictionary<int, Ksiazka> ksiazki)
        {
            List<OpisStanu> result = new List<OpisStanu>();
            string startTag = "<" + typeof(List<OpisStanu>).Name + " " + typeof(OpisStanu).Name + ">";
            string endTag = "</" + typeof(List<OpisStanu>).Name + " " + typeof(OpisStanu).Name + ">";
            int startIndex = textFile.IndexOf(startTag);
            int endIndex = textFile.IndexOf(endTag);
            string czytelnicyText = textFile.Substring(startIndex, endIndex - startIndex);
            int prev = 0;
            int endI = 0;
            do
            {
                SerializationInfo info = new SerializationInfo(typeof(OpisStanu), new FormatterConverter());
                StreamingContext context = new StreamingContext();

                string ksiazka = null;
                string begin = "(OpisStanu)";
                string end = "(/OpisStanu)";

                int beginI = czytelnicyText.IndexOf(begin, prev);
                endI = czytelnicyText.IndexOf(end, prev);
                if (endI != -1)
                {
                    ksiazka = czytelnicyText.Substring(beginI, endI - beginI);
                    string[] cut = ksiazka.Split('|');

                    stanIds.Add(Int32.Parse(cut[2]));
                    bool czy = cut[10] == "True" ? true : false;
                    int wartosc = Int32.Parse(cut[6]);
                    int key = Int32.Parse(cut[14]);

                    Ksiazka tempBook = null;
                    for (int i = 0; i < ksiazkiIds.Count; i++)
                    {
                        if (ksiazkiIds[i] == Int32.Parse(cut[14]))
                        {
                            tempBook = ksiazki[i];
                        }
                    }

                    info.AddValue(cut[5], wartosc, ConvertToType(cut[18]));
                    info.AddValue(cut[9], czy, ConvertToType(cut[22]));
                    info.AddValue(cut[13], tempBook, ConvertToType(cut[26]));

                    result.Add(new OpisStanu(info, context));
                }

                prev = endI + 1;
            } while (endI != -1);

            return result;
        }

        public ObservableCollection<Wypozyczenie> DeserializeWypozyczenia(List<OpisStanu> opis, List<Czytelnik> czytelnicy)
        {
            ObservableCollection<Wypozyczenie> result = new ObservableCollection<Wypozyczenie>();
            string startTag = "<" + typeof(ObservableCollection<Wypozyczenie>).Name + ">";
            string endTag = "</" + typeof(ObservableCollection<Wypozyczenie>).Name + ">";
            int startIndex = textFile.IndexOf(startTag);
            int endIndex = textFile.IndexOf(endTag);
            string czytelnicyText = textFile.Substring(startIndex, endIndex - startIndex);

            int prev = 0;
            int endI = 0;
            do
            {
                string ksiazka = null;
                string begin = "(Wypozyczenie)";
                string end = "(/Wypozyczenie)";

                int beginI = czytelnicyText.IndexOf(begin, prev);
                endI = czytelnicyText.IndexOf(end, prev);

                if (endI != -1)
                {
                    SerializationInfo info = new SerializationInfo(typeof(OpisStanu), new FormatterConverter());
                    StreamingContext context = new StreamingContext();
                    ksiazka = czytelnicyText.Substring(beginI, endI - beginI);
                    string[] cut = ksiazka.Split('|');

                    wypozyczeniaIds.Add(Int32.Parse(cut[2]));
                    int idCzytelnik = Int32.Parse(cut[6]);
                    int idOpis = Int32.Parse(cut[10]);
                    OpisStanu tempOpis = null;
                    Czytelnik tempCzytelnik = null;
                    bool found = false;
                    for (int i = 0; i < czytelnicyIds.Count; i++)
                    {

                        if (czytelnicyIds[i] == idCzytelnik && found == false)
                        {
                            tempCzytelnik = czytelnicy[i];
                            info.AddValue(cut[5], tempCzytelnik, ConvertToType(cut[14]));
                            found = true;
                        }
                    }

                    found = false;
                    for (int i = 0; i < stanIds.Count; i++)
                    {
                        if (stanIds[i] == idOpis && found == false)
                        {
                            tempOpis = opis[i];
                            info.AddValue(cut[9], tempOpis, ConvertToType(cut[18]));
                            found = true;
                        }
                    }
                    if (tempOpis != null && tempCzytelnik != null)
                    {
                        result.Add(new Wypozyczenie(info, context));
                    }
                }
                prev = endI + 1;
            } while (endI != -1);

            return result;
        }

        public Type ConvertToType(String value)
        {
            if (value == "String")
            {
                return typeof(string);
            }
            else if (value == "Int32")
            {
                return typeof(int);
            }
            else if (value == "Boolean")
            {
                return typeof(bool);
            }
            else if (value == "Ksiazka")
            {
                return typeof(Ksiazka);
            }
            else if (value == "Czytelnik")
            {
                return typeof(Czytelnik);
            }
            else if (value == "OpisStanu")
            {
                return typeof(OpisStanu);
            }
            else if (value == "Wypozyczenie")
            {
                return typeof(Wypozyczenie);
            }
            else
            {
                return null;
            }

        }
    }
}
