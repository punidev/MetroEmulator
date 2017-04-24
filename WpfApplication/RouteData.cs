using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p = System.Windows.Shapes;
using b = System.Windows.Controls;
namespace WpfApplication
{
    internal class RouteData
    {
        public static List<RouteData> Items = new List<RouteData>(); 
        public string Color;
        public Tuple<p.Path, b.Button> Pair;
        public bool IsTransfer;
    }
}
