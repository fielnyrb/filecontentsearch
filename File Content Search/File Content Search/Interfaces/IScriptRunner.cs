using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Interfaces
{
    internal interface IScriptRunner
    {
        public void Search(string searchString, string directory);
    }
}
