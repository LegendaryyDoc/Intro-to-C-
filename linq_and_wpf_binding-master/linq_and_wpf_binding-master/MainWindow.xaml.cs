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

namespace myWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    class hsEntry
    {
        private string name;
        private int rank;
        private int score;

        public string Name { get => name; set => name = value; }
        public int Rank { get => rank; set => rank = value; }
        public int Score { get => score; set => score = value; }

        public hsEntry(string v1, int v2, int v3)
        {
            Name = v1;
            Rank = v2;
            Score = v3;
        }
    }

    public partial class MainWindow : Window
    {
        List<hsEntry> hsTable = new List<hsEntry>();
        public MainWindow()
        {
            InitializeComponent();
           
            //populate the hstable, another ugly text file parse.
            LoadHSTable();

            //IEnumerable<object> top10 = from hsRow in hsTable where hsRow.Rank < 11 select hsRow;//Query Syntax
            IEnumerable<object> top10 = hsTable.Where(x => x.Rank < 11).OrderBy(x => x.Rank);//Method Syntax

            hsList1.DataContext = top10;
        }

        //Another ugly textfile parser.
        void LoadHSTable()
        {
            string[] tmpVals;
            string[] lines = System.IO.File.ReadAllLines(@"hscores.txt");
            foreach (string s in lines)
            {
                tmpVals = s.Split(',');
                hsTable.Add(new hsEntry(
                    tmpVals[0],
                    int.Parse(tmpVals[1]),
                    int.Parse(tmpVals[2])));
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            IEnumerable<object> player = from s in hsTable
                                         where s.Name == SearchBar.Text
                                         select s;

            hsList1.DataContext = player;
        }
    }
}
