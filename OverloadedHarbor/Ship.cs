using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverloadedHarbor
{
    public enum TypeShip
    {
        small,
        medium,
        big
    }
    public class Ship
    {
        public int shipNumber { get; set; }
        public TypeShip typeShip { get; set; }
        public double loadTime { get; set; }
        public double arrivePorTime { get; set; }
        public double harborarrive { get; set; }
        public double harborleave { get; set; }
        public Ship(int c,double apt)
        {
            shipNumber = c;
            arrivePorTime = apt;
            typeShip = createShip();
            loadTime = GetDistribution();
        }

        private TypeShip createShip()
        {
            var r = new Random();
            
            var likehood = r.Next(0,2);
            if(likehood == 0)
            {
                return TypeShip.big;
            }
            else
            {
                likehood = r.Next(0, 2);
                if (likehood == 0)
                    return TypeShip.medium;
                else
                    return TypeShip.small;
            }
        }

        private double GetDistribution()
        {
            if (typeShip == TypeShip.small)
                return Distribution.Normal(9.0, 1.0);
            if (typeShip == TypeShip.medium)
                return Distribution.Normal(12.0, 2.0);
            return Distribution.Normal(18.0, 3.0);
        }
    }
}
