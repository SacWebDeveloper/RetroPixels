using System;
using System.Collections.Generic;
using UnityEngine;
using ICities;
using ColossalFramework;

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
        RetroPixel pix;
        KeyCode menuToggle;
        KeyCode effectToggle;
        public Config.Tab tab;

        private Color EightBitSkiesLightGreenColor;
        public GUIStyle EightBitSkiesGreenButton;


        bool userWantsEffect = false;

        float vert = 256;
        float hor = 256;
        float num = 4;
        bool useActualColors = false;

        void Awake()
        {
            EightBitSkiesLightGreenColor = new Color(0.6f, 1.0f, 0.6f);
            config = Config.Deserialize(configPath);
            InitializeFromConfig();
        }

        void Start()
        {
            pix = GetComponent<RetroPixel>();
            LoadConfig(false);
            if (config.menuToggle == KeyCode.None)
                config.menuToggle = KeyCode.Quote;
            if (useActualColors)
                pix.SetActualColors(true);
            else
                pix.SetActualColors(false);
            SaveConfig();
        }

        void InitializeFromConfig()
        {
            if (config == null)
            {
                config = new Config();

                config.windowLoc = new Vector2(64, 64);
                config.vert = 256;
                config.hor = 256;
                config.num = 4;
                config.useActualColors = false;

                config.menuToggle = KeyCode.Quote;
                config.effectToggle = KeyCode.None;
                config.userWantsEffect = false;
            }
            else
            {
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

        void OnGUI()
        {
            if (EightBitSkiesGreenButton == null)
            {
                EightBitSkiesGreenButton = new GUIStyle(GUI.skin.button);
                EightBitSkiesGreenButton.normal.textColor = EightBitSkiesLightGreenColor;
                EightBitSkiesGreenButton.fontStyle = FontStyle.Bold;
            }
            if (showSettingsPanel)
            {
                windowRect = GUI.Window(391436, windowRect, SettingsPanel, "RetroPixels (NEW: Draggable Window!");
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
                ResizeWindow(730, 380);

                userWantsEffect = GUILayout.Toggle(userWantsEffect, "Toggle Effect On/Off");
                if (!useActualColors)
                {
                    if (GUILayout.Button("(NEW) Click to disable color overriding. (THIS IS AWESOME!!!!!)"))
                    {
                        useActualColors = true;
                        pix.SetActualColors(true);
                    }
                }
                else
                {
                    if (GUILayout.Button("(NEW) Click to enable color overriding."))
                    {
                        useActualColors = false;
                        pix.SetActualColors(false);
                    }
                }

                GUILayout.Label("Horizontal: " + hor.ToString("F0"));
                hor = GUILayout.HorizontalSlider(hor, 0, 3840);
                pix.horizontalResolution = Mathf.RoundToInt(hor);

                GUILayout.Label("Vertical: " + vert.ToString("F0"));
                vert = GUILayout.HorizontalSlider(vert, 0, 2160);
                pix.verticalResolution = Mathf.RoundToInt(vert);

                GUILayout.Label("Colors: " + num.ToString("F0"));
                num = GUILayout.HorizontalSlider(num, 1, 8);
                pix.numColors = Mathf.RoundToInt(num);


                GUILayout.Space(10f);
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
                GUILayout.Space(45f);
                GUILayout.Label("Set effect-toggle hotkey below: ");
                KeyboardGrid(1);
                if (effectToggle != KeyCode.None)
                    GUILayout.Label("Current 'Effect Toggle' hotkey: " + effectToggle);
                else
                    GUILayout.Label("No effect-toggle hotkey is bound to 8 Bit Skies.");
            }

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

            GUILayout.EndHorizontal();
            


                










            dragBar.width = windowRect.width;
            windowLoc.x = windowRect.x;
            windowLoc.y = windowRect.y;
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
                userWantsEffect = !userWantsEffect;

            if (userWantsEffect)
            {
                if (!pix.enabled)
                    pix.enabled = true;
            }
            else
            {
                if (pix.enabled)
                    pix.enabled = false;
            }
            
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
            
            menuToggle = config.menuToggle;
            effectToggle = config.effectToggle;
            hor = config.hor;
            vert = config.vert;
            num = config.num;
            useActualColors = config.useActualColors;
        }
    }
}
