using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Classes
{
    public class Presentation
    {
        public string Title { get; set; }
        public List<Line> Lines { get; set; }

        public Presentation(string Title)
        {
            Lines = new List<Line>();
            this.Title = Title;
        }
    }

    public class Line
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
