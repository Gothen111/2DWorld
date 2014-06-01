using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Model.Object
{
    public class PlayerObject : FactionObject
    {
        public static PlayerObject playerObject;
        public override void update()
        {
            base.update();

            if (Configuration.Configuration.isHost)
            {
                GameLibrary.Connection.Event.EventList.Add(new Connection.Event(new Connection.Message.UpdateLivingObjectMessage(this), Connection.GameMessageImportance.VeryImportant));
            }
        }
    }
}
