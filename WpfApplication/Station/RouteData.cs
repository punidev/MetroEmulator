using System;
using System.Collections.Generic;
using p = System.Windows.Shapes;
using b = System.Windows.Controls;
namespace MetroEmu.Station
{
    internal class RouteData
    {
        public static List<RouteData> Items = new List<RouteData>(); 
        public string Color;
        public Tuple<p.Path, b.Button> Pair;
    }
}
