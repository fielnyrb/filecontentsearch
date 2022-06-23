using File_Content_Search.Entities;
using File_Content_Search.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace File_Content_Search.Implementations
{
    internal class LibraryImporter : ILibraryImporter
    {
        ITextMinimizer minimizer;

        public LibraryImporter(ITextMinimizer minimizer)
        {
            this.minimizer = minimizer;
        }

        public void ImportLibrary(string libraryFilePath)
        {
            string libraryItemsContent = "";

            try
            {
                libraryItemsContent = File.ReadAllText(libraryFilePath);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            long newLibraryId = CreateLibraryDatabaseEntry(ExtractLibraryName(libraryFilePath));
            PutLibraryItemsIntoLibrary(newLibraryId, libraryItemsContent);
        }

        private long CreateLibraryDatabaseEntry(string libraryName)
        {
            long newLibraryId;
            using (var context = new MyContext())
            {
                var library = new Library
                {
                    Name = libraryName
                };

                context.Libraries.Add(library);
                context.SaveChanges();

                newLibraryId = library.LibraryId;
            }

            return newLibraryId;
        }

        private void PutLibraryItemsIntoLibrary(long newLibraryId, string libraryItemsContent)
        {
            List<string> fullItem = libraryItemsContent.Split("Title: ").ToList();

            foreach (string item in fullItem)
            {
                if (item == "")
                {
                    continue;
                }

                string title = item.Split("\r\n").ToList()[0];
                string content = item;

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
        }

        private string ExtractLibraryName(string libraryFilePath)
        {
            return libraryFilePath.Split("\\").Last();
        }
    }
}
