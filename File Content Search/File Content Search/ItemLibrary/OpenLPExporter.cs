using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using File_Content_Search.Entities;
using File_Content_Search.Migrations;
using File_Content_Search.Classes.OpenLP;

namespace File_Content_Search.ItemLibrary
{
    internal class OpenLPExporter
    {
        MyContext _dbContext;

        public void ExportLibrary(MyContext dbContext)
        {
            _dbContext = dbContext;

            ExportAllLibraryItemsToOpenLP();

            //WriteXmlStringToFile(xml, filePath);
        }

        private void ExportAllLibraryItemsToOpenLP()
        {
            //Get all library items from database
            var libraryItemLines = (from LibraryItemLine libraryItemLine in _dbContext.LibraryItemLines
                                    join LibraryItem libraryItem in _dbContext.LibraryItems on libraryItemLine.LibraryItemId equals libraryItem.LibraryItemId
                                    orderby libraryItemLine.LibraryItemLineId
                                    select new DTOSongLine
                                    {
                                        LibraryItemId = libraryItem.LibraryItemId,
                                        Title = libraryItem.Title,
                                        Name = libraryItemLine.Name,
                                        Text = libraryItemLine.Text
                                    }).ToList();

            DTOSongLine previousDTOSong = libraryItemLines.First<DTOSongLine>();

            String songTitle = "";
            String verseLines = "";
            List<Verse> openLPVerses = new List<Verse>();

            for (int i = 0; i < libraryItemLines.Count(); i++)
            {
                List<DTOSongLine> songLines = new List<DTOSongLine>();

                if (previousDTOSong.Name != libraryItemLines[i].Name)
                {
                    openLPVerses.Add(new Verse { Name = previousDTOSong.Name, Lines = verseLines });
                    verseLines = libraryItemLines[i].Text;
                }
                else
                {
                    if (verseLines == "")
                    {
                        verseLines = libraryItemLines[i].Text;
                    }
                    else
                    {
                        verseLines += "<br/>" + libraryItemLines[i].Text;
                    }
                }

                if (previousDTOSong.LibraryItemId != libraryItemLines[i].LibraryItemId)
                {
                    songTitle = libraryItemLines[i].Title;



                    //Export library item to OpenLP format
                    //Song openLPSong = BuildSong("", libraryItemLine);
                    //SerializeSongToXml(openLPSong, "song.xml");
                }
                else
                {
                    //Add verse to song
                    //song.Lyrics.Verse.Lines += libraryItemLine.Text;
                }

                previousDTOSong = libraryItemLines[i];

                //If is last element in list
                if (i == libraryItemLines.Count - 1)
                {
                    openLPVerses.Add(new Verse { Name = previousDTOSong.Name, Lines = verseLines });
                }
            }


            string filePath = "song.xml";
            //string xml = ReplaceEscapedBrTags(SerializeSongToXmlString(song));
        }

        private Song BuildSong(String songTitle, DTOSongLine songLine)
        {
            //Build song object
            Song song = new Song();
            song.Version = "1.0";
            song.CreatedIn = "FCS";
            song.ModifiedIn = "FCS";
            song.ModifiedDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            song.Properties = new Properties();
            song.Properties.Titles = new Titles();
            //song.Properties.Titles.Title = libraryItemLine.Name;

            song.Properties.Authors = new Authors();
            song.Properties.Authors.Author = "Anonymous";

            song.Lyrics = new Lyrics();
            //song.Lyrics.Verse = new Verse();
            //song.Lyrics.Verse.Name = "v1";
            //song.Lyrics.Verse.Lines = libraryItemLine.Text;

            return song;
        }

        public void SerializeSongToXml(Song song, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Song));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, song);
            }
        }

        public string SerializeSongToXmlString(Song song)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Song));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, song);
                return writer.ToString();
            }
        }

        public void WriteXmlStringToFile(string xmlString, string filePath)
        {
            File.WriteAllText(filePath, xmlString);
        }

        public static string ReplaceEscapedBrTags(string input)
        {
            return input.Replace("&lt;br/&gt;", "<br/>");
        }
    }
}
