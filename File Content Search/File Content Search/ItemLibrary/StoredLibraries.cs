using File_Content_Search.Entities;
using File_Content_Search.ItemLibrary.Interfaces;
using File_Content_Search.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace File_Content_Search.ItemLibrary
{
    public class StoredLibraries : IStoredLibraries
    {
        public List<LibraryInformation> GetLibrariesInformation()
        {
            List<Library> libraries = new List<Library>();
            var context = new MyContext();

            try
            {
                libraries = context.Libraries.Select(q => q).ToList();
            }
            catch (Exception e)
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

        public LibraryInformation GetLibraryInformation(long libraryId)
        {
            LibraryInformation libraryInformation = new LibraryInformation("", 0, DateTime.MinValue);

            try
            {
                Library? library = new MyContext().Libraries.Where(q => q.LibraryId == libraryId).FirstOrDefault();

                if (library == null)
                {
                    throw new Exception("Library not found");
                }

                return new LibraryInformation(library.Name, library.LibraryId, library.ImportDateTime);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return libraryInformation;
        }
    }
}
