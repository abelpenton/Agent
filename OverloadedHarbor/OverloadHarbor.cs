﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverloadedHarbor
{
    public class OverloadHarbor
    {
        private double _totalTime { get; set; }
        private double t0 { get; set; }
        private List<Harbor> harbors { get; set; }
        private Queue<Ship> port { get; set; }
        private TrailerShip trailer { get; set; }
        private List<Ship> ships { get; set; }
        private int c { get; set; }
        public double m { get; set; }
        public double m1 { get; set; }
        private double next { get; set; }
        private SortedList<double,EventPortToHarbor> events { get; set; }
        public OverloadHarbor(double totalTime)
        {
            _totalTime = totalTime*60;
            trailer = new TrailerShip();
            harbors = new List<Harbor>(3);
            port = new Queue<Ship>();
            ships = new List<Ship>();

            events = new SortedList<double,EventPortToHarbor>();
        }


        public void StartSimulation()
        {
            InitHarbors();

            while (t0 < _totalTime)
            {

                next += (Distribution.Exp(8) * 60);//lleguada del proximo barco

                var s = new Ship(++c,next);//genera proximo barco
                ships.Add(s);
                var fh = GetFreeHarbor();
                //si hay muelles libres añadir evento de llevar del puerto al muelle, en otro caso encolar en el puerto a la espera de muelles libres
                if (fh.free)
                {
                    events.Add(next, new EventPortToHarbor(s.shipNumber, fh.harbor._harborNumber));
                }
                else
                {
                    port.Enqueue(s);
                }
                //ejecutar el proximo evento, organizado por el tiempo trancurrido (t0)
                ExecuteEvents();
               
            }

            var first = events.First();
            while(first.Key < _totalTime && events.Count > 0)
            {
                ExecuteEvents();
                first = events.First();
            }
            Console.WriteLine($"Promedio de Espera en los muelles es de {m/c/60} horas");
            Console.WriteLine($"Promedio de Espera en los puertos es de {m1 / c / 60} horas");


        }

        private void ExecuteEvents()
        {
            if (events.Count > 0)
            {
                var e = events.First();
                events.Remove(e.Key);
                //Evento de llevar barco del puerto a un muelle
                if (e.Value.type == 0)
                {
                    //si el trailer esta en los muelles aumenta el tiempo transcurrido
                    if (trailer.place == "Harbor")
                    {
                        t0 += (Distribution.Exp(15));
                    }
                    //llevar el barco
                    t0 += e.Value.Time;
                    GoShip(e.Value.shipNumber, e.Value.harborNumber);
                }
                //Evento de Regresar un barco al puerto
                if (e.Value.type == 1)
                {
                    if (trailer.place == "Port")
                    {
                        t0 += (Distribution.Exp(15));
                    }
                    var s1 = GetShip(harbors[e.Value.harborNumber].shipCurrent);
                    //el barco s1 dejara el muelle en el tiempo trancurrido
                    s1.harborleave = t0;
                    //tiempo de regresar el barco al muelle
                    t0 += e.Value.Time;

                    trailer.place = "Port";
                    //todo barco espera su tiempo de carga en un muelle mas el tiempo que espero hasta que lo recogieran
                    var waiting = s1.loadTime;
                    if (s1.harborleave > e.Key)
                    {
                        waiting += s1.harborleave - e.Key;//si el tiempo en que va a dejar el muelle es mayor que el tiempo en que termino su carga, se le añade a la estancia en el muelle este tiempo
                    }
                    Console.WriteLine($"Barco {s1.shipNumber} espero el muelle {e.Value.harborNumber} {waiting / 60} horas");

                    m += waiting;

                    harbors[e.Value.harborNumber].free = true;

                    if (port.Count > 0)
                    {
                        //el trailer esta en el puerto y de seguro hay un muelle vacio, llevar el proximo barco en espera
                        var s = port.Dequeue();
                        var t = t0 - s.arrivePorTime;
                        Console.WriteLine($"Barco {s.shipNumber} espero en el puerto {t/60} horas");
                        m1 += t;
                        GoShip(s.shipNumber, harbors[e.Value.harborNumber]._harborNumber);
                    }

                }
            }
        }
        private void GoShip(int ship, int harbor)
        {
            //Asignar a un muelle un barco
            harbors[harbor].free = false;
            harbors[harbor].shipCurrent =ship;
            GetShip(ship).harborarrive = t0;
            //t = tiempo con el que llego + tiempo de carga, es decir que t indica el tiempo en que debe salir el barco del muelle
            var t  = t0 + GetShip(ship).loadTime;

            events.Add(t, new EventPortToHarbor(ship, harbor, 1));
            trailer.place = "Harbor";
        }
        private Ship GetShip(int id)
        {
            return ships.First(s => s.shipNumber == id);
        }

        private void InitHarbors()
        {
            for (int i = 0; i < 3; i++)
            {
                harbors.Add(new Harbor(i));
            }
        }
        private (bool free, Harbor harbor) GetFreeHarbor()
        {
            foreach (var h in harbors)
            {
                if (h.free)
                    return (true, h);
            }
            return (false, null);
        }

    }
}
