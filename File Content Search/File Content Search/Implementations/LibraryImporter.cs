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

            List<string> titles = fullLibrary.Split("Title: ").ToList();
        }
    }
}
