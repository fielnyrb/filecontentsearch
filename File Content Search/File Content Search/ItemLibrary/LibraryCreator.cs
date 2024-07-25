using File_Content_Search.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.ItemLibrary
{
    public class LibraryCreator
    {
        public long CreateLibraryDatabaseEntry(string libraryName)
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
    }
}
