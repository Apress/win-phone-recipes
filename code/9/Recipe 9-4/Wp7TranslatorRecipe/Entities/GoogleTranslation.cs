using System.Runtime.Serialization;
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

namespace Wp7TranslatorRecipe.Entities
{
    [DataContract]
    //[KnownType(typeof(Data))]
    //[KnownType(typeof(Translation))]
    public class GoogleTranslation
    {
        [DataMember(Name="data")]
        public Data Data { get; set; }
    }
}
