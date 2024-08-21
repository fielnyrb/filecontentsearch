﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using File_Content_Search.Entities;
using File_Content_Search.Migrations;

namespace File_Content_Search.ItemLibrary
{
    internal class OpenLPExporter
    {
        MyContext _dbContext;

        public void ExportLibrary(MyContext dbContext)
        {
            _dbContext = dbContext;

            //Export library item to OpenLP format
            Song song = BuildSong();

            string filePath = "song.xml";
            string xml = ReplaceEscapedBrTags(SerializeSongToXmlString(song));

            ExportAllLibraryItemsToOpenLP();

            //WriteXmlStringToFile(xml, filePath);
        }

        private void ExportAllLibraryItemsToOpenLP()
        {
            //Get all library items from database
            var libaryItemLines = from LibraryItemLine libraryItemLine in _dbContext.LibraryItemLines
                                  select libraryItemLine;

            foreach (var item in libaryItemLines)
            {
                var test = item;
            }
        }

            private Song BuildSong()
        {
            //Build song object
            Song song = new Song();
            song.Version = "1.0";
            song.CreatedIn = "FCS";
            song.ModifiedIn = "FCS";
            song.ModifiedDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            song.Properties = new Properties();
            song.Properties.Titles = new Titles();
            song.Properties.Titles.Title = "Title";

            song.Properties.Authors = new Authors();
            song.Properties.Authors.Author = "Anonymous";

            song.Lyrics = new Lyrics();
            song.Lyrics.Verse = new Verse();
            song.Lyrics.Verse.Name = "v1";
            song.Lyrics.Verse.Lines = "Lyri<br/>cs";

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
