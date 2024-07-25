using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.ItemLibrary.Interfaces
{
    internal interface ILibraryDeleter
    {
        void DeleteLibrary(long libraryId);
    }
}
