using OverloadedHarbor;
using System;

namespace Simulacion
{
    class Program
    {
        static void Main(string[] args)
        {
            var overloadHarbor = new OverloadHarbor(48.0);//numero horas
            overloadHarbor.StartSimulation();
          
        }
    }
}
