using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using ICities;
using ColossalFramework;
using ColossalFramework.IO;

namespace RetroPixels
{
    public class RPEffectController : MonoBehaviour
    {
        public bool showSettingsPanel = false;
        private Rect windowRect = new Rect(64, 64, 200, 200);
        private Rect dragBar = new Rect(0, 0, 803, 25);
        private Vector2 windowLoc;

        private const string configPath = "8BitSkiesConfig.xml";

        Config config;
        EBS_RetroPixel pixRP;
        EBS_OldSchoolPix pixOSP;
        KeyCode menuToggle;
        KeyCode effectToggle;
        public Config.Tab tab;

        public Config.SavedColor currentColor;
        public float currentGamma;

        public string userFile;
        public int userX;
        public int userY;

        private Color EightBitSkiesLightGreenColor;
        public GUIStyle EightBitSkiesGreenButton;
        public GUIStyle EightBitSkiesGreenLabel;

        private string guiColorModeLabel = "";
        private string guiBitModeLabel = "";
        bool userWantsEffect = false;

        bool hasFiles = false;
        string theMode = "";
        public static bool CheckIfExists()
        {
            string thePath = DataLocation.modsPath + @"\EightBitSkies\LUTs\monkey2.png";
            Debug.Log(thePath);
            if (File.Exists(thePath))
                return true;
            else
                return false;
        }

        float vert = 256;
        float hor = 256;
        float num = 4;
        int bits;
        bool useActualColors = false;

        public Config.EffectAsset currentAsset;

        void InitializeFromConfig()
        {
            if (config == null)
            {
                config = new Config();
                config.savedColor = Config.SavedColor.gameboy;
                config.savedGamma = 0f;
                config.currentAsset = Config.EffectAsset.RetroPixel;
                config.colorMode = EBS_RetroPixel.RPColorMode.Modern;
                config.windowLoc = new Vector2(64, 64);
                config.vert = 256;
                config.hor = 256;
                config.num = 4;
                config.useActualColors = false;
                config.bits = 8;
                config.retroColors = false;

                config.menuToggle = KeyCode.Quote;
                config.effectToggle = KeyCode.None;
                config.userWantsEffect = false;
            }
            else
            {
                if (IsNull(config.currentAsset))
                    config.currentAsset = Config.EffectAsset.RetroPixel;
                if (IsNull(config.savedColor))
                    config.savedColor = Config.SavedColor.gameboy;
                if (IsNull(config.savedGamma))
                    config.savedGamma = 0f;
                if (IsNull(config.colorMode))
                    config.colorMode = EBS_RetroPixel.RPColorMode.Modern;
                if (IsNull(config.windowLoc))
                    config.windowLoc = new Vector2(64, 64);
                if (IsNull(config.vert))
                    config.vert = 256;
                if (IsNull(config.hor))
                    config.hor = 256;
                if (IsNull(config.num))
                    config.num = 4;
                if (IsNull(config.useActualColors))
                    config.useActualColors = false;
                if (IsNull(config.bits))
                    config.bits = 8;
                if (IsNull(config.retroColors))
                    config.retroColors = false;


                if (IsNull(config.menuToggle))
                    config.menuToggle = KeyCode.Quote;
                if (IsNull(config.effectToggle))
                    config.effectToggle = KeyCode.None;
                if (IsNull(config.userWantsEffect))
                    config.userWantsEffect = false;
            }
        }

        static bool IsNull(System.Object aObj)
        {
            return aObj == null || aObj.Equals(null);
        }


        public void SaveConfig()
        {

            config.windowLoc = windowLoc;
            config.effectToggle = effectToggle;
            config.menuToggle = menuToggle;
            config.hor = hor;
            config.num = num;
            config.vert = vert;
            config.userWantsEffect = userWantsEffect;
            config.useActualColors = useActualColors;
            config.colorMode = pixRP.colorMode;
            config.bits = bits;
            config.currentAsset = currentAsset;
            config.savedGamma = currentGamma;
            config.savedColor = currentColor;
            Config.Serialize(configPath, config);
        }

