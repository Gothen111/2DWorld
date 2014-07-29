using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;
using Lidgren.Network.Xna;

using GameLibrary.Model.Object;

namespace GameLibrary.Connection.Message
{
    public class RequestPlayerMessage : IGameMessage
    {
        #region Constructors and Destructors

        public RequestPlayerMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public RequestPlayerMessage(PlayerObject _PlayerObject)
        {
            this.PlayerObject = _PlayerObject;
            this.MessageTime = NetTime.Now;
        }

        #endregion

        #region Properties

        public PlayerObject PlayerObject { get; set; }

        public double MessageTime { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.RequestPlayerMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.PlayerObject = Util.Serializer.DeserializeObjectFromString<PlayerObject>(im.ReadString());
            this.MessageTime = im.ReadDouble();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(Util.Serializer.SerializeObjectToString(this.PlayerObject));
            om.Write(this.MessageTime);
        }

        #endregion
    }
}
