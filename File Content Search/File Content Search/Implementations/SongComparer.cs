using File_Content_Search.Classes;
using File_Content_Search.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Implementations
{
    internal class SongComparer
    {
        public List<DuplicateGroupItem> SearchForDuplicates()
        {
            //Make query to SQLite DB
            var context = new MyContext();

            //Get all songs from DB
            var query = from LibraryItem libraryItem in context.LibraryItems
                        join Library library in context.Libraries on libraryItem.LibraryId equals library.LibraryId
                        group new { libraryItem, library } by libraryItem.Title into itemGroup
                        where itemGroup.Count() > 1
                        select new DuplicateGroupItem
                        {
                            ItemTitle = itemGroup.Key,
                            LibraryIds = itemGroup.Select(item => item.libraryItem.LibraryId).ToList(),
                            DuplicateItems = itemGroup.Select(item => new DuplicateItem
                            {
                                ItemOriginalContent = item.libraryItem.OriginalContent,
                                LibraryName = item.library.Name
                            }).ToList()
                        };

            List<DuplicateGroupItem> duplicates = query.ToList();

            context.Dispose();

            return duplicates;
        }
    }
}
