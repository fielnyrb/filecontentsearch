using System;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot(ElementName = "song", Namespace = "http://openlyrics.info/namespace/2009/song")]
public class Song
{
    [XmlElement(ElementName = "properties")]
    public Properties Properties { get; set; }

    [XmlElement(ElementName = "lyrics")]
    public Lyrics Lyrics { get; set; }

    [XmlAttribute(AttributeName = "version")]
    public string Version { get; set; }

    [XmlAttribute(AttributeName = "createdIn")]
    public string CreatedIn { get; set; }

    [XmlAttribute(AttributeName = "modifiedIn")]
    public string ModifiedIn { get; set; }

    [XmlAttribute(AttributeName = "modifiedDate")]
    public string ModifiedDate { get; set; }
}

public class Properties
{
    [XmlElement(ElementName = "titles")]
    public Titles Titles { get; set; }

    [XmlElement(ElementName = "authors")]
    public Authors Authors { get; set; }
}

public class Titles
{
    [XmlElement(ElementName = "title")]
    public List<String> Title { get; set; }
}

public class Authors
{
    [XmlElement(ElementName = "author")]
    public string Author { get; set; }
}

public class Lyrics
{
    [XmlElement(ElementName = "verse")]
    public List<Verse> Verse { get; set; }
}

public class Verse
{
    [XmlElement(ElementName = "lines")]
    public string Lines { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
}
