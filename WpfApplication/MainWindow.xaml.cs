using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using MetroEmu.Station;
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
        private static int GetIdbyPath(p.Path path)
        {
            var res = 0;
            for (var i = 0; i < RouteData.Items.Count; i++)
            {
                if (Equals(RouteData.Items[i].Pair.Item1, path))
                    res = i;
            }
            return res;
        }

        private void Switch(p.Path start, p.Path end)
        {
            bool isEndBlue = false, 
                 isEndRed = false,
                 isEndGreen=false, 
                 isStartGreen = false, 
                 isStartBlue = false, 
                 isStartRed=false;

            if (Equals(
                RouteData.Items
                    .Where(t => Equals(t.Pair.Item1, start))
                    .FirstOrDefault(t => t.Color != null)
                    ?.Color,
                RouteData.Items
                    .Where(t => Equals(t.Pair.Item1, end))
                    .FirstOrDefault(t => t.Color != null)
                    ?.Color))
            {
                RouteController(start, end);
            }
            else
            {
                foreach (RouteData t in RouteData.Items)
                {
                    if (Equals(t.Pair.Item1, end))
                        if (t.Color == "R")
                            isEndRed = true;
                    if (Equals(t.Pair.Item1, start))
                        if (t.Color == "R")
                            isStartRed = true;
                    if (Equals(t.Pair.Item1, start))
                        if (t.Color == "B")
                            isStartBlue = true;
                    if (Equals(t.Pair.Item1, end))
                        if (t.Color == "B")
                            isEndBlue = true;
                    if (Equals(t.Pair.Item1, end))
                        if (t.Color == "G")
                            isEndGreen = true;
                    if (Equals(t.Pair.Item1, start))
                        if (t.Color == "G")
                            isStartGreen = true;
                }
                // start 
                foreach (RouteData t in RouteData.Items.Where(t => Equals(t.Pair.Item1, start)))
                {
                    switch (t.Color)
                    {
                        case "R":
                            RouteController(start, isEndRed ? end : isEndBlue ? RedL_kreshatik : RedL_teatralna);
                            break;
                        case "G":
                            RouteController(start, isEndBlue & isStartGreen ? GreenL_palatssportu : GreenL_zolotivorota);
                            break;
                        case "B":
                            RouteController(start, isEndRed ? BlueL_maidan : BlueL_lt);
                            break;
                    }
                }
                // end
                foreach (RouteData t in RouteData.Items.Where(t => Equals(t.Pair.Item1, end)))
                {
                    switch (t.Color)
                    {
                        case "R":
                            RouteController(isEndRed && isStartGreen ? RedL_teatralna : RedL_kreshatik, end);
                            break;
                        case "G":
                            RouteController(isEndGreen & isStartBlue ? GreenL_palatssportu : GreenL_zolotivorota, end);
                            break;
                        case "B":
                            RouteController(isEndBlue & isStartGreen ? BlueL_lt : BlueL_maidan, end);
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
            if (GetIdbyPath(start) < GetIdbyPath(end))
            {
                for (var i = GetIdbyPath(start); i <= GetIdbyPath(end) - 1; i++)
                {
                    RouteData.Items[i].Pair.Item1.Stroke = new SolidColorBrush(color);
                    RouteData.Items[i].Pair.Item1.Opacity = 1.0;
                }
                for (var i = GetIdbyPath(start); i <= GetIdbyPath(end); i++)
                {
                    RouteData.Items[i].Pair.Item2.BorderBrush = new SolidColorBrush(color);
                    RouteData.Items[i].Pair.Item2.Opacity = 1.0;
                }
            }
            else
            {
                for (var i = GetIdbyPath(start) - 1; i >= GetIdbyPath(end); i--)
                {
                    RouteData.Items[i].Pair.Item1.Stroke = new SolidColorBrush(color);
                    RouteData.Items[i].Pair.Item1.Opacity = 1.0;
                }
                for (var i = GetIdbyPath(start); i >= GetIdbyPath(end); i--)
                {
                    RouteData.Items[i].Pair.Item2.BorderBrush = new SolidColorBrush(color);
                    RouteData.Items[i].Pair.Item2.Opacity = 1.0;
                }
            }
        }
        
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RouteData.Items.AddRange(FillList());
            RouteData.ColorsFill(RouteData.Items);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            RouteData.Refresh();
        }
        
        private void ClickStation(object sender, RoutedEventArgs e)
        {
            var sendData = (b.Button) sender;
            if (!RouteData.CheckBtnArray())
            {
                foreach (var t in RouteData.Items.Where(t => Equals(t.Pair.Item2, sendData)))
                {
                    RouteData.ButtonStack.Add(t.Pair.Item1);
                    t.Pair.Item2.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                if (RouteData.CheckBtnArray())
                {
                    RouteData.OpacityControll();
                    Switch(RouteData.ButtonStack[0], RouteData.ButtonStack[1]);
                }
            }
        }
        private IEnumerable<RouteData> FillList()
        {
            return new List<RouteData>
            {
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_akadem, bRedL_akadem),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_jitomyska, bRedL_jytomirska),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_svyatoshyn, bRedL_svyatoshyn),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_nivki, bRedL_nivki),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_berest, bRedL_berest),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_shuliavska, bRedL_shulyavska),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_polytech, bRedL_polytech),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_vokzalna, bRedL_vokzalna),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_univer, bRedL_univer),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_teatralna, bRedL_teatralna),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_kreshatik, bRedL_kreshatik),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_arsenalna, bRedL_arsenalna),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_dnipro, bRedL_dnipro),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_gidropark, bRedL_gidropark),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_livoberejna, bRedL_livoberezna),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_darnitsa, bRedL_darnitsa),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_chernigovska, bRedL_chernigivska),
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_lisova, bRedL_lisova),
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_syrets, bGreenL_syrets),
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_dorogozhichi, bGreenL_dorogozhichi),
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_lukyanivska, bGreenL_lukyanivska),
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_zolotivorota, bGreenL_zolotivorota),
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_palatssportu, bGreenL_palatssportu),
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_klovska, bGreenL_klovska),
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_pecherska, bGreenL_pecherska),
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_druzbinarodiv, bGreenL_druzbinarodiv),
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_vidubichi, bGreenL_vidubichi),
                },
                new RouteData
                {
                    Color = "G",
                    Pair =
                        Tuple.Create(GreenL_slavutich, bGreenL_slavutich),
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_osokorki, bGreenL_osokorki),
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_poznyaki, bGreenL_poznyaki),
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_harkivksa, bGreenL_harkivska),
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_vurlytsa, bGreenL_vyrlytsa),
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_boryspilska, bGreenL_borispilska),
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_chervhutor, bGreenL_chervhutor),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_teremki, bBlueL_teremki),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_ipodrom, bBlueL_ipodrom),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_mvc, bBlueL_mvc),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_vasylkivska, bBlueL_vasylkivska),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_holosiivska, bBlueL_holosiivska),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_demiivska, bBlueL_demiivska),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_lybidska, bBlueL_lyubidska),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_palatsukraina, bBlueL_palatsukraina),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_olimpiiska, bBlueL_olimpiiska),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_lt, bBlueL_lt),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_maidan, bBlueL_maidan),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_postplosha, bBlueL_postplosha),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_kontrplosha, bBlueL_kontrplosha),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_tshevchenka, bBlueL_tshevchenka),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_petrivka, bBlueL_petrivka),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_obolon, bBlueL_obolon),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_minska, bBlueL_minska),
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_heroevdnipra, bBlueL_heroevdnipra),
                },
            };
        }
    }
}
