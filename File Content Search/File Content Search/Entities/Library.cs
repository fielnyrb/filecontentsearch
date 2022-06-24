using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Entities
{
    public class Library
    {
        public long LibraryId { get; set; }
        public string Name { get; set; }
        public DateTime ImportDateTime { get; set; }
    }
}
