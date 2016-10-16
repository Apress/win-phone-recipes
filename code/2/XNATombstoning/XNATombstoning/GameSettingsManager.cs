using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Phone.Shell;

namespace XNATombstoning
{
    public class GameSettingsManager
    {
        //public static void Save(GameSettings settings)
        //{
        //    IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication();
        //    IsolatedStorageFileStream stream = iso.CreateFile("GameSettings.xml");
        //    StreamWriter writer = new StreamWriter(stream);

        //    XmlSerializer ser = new XmlSerializer(typeof(GameSettings));

        //    ser.Serialize(writer, settings);

        //    writer.Close();
        //    writer.Dispose();
        //}

        //public static GameSettings Load()
        //{
        //    GameSettings settings = new GameSettings();

        //    IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();

        //    if (storage.FileExists("GameSettings.xml"))
        //    {
        //        IsolatedStorageFileStream stream = storage.OpenFile("GameSettings.xml", FileMode.Open);
        //        XmlSerializer xml = new XmlSerializer(typeof(GameSettings));
        //        settings = xml.Deserialize(stream) as GameSettings;
        //        stream.Close();
        //        stream.Dispose();
        //    }

        //    return settings;
        //}

        public static void Save(GameSettings settings)
        {
            PhoneApplicationService.Current.State["GameSettings"] = settings;            
        }

        public static GameSettings Load()
        {
            GameSettings settings = new GameSettings();

            if (PhoneApplicationService.Current.State.ContainsKey("GameSettings"))
                settings = PhoneApplicationService.Current.State["GameSettings"] as GameSettings;

            return settings;
        }

    }
}
