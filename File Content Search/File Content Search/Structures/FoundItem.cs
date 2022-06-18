using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Structures
{
    internal class FoundItem
    {
        public string ItemName { get; set; }
        public string LibraryName { get; set; }

        public FoundItem(string itemName, string libraryName)
        {
            ItemName = itemName;
            LibraryName = libraryName;
        }
    }
}
