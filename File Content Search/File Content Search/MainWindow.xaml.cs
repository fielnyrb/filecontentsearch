﻿using File_Content_Search.Implementations;
using File_Content_Search.Interfaces;
using File_Content_Search.Structures;
using Microsoft.Win32;
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
        public string libraryImportDirectory;

        public MainWindow()
        {
            InitializeComponent();

            libraryImportDirectory = "D:\\songs2\\ProPresenter Export.txt";

            ILibraryDataSource libraryDataSource = new LibrarySource();

            listBoxLibraries.ItemsSource = libraryDataSource.GetLibraries();
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            IContentSearcher searcher = new SQLiteSearcher();

            List<FoundItem> foundItems = searcher.Search(searchString: textBox.Text, directory: "");

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
                    ILibraryImporter libraryImporter = new LibraryImporter();
                    libraryImporter.ImportLibrary(libraryImportDirectory);
                }
            }

        }
    }
}