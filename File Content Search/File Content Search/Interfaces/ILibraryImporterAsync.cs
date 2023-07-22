using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Interfaces
{
    internal interface ILibraryImporterAsync
    {
        public Task ImportLibrary(string libraryFilePath);
    }
}
