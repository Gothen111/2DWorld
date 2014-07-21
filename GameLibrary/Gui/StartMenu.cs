using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameLibrary.Gui
{
    public class StartMenu : Container
    {
        TextField serverIPTextField;
        TextField serverPortTextField;
        Button connectServerButton;

        public StartMenu()
        {
            this.serverIPTextField = new TextField(new Rectangle(100, 100, 100, 20));
            this.add(this.serverIPTextField);
            this.serverPortTextField = new TextField(new Rectangle(200, 100, 100, 20));
            this.add(this.serverPortTextField);
            this.connectServerButton = new Button(new Rectangle(200, 100, 50, 20));
            this.connectServerButton.BackgroundGraphicPath = "Gui/Button";
            this.add(this.connectServerButton);
            this.connectServerButton.Action = connectToServer;
        }

        public void connectToServer()
        {
            Configuration.Configuration.networkManager.Start(this.serverIPTextField.Text, this.serverPortTextField.Text);
        }
    }
}
