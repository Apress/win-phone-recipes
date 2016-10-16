using System;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;


namespace _7Drum
{
    public class ExerciseManager
    {
        public static void Save(List<ExerciseSettings> exercises)
        {
            IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream stream = iso.CreateFile("exercises.xml");
            StreamWriter writer = new StreamWriter(stream);

            XmlSerializer ser = new XmlSerializer(typeof(List<ExerciseSettings>));

            ser.Serialize(writer, exercises);

            writer.Close();
            writer.Dispose();
        }

        public static List<ExerciseSettings> Load()
        {
            List<ExerciseSettings> exercises = new List<ExerciseSettings>();

            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication(); 
            
            if (storage.FileExists("exercises.xml"))
            {
                IsolatedStorageFileStream stream = storage.OpenFile("exercises.xml", FileMode.Open);
                XmlSerializer xml = new XmlSerializer(typeof(List<ExerciseSettings>)); 
                exercises = xml.Deserialize(stream) as List<ExerciseSettings>; 
                stream.Close(); 
                stream.Dispose();
            }

            return exercises;
        }
    }
}
