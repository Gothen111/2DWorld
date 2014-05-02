using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Server.Model.Object;
namespace Server.Model.Player
{
    class PlayerContoller
    {
        private PlayerObject playerObject;
        private List<InputAction> inputActions;

        public PlayerContoller(PlayerObject _PlayerObject)
        {
            this.playerObject = _PlayerObject;
            this.inputActions = new List<InputAction>();
        }

        public void update()
        {
            foreach (InputAction var_InputAction in this.inputActions)
            {
                var_InputAction.performAction();
            }
        }

        private void perfomFirstAction()
        {
            if (this.inputActions.Count > 0)
            {
                this.inputActions.ElementAt(0).performAction();
                this.inputActions.RemoveAt(0);
            }
        }

        public void addInputAction(InputAction _InputAction)
        {
            this.inputActions.Add(_InputAction);
        }
    }
}
