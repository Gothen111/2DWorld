using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameLibrary.UserInterface;
using GameLibrary.UserInterface.MouseEnum;

namespace GameLibrary.Gui
{
    public class Component : MouseListener, KeyboardListener
    {
        private Rectangle bounds;

        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        private bool isFocusAble;

        public bool IsFocusAble
        {
            get { return isFocusAble; }
            set { isFocusAble = value; this.IsFocused = this.IsFocused; }
        }

        private bool isFocused;

        public bool IsFocused
        {
            get { return isFocused; }
            set { isFocused = value && IsFocusAble && IsVisible; }
        }

        private bool isHovered;

        public bool IsHovered
        {
            get { return isHovered; }
            set { isHovered = value; }
        }

        private bool isVisible;

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        private bool allowMultipleFocus;

        public bool AllowMultipleFocus
        {
            get { return allowMultipleFocus; }
            set { allowMultipleFocus = value; }
        }

        public Component()
        {
            Peripherals.MouseManager.mouseFocus.Add(this);
        }

        public Component(Rectangle _Bounds) : this()
        {
            this.bounds = _Bounds;
        }

        public bool mouseClicked(MouseEnum mouseButtonClicked, Vector2 position)
        {
            if (position.X >= bounds.Left && position.X <= bounds.Right && position.Y >= bounds.Top && position.Y <= bounds.Bottom)
            {
                onClick(mouseButtonClicked);
                return true;
            }
            else
            {
                this.IsFocused = false;
                GameLibrary.Peripherals.KeyboardManager.keyboardFocus.Remove(this);
                return false;
            }
        }

        public void mouseReleased(MouseEnum mouseButtonReleased, Vector2 position)
        {

        }

        public void mouseMoved(Vector2 position)
        {
            if (position.X >= bounds.Left && position.X <= bounds.Right && position.Y >= bounds.Top && position.Y <= bounds.Bottom)
            {
                this.IsHovered = true;
            }
            else
            {
                this.IsHovered = false;
            }
        }

        public void onClick(MouseEnum mouseButton)
        {
            this.IsFocused = true;
            if (!GameLibrary.Peripherals.KeyboardManager.keyboardFocus.Contains(this))
            {
                if (!allowMultipleFocus)
                    GameLibrary.Peripherals.KeyboardManager.keyboardFocus.Clear();
                GameLibrary.Peripherals.KeyboardManager.keyboardFocus.Add(this);
            }
        }

        public void keyboardButtonClicked(Keys buttonPressed)
        {

        }

        public void keyboardButtonReleased(Keys buttonReleased)
        {

        }
    }
}
