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
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Wp7Recipe0902_Atom_Reader.EntitiesAtom
{
    [DataContract]
    public class Channel
    {
        private AtomLink _atomLink;

        private string _title;

        private string _link;

        private string _description;

        private string _language;

        private string _copyright;

        private Item[] _items;

        //[DataMember(Name = "atom:link")]
        [XmlElement(ElementName = "link",Namespace="atom", Type= typeof(AtomLink))]
        public AtomLink AtomLink
        {
            get
            {
                return this._atomLink;
            }
            set
            {
                this._atomLink = value;
            }
        }

        //[DataMember(Name = "title")]
        [XmlElement(ElementName = "title")]
        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        //[DataMember(Name = "link")]
        [XmlElement(ElementName = "link")]
        public string Link
        {
            get
            {
                return this._link;
            }
            set
            {
                this._link = value;
            }
        }

        //[DataMember(Name = "description")]
        [XmlElement(ElementName = "description")]
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        //[DataMember(Name = "language")]
        [XmlElement(ElementName = "language")]
        public string Language
        {
            get
            {
                return this._language;
            }
            set
            {
                this._language = value;
            }
        }

        //[DataMember(Name = "copyright")]
        [XmlElement(ElementName = "copyright")]
        public string Copyright
        {
            get
            {
                return this._copyright;
            }
            set
            {
                this._copyright = value;
            }
        }

        //[DataMember(Name = "item")]
        [XmlElement(ElementName = "item")]
        public Item[] Items
        {
            get
            {
                return this._items;
            }
            set
            {
                this._items = value;
            }
        }
    }
}
