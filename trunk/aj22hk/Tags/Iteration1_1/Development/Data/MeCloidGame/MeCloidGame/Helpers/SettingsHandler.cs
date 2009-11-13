﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Input;

namespace MeCloidGame.Helpers
{
    [Serializable]
    public struct DisplaySettings
    {
        public bool FullScreen;
        public int WindowWidth;
        public int WindowHeight;
    }

    [Serializable]
    public struct KeyboardSettings
    {
        public Keys A;
        public Keys B;
        public Keys X;
        public Keys Y;

        public Keys LeftShoulder;
        public Keys RightShoulder;

        public Keys Start;
        public Keys Back;

        public Keys LeftTrigger;
        public Keys RightTrigger;

        public Keys LeftStick;
        public Keys RightStick;

        public Keys DPadDown;
        public Keys DPadLeft;
        public Keys DPadRight;
        public Keys DPadUp;

        public Keys LeftThumbstickDown;
        public Keys LeftThumbstickLeft;
        public Keys LeftThumbstickRight;
        public Keys LeftThumbstickUp;

        public Keys RightThumbstickDown;
        public Keys RightThumbstickLeft;
        public Keys RightThumbstickRight;
        public Keys RightThumbstickUp;
    }

    public static class SettingsHandler
    {
        public static KeyboardSettings ReadKeyboardSettings(string a_filename)
        {
            KeyboardSettings keyboardSettings;
            Stream stream = File.OpenRead(a_filename);
            XmlSerializer serializer = new XmlSerializer(typeof(KeyboardSettings));

            keyboardSettings = (KeyboardSettings)serializer.Deserialize(stream);

            return keyboardSettings;
        }

        public static Dictionary<Buttons, Keys> GetKeyboardDictionary(KeyboardSettings a_keyboardSettings)
        {
            Dictionary<Buttons, Keys> dictionary = new Dictionary<Buttons, Keys>();

            dictionary.Add(Buttons.A, a_keyboardSettings.A);
            dictionary.Add(Buttons.B, a_keyboardSettings.B);
            dictionary.Add(Buttons.X, a_keyboardSettings.X);
            dictionary.Add(Buttons.Y, a_keyboardSettings.Y);

            dictionary.Add(Buttons.LeftShoulder, a_keyboardSettings.LeftShoulder);
            dictionary.Add(Buttons.RightShoulder, a_keyboardSettings.RightShoulder);

            dictionary.Add(Buttons.Start, a_keyboardSettings.Start);
            dictionary.Add(Buttons.Back, a_keyboardSettings.Back);

            dictionary.Add(Buttons.LeftTrigger, a_keyboardSettings.LeftTrigger);
            dictionary.Add(Buttons.RightTrigger, a_keyboardSettings.RightTrigger);

            dictionary.Add(Buttons.LeftStick, a_keyboardSettings.LeftStick);
            dictionary.Add(Buttons.RightStick, a_keyboardSettings.RightStick);

            dictionary.Add(Buttons.DPadDown, a_keyboardSettings.DPadDown);
            dictionary.Add(Buttons.DPadLeft, a_keyboardSettings.DPadLeft);
            dictionary.Add(Buttons.DPadRight, a_keyboardSettings.DPadRight);
            dictionary.Add(Buttons.DPadUp, a_keyboardSettings.DPadUp);

            dictionary.Add(Buttons.LeftThumbstickDown, a_keyboardSettings.LeftThumbstickDown);
            dictionary.Add(Buttons.LeftThumbstickLeft, a_keyboardSettings.LeftThumbstickLeft);
            dictionary.Add(Buttons.LeftThumbstickRight, a_keyboardSettings.LeftThumbstickRight);
            dictionary.Add(Buttons.LeftThumbstickUp, a_keyboardSettings.LeftThumbstickUp);

            dictionary.Add(Buttons.RightThumbstickDown, a_keyboardSettings.RightThumbstickDown);
            dictionary.Add(Buttons.RightThumbstickLeft, a_keyboardSettings.RightThumbstickLeft);
            dictionary.Add(Buttons.RightThumbstickRight, a_keyboardSettings.RightThumbstickRight);
            dictionary.Add(Buttons.RightThumbstickUp, a_keyboardSettings.RightThumbstickUp);

            return dictionary;
        }
    }
}
