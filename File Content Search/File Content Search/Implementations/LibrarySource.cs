using File_Content_Search.Entities;
using File_Content_Search.Interfaces;
using File_Content_Search.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace File_Content_Search.Implementations
{
    internal class LibrarySource : ILibraryDataSource
    {
        public List<LibraryInformation> GetLibrariesInformation()
        {
            List<Library> libraries = new List<Library>();
            var context = new MyContext();

            try
            {
                libraries = context.Libraries.Select(q => q).ToList();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            List<LibraryInformation> librariesInformation = new List<LibraryInformation>();
            foreach (var library in libraries)
            {
                librariesInformation.Add(new LibraryInformation(library.Name, library.LibraryId, library.ImportDateTime));
            }

            return librariesInformation;
        }
    }
}
