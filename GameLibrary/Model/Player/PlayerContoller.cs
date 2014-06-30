using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using GameLibrary.Model.Object;
namespace GameLibrary.Model.Player
{
    public class PlayerContoller
    {
        public static PlayerContoller playerContoller = new PlayerContoller();

        private List<InputAction> inputActions;

        public PlayerContoller()
        {
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
