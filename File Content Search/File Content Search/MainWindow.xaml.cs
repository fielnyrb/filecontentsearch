using File_Content_Search.Entities;
using File_Content_Search.Implementations;
using File_Content_Search.Interfaces;
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
        ILibraryDataSource libraryDataSource;
        ITextMinimizer minimizer;
        string portNumber;

        public MainWindow()
        {
            libraryDataSource = new LibrarySource();

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

        private void button_Import_Library_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            string fileName = "";

            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;
            }

            if (fileName != "")
            {
                if (File.Exists(fileName))
                {
                    ILibraryImporter libraryImporter = new LibraryImporter(minimizer);
                    libraryImporter.ImportLibrary(fileName);
                }
            }

            PopulateLibraryList();
        }

        private void button_Import_LibraryFolder_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog folderDialog = new CommonOpenFileDialog();

            string folderName = "";
            folderDialog.IsFolderPicker = true;

            if (folderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                folderName = folderDialog.FileName;
            }

            if (folderName != "")
            {
                if (System.IO.Directory.Exists(folderName))
                {
                    ILibraryImporter libraryImporter = new LibraryImporterMultipleFiles(minimizer);
                    libraryImporter.ImportLibrary(folderName);
                }
            }

            PopulateLibraryList();
        }

        private async void button_Import_LibraryREST_Click(object sender, RoutedEventArgs e)
        {
            ILibraryImporterAsync libraryImporterREST = new LibraryImporterREST(portNumber, minimizer);

            button_Import_Library.IsEnabled = false;
            button_Import_Library.Content = "Importing Libraries...";

            await libraryImporterREST.ImportLibrary("");

            PopulateLibraryList();

            button_Import_Library.Content = "Import Libraries";
            button_Import_Library.IsEnabled = true;
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
    }
}