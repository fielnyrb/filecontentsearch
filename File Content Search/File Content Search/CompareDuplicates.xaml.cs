using File_Content_Search.Classes;
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
            List<DuplicateGroupItem> items = comparer.SearchForDuplicates();

            //Bind data to ListBox
            MyListView.ItemsSource = items;
        }

        private void MyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as ListView;
            DuplicateGroupItem item = listView.SelectedItem as DuplicateGroupItem;

            ComboBox1.ItemsSource = item.DuplicateItems;
            ComboBox1.DisplayMemberPath = "LibraryName";
            ComboBox1.SelectedValuePath = "ItemOriginalContent";

            ComboBox2.ItemsSource = item.DuplicateItems;
            ComboBox2.DisplayMemberPath = "LibraryName";
            ComboBox2.SelectedValuePath = "ItemOriginalContent";
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            DuplicateItem item = comboBox.SelectedItem as DuplicateItem;

            TextBox1.Text = item.ItemOriginalContent;
        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            DuplicateItem item = comboBox.SelectedItem as DuplicateItem;

            TextBox2.Text = item.ItemOriginalContent;
        }
    }
}
