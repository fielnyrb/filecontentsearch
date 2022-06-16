using File_Content_Search.Entities;
using File_Content_Search.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace File_Content_Search.Implementations
{
    internal class LibraryImporter : ILibraryImporter
    {
        public void ImportLibrary(string libraryFilePath)
        {

            long newLibrary;

            using (var context = new MyContext())
            {
                var library = new Library
                {
                    Name = "lib"
                };

                context.Libraries.Add(library);
                context.SaveChanges();

                newLibrary = library.LibraryId;
            }


            string fullLibrary = File.ReadAllText(libraryFilePath);

            List<string> fullItem = fullLibrary.Split("Title: ").ToList();

            foreach (string item in fullItem)
            {
                if (item == "")
                {
                    continue;
                }

                string title = item.Split("\r\n").ToList()[0];
                string content = item;

                using (var context = new MyContext())
                {
                    var libraryItem = new LibraryItem
                    {
                        Title = title,
                        Content = content,
                        LibraryId = newLibrary
                    };

                    context.LibraryItems.Add(libraryItem);

                    context.SaveChanges();
                }
            }
        }
    }
}
