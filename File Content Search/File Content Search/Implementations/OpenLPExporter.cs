﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace File_Content_Search.Implementations
{
    internal class OpenLPExporter
    {
        public void ExportLibrary()
        {
            //Export library item to OpenLP format
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
            song.Lyrics.Verse.Lines = "Lyrics";

            string filePath = "song.xml";
            SerializeSongToXml(song, filePath);
        }

        public void SerializeSongToXml(Song song, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Song));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, song);
            }
        }

    }
}
