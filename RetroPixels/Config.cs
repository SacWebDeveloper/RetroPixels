using System.IO;
using System.Xml.Serialization;
using System.Xml;
using UnityEngine;
using ColossalFramework.IO;

namespace RetroPixels
{
    
    public class Config
    {

        public KeyCode menuToggle;
        public KeyCode effectToggle;
        public Vector2 windowLoc;
        public EBS_RetroPixel.RPColorMode colorMode;
        public float vert = 256;
        public float hor = 256;
        public float num = 4;
        public bool useActualColors;
        public int bits;
        public bool retroColors;
        
        public enum SavedColor
        {
            amos32 = 0,
            amstradc = 1,
            apple2 = 2,
            arne8 = 3,
            arne16 = 4,
            arne64 = 5,
            atari2600 = 6,
            c64 = 7,
            dp32 = 8,
            dp256 = 9,
            dwm = 10,
            gameboy = 11,
            hsv = 12,
            ink = 13,
            monk = 14,
            msx2 = 15,
            msx = 16,
            nes = 17,
            sam = 18,
            ss = 19,
            tango = 20,
            websafe = 21,
            zxspectrum = 22,
        }

        public SavedColor savedColor;
        public float savedGamma;

        public bool userWantsEffect;

        public enum Tab
        {
            EightBit,
            Hotkey,
        }

        public enum EffectAsset
        {
            RetroPixel = 0,
            OldSchoolPixels = 1,
        }

        public EffectAsset currentAsset;
        

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
        public static void MakeFolderIfNonexistent()
        {
            DirectoryInfo di = Directory.CreateDirectory(DataLocation.modsPath + @"\EightBitSkies");
            DirectoryInfo dir = Directory.CreateDirectory(DataLocation.modsPath + @"\EightBitSkies\LUTs");
            
        }


        
    }
}