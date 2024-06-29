using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Entities
{
    internal class LibraryItemLine
    {
        public long LibraryItemLineId { get; set; }
        public string Text { get; set; }
        public long LibraryItemId { get; set; }
    }
}
