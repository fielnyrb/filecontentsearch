using File_Content_Search.Entities;
using File_Content_Search.Implementations;
using File_Content_Search.Interfaces;
using File_Content_Search.ItemLibrary;
using File_Content_Search.ItemLibrary.Interfaces;
using File_Content_Search.Structures;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace File_Content_Search
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IStoredLibraries libraryDataSource;
        ITextMinimizer minimizer;
        string portNumber;

        public MainWindow()
        {
            libraryDataSource = new StoredLibraries();

            InitializeComponent();
            PopulateLibraryList();

            minimizer = new TextMinimizer();

            portNumber = new PortNumberSetting().GetPortNumber();
        }

        private void PopulateLibraryList()
        {
            listBoxLibraries.ItemsSource = libraryDataSource.GetLibrariesInformation();
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            IContentSearcher searcher = new SQLiteSearcher();

            List<FoundItem> foundItems = searcher.Search(searchString: minimizer.minimize(textBox.Text), directory: "");

            listBox.ItemsSource = foundItems;
        }

        private void button_Import_LibraryREST_Click(object sender, RoutedEventArgs e)
        {
            Import import = new Import();

            import.DataChanged += Import_DataChanged;

            import.Show();
        }

        private void Import_DataChanged(object sender, System.EventArgs e)
        {
            listBoxLibraries.ItemsSource = null;
            PopulateLibraryList();
        }

        private void buttonDeleteSelectedLibrary_Click(object sender, RoutedEventArgs e)
        {
            LibraryInformation libraryInformation;

            if (listBoxLibraries.SelectedIndex != -1)
            {
                libraryInformation = (LibraryInformation)listBoxLibraries.SelectedItem;
                ILibraryDeleter libraryDeleter = new LibraryDeleter(new MyContext());
                libraryDeleter.DeleteLibrary(libraryInformation.LibraryInformationId);
            }

            PopulateLibraryList();
        }

        private void buttonFindDuplicates_Click(object sender, RoutedEventArgs e)
        {
            CompareDuplicates compareDuplicates = new CompareDuplicates();
            compareDuplicates.Show();
            compareDuplicates.InitializeSearchForDuplicates();
        }

        private void buttonSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void button_Export_LibraryREST_Click(object sender, RoutedEventArgs e)
        {
            OpenLPExporter openLPExporter = new OpenLPExporter();
            openLPExporter.ExportLibrary(new MyContext());
        }
    }
}