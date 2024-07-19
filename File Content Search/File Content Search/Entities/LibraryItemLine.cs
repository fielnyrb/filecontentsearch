using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Entities
{
    public class LibraryItemLine
    {
        public long LibraryItemLineId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid LibraryItemId { get; set; }
    }
}
