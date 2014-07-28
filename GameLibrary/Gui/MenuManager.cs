using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GameLibrary.Gui.Menu;

namespace GameLibrary.Gui
{
    public class MenuManager
    {
        public static MenuManager menuManager = new MenuManager();

        private Container activeContainer;

        public Container ActiveContainer
        {
            get { return activeContainer; }
            set { activeContainer = value; }
        }

        private MenuManager()
        {
            this.setMenu(new StartMenu());
        }

        public void setMenu(Container _Menu)
        {
            if(this.activeContainer!=null)
            {
                this.activeContainer.close();
            }
            this.activeContainer = _Menu;
        }
    }
}
