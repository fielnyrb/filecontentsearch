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
        public DateTime ImportDateTime { get; set; }
        public string FormattedImportDateTime { get; set; }

        public LibraryInformation(string name, long libraryInformationId, DateTime importDateTime)
        {
            Name = name;
            LibraryInformationId = libraryInformationId;
            ImportDateTime = importDateTime;
            FormattedImportDateTime = importDateTime.ToString("d/MM/yyyy HH:mm");
        }
    }
}
