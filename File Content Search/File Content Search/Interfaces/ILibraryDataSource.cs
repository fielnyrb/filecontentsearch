using File_Content_Search.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Interfaces
{
    internal interface ILibraryDataSource
    {
        List<Library> GetLibraries();
    }
}
