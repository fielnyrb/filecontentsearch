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

            var query = from LibraryItem libraryItem in context.LibraryItems
                        join Library libraries in context.Libraries on libraryItem.LibraryId equals libraries.LibraryId
                        where libraryItem.Content.Contains(searchString)
                        select new { ItemTitle = libraryItem.Title, LibraryName = libraries.Name };

            foreach (var item in query)
            {
                FoundItem tempFoundItem = new FoundItem(item.ItemTitle, item.LibraryName);

                foundItems.Add(tempFoundItem);
            }

            context.Dispose();

            return foundItems;
        }
    }
}
