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
using Microsoft.Win32;

using FolderDialog = System.Windows.Forms.FolderBrowserDialog;
using DResult = System.Windows.Forms.DialogResult;

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

    public class grid // grid properties
    {
        static public string filePathLoadedFrom;
        static public bool fromLoaded = false;

        static public string[] gridTileNames;

        public string[] holderForGridTileNames;

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

    /*--------------------------------------------------------------------------------------------------*/
    /*--------------------------------------------------------------------------------------------------*/
    /*--------------------------------------------------------------------------------------------------*/

    public partial class MainWindow : Window
    {
        /*--------------------------------------------------------------------------------------------------*/
        /*------------------------------------ Map Loader and Saver ----------------------------------------*/
        /*--------------------------------------------------------------------------------------------------*/

        void saveToJSONFile(bool filePath = false) // serializes into a json file and then stores it into a text file
        {
            saveToJSON mapSave = new saveToJSON();
            mapSave.xSize = grid.GridCols; // sets x to be the amount of grid collumns
            mapSave.ySize = grid.GridRows; // sets y to be the amount of grid rows       
            mapSave.imagePaths = grid.gridTileNames;

            string json = JsonConvert.SerializeObject(mapSave, Formatting.Indented);

            if(filePath == true)
            {
                saveToJSON.fileName = grid.filePathLoadedFrom;
            }

            System.IO.File.WriteAllText("JSON/" + saveToJSON.fileName + ".txt", json);
        }

        void loadMap() // can load a saved json file back into the map
        {
            string filePathContents = null;
            OpenFileDialog opf = new OpenFileDialog();
            if(opf.ShowDialog() == true)
            {
                filePathContents = File.ReadAllText(opf.FileName);
            }

            if(filePathContents == null)
            {
                return;
            }

            grid.fromLoaded = true;
            grid.filePathLoadedFrom = filePathContents;

            saveToJSON json = JsonConvert.DeserializeObject<saveToJSON>(filePathContents);

            grid.GridCols = json.xSize;
            grid.GridRows = json.ySize;
            grid.gridTileNames = json.imagePaths;

            TileMap.Children.Clear();
            TileMap.RowDefinitions.Clear();
            TileMap.ColumnDefinitions.Clear();
            gridCreator(true);

            for (int i = 0; i < grid.GridCols * grid.GridRows; i++) // assigns the tiles to be in the map
            {
                if (grid.gridTileNames[i] != null)
                {
                    BitmapImage copy = new BitmapImage();
                    copy.BeginInit();
                    copy.UriSource = new Uri(grid.gridTileNames[i]);
                    copy.EndInit();
                    ImageBrush b = new ImageBrush(copy);

                    (TileMap.Children[i] as Label).Background = b;
                }
            }
        }

        /*--------------------------------------------------------------------------------------------------*/
        /*--------------------------------------------------------------------------------------------------*/
        /*--------------------------------------------------------------------------------------------------*/

        void imageFileNameSaver(string filePath = "/tiles") // adds files into a file to be loaded back into wpf as a image source
        { 
            using (StreamWriter a = File.AppendText("ImageSources.txt"))
            {
                string[] b = Directory.GetFiles(filePath);
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
                    myBitMapImage[i].UriSource = new Uri(System.IO.Path.GetFullPath(textReader[i]));
                    myBitMapImage[i].EndInit();

                    Tiles.Items.Add(myBitMapImage[i]);
                } 
            }
        }

        /*--------------------------------------------------------------------------------------------------*/
        /*------------------------------------Grid Actions--------------------------------------------------*/
        /*--------------------------------------------------------------------------------------------------*/

        void gridCreator(bool skipInit = false) // generates the grid column, rows, and does the initial labeling of the grid
        {
            if (skipInit != true)
            {
                grid.gridTileNames = new string[grid.GridCols * grid.GridRows];
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

            for(int i = 0; i < grid.GridRows; i++)
            {
                for (int j = 0; j < grid.GridCols; j++)
                {
                    Label finalLabel = new Label();
                    finalLabel.Content = j.ToString() + "," + i.ToString();
                    Grid.SetColumn(finalLabel, j);
                    Grid.SetRow(finalLabel, i);
                    TileMap.Children.Add(finalLabel);
                }
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

        /*--------------------------------------------------------------------------------------------------*/
        /*----------------------------------------Object Actions--------------------------------------------*/
        /*--------------------------------------------------------------------------------------------------*/

        private void Button_Click(object sender, RoutedEventArgs e) // saves the file when the save button is clicked
        {
            if (saveToJSON.fileName != null)
            {
                saveToJSONFile();
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e) // saves the item to the same filepath it was loaded from
        {
            

            if (grid.fromLoaded == true)
            {
                saveToJSONFile(true);
            }
            else
            {
                Console.WriteLine("Failed to write to filepath because it does not exist");
                return;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) // checks if the save name box has changed so it can store the name
        {
            saveToJSON.fileName = SaveName.Text;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) // checks if the open option button has been clicked
        {
            loadMap();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) // mouse click for import images to allow for the import of one image at a time
        {
            string filePath = null;
            OpenFileDialog opf = new OpenFileDialog(); // opens a windows explorer

            // add a filter to the OpenFileDialog to only show images
            opf.DefaultExt = ".png";
            opf.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"; // got help from stackoverflow

            if (opf.ShowDialog() == true)
            {
                filePath = opf.FileName;
                //filePathContents = Directory.GetFiles(filePath); // get the contents

                string fileToCopy = filePath;
                string fileToCopyName = System.IO.Path.GetFileName(fileToCopy);

                string destinationDirectory = @"./tiles/" + fileToCopyName;

                if(destinationDirectory == fileToCopy)
                {
                    return;
                }

                File.Copy(fileToCopy, destinationDirectory);

                using (StreamWriter a = File.AppendText("ImageSources.txt"))
                {
                    a.WriteLine(destinationDirectory);
                    a.Close();
                }
                imageSourceLoader();
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e) // imorting a folder and will take all of the images from it and load them into the app
        {
            string[] fileNameDirectories;
            string fileDirectory = null;
            FolderDialog fbd = new FolderDialog();

            DResult result = fbd.ShowDialog();
            if ( result == DResult.OK) // got help figuring out how to check from c-sharpcorner
            {
                fileDirectory = fbd.SelectedPath;
            }
            fileNameDirectories = System.IO.Directory.GetFiles(fileDirectory);

            using (StreamWriter a = File.AppendText("ImageSources.txt"))
            {
                for (int i = 0; i < fileNameDirectories.Length; i++)
                {
                    string fileToCopyName = System.IO.Path.GetFileName(fileNameDirectories[i]);
                    string newFileDirectory = @"./tiles/" + fileToCopyName;

                    File.Copy(fileNameDirectories[i], newFileDirectory);

                    if (fileNameDirectories[i] != null)
                    {
                        a.WriteLine(newFileDirectory);
                    }
                }
                a.Close();
            }
            imageSourceLoader();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) // reset button to allow for creation of new grid without reseting the program
        {
            TileMap.Children.Clear();
            TileMap.RowDefinitions.Clear();
            TileMap.ColumnDefinitions.Clear();
            gridCreator();
        }

        /*--------------------------------------------------------------------------------------------------*/
        /*------------------------------------Main Window---------------------------------------------------*/
        /*--------------------------------------------------------------------------------------------------*/

        public MainWindow() // main window of the game
        {
            InitializeComponent();

            imageSourceLoader();

            gridCreator();
        }
    }
}