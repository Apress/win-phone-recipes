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
    //[DataContract(Name="rss",Namespace="")]
    [XmlRoot(ElementName="rss",Namespace="")]
    public class Rss
    {
        private Channel _channel;

        private string _version;

        //[DataMember(Name = "channel")]
        [XmlElement(ElementName="channel")]
        public Channel Channel
        {
            get
            {
                return this._channel;
            }
            set
            {
                this._channel = value;
            }
        }

        //[DataMember(Name = "version")]
        [XmlElement(ElementName = "version")]
        public string Version
        {
            get
            {
                return this._version;
            }
            set
            {
                this._version = value;
            }
        }
    }
}
