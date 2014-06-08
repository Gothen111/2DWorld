using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class PlayerObject : FactionObject
    {
        public static PlayerObject playerObject;

        public PlayerObject() :base()
        {

        }

        public PlayerObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }
        public override void update()
        {
            if (this.NeedUpdate)
            {
                base.update();

                if (Configuration.Configuration.isHost)
                {
                    GameLibrary.Connection.Event.EventList.Add(new Connection.Event(new Connection.Message.UpdateLivingObjectMessage(this), Connection.GameMessageImportance.VeryImportant));
                }

                //Console.WriteLine("UPDATE");
                //Console.WriteLine(this.Animation.ToString() + " " + this.Animation.finishedAnimation());
            }
        }
    }
}
