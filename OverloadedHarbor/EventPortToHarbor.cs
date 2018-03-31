using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverloadedHarbor
{
    public class EventPortToHarbor
    {
        public double Time { get; set; }
        public int shipNumber { get; set; }
        public int harborNumber { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public EventPortToHarbor(int ShipNumber, int HarborNumber, int type = 0)
        {
            if(type == 0)
            {
                name = $"Llevar barco {shipNumber} al muelle {harborNumber}";
            }
            else
            {
                name = $"Llevar barco {ShipNumber} de vuelta al puerto";
            }
            this.type = type;           
            Time = type == 0 ? Distribution.Exp(2)*60 :Distribution.Exp(1)*60;
            shipNumber = ShipNumber;
            harborNumber = HarborNumber;
        }
    }
}
