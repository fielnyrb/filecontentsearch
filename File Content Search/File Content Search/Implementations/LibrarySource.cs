using File_Content_Search.Entities;
using File_Content_Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Implementations
{
    internal class LibrarySource : ILibraryDataSource
    {
        public List<Library> GetLibraries()
        {
            List<Library> libraries = new List<Library>();
            
            var context = new MyContext();

            libraries = context.Libraries.Select(q => q).ToList();

            return libraries;
        }
    }
}
