using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameLibrary.UserInterface;
using GameLibrary.UserInterface.MouseEnum;

using Microsoft.Xna.Framework.Graphics;

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

        private bool isPressed;

        public bool IsPressed
        {
            get { return isPressed; }
            set { isPressed = value; }
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

        private String backgroundGraphicPath;

        public String BackgroundGraphicPath
        {
            get { return backgroundGraphicPath; }
            set { backgroundGraphicPath = value; }
        }

        public Component()
        {
            Peripherals.MouseManager.mouseFocus.Add(this);
            IsFocusAble = true;
            isVisible = true;
        }

        public Component(Rectangle _Bounds) : this()
        {
            this.bounds = _Bounds;
        }

        public virtual bool mouseClicked(MouseEnum mouseButtonClicked, Vector2 position)
        {
            if (position.X >= bounds.Left && position.X <= bounds.Right && position.Y >= bounds.Top && position.Y <= bounds.Bottom)
            {
                this.isPressed = true;
                return true;
            }
            else
            {
                this.IsFocused = false;
                this.isPressed = false;
                GameLibrary.Peripherals.KeyboardManager.keyboardFocus.Remove(this);
                return false;
            }
        }

        public virtual bool mouseReleased(MouseEnum mouseButtonReleased, Vector2 position)
        {
            Logger.Logger.LogInfo("X: " + position.X + " Y: " + position.Y);
            if (position.X >= bounds.Left && position.X <= bounds.Right && position.Y >= bounds.Top && position.Y <= bounds.Bottom)
            {
                if (this.isPressed)
                {
                    onClick(mouseButtonReleased, position);
                    return true;
                }
            }
            else
            {
                this.IsFocused = false;
                this.isPressed = false;
                GameLibrary.Peripherals.KeyboardManager.keyboardFocus.Remove(this);
            }
            return false;
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
                this.isPressed = false;
            }
        }

        public virtual void onClick(MouseEnum mouseButton, Vector2 _MousePosition)
        {
            this.IsFocused = true;
            if (!GameLibrary.Peripherals.KeyboardManager.keyboardFocus.Contains(this))
            {
                if (!allowMultipleFocus)
                    GameLibrary.Peripherals.KeyboardManager.keyboardFocus.Clear();
                GameLibrary.Peripherals.KeyboardManager.keyboardFocus.Add(this);
            }
        }

        public virtual void keyboardButtonClicked(Keys buttonPressed)
        {

        }

        public virtual void keyboardButtonReleased(Keys buttonReleased)
        {

        }

        public virtual void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            if (this.backgroundGraphicPath != null && !this.backgroundGraphicPath.Equals(""))
            {
                if (!this.IsHovered)
                {
                    _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath], new Vector2(this.Bounds.X, this.Bounds.Y), Color.White);
                }
                else
                {
                    if (!this.isPressed)
                    {
                        try
                        {
                            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath + "_Hover"], new Vector2(this.Bounds.X, this.Bounds.Y), Color.White);
                        }
                        catch (Exception e)
                        {
                            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath], new Vector2(this.Bounds.X, this.Bounds.Y), Color.White);
                        }
                    }
                    else
                    {
                        try
                        {
                            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath + "_Pressed"], new Vector2(this.Bounds.X, this.Bounds.Y), Color.White);
                        }
                        catch (Exception e)
                        {
                            try
                            {
                                _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath + "_Hover"], new Vector2(this.Bounds.X, this.Bounds.Y), Color.White);
                            }
                            catch (Exception f)
                            {
                                _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.backgroundGraphicPath], new Vector2(this.Bounds.X, this.Bounds.Y), Color.White);
                            }
                        }
                    }
                }
            }
        }
    }
}
