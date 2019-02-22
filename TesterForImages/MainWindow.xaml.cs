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
            Setter tileHolder = new Setter();
            Border LBIBorder = new Border();
            ImageBrush backGroundColor = new ImageBrush();

            for (int i = 0; i < textReader.Length; i++)
            {
                if (textReader[i] != null)
                {
                    myBitMapImage[i] = new BitmapImage();
                    myBitMapImage[i].BeginInit();
                    myBitMapImage[i].UriSource = new Uri(textReader[i]);
                    myBitMapImage[i].EndInit();

                    ImageHolder.ImageSource = myBitMapImage[i];
                } // need to make it so will auto make all the files added
            }
        }

        void gridCreator() // need to add labels to each tile in grid
        {
            int GridRows = 10;
            int GridCols = 10;
            for (int i = 0; i < GridRows; i++)
            {
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = new GridLength(TileMap.Height / GridRows);

                TileMap.RowDefinitions.Add(gridRow);
            }

            for (int i = 0; i < GridCols; i++)
            {
                ColumnDefinition gridCol = new ColumnDefinition();
                gridCol.Width = new GridLength(TileMap.Width / GridCols);

                TileMap.ColumnDefinitions.Add(gridCol);
            }

            for(int i = 0; i < GridRows; i++)
            {
                Label nameLabel = new Label();
                for(int j = 0; j < GridCols; j++)
                {
                    nameLabel.Content = i.ToString() + "," + j.ToString(); 
                }
                
            }
        }
        public MainWindow()
        {
            InitializeComponent();

            imageSourceLoader();

            //imageFileNameSaver();

            gridCreator();
        }
    }
}
