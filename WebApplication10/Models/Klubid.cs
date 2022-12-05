using static System.Formats.Asn1.AsnWriter;
using System.ComponentModel;

namespace WebApplication10.Models
{
    public class Klubid
    {
        public string title { get; set; }
        public string kohtliigas { get; set; }
        public string logo { get; set; }
        public string mänge { get; set; }
        public string võite { get; set; }
        public string viike { get; set; }
        public string kaotusi { get; set; }
        public string väravaid { get; set; }
        public string punkte { get; set; }


        public string date { get; set; }
        public string teamleft { get; set; }
        public string teamright { get; set; }
        public string score { get; set; }
    }
}
