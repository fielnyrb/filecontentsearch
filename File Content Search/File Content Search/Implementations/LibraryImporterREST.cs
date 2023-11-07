using File_Content_Search.Entities;
using File_Content_Search.Interfaces;
using File_Content_Search.Structures;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace File_Content_Search.Implementations
{
    public class LibraryImporterREST : Interfaces.ILibraryImporterAsync
    {
        private static readonly HttpClient client = new HttpClient();
        private string baseUrl;
        private ITextMinimizer minimizer;
        private List<SelectableLibrary> libraries;

        public LibraryImporterREST(string port, ITextMinimizer minimizer, List<SelectableLibrary> libraries)
        {
            this.baseUrl = $"http://localhost:{port}/v1";
            this.minimizer = minimizer;
            this.libraries = libraries;
        }
        public async Task ImportLibrary(string libraryFilePath)
        {
            try
            {
                await ImportLibraryContentAsync();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public async Task ImportLibraryContentAsync()
        {
            foreach (SelectableLibrary library in libraries)
            {
                string libraryId = library.Uuid;
                string name = library.Name;
                JObject libraryContent = await GetLibraryAsync(libraryId);

                long databaseId = CreateLibraryDatabaseEntry(name);

                foreach (JObject item in libraryContent["items"])
                {
                    string itemId = (string)item["uuid"];
                    JObject presentation = await GetPresentationAsync(itemId);

                    string presentationName = (string)presentation["presentation"]["id"]["name"];
                    string presentationContent = presentationName;

                    foreach (var group in presentation["presentation"]["groups"])
                    {
                        foreach (var slide in group["slides"])
                        {
                            presentationContent += (string)slide["text"];
                        }
                    }
                    PutItemIntoLibrary(databaseId, presentationName, presentationContent);
                }
            }
        }

        public async Task<JArray> GetLibrariesAsync()
        {
            var response = await client.GetAsync($"{baseUrl}/libraries");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return JArray.Parse(responseBody);
        }

        private async Task<JObject> GetLibraryAsync(string libraryId)
        {
            var response = await client.GetAsync($"{baseUrl}/library/{libraryId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseBody);
        }

        private async Task<JObject> GetPresentationAsync(string presentationId)
        {
            var response = await client.GetAsync($"{baseUrl}/presentation/{presentationId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseBody);
        }

        private long CreateLibraryDatabaseEntry(string libraryName)
        {
            long newLibraryId;
            using (var context = new MyContext())
            {
                var library = new Library
                {
                    Name = libraryName,
                    ImportDateTime = DateTime.Now,
                };

                context.Libraries.Add(library);
                context.SaveChanges();

                newLibraryId = library.LibraryId;
            }

            return newLibraryId;
        }

        private void PutItemIntoLibrary(long newLibraryId, string itemTitle, string itemContent)
        {
            if (itemContent == "" || itemTitle == "")
            {
                return;
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
            }

        }
    }
}
