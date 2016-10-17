using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipTiles.Models
{
    public class Country
    {
        public string EnglishName { get; set; }
        public string FrenchName { get; set; }
        public string LocalName { get; set; }
        public string WorldRegion { get; set; }
        public string Flag { get; set; }
        public string FlagUri
        {
            get
            {
                return string.Format("/Assets/Flags/{0}", Flag);
            }
        }
    }
}
