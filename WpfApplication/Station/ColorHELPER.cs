using System.Linq;
using System.Windows.Media;
using static MetroEmu.Station.RouteData;

namespace MetroEmu.Station
{
    internal static class ColorHelper
    {
        public static Color Convert(LineColor color)
        {
            switch (color)
            {
                case LineColor.Red:
                    return Colors.Red;
                case LineColor.Green:
                    return Colors.Green;
                case LineColor.Blue:
                    return Colors.Blue;
                default:
                    return Colors.White;
            }
        }

        internal static LineColor Select(this System.Windows.Shapes.Path start)
        {
            return Items.Where(t => Equals(t.Pair.Item1, start)).Select(t => t.Color).FirstOrDefault();
        }
    }
}
