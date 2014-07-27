using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.Gui
{
	public class ListView : Container
    {
		private Component lastSelected;

		public ListView()
            : base()
        {

        }

		public ListView(Rectangle _Bounds)
            : base(_Bounds)
        {

        }

		//TODO: Lässt sich bestimmt per Strategie machen ;)
		public void addAtTop(Component _Component)
		{
			_Component.Bounds.X = this.Bounds.X;
			_Component.Bounds.Y = this.Bounds.Y;

			foreach (Component var_Component in this.Components)
			{
				var_Component.Bounds.X += _Component.Bounds.Width;
				var_Component.Bounds.Y += _Component.Bounds.Height;
			}

			this.add(_Component);
		}

		public void addAtBottom(Component _Component)
		{
			float var_PositionY = this.Bounds.Y;

			foreach (Component var_Component in this.Components)
			{
				if (var_Component.Bounds.Y + var_Component.Bounds.Height >= var_PositionY) 
				{
					var_PositionY = var_Component.Bounds.Y + var_Component.Bounds.Height;
				}
			}

			_Component.Bounds.X = this.Bounds.X;
			_Component.Bounds.Y = var_PositionY;

			this.add(_Component);
		}
			
		public Component getSelectedComponent()
		{
			return this.lastSelected;
		}

		public override void onClick(UserInterface.MouseEnum.MouseEnum mouseButton, Vector2 _MousePosition)
		{
			base.onClick(mouseButton, _MousePosition);

			this.lastSelected = null;

			//TODO: Texture / Background anpassen!
			foreach (Component var_Component in this.Components)
			{
				// VLL auch mit isPressed ;) da mouse event öfters als einmal abegefeuert wird usw...
				if (position.X >= var_Component.Bounds.Left && position.X <= var_Component.Bounds.Right && position.Y >= var_Component.Bounds.Top && position.Y <= var_Component.Bounds.Bottom)
				{
					this.lastSelected = var_Component;
				}
			}
		}
    }
}
