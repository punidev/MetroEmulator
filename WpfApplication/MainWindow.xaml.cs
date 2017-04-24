using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using p = System.Windows.Shapes;
using b = System.Windows.Controls;
namespace WpfApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Tuple<p.Path, b.Button>> RedLineObjects;
        public List<Tuple<p.Path, b.Button>> GreenLineObjects;
        public List<Tuple<p.Path, b.Button>> BlueLineObjects;
        //public List<p.Path> GreenLineObjects;
        //public List<p.Path> BlueLineObjects;
        public MainWindow()
        {
            InitializeComponent();
            string path = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "backg.png");
            Background = new ImageBrush(new BitmapImage(new Uri(path)));
            
        }

        private int GetIdbyPath(p.Path path)
        {
            int res = 0;
            for (int i = 0; i < RedLineObjects.Count; i++)
            {
                if (Equals(RedLineObjects[i].Item1, path))
                    res = i;
            }
            return res;
        }

        private void RouteController(p.Path start, p.Path end)
        {
          
            if (GetIdbyPath(start) < GetIdbyPath(end))
            {
                for (int i = GetIdbyPath(start); i <= GetIdbyPath(end)+1; i++)
                {
                    RouteData.Items[i].Pair.Item1.Stroke = new SolidColorBrush(Colors.Purple);
                }
                for (int i = GetIdbyPath(start); i <= GetIdbyPath(end) + 2; i++)
                {
                    RouteData.Items[i].Pair.Item2.BorderBrush = new SolidColorBrush(Colors.Purple);
                }
            }
            else
            {
                for (int i = GetIdbyPath(start) - 1; i >= GetIdbyPath(end); i--)
                {
                    RouteData.Items[i].Pair.Item1.Stroke = new SolidColorBrush(Colors.Purple);

                }
                for (int i = GetIdbyPath(start); i >= GetIdbyPath(end); i--)
                {
                    RouteData.Items[i].Pair.Item2.BorderBrush = new SolidColorBrush(Colors.Purple);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var i = new List<RouteData>
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
                    Pair = Tuple.Create(RedL_akadem, bRedL_akadem),
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
                    Pair = Tuple.Create(RedL_arsenalna, bRedL_arsenalna),
                    IsTransfer = false
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
                }
            };
            RouteData.Items.AddRange(i);
            RedLineObjects = new List<Tuple<p.Path, b.Button>>
            {
               Tuple.Create(RedL_akadem,       bRedL_akadem),
               Tuple.Create(RedL_jitomyska,    bRedL_jytomirska),
               Tuple.Create(RedL_svyatoshyn,   bRedL_svyatoshyn),
               Tuple.Create(RedL_nivki,        bRedL_nivki),
               Tuple.Create(RedL_berest,       bRedL_berest),
               Tuple.Create(RedL_shuliavska,   bRedL_shulyavska),
               Tuple.Create(RedL_polytech,     bRedL_polytech),
               Tuple.Create(RedL_vokzalna,     bRedL_vokzalna),
               Tuple.Create(RedL_univer,       bRedL_univer),
               Tuple.Create(RedL_teatralna,    bRedL_teatralna),
               Tuple.Create(RedL_kreshatik,    bRedL_kreshatik),
               Tuple.Create(RedL_arsenalna,    bRedL_arsenalna),
               Tuple.Create(RedL_dnipro,       bRedL_dnipro),
               Tuple.Create(RedL_gidropark,    bRedL_gidropark),
               Tuple.Create(RedL_livoberejna,  bRedL_livoberezna),
               Tuple.Create(RedL_darnitsa,     bRedL_darnitsa),
               Tuple.Create(RedL_chernigovska, bRedL_chernigivska),
               Tuple.Create(RedL_lisova,       bRedL_lisova)
            };
            GreenLineObjects = new List<Tuple<p.Path, b.Button>>
            {
                Tuple.Create(GreenL_syrets,        bGreenL_syrets),       
                Tuple.Create(GreenL_dorogozhichi,  bGreenL_dorogozhichi), 
                Tuple.Create(GreenL_lukyanivska,   bGreenL_lukyanivska),
                Tuple.Create(GreenL_zolotivorota,  bGreenL_zolotivorota),
                Tuple.Create(GreenL_palatssportu,  bGreenL_palatssportu),
                Tuple.Create(GreenL_klovska,       bGreenL_klovska),
                Tuple.Create(GreenL_pecherska,     bGreenL_pecherska),
                Tuple.Create(GreenL_druzbinarodiv, bGreenL_druzbinarodiv),
                Tuple.Create(GreenL_vidubichi,     bGreenL_vidubichi),
                Tuple.Create(GreenL_slavutich,     bGreenL_slavutich),
                Tuple.Create(GreenL_osokorki,      bGreenL_osokorki),
                Tuple.Create(GreenL_poznyaki,      bGreenL_poznyaki),
                Tuple.Create(GreenL_harkivksa,     bGreenL_harkivska),
                Tuple.Create(GreenL_vurlytsa,      bGreenL_vyrlytsa),
                Tuple.Create(GreenL_boryspilska,   bGreenL_borispilska),
                Tuple.Create(GreenL_chervhutor,    bGreenL_chervhutor)
            };
            BlueLineObjects = new List<Tuple<p.Path, b.Button>>
            {
                Tuple.Create(BlueL_heroevdnipra,   bBlueL_heroevdnipra),
                Tuple.Create(BlueL_minska,         bBlueL_minska),
                Tuple.Create(BlueL_obolon,         bBlueL_obolon),
                Tuple.Create(BlueL_petrivka,       bBlueL_petrivka),
                Tuple.Create(BlueL_tshevchenka,    bBlueL_tshevchenka),
                Tuple.Create(BlueL_kontrplosha,    bBlueL_kontrplosha),
                Tuple.Create(BlueL_postplosha,     bBlueL_postplosha),
                Tuple.Create(BlueL_maidan,         bBlueL_maidan),
                Tuple.Create(BlueL_lt,             bBlueL_lt),
                Tuple.Create(BlueL_olimpiiska,     bBlueL_olimpiiska),
                Tuple.Create(BlueL_palatsukraina,  bBlueL_palatsukraina),
                Tuple.Create(BlueL_lybidska,       bBlueL_lyubidska),
                Tuple.Create(BlueL_demiivska,      bBlueL_demiivska),
                Tuple.Create(BlueL_holosiivska,    bBlueL_holosiivska),
                Tuple.Create(BlueL_vasylkivska,    bBlueL_vasylkivska),
                Tuple.Create(BlueL_mvc,            bBlueL_mvc),
                Tuple.Create(BlueL_ipodrom,        bBlueL_ipodrom),
                Tuple.Create(BlueL_teremki,        bBlueL_teremki)
            };
            GreenLineObjects.ForEach(t => t.Item2.BorderBrush = new SolidColorBrush(Colors.Green));
            RedLineObjects.ForEach(t => t.Item2.BorderBrush = new SolidColorBrush(Colors.Red));
            BlueLineObjects.ForEach(t => t.Item2.BorderBrush = new SolidColorBrush(Colors.Blue));
            RouteController(RedL_polytech, RedL_chernigovska);
            //RedLineObjects[GetIdbyPath(RedL_teatralna)].Stroke = new SolidColorBrush(Colors.Green);
            //(t => t.Stroke = new SolidColorBrush(Colors.Green));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Title = "123";
        }
    }
}
