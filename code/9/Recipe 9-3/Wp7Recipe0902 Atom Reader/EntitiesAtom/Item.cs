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
    public class Item
    {
        private string _title;

        private string _description;

        private string _link;

        private string _pubDate;

        private string _guid;

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

        //[DataMember(Name = "pubDate")]
        [XmlElement(ElementName = "pubDate")]
        public string PubDate
        {
            get
            {
                return this._pubDate;
            }
            set
            {
                this._pubDate = value;
            }
        }

        //[DataMember(Name = "guid")]
        [XmlElement(ElementName = "guid")]
        public string Guid
        {
            get
            {
                return this._guid;
            }
            set
            {
                this._guid = value;
            }
        }
    }
}
