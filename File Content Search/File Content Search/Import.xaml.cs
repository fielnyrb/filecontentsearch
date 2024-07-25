using File_Content_Search.Implementations;
using File_Content_Search.Interfaces;
using File_Content_Search.ItemLibrary;
using File_Content_Search.Structures;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Import.xaml
    /// </summary>
    public partial class Import : Window
    {
        string portNumber;
        ITextMinimizer minimizer;
        ObservableCollection<SelectableLibrary> Items { get; set; }

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler DataChanged;

        public Import()
        {
            portNumber = new PortNumberSetting().GetPortNumber();
            minimizer = new TextMinimizer();
            Items = new ObservableCollection<SelectableLibrary>();

            InitializeComponent();

            Loaded += OnLoaded;

        }
        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await PopulateLibraryListAsync();
        }

        private async Task PopulateLibraryListAsync()
        {
            ItemsListBox.ItemsSource = Items;

            ProPresenterAPI proPresenterAPI = new ProPresenterAPI(portNumber);
            JArray libraries = await proPresenterAPI.GetLibrariesAsync();

            foreach (JObject library in libraries)
            {
                SelectableLibrary temp = new SelectableLibrary();
                temp.Uuid = library["uuid"].ToString();
                temp.Name = library["name"].ToString();

                Items.Add(temp);
            }
        }

        private async void ImportSelected_Click(object sender, RoutedEventArgs e)
        {
            AddItemButton.IsEnabled = false;
            ItemsListBox.IsEnabled = false;

            foreach (SelectableLibrary item in Items)
            {
                if (item.IsChecked == true)
                {
                    LibraryImporterREST libraryImporterREST = new LibraryImporterREST(portNumber, minimizer);
                    await libraryImporterREST.ImportLibrary(item);
                }
            }


            AddItemButton.IsEnabled = true;
            ItemsListBox.IsEnabled = true;

            MessageBox.Show("Import Complete");

            DataChangedEventHandler handler = DataChanged;

            if (handler != null)
            {
                handler(this, new EventArgs());
            }


        }
    }
}
