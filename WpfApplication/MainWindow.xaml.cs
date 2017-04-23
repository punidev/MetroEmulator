using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace WpfApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Tuple<System.Windows.Shapes.Path, System.Windows.Controls.Button>> RedLineObjects;
        public MainWindow()
        {
            InitializeComponent();
            string path = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "backg.png");
            Background = new ImageBrush(new BitmapImage(new Uri(path)));
        }

        private int GetIdbyPath(System.Windows.Shapes.Path path)
        {
            int res = 0;
            for (int i = 0; i < RedLineObjects.Count; i++)
            {
                if (Equals(RedLineObjects[i].Item1, path))
                    res = i;
            }
            return res;
        }

        private void RouteController(System.Windows.Shapes.Path start, System.Windows.Shapes.Path end)
        {
            if (GetIdbyPath(start) < GetIdbyPath(end))
            {
                for (int i = GetIdbyPath(start); i < GetIdbyPath(end); i++)
                {
                    RedLineObjects[i].Item1.Stroke = new SolidColorBrush(Colors.Purple);
                    //RedLineObjects[i].Item2.BorderBrush = new SolidColorBrush(Colors.Purple);
                }
            }// ТЗ в пункты 
            // вытянуть
            // аналитика по коду - поверхностная 
            // тз по командам 
            else
            {
                for (int i = GetIdbyPath(start)-1; i >= GetIdbyPath(end); i--)
                {
                    RedLineObjects[i].Item1.Stroke = new SolidColorBrush(Colors.Purple);
                    //RedLineObjects[i].Item2.Foreground = new SolidColorBrush(Colors.Purple);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RedLineObjects = new List<Tuple<System.Windows.Shapes.Path,
                                            System.Windows.Controls.Button>>
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
            RouteController(RedL_lisova, RedL_polytech);
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
