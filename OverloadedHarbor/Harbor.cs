using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverloadedHarbor
{
    public class Harbor
    {
        public int _harborNumber { get; set; }
        public bool free { get; set; }
        public int shipCurrent { get; set; }
        public Harbor(int harborNumber)
        {
            free = true;
            _harborNumber = harborNumber;
        }
    }
}
