using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverloadedHarbor
{
    public class TrailerShip
    {
        public bool free { get; set;}
        public string place { get; set; }
        public TrailerShip()
        {
            free = true;
            place = "Port";
        }
    }
}
