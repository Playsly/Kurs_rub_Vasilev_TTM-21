using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs_rub_Vasilev_TTM_21
{
    internal class Tanks
    {
        public string Name { get; set; }
        public string Img { get; set; }
        public string Categ { get; set; }
        public List<string> Tags { get; set; }
        public Tanks() { }
        public Tanks(string name, string img)
        {
            Name = name;
            Img = img;
            Categ = "";
            Tags = new List<string>();
        }
        public Tanks(string name, string img, string categ)
        {
            Name = name;
            Img = img;
            Categ = categ;
            Tags = new List<string>();
        }
        public void add_tag(string tag)
        {
            Tags.Add(tag);
        }
    }
}
