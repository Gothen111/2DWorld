using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GameLibrary.UserInterface
{
    public interface MouseListener
    {
        bool mouseClicked(MouseEnum.MouseEnum mouseButtonClicked, Vector2 position);
        void mouseReleased(MouseEnum.MouseEnum mouseButtonReleased, Vector2 position);
        void mouseMoved(Vector2 position);
        void onClick(MouseEnum.MouseEnum mouseButton);
    }
}
