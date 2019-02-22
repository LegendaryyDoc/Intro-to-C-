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
using System.IO;

/*-------------------------------------------------------------------*/
/*------------------------- TO DO -----------------------------------*/
/*-------------------------------------------------------------------*/

// create a class for tile details
// check to see if file is added or not
// if file is added proceed to loading all of the images
// if not add file to a json text file
// then load the needed images

// all image sources sould be saved and loaded from a json file library 

// later down the time can add so certain blocks have certain attributes
// water tile
// bouncy tile
// wall tile
// hill block
// corner block
// etc

/*-------------------------------------------------------------------*/
/*-------------------------------------------------------------------*/
/*-------------------------------------------------------------------*/

namespace TesterForImages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    static public class grid
    {
        static public int GridRows = 5;
        static public int GridCols = 5;
    };

    public partial class MainWindow : Window
    {
        void imageFileNameSaver() // adds files into a file to be loaded back into wpf as a image source
        { 
            using (StreamWriter a = File.AppendText("ImageSources.txt"))
            {
                string[] b = Directory.GetFiles("C:/Users/s189065/source/repos/IntroCSharp/TesterForImages/bin/Debug/tiles");
                foreach(string dir in b)
                {
                    a.WriteLine(dir);
                }
                a.Close();
            } 
        }

        void imageSourceLoader() // loads stuff from file as a image source
        {
            string[] textReader = System.IO.File.ReadAllLines("ImageSources.txt");
            BitmapImage[] myBitMapImage = new BitmapImage[textReader.Length];

            for (int i = 0; i < textReader.Length; i++)
            {
                if (textReader[i] != null)
                {
                    myBitMapImage[i] = new BitmapImage();
                    myBitMapImage[i].BeginInit();
                    myBitMapImage[i].UriSource = new Uri(textReader[i]);
                    myBitMapImage[i].EndInit();

                    Tiles.Items.Add(myBitMapImage[i]);
                } 
            }
        }

        void gridCreator() // need to add labels to each tile in grid
        {

            for (int i = 0; i < grid.GridRows; i++)
            {
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = new GridLength(40);

                TileMap.RowDefinitions.Add(gridRow);
                TileMap.SetValue(Grid.RowProperty, i);
            }

            for (int i = 0; i < grid.GridCols; i++)
            {
                ColumnDefinition gridCol = new ColumnDefinition();
                gridCol.Width = new GridLength(40);

                TileMap.ColumnDefinitions.Add(gridCol);
                TileMap.SetValue(Grid.ColumnProperty, i);
            }

           for(int i = 0; i < grid.GridRows; i++)
            {
                for(int j = 0; j < grid.GridCols; j++)
                {
                    Label gridLabel = new Label();
                    gridLabel.Content = j.ToString() + "," + i.ToString();

                    Grid.SetRow(gridLabel, i);  
                    Grid.SetColumn(gridLabel, j);
                    TileMap.Children.Add(gridLabel);
                }
            }
        }

        private void TileMap_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Tiles.SelectedItem == null)
            {
                return;
            }

            BitmapImage copy = (BitmapImage)(Tiles.SelectedItem);
            ImageBrush b = new ImageBrush(copy);

            var label = e.Source as Label;

            if (label == null)
            {
                return;
            }

            var row = Grid.GetRow(label);
            var col = Grid.GetColumn(label);

            int indexNumber = ((int)(row * grid.GridRows) + col);

            (TileMap.Children[indexNumber] as Label).Background = b;
        }
                                                                                         // used to only allow numbers to be put into the box
        private void xButton_PreviewTextInput(object sender, TextCompositionEventArgs e) // used stack overflow to help
        {
            var text = sender as TextBox;

            var fullText = xButton.Text.Insert(xButton.SelectionStart, e.Text);

            int val;

            e.Handled = !int.TryParse(fullText, out val);

            if(e.Handled != true)
            {
                grid.GridRows = int.Parse(fullText);
                TileMap.Children.Clear();
                TileMap.RowDefinitions.Clear();
                gridCreator();
            }
        }

        private void yButton_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var text = sender as TextBox;

            var fullText = yButton.Text.Insert(yButton.SelectionStart, e.Text);

            double val;

            e.Handled = !double.TryParse(fullText, out val);

            if (e.Handled != true)
            {
                grid.GridCols = int.Parse(fullText);
                TileMap.Children.Clear();
                TileMap.ColumnDefinitions.Clear();
                gridCreator();
            }
        }

        /*--------------------------------------------------------------------------------------------------*/
        /*--------------------------------------------------------------------------------------------------*/
        /*--------------------------------------------------------------------------------------------------*/

        public MainWindow()
        {
            InitializeComponent();

            imageSourceLoader();

            //imageFileNameSaver();

            gridCreator();
        }
    }
}
