using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Classes
{
    internal class DuplicateGroupItem
    {
        public string ItemTitle { get; set; }
        public List<long> LibraryIds { get; set; }
        public List<DuplicateItem> DuplicateItems { get; set; }
    }
}
