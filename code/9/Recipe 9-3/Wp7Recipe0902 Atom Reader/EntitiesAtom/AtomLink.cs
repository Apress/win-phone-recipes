using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Wp7Recipe0902_Atom_Reader.EntitiesAtom
{
    [System.Xml.Serialization.XmlRoot(Namespace="atom",ElementName="link")]
    public class AtomLink
    {
        [System.Xml.Serialization.XmlAttribute(AttributeName = "rel")]
        public string Rel { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [System.Xml.Serialization.XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }
        
    }
}
