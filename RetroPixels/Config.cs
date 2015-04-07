using System.IO;
using System.Xml.Serialization;
using System.Xml;
using UnityEngine;

namespace RetroPixels
{
    
    public class Config
    {

        public KeyCode menuToggle;
        public KeyCode effectToggle;
        public Vector2 windowLoc;

        public float vert = 256;
        public float hor = 256;
        public float num = 4;
        public bool useActualColors;

        public bool userWantsEffect;

        public enum Tab
        {
            EightBit,
            Hotkey,
        }
        

        public static void Serialize(string filename, Config config)
        {
            var serializer = new XmlSerializer(typeof(Config));

            using (var writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, config);
            }
        }

        public static Config Deserialize(string filename)
        {
            var serializer = new XmlSerializer(typeof(Config));

            try
            {
                using (var reader = new StreamReader(filename))
                {
                    var config = (Config)serializer.Deserialize(reader);
                    return config;
                }
            }
            catch { }
            return null;
        }

    }
}