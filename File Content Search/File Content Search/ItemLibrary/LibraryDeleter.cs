using File_Content_Search.Entities;
using File_Content_Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace File_Content_Search.ItemLibrary
{
    internal class LibraryDeleter : ILibraryDeleter
    {
        MyContext _dbContext;

        public LibraryDeleter(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteLibrary(long libraryId)
        {
            DeleteCurrentLibraryItems(libraryId);
            DeleteCurrentLibrary(libraryId);
        }

        private void DeleteCurrentLibraryItems(long libraryId)
        {
            var deleteLibraryItems = from LibraryItem libraryItem in _dbContext.LibraryItems
                                     where libraryItem.LibraryId == libraryId
                                     select libraryItem;

            foreach (var item in deleteLibraryItems)
            {
                _dbContext.LibraryItems.Remove(item);
            }

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void DeleteCurrentLibrary(long libraryId)
        {
            var deleteLibrary = from Library library in _dbContext.Libraries
                                where library.LibraryId == libraryId
                                select library;

            foreach (var item in deleteLibrary)
            {
                _dbContext.Libraries.Remove(item);
            }

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
