using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameLibrary.Gui
{
    class ContainerManager
    {
        public static ContainerManager containerManager = new ContainerManager();

        private Container activeContainer;

        public Container ActiveContainer
        {
            get { return activeContainer; }
            set { activeContainer = value; }
        }

        Container mainMenu;

        private ContainerManager()
        {
            this.createMainMenu();
        }

        public void createMainMenu()
        {
            this.mainMenu = new Container(new Rectangle(0,0,500,500));
            Button var_Button = new Button(new Rectangle(100,100,50,50));
            var_Button.Action = 
            this.mainMenu.add(var_Button);
        }
    }
}
