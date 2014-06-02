using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.UserInterface.MouseEnum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameLibrary.Peripherals
{
    public class MouseManager
    {
        public static MouseManager mouseManager = new MouseManager();

        public static List<UserInterface.MouseListener> mouseFocus = new List<UserInterface.MouseListener>();

        List<MouseEnum> mouseKeysPressed;

        private Vector2 oldMousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

        private MouseManager()
        {
            mouseKeysPressed = new List<MouseEnum>();
        }

        public void update()
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!mouseKeysPressed.Contains(MouseEnum.Left))
                {
                    mouseKeysPressed.Add(MouseEnum.Left);
                    notifyMouseFocusAboutClickEvent(MouseEnum.Left, new Vector2(mouseState.X, mouseState.Y));
                }
            }
            else
            {
                if (mouseKeysPressed.Contains(MouseEnum.Left))
                {
                    mouseKeysPressed.Remove(MouseEnum.Left);
                    notifyMouseFocusAboutReleaseEvent(MouseEnum.Left, new Vector2(mouseState.X, mouseState.Y));
                }
            }

            if (mouseState.MiddleButton == ButtonState.Pressed)
            {
                if (!mouseKeysPressed.Contains(MouseEnum.Middle))
                {
                    mouseKeysPressed.Add(MouseEnum.Middle);
                    notifyMouseFocusAboutClickEvent(MouseEnum.Middle, new Vector2(mouseState.X, mouseState.Y));
                }
            }
            else
            {
                if (mouseKeysPressed.Contains(MouseEnum.Middle))
                {
                    mouseKeysPressed.Remove(MouseEnum.Middle);
                    notifyMouseFocusAboutReleaseEvent(MouseEnum.Middle, new Vector2(mouseState.X, mouseState.Y));
                }
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                if (!mouseKeysPressed.Contains(MouseEnum.Right))
                {
                    mouseKeysPressed.Add(MouseEnum.Right);
                    notifyMouseFocusAboutClickEvent(MouseEnum.Right, new Vector2(mouseState.X, mouseState.Y));
                }
            }
            else
            {
                if (mouseKeysPressed.Contains(MouseEnum.Right))
                {
                    mouseKeysPressed.Remove(MouseEnum.Right);
                    notifyMouseFocusAboutReleaseEvent(MouseEnum.Right, new Vector2(mouseState.X, mouseState.Y));
                }
            }

            if (mouseState.X != oldMousePosition.X || mouseState.Y != oldMousePosition.Y)
            {
                oldMousePosition.X = mouseState.X;
                oldMousePosition.Y = mouseState.Y;
                notifyMouseFocusAboutMoveEvent(oldMousePosition);
            }


            
        }

        private void notifyMouseFocusAboutClickEvent(UserInterface.MouseEnum.MouseEnum mouseButton, Vector2 position)
        {
            foreach (UserInterface.MouseListener listener in mouseFocus)
            {
                listener.mouseClicked(mouseButton, position);
            }
        }

        private void notifyMouseFocusAboutReleaseEvent(UserInterface.MouseEnum.MouseEnum mouseButton, Vector2 position)
        {
            foreach (UserInterface.MouseListener listener in mouseFocus)
            {
                listener.mouseReleased(mouseButton, position);
            }
        }

        private void notifyMouseFocusAboutMoveEvent(Vector2 position)
        {
            foreach (UserInterface.MouseListener listener in mouseFocus)
            {
                listener.mouseMoved(position);
            }
        }

    }
}
