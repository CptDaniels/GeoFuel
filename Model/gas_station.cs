using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFuel.Model
{
    public class gas_station{
        public string dkn { get; set; }
        public string nazwa { get; set; }
        public string nip { get; set; }
        public string regon { get; set; }
        public string adres { get; set; }
        public string kod { get; set; }
        public string miejscowosc { get; set; }
        public string poczta { get; set; }
        public string wojewodztwo { get; set; }
        public string nazwaStacji { get; set; }
        public string liczbaZbiornikow { get; set; }
        public double lacznaPojemnosc { get; set; }
        public string infraUlica { get; set; }
        public string infraNrLokalu { get; set; }
        public string infraKod { get; set; }
        public string infraPoczta { get; set; }
        public string wspolrzedne { get; set; }
        public int benzynySilnikowe { get; set; }
        public int olejeNapedowe { get; set; }
        public int gazPlynnyLPG { get; set; }
        public int pojemnikiZLOO { get; set; }
        public int olejOpalowy { get; set; }
    }
}
