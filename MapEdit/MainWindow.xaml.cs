using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapEdit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBoxItem_Drop(object sender, DragEventArgs e)
        {

        }

        private void ListBoxItem_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void Tiles_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //(Tile_Options.SelectedItem as ListBoxItem).Content;
            SolidColorBrush b = (SolidColorBrush)(Tile_Options.SelectedItem as ListBoxItem).Background;
            /*var ee = (UIElement)e.Source;
            int c = Grid.GetColumn(ee);
            int r = Grid.GetRow(ee);*/

            var label = e.Source as UIElement;
            var row = Grid.GetRow(label);
            var col = Grid.GetColumn(label);


        }
    }
}
