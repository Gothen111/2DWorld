﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.Gui
{
    public class Container : Component
    {
        private List<Component> components;

        public List<Component> Components
        {
            get { return components; }
            set { components = value; }
        }

        private ContainerStrategy.Strategy strategy;

		public ContainerStrategy.Strategy Strategy
        {
            get { return strategy; }
            set { strategy = value; }
        }

        public Container() : base()
        {
            components = new List<Component>();
        }

        public Container(Rectangle _Bounds) : base(_Bounds)
        {
            components = new List<Component>();
        }

		public virtual void add(Component _Component)
        {
            if (this.strategy != null && !this.strategy.checkComponent(_Component))
            {
                Logger.Logger.LogInfo("Strategy " + this.strategy.ToString() + " verhindert das Hinzufügen von " + _Component.ToString());
                return;
            }
            this.components.Add(_Component);
        }

		public virtual void remove(Component _Component)
        {
            if(this.components.Contains(_Component))
                this.components.Remove(_Component);
        }

        public override void release()
        {
            base.release();
            foreach (Component var_Component in this.components)
            {
                var_Component.release();
            }
        }

        public override void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            base.draw(_GraphicsDevice, _SpriteBatch);

            if (this.BackgroundGraphicPath != null && !this.BackgroundGraphicPath.Equals(""))
            {
                _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.BackgroundGraphicPath], new Vector2(this.Bounds.X, this.Bounds.Y), Color.White);
            }
            foreach (Component var_Component in this.components)
            {
                var_Component.draw(_GraphicsDevice, _SpriteBatch);
            }
        }
    }
}
