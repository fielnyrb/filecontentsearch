using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Structures
{
    public class SelectableLibrary
    {
        public string Uuid { get; set; }
        public string Name { get; set; }

        public Boolean IsChecked { get; set; }
    }
}
