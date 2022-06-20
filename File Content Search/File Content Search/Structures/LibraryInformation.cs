using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Structures
{
    internal class LibraryInformation
    {
        public long LibraryInformationId { get; set; }
        public string Name { get; set; }

        public LibraryInformation(string name, long libraryInformationId)
        {
            Name = name;
            LibraryInformationId = libraryInformationId;
        }
    }
}
