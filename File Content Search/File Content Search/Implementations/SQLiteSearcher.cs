using File_Content_Search.Entities;
using File_Content_Search.Interfaces;
using File_Content_Search.Structures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace File_Content_Search.Implementations
{
    internal class SQLiteSearcher : IContentSearcher
    {
        public List<FoundItem> Search(string searchString, string directory)
        {
            List<FoundItem> foundItems = new List<FoundItem>();

            if (searchString.Trim() == "")
            {
                return foundItems;
            }

            var context = new MyContext();

            List<string> query = context.LibraryItems
                .Where(p => p.Content.Contains(searchString))
                .Select(q => q.Title)
                .ToList();

            foreach (string item in query)
            {
                foundItems.Add(new FoundItem(item));
            }

            return foundItems;
        }
    }
}
