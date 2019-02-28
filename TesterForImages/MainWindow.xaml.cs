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
using Newtonsoft.Json;

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

    static public class grid // grid properties
    {
        static public string[] gridTileNames;

        static public int GridRows = 5;
        static public int GridCols = 5;
    };

    public class saveToJSON // what it needs to save to a file
    {
        /*   Loading it into a JSON file   */

        // file name
        static public string fileName = null;

        // grid size
        public int xSize;
        public int ySize;

        // contents of the tile
        public string[] imagePaths;
    };


    public partial class MainWindow : Window
    {
        void saveToJSONFile()
        {
            saveToJSON mapSave = new saveToJSON();
            mapSave.xSize = grid.GridCols; // sets x to be the amount of grid collumns
            mapSave.ySize = grid.GridRows; // sets y to be the amount of grid rows       
            mapSave.imagePaths = grid.gridTileNames;

            string json = JsonConvert.SerializeObject(mapSave, Formatting.Indented);

            System.IO.File.WriteAllText("C: /Users/s189065/source/repos/IntroCSharp/TesterForImages/bin/Debug/JSON/" + saveToJSON.fileName + ".txt", json);
        }

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

        void gridCreator() // generates the grid column, rows, and does the initial labeling of the grid
        {
            grid.gridTileNames = new string[grid.GridRows * grid.GridCols]; // sets the grid tile path array to be null

            for(int i = 0; i < grid.GridRows * grid.GridCols; i++)
            {
                grid.gridTileNames[i] = null;
            }

            for (int i = 0; i < grid.GridRows; i++)
            {
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = new GridLength(40);

                TileMap.RowDefinitions.Add(gridRow);
                TileMap.SetValue(Grid.RowProperty, i);
            }

            for (int j = 0; j < grid.GridCols; j++)
            {
                ColumnDefinition gridCol = new ColumnDefinition();
                gridCol.Width = new GridLength(40);

                TileMap.ColumnDefinitions.Add(gridCol);
                TileMap.SetValue(Grid.ColumnProperty, j);
            }

            int x = 0; // used for the labels to be used for cords on grid
            int y = 0; //

            for ( int i = 0; i < grid.GridRows * grid.GridRows; i++) // used to help label all of the tiles
            {
                if(y == grid.GridCols) // checks to see if the cols are at max
                {
                    ++x; // adds onto x
                    y = 0; // resets y
                }

                Label finalLabel = new Label();
                finalLabel.Content = x.ToString() + "," + y.ToString();
                Grid.SetRow(finalLabel, x);
                Grid.SetColumn(finalLabel, y);
                TileMap.Children.Add(finalLabel);

                ++y; // adds onto the col number
            }
        }

        private void TileMap_MouseUp(object sender, MouseButtonEventArgs e) // used to copy the contents to the tilemap
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

            int indexNumber = ((int)(row * grid.GridCols) + col);

            grid.gridTileNames[indexNumber] = copy.ToString();

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
                grid.GridCols = int.Parse(fullText);
                TileMap.Children.Clear();
                TileMap.ColumnDefinitions.Clear();
                gridCreator();
            }
        }

        private void yButton_PreviewTextInput(object sender, TextCompositionEventArgs e) // changes the TextBox for the Y cords for the graph
        {
            var text = sender as TextBox;

            var fullText = yButton.Text.Insert(yButton.SelectionStart, e.Text);

            double val;

            e.Handled = !double.TryParse(fullText, out val);

            if (e.Handled != true)
            {
                grid.GridRows = int.Parse(fullText);
                TileMap.Children.Clear();
                TileMap.RowDefinitions.Clear();
                gridCreator();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) // saves the file when the save button is clicked
        {
            if (saveToJSON.fileName != null)
            {
                saveToJSONFile();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            saveToJSON.fileName = SaveName.Text;
        }

        /*--------------------------------------------------------------------------------------------------*/
        /*--------------------------------------------------------------------------------------------------*/
        /*--------------------------------------------------------------------------------------------------*/

        public MainWindow() // main window of the game
        {
            InitializeComponent();

            imageSourceLoader();

            //imageFileNameSaver();

            gridCreator();
        }
    }
}
