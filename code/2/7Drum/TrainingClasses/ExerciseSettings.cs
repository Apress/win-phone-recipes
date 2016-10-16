using System;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.IO;

namespace _7Drum
{
    public class ExerciseSettings
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
    }
}
