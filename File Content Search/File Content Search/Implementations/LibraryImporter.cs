using File_Content_Search.Entities;
using File_Content_Search.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Implementations
{
    internal class LibraryImporter : ILibraryImporter
    {
        public void ImportLibrary(string libraryFilePath)
        {
            string fullLibrary = File.ReadAllText(libraryFilePath);

            List<string> fullItem = fullLibrary.Split("Title: ").ToList();

            foreach (string item in fullItem)
            {
                string title = item.Split("\r\n").ToList()[0];

                using (var context = new MyContext())
                {
                    var libraryItem = new LibraryItem
                    {
                        Title = title
                    };

                    context.LibraryItems.Add(libraryItem);
                    context.SaveChanges();
                }

                //File.WriteAllText(@"C:\Users\User\Documents\FileContentSearch\Libraries\NewLibrary\" + title, title);
            }

            //Directory.CreateDirectory(@"C:\Users\User\Documents\FileContentSearch\Libraries\NewLibrary");


        }
    }
}
