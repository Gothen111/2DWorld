using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Map.Region;
using Lidgren.Network;
using Lidgren.Network.Xna;

using GameLibrary.Model.Map.World;

namespace GameLibrary.Connection.Message
{
    public class UpdateWorldMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateWorldMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateWorldMessage(World _World)
        {
            this.MessageTime = NetTime.Now;
            this.World = _World;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public World World { get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateWorldMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            this.World = Util.Serializer.DeserializeObjectFromString<World>(im.ReadString());
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            om.Write(Util.Serializer.SerializeObjectToString(this.World));
        }

        #endregion
    }
}
