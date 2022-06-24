using File_Content_Search.Entities;
using File_Content_Search.Interfaces;
using File_Content_Search.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Implementations
{
    internal class LibrarySource : ILibraryDataSource
    {
        public List<LibraryInformation> GetLibrariesInformation()
        {
            List<Library> libraries = new List<Library>();
            var context = new MyContext();
            libraries = context.Libraries.Select(q => q).ToList();

            List<LibraryInformation> librariesInformation = new List<LibraryInformation>();
            foreach (var library in libraries)
            {
                librariesInformation.Add(new LibraryInformation(library.Name, library.LibraryId, library.ImportDateTime));
            }

            return librariesInformation;
        }
    }
}
