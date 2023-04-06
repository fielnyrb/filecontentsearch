using File_Content_Search.Entities;
using File_Content_Search.Implementations;
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
using System.Windows.Shapes;

namespace File_Content_Search
{
    /// <summary>
    /// Interaction logic for CompareDuplicates.xaml
    /// </summary>
    public partial class CompareDuplicates : Window
    {
        public CompareDuplicates()
        {
            InitializeComponent();
        }

        public void InitializeSearchForDuplicates()
        {
            //Create instance of SongComparer and call SearchForDuplicates
            SongComparer comparer = new SongComparer();
            List<NameItem> items = comparer.SearchForDuplicates();

            //Bind data to ListBox
            MyListView.ItemsSource = items;
        }
    }
}
