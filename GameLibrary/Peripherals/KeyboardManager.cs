using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace GameLibrary.Peripherals
{
    public class KeyboardManager
    {
        public static KeyboardManager keyboardManager = new KeyboardManager();

        public static List<UserInterface.KeyboardListener> keyboardFocus = new List<UserInterface.KeyboardListener>();

        List<Keys> keysPressed;

        private KeyboardManager()
        {
            keysPressed = new List<Keys>();
        }

        public void update()
        {
            foreach (Keys key in Keyboard.GetState().GetPressedKeys())
            {
                if (!keysPressed.Contains(key))
                {
                    keysPressed.Add(key);
                    notifyKeyboardFocusAboutClickEvent(key);
                }
            }

            List<Keys> keysToRemove = new List<Keys>();
            foreach (Keys key in keysPressed)
            {
                if (!Keyboard.GetState().IsKeyDown(key))
                {
                    keysToRemove.Add(key);
                }
            }

            foreach (Keys key in keysToRemove)
            {
                keysPressed.Remove(key);
                notifyKeyboardFocusAboutReleaseEvent(key);
            }
        }

        private void notifyKeyboardFocusAboutClickEvent(Keys key)
        {
            foreach (UserInterface.KeyboardListener listener in keyboardFocus)
            {
                listener.keyboardButtonClicked(key);
            }
        }

        private void notifyKeyboardFocusAboutReleaseEvent(Keys key)
        {
            foreach (UserInterface.KeyboardListener listener in keyboardFocus)
            {
                listener.keyboardButtonReleased(key);
            }
        }
    }
}
