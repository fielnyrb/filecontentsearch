using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Entities
{
    public class LibraryItem
    {
        public Guid LibraryItemId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string OriginalContent { get; set; }
        public long LibraryId { get; set; }
    }
}