        void LoadConfig(bool falseIfDoInitialization)
        {
            if (!falseIfDoInitialization)
            {
                userWantsEffect = false;
                windowLoc = config.windowLoc;
                windowRect.x = windowLoc.x;
                windowRect.y = windowRect.y;
            }
            currentAsset = config.currentAsset;
            menuToggle = config.menuToggle;
            effectToggle = config.effectToggle;
            hor = config.hor;
            vert = config.vert;
            num = config.num;
            useActualColors = config.useActualColors;
            pixRP.colorMode = config.colorMode;
            pixRP.bits = config.bits;
            bits = pixRP.bits;
            currentGamma = config.savedGamma;
            pixOSP.grayScaleSuppression = currentGamma;
            currentColor = config.savedColor;
            ActivateOSPColor(currentColor);

        }

        void ActivateOSPColor(Config.SavedColor color)
        {
            currentColor = color;
            switch (color)
            {
                case Config.SavedColor.amos32:
                    MakeTex("Amos32c.png", 256, 64);
                    break;
                case Config.SavedColor.amstradc:
                    MakeTex("amstradcpc.png", 256, 32);
                    break;
                case Config.SavedColor.apple2:
                    MakeTex("apple2.png", 256, 64);
                    break;
                case Config.SavedColor.arne16:
                    MakeTex("arne16_v20.png", 170, 64);
                    break;
                case Config.SavedColor.arne64:
                    MakeTex("arne64.png", 122, 64);
                    break;
                case Config.SavedColor.arne8:
                    MakeTex("arne8.png", 256, 64);
                    break;
                case Config.SavedColor.atari2600:
                    MakeTex("atari2600.png", 256, 64);
                    break;
                case Config.SavedColor.c64:
                    MakeTex("c64.png", 64, 64);
                    break;
                case Config.SavedColor.dp256:
                    MakeTex("dp4_256.png", 256, 64);
                    break;
                case Config.SavedColor.dp32:
                    MakeTex("dp4_32.png", 256, 64);
                    break;
                case Config.SavedColor.dwm:
                    MakeTex("dwm.png", 256, 64);
                    break;
                case Config.SavedColor.gameboy:
                    MakeTex("gameboy.png", 64, 64);
                    break;
                case Config.SavedColor.hsv:
                    MakeTex("hsv256.png", 256, 64);
                    break;
                case Config.SavedColor.ink:
                    MakeTex("inkscape.png", 256, 64);
                    break;
                case Config.SavedColor.monk:
                    MakeTex("monkey2.png", 256, 64);
                    break;
                case Config.SavedColor.msx:
                    MakeTex("msx16.png", 256, 64);
                    break;
                case Config.SavedColor.msx2:
                    MakeTex("msx2_scr8.png", 256, 64);
                    break;
                case Config.SavedColor.nes:
                    MakeTex("nes64.png", 256, 64);
                    break;
                case Config.SavedColor.sam:
                    MakeTex("samcoupe.png", 256, 64);
                    break;
                case Config.SavedColor.ss:
                    MakeTex("ss.png", 256, 64);
                    break;
                case Config.SavedColor.tango:
                    MakeTex("tango.png", 256, 64);
                    break;
                case Config.SavedColor.websafe:
                    MakeTex("websafe.png", 256, 64);
                    break;
                case Config.SavedColor.zxspectrum:
                    MakeTex("zxspectrum.png", 256, 64);
                    break;
                default:
                    MakeTex("gameboy.png", 64, 64);
                    break;
            }
        }

        /*
         * 
                                    
                                    
                                    
                                    
                                    
                                    
                                    
                                    
                                    
                                    
                                    
                                    
                                    
                                    
                                    
                                    
                                    
                                    
                                    
                                    
                                    
         * */
        void Awake()
        {
            Config.MakeFolderIfNonexistent();
            EightBitSkiesLightGreenColor = new Color(0.6f, 1.0f, 0.6f);
            config = Config.Deserialize(configPath);
            InitializeFromConfig();
            currentAsset = config.currentAsset;
            hasFiles = CheckIfExists();
            theMode = AssetModeLabel();
        }

