using File_Content_Search.Entities;
using File_Content_Search.Interfaces;
using File_Content_Search.ItemLibrary.Interfaces;
using File_Content_Search.Structures;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace File_Content_Search.ItemLibrary
{
    public class LibraryImporterREST : ILibraryImporterAsync
    {
        private ITextMinimizer minimizer;
        private ProPresenterAPI proPresenterAPI;

        public LibraryImporterREST(string port, ITextMinimizer minimizer)
        {
            this.minimizer = minimizer;
            this.proPresenterAPI = new ProPresenterAPI(port);
        }
        public async Task ImportLibrary(SelectableLibrary library)
        {
            long dbId = CreateLibrary(library.Name);
            try
            {
                await ImportLibraryContentAsync(library, dbId);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private long CreateLibrary(string libraryName)
        {
            LibraryCreator libraryCreator = new LibraryCreator();
            return libraryCreator.CreateLibraryDatabaseEntry(libraryName);
        }

        public async Task ImportLibraryContentAsync(SelectableLibrary library, long databaseId)
        {
            string libraryId = library.Uuid;

            JObject libraryContent = await this.proPresenterAPI.GetLibraryAsync(libraryId);
            if (libraryContent["items"] is not null)
            {
                foreach (JObject item in libraryContent["items"])
                {
                    if (item["uuid"] is not null)
                    {
                        string itemId = (string)item["uuid"];
                        JObject presentation = await this.proPresenterAPI.GetPresentationAsync(itemId);

                        string presentationName = (string)presentation["presentation"]["id"]["name"];
                        string presentationContent = presentationName;

                        foreach (var group in presentation["presentation"]["groups"])
                        {
                            foreach (var slide in group["slides"])
                            {
                                presentationContent += (string)slide["text"];
                            }
                        }
                        Guid itemGuid = PutItemIntoLibrary(databaseId, presentationName, presentationContent);
                        PutLinesIntoLibrary(itemGuid, presentation["presentation"]["groups"]);
                    }
                }
            }
        }

        private Guid PutItemIntoLibrary(long newLibraryId, string itemTitle, string itemContent)
        {
            Guid newLibraryItemId = Guid.Empty;

            if (itemContent == "" || itemTitle == "")
            {
                return newLibraryItemId;
            }

            string content = minimizer.minimize(itemContent);

            using (var context = new MyContext())
            {
                var libraryItem = new LibraryItem
                {
                    Title = itemTitle,
                    Content = content,
                    OriginalContent = itemContent,
                    LibraryId = newLibraryId
                };

                context.LibraryItems.Add(libraryItem);

                context.SaveChanges();

                newLibraryItemId = libraryItem.LibraryItemId;
            }

            return newLibraryItemId;
        }

        private void PutLinesIntoLibrary(Guid itemGuid, JToken? groups)
        {
            if (groups is null)
            {
                return;
            }
            if (Guid.Empty.Equals(itemGuid))
            {
                return;
            }

            using (MyContext context = new MyContext())
            {
                foreach (JToken group in groups)
                {
                    foreach (JToken slide in group["slides"])
                    {
                        string unSplitText = (string)slide["text"];
                        string[] lines = unSplitText.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                        foreach (string line in lines)
                        {
                            LibraryItemLine itemLine = new LibraryItemLine
                            {
                                Name = CreateNewItemName((string)group["name"], groups),
                                Text = line,
                                LibraryItemId = itemGuid
                            };

                            context.LibraryItemLines.Add(itemLine);
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        private string CreateNewItemName(string oldItemName, JToken groups)
        {
            string newItemName = oldItemName;
            if (oldItemName == "Group")
            {
                // Find largest "Verse" number in groups
                int largestVerseNumber = 1;
                foreach (JToken group in groups)
                {
                    string groupName = (string)group["name"];
                    if (groupName.Contains("Verse"))
                    {
                        string verseNumber = groupName.Substring(5);
                        int verseNumberInt = int.Parse(verseNumber);
                        if (verseNumberInt > largestVerseNumber)
                        {
                            largestVerseNumber = verseNumberInt;
                        }
                    }
                }
                newItemName = "Verse " + largestVerseNumber;
            }

            return newItemName;
        }
    }
}