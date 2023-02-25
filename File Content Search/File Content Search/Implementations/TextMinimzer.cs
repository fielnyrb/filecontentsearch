using File_Content_Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Implementations
{
    internal class TextMinimizer : ITextMinimizer
    {
        public string minimize(string textToBeMinimized)
        {
            return textToBeMinimized.Replace(",", "").
                Replace(".", "").
                Replace("!", "").
                Replace("?", "").
                Replace(" ", "").
                Replace("'", "").
                Replace("\"","").
                Replace("\n", "").
                Replace("\r\n", "").ToLower();
        }
    }
}
