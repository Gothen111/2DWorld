﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameLibrary.Gui
{
    class Container : Component
    {
        private List<Component> components;

        public List<Component> Components
        {
            get { return components; }
            set { components = value; }
        }

        private ContainerStrategy.Strategy strategy;

        internal ContainerStrategy.Strategy Strategy
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

        public void add(Component _Component)
        {
            if (this.strategy != null && !this.strategy.checkComponent(_Component))
            {
                Logger.Logger.LogInfo("Strategy " + this.strategy.ToString() + " verhindert das Hinzufügen von " + _Component.ToString());
                return;
            }
            this.components.Add(_Component);
        }

        public void remove(Component _Component)
        {
            if(this.components.Contains(_Component))
                this.components.Remove(_Component);
        }
    }
}