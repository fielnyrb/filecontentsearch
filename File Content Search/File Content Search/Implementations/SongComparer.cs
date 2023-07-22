using File_Content_Search.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Implementations
{
    internal class SongComparer
    {
        public List<NameItem> SearchForDuplicates()
        {
            //Make query to SQLite DB
            var context = new MyContext();

            //Get all songs from DB
            var query = from LibraryItem libraryItem in context.LibraryItems
                        group libraryItem by libraryItem.Title into itemGroup
                        where itemGroup.Count() > 1
                        select new NameItem { ItemTitle = itemGroup.Key };

            List<NameItem> duplicates = query.ToList();

            context.Dispose();

            return duplicates;
        }
    }
}
