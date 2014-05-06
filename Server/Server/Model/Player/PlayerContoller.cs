﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Server.Model.Object;
namespace Server.Model.Player
{
    class PlayerContoller
    {
        public static PlayerContoller playerContoller = new PlayerContoller();

        //private PlayerObject playerObject;
        private List<InputAction> inputActions;

        public PlayerContoller()//PlayerObject _PlayerObject)
        {
            //this.playerObject = _PlayerObject;
            this.inputActions = new List<InputAction>();
        }

        public void update()
        {
            foreach (InputAction var_InputAction in this.inputActions)
            {
                if (var_InputAction.wantsToPeformAction())
                {
                    var_InputAction.performAction();
                }
                else
                {
                    var_InputAction.stopAction();
                }
            }
        }

        public void addInputAction(InputAction _InputAction)
        {
            this.inputActions.Add(_InputAction);
        }

        public void clearInputActions()
        {
            this.inputActions.Clear();
        }
    }
}