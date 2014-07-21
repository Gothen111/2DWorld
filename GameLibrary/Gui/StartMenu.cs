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
            this.AllowMultipleFocus = true;

            this.serverIPTextField = new TextField(new Rectangle(200, 100, 289, 85));
            this.serverIPTextField.BackgroundGraphicPath = "Gui/TextField";
            this.add(this.serverIPTextField);
            this.serverPortTextField = new TextField(new Rectangle(200, 200, 289, 85));
            this.serverPortTextField.BackgroundGraphicPath = "Gui/TextField";
            this.add(this.serverPortTextField);
            this.connectServerButton = new Button(new Rectangle(200, 300, 289, 85));
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
