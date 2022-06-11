using File_Content_Search.Interfaces;
using File_Content_Search.Structures;
using System.Collections.Generic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File_Content_Search.Entities;
using Microsoft.EntityFrameworkCore;

namespace File_Content_Search.Implementations
{
    internal class SQLiteSearcher : IContentSearcher
    {
        public List<FoundItem> Search(string searchString, string directory)
        {
            List<FoundItem> foundItems = new List<FoundItem>();

            var context = new MyContext();

            var query = context.LibraryItems.Include(p => new FoundItem(p.Title))
                .Where(p => EF.Functions.Like(searchString, p.Title)).ToList();

            return foundItems;
        }
    }
}
