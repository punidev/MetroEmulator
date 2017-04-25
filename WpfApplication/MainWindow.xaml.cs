using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using MetroEmu.Station;
using static MetroEmu.Station.RouteData;
using p = System.Windows.Shapes;
using b = System.Windows.Controls;

namespace MetroEmu
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception)
            {
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Items.AddRange(FillList());
            ColorsFill(Items);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private static int GetIdbyPath(p.Path path)
        {
            var res = 0;
            for (var i = 0; i < Items.Count; i++)
            {
                if (Equals(Items[i].Pair.Item1, path))
                    res = i;
            }
            return res;
        }

        private void Switch(p.Path start, p.Path end)
        {
            if (Equals(
                Items.FirstOrDefault(t => Equals(t.Pair.Item1, start))?.Color,
                Items.FirstOrDefault(t => Equals(t.Pair.Item1, end))?.Color))
            {
                RouteController(start, end);
            }
            else
            {
                // start 
                foreach (var t in Items.Where(t => Equals(t.Pair.Item1, start)))
                {
                    switch (t.Color)
                    {
                        case LineColor.Red:
                            RouteController(start, 
                                Equals(end.Select(), LineColor.Blue) 
                                    ? RedL_kreshatik 
                                    : RedL_teatralna);
                            break;
                        case LineColor.Green:
                            RouteController(start,
                                Equals(end.Select(), LineColor.Blue) && 
                                Equals(start.Select(), LineColor.Green)
                                    ? GreenL_palatssportu
                                    : GreenL_zolotivorota);
                            break;
                        case LineColor.Blue:
                            RouteController(start, 
                                Equals(end.Select(), LineColor.Red) 
                                    ? BlueL_maidan 
                                    : BlueL_lt);
                            break;
                    }
                }
                // end
                foreach (var t in Items.Where(t => Equals(t.Pair.Item1, end)))
                {
                    switch (t.Color)
                    {
                        case LineColor.Red:
                            RouteController(
                                Equals(end.Select(), LineColor.Red) && 
                                Equals(start.Select(), LineColor.Green)
                                    ? RedL_teatralna
                                    : RedL_kreshatik, end);
                            break;
                        case LineColor.Green:
                            RouteController(
                                Equals(end.Select(), LineColor.Green) && 
                                Equals(start.Select(), LineColor.Blue)
                                    ? GreenL_palatssportu
                                    : GreenL_zolotivorota, end);
                            break;
                        case LineColor.Blue:
                            RouteController(
                                Equals(end.Select(), LineColor.Blue) && 
                                Equals(start.Select(), LineColor.Green)
                                    ? BlueL_lt
                                    : BlueL_maidan, end);
                            break;
                    }
                }
            }
        }

        private void Test()
        {
            Switch(RedL_shuliavska, GreenL_dorogozhichi);
            //Switch(GreenL_dorogozhichi, RedL_shuliavska);
            //Switch(RedL_shuliavska, BlueL_petrivka);
            //Switch(BlueL_petrivka, RedL_shuliavska);
            //Switch(GreenL_poznyaki, BlueL_teremki);
            //Switch(BlueL_teremki, GreenL_poznyaki);
        }
        

        private static void RouteController(p.Path start, p.Path end)
        {
            var color = Colors.Purple;
            var currentColor = new SolidColorBrush(color);
            if (GetIdbyPath(start) < GetIdbyPath(end))
            {
                for (var i = GetIdbyPath(start); i <= GetIdbyPath(end) - 1; i++)
                {
                    Items[i].Pair.Item1.Stroke = currentColor;
                    Items[i].Pair.Item1.Opacity = 1.0;
                }
                for (var i = GetIdbyPath(start); i <= GetIdbyPath(end); i++)
                {
                    Items[i].Pair.Item2.BorderBrush = currentColor;
                    Items[i].Pair.Item2.Opacity = 1.0;
                }
            }
            else
            {
                for (var i = GetIdbyPath(start) - 1; i >= GetIdbyPath(end); i--)
                {
                    Items[i].Pair.Item1.Stroke = currentColor;
                    Items[i].Pair.Item1.Opacity = 1.0;
                }
                for (var i = GetIdbyPath(start); i >= GetIdbyPath(end); i--)
                {
                    Items[i].Pair.Item2.BorderBrush = currentColor;
                    Items[i].Pair.Item2.Opacity = 1.0;
                }
            }
        }

        private void ClickStation(object sender, RoutedEventArgs e)
        {
            var sendData = (b.Button) sender;
            if (!CheckBtnArray())
            {
                foreach (var t in Items.Where(t => Equals(t.Pair.Item2, sendData)))
                {
                    ButtonStack.Add(t.Pair.Item1);
                    t.Pair.Item2.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                if (CheckBtnArray())
                {
                    OpacityControll();
                    Switch(ButtonStack[0], ButtonStack[1]);
                }
            }
        }

<<<<<<< HEAD
        private static IEnumerable<RouteData> GetElements(
            b.Panel gridLines, 
            b.Panel gridButtons, 
            LineColor color
            )
        {
            var items = new List<RouteData>();
            for (var i = 0; i < gridLines.Children.Count; i++)
=======
        private static IEnumerable<RouteData> GetElements(b.Panel gridLines, b.Panel gridButtons, LineColor color)
        {
            var items = new List<RouteData>();
            for (int i = 0; i < gridLines.Children.Count; i++)
>>>>>>> origin/master
            {
                items.Add(new RouteData
                {
                    Color = color,
                    Pair = Tuple.Create((p.Path) gridLines.Children[i], (b.Button) gridButtons.Children[i])
                });
            }
            return items;
        }

        private IEnumerable<RouteData> FillList()
        {
            var items = new List<RouteData>();
            items.AddRange(GetElements(RedLine, RedLinesButtons, LineColor.Red));
            items.AddRange(GetElements(GreenLine, GreenLinesButtons, LineColor.Green));
            items.AddRange(GetElements(BlueLine, BlueLinesButtons, LineColor.Blue));
            return items;
        }
    }
}