        void Start()
        {
            pixRP = GetComponent<EBS_RetroPixel>();
            pixOSP = GetComponent<EBS_OldSchoolPix>();
            LoadConfig(false);
            
            InitializeColorMode();
            InitializeBitToggleLabel();
            if (config.menuToggle == KeyCode.None)
                config.menuToggle = KeyCode.Quote;
            
            
            if (useActualColors)
                pixRP.SetActualColors(true);
            else
                pixRP.SetActualColors(false);


            

           


            SaveConfig();
        }

        void InitializeColorMode()
        {
            switch (config.colorMode)
            {
                case EBS_RetroPixel.RPColorMode.Modern:
                    pixRP.SetModernColors();
                    guiColorModeLabel = "Internal Color Palette: Modern";
                    break;
                case EBS_RetroPixel.RPColorMode.Retro:
                    pixRP.SetRetroColors();
                    guiColorModeLabel = "Internal Color Palette: Retro";
                    break;
                case EBS_RetroPixel.RPColorMode.SuperRetro:
                    pixRP.SetSuperRetroColors();
                    guiColorModeLabel = "Internal Color Palette: Super Retro";
                    break;
            }
        }

        void InitializeBitToggleLabel()
        {
            switch (bits)
            {
                case 8:
                    guiBitModeLabel = "2-8 Colors";
                    break;
                case 16:
                    guiBitModeLabel = "16 Colors";
                    break;
                case 32:
                    guiBitModeLabel = "32 Colors";
                    break;
                case 64:
                    guiBitModeLabel = "64 Colors";
                    break;
            }
        }
        void CycleColorMode()
        {
            if (bits < 999)
            {
                switch (pixRP.colorMode)
                {
                    case EBS_RetroPixel.RPColorMode.Modern:
                        {
                            pixRP.colorMode = EBS_RetroPixel.RPColorMode.SuperRetro;
                            pixRP.SetSuperRetroColors();
                            guiColorModeLabel = "Internal Color Palette: Super Retro";
                            break;
                        }
                    case EBS_RetroPixel.RPColorMode.Retro:
                        {
                            pixRP.colorMode = EBS_RetroPixel.RPColorMode.Modern;
                            pixRP.SetModernColors();
                            guiColorModeLabel = "Internal Color Palette: Modern";
                            break;
                        }
                    case EBS_RetroPixel.RPColorMode.SuperRetro:
                        {
                            pixRP.colorMode = EBS_RetroPixel.RPColorMode.Retro;
                            pixRP.SetRetroColors();
                            guiColorModeLabel = "Internal Color Palette: Retro";
                            break;
                        }
                }
            }
            else
            {
                switch (pixRP.colorMode)
                {
                    case EBS_RetroPixel.RPColorMode.Modern:
                        {
                            pixRP.colorMode = EBS_RetroPixel.RPColorMode.Retro;
                            pixRP.SetRetroColors();
                            guiColorModeLabel = "Retro";
                            break;
                        }
                    case EBS_RetroPixel.RPColorMode.Retro:
                        {
                            pixRP.colorMode = EBS_RetroPixel.RPColorMode.Modern;
                            pixRP.SetModernColors();
                            guiColorModeLabel = "Modern";
                            break;
                        }
                }
            }
        }

       
        void LoadMode(Config.EffectAsset asset)
        {
            if (asset == Config.EffectAsset.OldSchoolPixels)
            {
                pixRP.enabled = false;
                pixOSP.enabled = true;
            }
            else
            {
                pixRP.enabled = true;
                pixOSP.enabled = false;
            }
        }

