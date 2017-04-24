using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Linq;
using p = System.Windows.Shapes;
using b = System.Windows.Controls;
namespace WpfApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string path = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "backg.png");
            Background = new ImageBrush(new BitmapImage(new Uri(path)));
            
        }

        private int GetIdbyPath(p.Path path)
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
                 isStartGreen = false, 
                 isStartBlue = false, 
                 isEndGreen=false, 
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
            }
        }

        private void Test()
        {
            //Switch(RedL_shuliavska, GreenL_dorogozhichi);
            //Switch(GreenL_dorogozhichi, RedL_shuliavska);
            //Switch(RedL_shuliavska, BlueL_petrivka);
            //Switch(BlueL_petrivka, RedL_shuliavska);
            //Switch(GreenL_poznyaki, BlueL_teremki);
            //Switch(BlueL_teremki,GreenL_poznyaki);
        }

        private void RouteController(p.Path start, p.Path end)
        {
            if (GetIdbyPath(start) < GetIdbyPath(end))
            {
                for (var i = GetIdbyPath(start); i <= GetIdbyPath(end) - 1; i++)
                    RouteData.Items[i].Pair.Item1.Stroke = new SolidColorBrush(Colors.Purple);
                for (var i = GetIdbyPath(start); i <= GetIdbyPath(end); i++)
                    RouteData.Items[i].Pair.Item2.BorderBrush = new SolidColorBrush(Colors.Purple);
            }
            else
            {
                for (var i = GetIdbyPath(start) - 1; i >= GetIdbyPath(end); i--)
                    RouteData.Items[i].Pair.Item1.Stroke = new SolidColorBrush(Colors.Purple);
                for (var i = GetIdbyPath(start); i >= GetIdbyPath(end); i--)
                    RouteData.Items[i].Pair.Item2.BorderBrush = new SolidColorBrush(Colors.Purple);
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
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_jitomyska, bRedL_jytomirska),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_svyatoshyn,   bRedL_svyatoshyn),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_nivki, bRedL_nivki),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_berest, bRedL_berest),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_shuliavska, bRedL_shulyavska),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_polytech, bRedL_polytech),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_vokzalna, bRedL_vokzalna),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_univer, bRedL_univer),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_teatralna, bRedL_teatralna),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_kreshatik, bRedL_kreshatik),
                    IsTransfer = true
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_arsenalna, bRedL_arsenalna),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_dnipro, bRedL_dnipro),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_gidropark, bRedL_gidropark),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_livoberejna, bRedL_livoberezna),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_darnitsa, bRedL_darnitsa),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_chernigovska, bRedL_chernigivska),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "R",
                    Pair = Tuple.Create(RedL_lisova, bRedL_lisova),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_syrets, bGreenL_syrets),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_dorogozhichi, bGreenL_dorogozhichi),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_lukyanivska, bGreenL_lukyanivska),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_zolotivorota, bGreenL_zolotivorota),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_palatssportu, bGreenL_palatssportu),
                    IsTransfer = true
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_klovska, bGreenL_klovska),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_pecherska, bGreenL_pecherska),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_druzbinarodiv, bGreenL_druzbinarodiv),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_vidubichi, bGreenL_vidubichi),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "G",
                    Pair =
                        Tuple.Create(GreenL_slavutich, bGreenL_slavutich),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_osokorki, bGreenL_osokorki),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_poznyaki, bGreenL_poznyaki),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_harkivksa, bGreenL_harkivska),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_vurlytsa, bGreenL_vyrlytsa),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_boryspilska, bGreenL_borispilska),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "G",
                    Pair = Tuple.Create(GreenL_chervhutor, bGreenL_chervhutor),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_teremki, bBlueL_teremki),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_ipodrom, bBlueL_ipodrom),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_mvc, bBlueL_mvc),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_vasylkivska, bBlueL_vasylkivska),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_holosiivska, bBlueL_holosiivska),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_demiivska, bBlueL_demiivska),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_lybidska, bBlueL_lyubidska),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_palatsukraina, bBlueL_palatsukraina),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_olimpiiska, bBlueL_olimpiiska),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_lt, bBlueL_lt),
                    IsTransfer = true
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_maidan, bBlueL_maidan),
                    IsTransfer = true
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_postplosha, bBlueL_postplosha),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_kontrplosha, bBlueL_kontrplosha),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_tshevchenka, bBlueL_tshevchenka),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_petrivka, bBlueL_petrivka),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_obolon, bBlueL_obolon),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_minska, bBlueL_minska),
                    IsTransfer = false
                },
                new RouteData
                {
                    Color = "B",
                    Pair = Tuple.Create(BlueL_heroevdnipra, bBlueL_heroevdnipra),
                    IsTransfer = false
                },
            };
        }

        private void ColorsFill(List<RouteData> lst)
        {
            foreach (var t in lst)
            {
                switch (t.Color)
                {
                    case "R":
                        t.Pair.Item2.BorderBrush = new SolidColorBrush(Colors.Red);
                        t.Pair.Item1.Stroke = new SolidColorBrush(Colors.Red);
                        break;
                    case "G":
                        t.Pair.Item2.BorderBrush = new SolidColorBrush(Colors.Green);
                        t.Pair.Item1.Stroke = new SolidColorBrush(Colors.Green);
                        break;
                    case "B":
                        t.Pair.Item2.BorderBrush = new SolidColorBrush(Colors.Blue);
                        t.Pair.Item1.Stroke = new SolidColorBrush(Colors.Blue);
   
                        break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RouteData.Items.AddRange(FillList());
            ColorsFill(RouteData.Items);
            //Test();

        }

        private bool isFirstSelected = false, isSecondSelected = false, isReseted = false;
        private List<p.Path> _buttonStack = new List<p.Path>(2);

        private void button_Click(object sender, RoutedEventArgs e)
        {
            isReseted = true;
            _buttonStack.Clear();
            ColorsFill(RouteData.Items);
        }
        

        private bool CheckBtnArray()
        {
            return _buttonStack.Count==2;
        }

        private void ClickStation(object sender, RoutedEventArgs e)
        {
            var a = (b.Button) sender;
            if (!CheckBtnArray())
            {
                foreach (var t in RouteData.Items.Where(t => Equals(t.Pair.Item2, a)))
                {
                    _buttonStack.Add(t.Pair.Item1);
                    t.Pair.Item2.BorderBrush = new SolidColorBrush(Colors.Blue); ;
                }
                if (CheckBtnArray())
                {
                    Switch(_buttonStack[0], _buttonStack[1]);
                }
            }
        }
    }
}
