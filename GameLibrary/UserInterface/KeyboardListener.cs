using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace GameLibrary.UserInterface
{
    public interface KeyboardListener
    {
        void keyboardButtonClicked(Keys buttonPressed);
        void keyboardButtonReleased(Keys buttonReleased);
    }
}