        void OnGUI()
        {
            if (EightBitSkiesGreenButton == null)
            {
                EightBitSkiesGreenButton = new GUIStyle(GUI.skin.button);
                EightBitSkiesGreenButton.normal.textColor = EightBitSkiesLightGreenColor;
                EightBitSkiesGreenButton.fontStyle = FontStyle.Bold;
            }
            if (EightBitSkiesGreenLabel == null)
            {
                EightBitSkiesGreenLabel = new GUIStyle(GUI.skin.label);
                EightBitSkiesGreenLabel.normal.textColor = EightBitSkiesLightGreenColor;
                EightBitSkiesGreenLabel.fontStyle = FontStyle.Bold;
            }
            if (showSettingsPanel)
            {
                windowRect = GUI.Window(391436, windowRect, SettingsPanel, "8 Bit Skies (NEW: Draggable Window!)");
            }
        }

      

        string displayText = "";
        void SettingsPanel(int wnd)
        {
            GUI.DragWindow(dragBar);
            #region Top Navigation Buttons
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Main"))
            {
                tab = Config.Tab.EightBit;
            }
            if (GUILayout.Button("Hotkey Configuration"))
            {
                tab = Config.Tab.Hotkey;
            }
            GUILayout.EndHorizontal();
            #endregion
            if (tab == Config.Tab.EightBit)
            {
                if (userWantsEffect)
                {
                    if (currentAsset == Config.EffectAsset.OldSchoolPixels)
                    {
                        ResizeWindow(730, 700);
                    }
                    else
                    {
                        ResizeWindow(730, 520);
                    }
                }
                else
                    ResizeWindow(730, 125);

                if (GUILayout.Button("Toggle Effect"))
                {
                    userWantsEffect = !userWantsEffect;
                    if (userWantsEffect)
                    {
                        LoadMode(currentAsset);
                    }
                    else
                    {
                        pixOSP.enabled = false;
                        pixRP.enabled = false;
                    }
                }
                
                if (userWantsEffect)
                {
                    if (!useActualColors)
                    {

                        if (GUILayout.Button("Modify colors: True"))
                        {
                            useActualColors = true;
                            pixRP.SetActualColors(true);
                            pixOSP.useColormap = false;
                        }
                        

                    }
                    else
                    {
                        if (GUILayout.Button("Modify colors: False"))
                        {
                            useActualColors = false;
                            pixRP.SetActualColors(false);
                            pixOSP.useColormap = true;
                        }
                        
                    }

                    GUILayout.Space(16);
                    
                    
                    if (!useActualColors)
                    {
                        GUILayout.BeginHorizontal();
                        if (currentAsset == Config.EffectAsset.OldSchoolPixels)
                        {
                            if (hasFiles)
                            {
                                if (GUILayout.Button("Switch to 'Internal Color Palettes' mode"))
                                {
                                    currentAsset = Config.EffectAsset.RetroPixel;
                                    LoadMode(currentAsset);
                                    theMode = AssetModeLabel();
                                }
                            }
                            else
                            {
                                if (GUILayout.Button("Switch back to 'Internal Color Palettes' mode"))
                                {
                                    currentAsset = Config.EffectAsset.RetroPixel;
                                    LoadMode(currentAsset);
                                    theMode = AssetModeLabel();
                                }
                            }
                        }
                        else
                        {
                            if (GUILayout.Button("Switch to 'External LUT Palettes' mode", EightBitSkiesGreenButton))
                            {
                                currentAsset = Config.EffectAsset.OldSchoolPixels;
                                LoadMode(currentAsset);
                                theMode = AssetModeLabel();

                            }
                        }
                        GUILayout.Label("Current color mode: " + theMode);
                        GUILayout.EndHorizontal();

                        if (currentAsset == Config.EffectAsset.RetroPixel)
                        {

                            GUILayout.Label("PROS: You will soon be able to customize your color palette from within game!   CONS: Hefty CPU cost at higher palette sizes.");
                            GUILayout.BeginHorizontal();
                            if (bits == 32)
                            {
                                if (GUILayout.Button("Max colors: 32"))
                                {
                                    bits = 64;
                                    pixRP.SetBit(64);
                                }
                            }
                            else if (bits == 64)
                            {
                                if (GUILayout.Button("Max colors: 64"))
                                {
                                    bits = 8;
                                    pixRP.SetBit(8);
                                }
                            }
                            else if (bits == 8)
                            {
                                if (GUILayout.Button("Max colors: 2-8"))
                                {
                                    /*if (pix.colorMode == RetroPixel.RPColorMode.SuperRetro)
                                    {
                                        pix.colorMode = RetroPixel.RPColorMode.Retro;
                                        pix.SetRetroColors();
                                        guiColorModeLabel = "Retro";
                                    }*/
                                    bits = 16;
                                    pixRP.SetBit(16);
                                }
                            }
                            else
                            {
                                if (GUILayout.Button("Max colors: 16"))
                                {
                                    bits = 32;
                                    pixRP.SetBit(32);
                                }
                            }


                            if (GUILayout.Button(guiColorModeLabel))
                            {
                                CycleColorMode();
                            }

                            GUILayout.EndHorizontal();
                            if (bits == 8)
                            {
                                GUILayout.Label("Colors: " + num.ToString("F0"));
                                num = GUILayout.HorizontalSlider(num, 1, 8);
                                pixRP.numColors = Mathf.RoundToInt(num);
                            }

                            
                        }


                        else
                        {
                            GUILayout.Label("PROS: Extremely lightweight performance, even at high color counts.  CONS: Due to the way it works, you cannot design color palettes from within game. (Custom palettes available soon for 'Internal Color Palette' mode!)");
                            GUILayout.Space(10);

                            GUILayout.Label("Model color after...");

                            GUILayout.Space(5f);
                            if (hasFiles)
                            {
                                
                                GUILayout.BeginHorizontal();
                                if (GUILayout.Button("Amiga AMOS"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.amos32);
                                }
                                if (GUILayout.Button("Amstrad CPC"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.amstradc);
                                }
                                if (GUILayout.Button("Apple II"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.apple2);
                                }
                                if (GUILayout.Button("8c"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.arne8);
                                }
                                if (GUILayout.Button("8c|b"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.ss);
                                }
                                if (GUILayout.Button("16c"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.arne16);
                                }
                                if (GUILayout.Button("32c"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.dp32);
                                }
                                if (GUILayout.Button("64c"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.arne64);
                                }
                                if (GUILayout.Button("256c|a"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.dp256);
                                }
                                if (GUILayout.Button("256c|b"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.hsv);
                                }
                                if (GUILayout.Button("Atari 2600"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.atari2600);
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.BeginHorizontal();

                                if (GUILayout.Button("Commodore 64"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.c64);
                                }
                                if (GUILayout.Button("NES"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.nes);
                                }

                                if (GUILayout.Button("Game Boy"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.gameboy);
                                }
                                if (GUILayout.Button("SGI DWM"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.dwm);
                                }
                                if (GUILayout.Button("MSX1"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.msx);
                                }
                                if (GUILayout.Button("MSX2"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.msx2);
                                }

                                if (GUILayout.Button("Monkey Island 2"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.monk);
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.BeginHorizontal();
                                if (GUILayout.Button("Inkscape"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.ink);
                                }

                                if (GUILayout.Button("SAM Coupe"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.sam);
                                }
                                if (GUILayout.Button("Tango"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.tango);
                                }
                                if (GUILayout.Button("Websafe"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.websafe);
                                }
                                if (GUILayout.Button("ZX Spectrum"))
                                {
                                    displayText = "";
                                    ActivateOSPColor(Config.SavedColor.zxspectrum);
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(5);
                                GUILayout.Label("Grayscale suppression threshold (Applied upon click of preset.  Higher = greater amount of conversion of grayscale to nearest color.)");
                                pixOSP.grayScaleSuppression = GUILayout.HorizontalSlider(pixOSP.grayScaleSuppression, 0, Mathf.Sqrt(3));
                                currentGamma = pixOSP.grayScaleSuppression;
                            }
                            else
                            {
                                GUILayout.Label("This mode becomes unlocked upon completion of a simple folder move.  See the 8BS Steam Workshop page for details.", EightBitSkiesGreenLabel);
                            }

                            GUILayout.Space(10);
                            


                        }
                        
                    }
                
                }


               
                
                

                if (userWantsEffect)
                {
                    GUILayout.Space(20);
                    GUILayout.Label("Quick-model pixel resolution after...");
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Microvision"))
                    {
                        displayText = "";
                        hor = 16;
                        vert = 16;

                    }
                    if (GUILayout.Button("Pocketstation"))
                    {
                        displayText = "";
                        hor = 32;
                        vert = 32;
                    }
                    if (GUILayout.Button("Etch A Sketch"))
                    {
                        displayText = "";
                        hor = 40;
                        vert = 40;
                    }
                    if (GUILayout.Button("TI-81"))
                    {
                        displayText = "";
                        hor = 96;
                        vert = 64;
                    }
                    if (GUILayout.Button("TRS-80"))
                    {
                        displayText = "";
                        hor = 128;
                        vert = 48;
                    }
                    if (GUILayout.Button("Apple II HiRes"))
                    {
                        displayText = "";
                        hor = 140;
                        vert = 192;
                    }
                    if (GUILayout.Button("Atari Lynx"))
                    {
                        displayText = "";
                        hor = 160;
                        vert = 102;
                    }
                    if (GUILayout.Button("Apple iPod Nano 6G"))
                    {
                        displayText = "";
                        hor = 240;
                        vert = 240;
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("NES"))
                    {
                        displayText = "";
                        hor = 256;
                        vert = 240;
                    }
                    if (GUILayout.Button("Master System"))
                    {
                        displayText = "";
                        hor = 256;
                        vert = 192;
                    }
                    if (GUILayout.Button("SNES"))
                    {
                        displayText = "";
                        hor = 256;
                        vert = 224;
                    }
                    if (GUILayout.Button("Virtual Boy"))
                    {
                        displayText = "";
                        hor = 384;
                        vert = 224;
                    }
                    if (GUILayout.Button("Genesis"))
                    {
                        displayText = "";
                        hor = 320;
                        vert = 224;
                    }
                    if (GUILayout.Button("Game Boy"))
                    {
                        displayText = "";
                        hor = 160;
                        vert = 144;
                    }
                    if (GUILayout.Button("GBA"))
                    {
                        displayText = "";
                        hor = 240;
                        vert = 160;
                    }
                    if (GUILayout.Button("N64/PS1"))
                    {
                        displayText = "";
                        hor = 320;
                        vert = 240;
                    }
                    if (GUILayout.Button("GameCube|PS2|Dreamcast"))
                    {
                        displayText = "";
                        hor = 640;
                        vert = 480;
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("3DS"))
                    {
                        displayText = "";
                        hor = 400;
                        vert = 240;
                    }
                    if (GUILayout.Button("PSP"))
                    {
                        displayText = "";
                        hor = 480;
                        vert = 272;
                    }
                    if (GUILayout.Button("N-Gage QD"))
                    {
                        displayText = "";
                        hor = 176;
                        vert = 208;
                    }
                    if (GUILayout.Button("Vita"))
                    {
                        displayText = "";
                        hor = 960;
                        vert = 544;
                    }
                    if (GUILayout.Button("iPhone 5"))
                    {
                        displayText = "";
                        hor = 1136;
                        vert = 640;
                    }
                    if (GUILayout.Button("PS3/360"))
                    {
                        displayText = "";
                        hor = 1280;
                        vert = 720;
                    }
                    if (GUILayout.Button("PowerBook G4"))
                    {
                        displayText = "";
                        hor = 1440;
                        vert = 960;
                    }
                    if (GUILayout.Button("iPad(3rdGen)"))
                    {
                        displayText = "";
                        hor = 2048;
                        vert = 1536;
                    }
                    if (GUILayout.Button("PS4/XB1"))
                    {
                        displayText = "Complete the PS4/XB1 effect by enabling a frame limiter.";
                        hor = 1920;
                        vert = 1080;
                    }
                    if (GUILayout.Button("Surface Pro 3"))
                    {
                        displayText = "";
                        hor = 2160;
                        vert = 1440;
                    }


                    GUILayout.EndHorizontal();
                    GUILayout.Label(displayText);

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Horizontal: " + hor.ToString("F0"), GUILayout.MaxWidth(70));
                    hor = GUILayout.HorizontalSlider(hor, 0, 3840);
                    pixRP.horizontalResolution = Mathf.RoundToInt(hor);
                    pixOSP.hor = Mathf.RoundToInt(hor);

                    GUILayout.Label("Vertical: " + vert.ToString("F0"), GUILayout.MaxWidth(70));
                    vert = GUILayout.HorizontalSlider(vert, 0, 2160);
                    pixRP.verticalResolution = Mathf.RoundToInt(vert);
                    pixOSP.vert = Mathf.RoundToInt(vert);
                    GUILayout.EndHorizontal();
                }

                if (userWantsEffect)
                {
                    GUILayout.BeginHorizontal();

                    if (GUILayout.Button("Save", EightBitSkiesGreenButton))
                    {
                        SaveConfig();
                    }
                    if (GUILayout.Button("Load"))
                    {
                        LoadConfig(true);
                        displayText = "";
                    }
                    GUILayout.Space(10);

                    if (currentAsset == Config.EffectAsset.RetroPixel)
                    {
                        GUILayout.Label("The 'Internal Color Palettes' mode is made possible by the 'Retro Pixel' shader, created by Alpaca Sound and available for use in your own projects via the Unity Asset Store!  Adapted for use in C:S by Hingo!", EightBitSkiesGreenLabel);
                    }
                    else
                    {
                        GUILayout.Label("The 'External LUT Palettes' mode is made possible by the 'Old School Pixel FX' shader, created by Peculiar Developer and available for use in your own projects via the Unity Asset Store!  Adapted for use in C:S by Hingo!", EightBitSkiesGreenLabel);
                    }
                    GUILayout.EndHorizontal();
                }
                
            }
               

            if (tab == Config.Tab.Hotkey)
            {
                ResizeWindow(575, 480);
                GUILayout.Label("Set config-toggle hotkey below: ");
                KeyboardGrid(0);
                if (menuToggle != KeyCode.None)
                    GUILayout.Label("Current 'Config' hotkey: " + menuToggle);
                else
                    GUILayout.Label("No config hotkey is bound to 8 Bit Skies.");
                GUILayout.Space(7f);
                GUILayout.Label("Set effect-toggle hotkey below: ");
                KeyboardGrid(1);
                if (effectToggle != KeyCode.None)
                    GUILayout.Label("Current 'Effect Toggle' hotkey: " + effectToggle);
                else
                    GUILayout.Label("No effect-toggle hotkey is bound to 8 Bit Skies.");
            }
            
            

            


                










            dragBar.width = windowRect.width;
            windowLoc.x = windowRect.x;
            windowLoc.y = windowRect.y;
        }
        



        string AssetModeLabel()
        {
            switch (currentAsset)
            {
                case Config.EffectAsset.OldSchoolPixels:
                    return "External LUT Palettes";
                case Config.EffectAsset.RetroPixel:
                    return "Internal Color Palettes";
                default:
                    return "";
            }
        }

        void MakeTex(string fileNameInFilesFolder, int width, int height)
        {
            Texture2D output = new Texture2D(width, height);
            try
            {
                byte[] data = File.ReadAllBytes(DataLocation.modsPath + @"\EightBitSkies\LUTs\" + fileNameInFilesFolder);
                output.LoadImage(data);
                pixOSP.Convert(output);
            }
            catch
            {
                return;
            }
        }











        void KeyboardGrid(int purpose)
        {
            GUILayout.BeginHorizontal();
            Hotkey("F5", KeyCode.F5, purpose);
            Hotkey("F6", KeyCode.F6, purpose);
            Hotkey("F7", KeyCode.F7, purpose);
            Hotkey("F8", KeyCode.F8, purpose);
            Hotkey("F9", KeyCode.F9, purpose);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            Hotkey("F10", KeyCode.F10, purpose);
            Hotkey("F11", KeyCode.F11, purpose);
            Hotkey("F12", KeyCode.F12, purpose);
            Hotkey("[", KeyCode.LeftBracket, purpose);
            Hotkey("]", KeyCode.RightBracket, purpose);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            Hotkey("=", KeyCode.Equals, purpose);
            Hotkey("Slash", KeyCode.Slash, purpose);
            Hotkey("Backslash", KeyCode.Backslash, purpose);
            Hotkey("Home", KeyCode.Home, purpose);
            Hotkey("End", KeyCode.End, purpose);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            Hotkey("Numpad *", KeyCode.KeypadMultiply, purpose);
            Hotkey("Numpad -", KeyCode.KeypadMinus, purpose);
            Hotkey("Numpad =", KeyCode.KeypadEquals, purpose);
            Hotkey("Numpad +", KeyCode.KeypadPlus, purpose);
            Hotkey("Numpad /", KeyCode.KeypadDivide, purpose);
            GUILayout.EndHorizontal();

        }
        void ResizeWindow(int width, int height)
        {
            if (windowRect.height != height)
                windowRect.height = height;
            if (windowRect.width != width)
                windowRect.width = width;

        }
        string KeyToString(KeyCode kc)
        {
            switch (kc)
            {
                case KeyCode.F5:
                    return "F5";
                case KeyCode.F6:
                    return "F6";
                case KeyCode.F7:
                    return "F7";
                case KeyCode.F8:
                    return "F8";
                case KeyCode.F9:
                    return "F9";
                case KeyCode.F10:
                    return "F10";
                case KeyCode.F11:
                    return "F11";
                case KeyCode.F12:
                    return "F12";
                case KeyCode.LeftBracket:
                    return "LeftBracket";
                case KeyCode.RightBracket:
                    return "RightBracket";
                case KeyCode.Equals:
                    return "=";
                case KeyCode.Slash:
                    return "Slash";
                case KeyCode.Backslash:
                    return "Backslash";
                case KeyCode.Home:
                    return "Home";
                case KeyCode.End:
                    return "End";
                case KeyCode.KeypadDivide:
                    return "Numpad /";
                case KeyCode.KeypadMultiply:
                    return "Numpad *";
                case KeyCode.KeypadMinus:
                    return "Numpad -";
                case KeyCode.KeypadPlus:
                    return "Numpad +";
                case KeyCode.KeypadEquals:
                    return "Numpad =";
                default:
                    return kc.ToString();
            }

        }

        private string menuKeystring = "";
        private string effectKeyString = "";
        void Hotkey(string label, KeyCode keycode, int purpose)
        {
            if (GUILayout.Button(label, GUILayout.MaxWidth(100)))
            {
                switch (purpose)
                {
                    case 0:
                        menuToggle = keycode;
                        menuKeystring = label;
                        break;
                    case 1:
                        effectToggle = keycode;
                        effectKeyString = label;
                        break;
                    default:
                        break;
                }
                SaveConfig();
            }
        }

        void Update()
        {
            if (Input.GetKeyUp(menuToggle))
                showSettingsPanel = !showSettingsPanel;
            if (Input.GetKeyUp(effectToggle))
            {
                userWantsEffect = !userWantsEffect;
                if (!userWantsEffect)
                {

                    pixOSP.enabled = false;
                    pixRP.enabled = false;
                }
                else
                {
                    LoadMode(currentAsset);
                }
            }

            
            
        }


       
    }
}
