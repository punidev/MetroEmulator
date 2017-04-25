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
        private IEnumerable<RouteData> FillList()
        {
            return new List<RouteData>
            {
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_akadem, bRedL_akadem),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_jitomyska, bRedL_jytomirska),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_svyatoshyn, bRedL_svyatoshyn),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_nivki, bRedL_nivki),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_berest, bRedL_berest),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_shuliavska, bRedL_shulyavska),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_polytech, bRedL_polytech),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_vokzalna, bRedL_vokzalna),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_univer, bRedL_univer),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_teatralna, bRedL_teatralna),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_kreshatik, bRedL_kreshatik),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_arsenalna, bRedL_arsenalna),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_dnipro, bRedL_dnipro),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_gidropark, bRedL_gidropark),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_livoberejna, bRedL_livoberezna),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_darnitsa, bRedL_darnitsa),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_chernigovska, bRedL_chernigivska),
                },
                new RouteData
                {
                    Color = LineColor.Red,
                    Pair = Tuple.Create(RedL_lisova, bRedL_lisova),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair = Tuple.Create(GreenL_syrets, bGreenL_syrets),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair = Tuple.Create(GreenL_dorogozhichi, bGreenL_dorogozhichi),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair = Tuple.Create(GreenL_lukyanivska, bGreenL_lukyanivska),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair = Tuple.Create(GreenL_zolotivorota, bGreenL_zolotivorota),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair = Tuple.Create(GreenL_palatssportu, bGreenL_palatssportu),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair = Tuple.Create(GreenL_klovska, bGreenL_klovska),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair = Tuple.Create(GreenL_pecherska, bGreenL_pecherska),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair = Tuple.Create(GreenL_druzbinarodiv, bGreenL_druzbinarodiv),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair = Tuple.Create(GreenL_vidubichi, bGreenL_vidubichi),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair =
                        Tuple.Create(GreenL_slavutich, bGreenL_slavutich),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair = Tuple.Create(GreenL_osokorki, bGreenL_osokorki),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair = Tuple.Create(GreenL_poznyaki, bGreenL_poznyaki),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair = Tuple.Create(GreenL_harkivksa, bGreenL_harkivska),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair = Tuple.Create(GreenL_vurlytsa, bGreenL_vyrlytsa),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair = Tuple.Create(GreenL_boryspilska, bGreenL_borispilska),
                },
                new RouteData
                {
                    Color = LineColor.Green,
                    Pair = Tuple.Create(GreenL_chervhutor, bGreenL_chervhutor),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_teremki, bBlueL_teremki),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_ipodrom, bBlueL_ipodrom),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_mvc, bBlueL_mvc),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_vasylkivska, bBlueL_vasylkivska),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_holosiivska, bBlueL_holosiivska),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_demiivska, bBlueL_demiivska),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_lybidska, bBlueL_lyubidska),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_palatsukraina, bBlueL_palatsukraina),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_olimpiiska, bBlueL_olimpiiska),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_lt, bBlueL_lt),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_maidan, bBlueL_maidan),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_postplosha, bBlueL_postplosha),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_kontrplosha, bBlueL_kontrplosha),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_tshevchenka, bBlueL_tshevchenka),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_petrivka, bBlueL_petrivka),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_obolon, bBlueL_obolon),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_minska, bBlueL_minska),
                },
                new RouteData
                {
                    Color = LineColor.Blue,
                    Pair = Tuple.Create(BlueL_heroevdnipra, bBlueL_heroevdnipra),
                },
            };
        }
    }
}
