using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameLibrary.Commands.CommandTypes;
namespace GameLibrary.Model.Player
{
    public class InputAction
    {
        //private MouseState mouseState;
      
        private List<Keys> keysToWatchOn;
        private Command command;
        public InputAction(List<Keys> _KeysToWatchOn, Command _Command)
        {
            this.keysToWatchOn = _KeysToWatchOn;
            this.command = _Command;
        }
        public bool wantsToPeformAction()
        {
            if(this.keysToWatchOn!=null)
            {
                foreach(Keys key in this.keysToWatchOn)
                {
                    if (Keyboard.GetState().IsKeyUp(key))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        public void performAction()
        {
            if (this.command != null)
            {
                this.command.doCommand();
            }
        }
        public void stopAction()
        {
            if (this.command != null)
            {
                this.command.stopCommand();
            }
        }
    }
}
