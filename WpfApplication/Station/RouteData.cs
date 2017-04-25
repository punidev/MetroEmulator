using System;
using System.Collections.Generic;
using System.Windows.Media;
using p = System.Windows.Shapes;
using b = System.Windows.Controls;
namespace MetroEmu.Station
{
    internal class RouteData
    {
        public enum LineColor { Red, Green, Blue }
        public static List<RouteData> Items = new List<RouteData>();
        public static List<p.Path> ButtonStack = new List<p.Path>(2);
        public LineColor Color;
        public Tuple<p.Path, b.Button> Pair;
        public static bool CheckBtnArray() => ButtonStack.Count == 2;

        internal static void Refresh()
        {
            ButtonStack.Clear();
            OpacityControll(true);
            ColorsFill(Items);
        }

        internal static void ColorsFill(IEnumerable<RouteData> lst)
        {
            foreach (var t in lst)
            {
                t.Pair.Item2.BorderBrush = new SolidColorBrush(ColorHelper.Convert(t.Color));
                t.Pair.Item1.Stroke = new SolidColorBrush(ColorHelper.Convert(t.Color));
            }
        }

        internal static void OpacityControll(bool rev = false)
        {
            Items.ForEach(t => t.Pair.Item1.Opacity = rev ? 1.0 : 0.2);
            Items.ForEach(t => t.Pair.Item2.Opacity = rev ? 1.0 : 0.2);
        }
    }
}
