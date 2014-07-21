using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameLibrary.Gui
{
    public class ContainerManager
    {
        public static ContainerManager containerManager = new ContainerManager();

        private Container activeContainer;

        public Container ActiveContainer
        {
            get { return activeContainer; }
            set { activeContainer = value; }
        }

        private ContainerManager()
        {
            this.setMenu(new StartMenu());
        }

        public void setMenu(Container _Menu)
        {
            this.activeContainer = _Menu;
        }
    }
}
