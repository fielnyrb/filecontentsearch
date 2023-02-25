using File_Content_Search.Entities;
using File_Content_Search.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace File_Content_Search.Implementations
{
    internal class LibraryImporterMultipleFiles : ILibraryImporter
    {
        ITextMinimizer minimizer;

        public LibraryImporterMultipleFiles(ITextMinimizer minimizer)
        {
            this.minimizer = minimizer;
        }

        public void ImportLibrary(string libraryFolderPath)
        {
            try
            {
                string path = "D:\\export";
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] files1 = dir.GetFiles();
                long newLibraryId = CreateLibraryDatabaseEntry(ExtractLibraryName(libraryFolderPath));

                foreach (FileInfo file in files1)
                {
                    using (StreamReader reader = file.OpenText())
                    {
                        string content = reader.ReadToEnd();
                        PutItemIntoLibrary(newLibraryId, content);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private long CreateLibraryDatabaseEntry(string libraryName)
        {
            long newLibraryId;
            using (var context = new MyContext())
            {
                var library = new Library
                {
                    Name = libraryName,
                    ImportDateTime = DateTime.Now,
                };

                context.Libraries.Add(library);
                context.SaveChanges();

                newLibraryId = library.LibraryId;
            }

            return newLibraryId;
        }

        private void PutItemIntoLibrary(long newLibraryId, string itemContent)
        {
            if (itemContent == "")
            {
                return;
            }

            string title = itemContent.Split("\r\n").ToList()[0];
            string content = itemContent;

            content = minimizer.minimize(content);

            using (var context = new MyContext())
            {
                var libraryItem = new LibraryItem
                {
                    Title = title,
                    Content = content,
                    LibraryId = newLibraryId
                };

                context.LibraryItems.Add(libraryItem);

                context.SaveChanges();
            }

        }

        private string ExtractLibraryName(string libraryFilePath)
        {
            return libraryFilePath.Split("\\").Last();
        }
    }
}
