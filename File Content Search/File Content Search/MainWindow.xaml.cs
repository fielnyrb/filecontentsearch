using File_Content_Search.Implementations;
using File_Content_Search.Interfaces;
using File_Content_Search.Structures;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace File_Content_Search
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string searchDirectory;

        public MainWindow()
        {
            InitializeComponent();

            searchDirectory = File.ReadAllText(@"DataFiles\directory.txt");

            textBoxDirectory.Text = searchDirectory;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ICharacterEscaper characterEscaper = new SearchStringCharacterEscaper();
            IScriptRunner searcher = new PowerShellSearcher(characterEscaper);

            List<FoundItem> foundItems = searcher.Search(searchString: textBox.Text, directory: searchDirectory);

            listBox.ItemsSource = foundItems;
        }
    }
}