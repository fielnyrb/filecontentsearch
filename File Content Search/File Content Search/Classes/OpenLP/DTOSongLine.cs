using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Classes.OpenLP
{
    public class DTOSongLine
    {
        public Guid LibraryItemId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
