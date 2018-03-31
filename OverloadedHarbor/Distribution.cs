﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverloadedHarbor
{
    public static class Distribution
    {
        private static Random  u1 = new Random();
        private static Random a1 = new Random();
        private static Random a2 = new Random();


        public static double Normal(double mu, double sigma)
        {
            var f = Math.Sqrt(-2 * Math.Log(a1.NextDouble(), Math.E)) * Math.Cos(2 * Math.PI * a2.NextDouble());
            return Math.Sqrt(sigma) * f + mu;
        }

        public static double Exp(double lambda)
        {
            return (-1.0 / lambda) * (Math.Log(u1.NextDouble(), Math.E));
        }
    }
}
