using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;
using Lidgren.Network.Xna;

namespace GameLibrary.Connection.Message
{
    public class UpdatePlayerMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdatePlayerMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdatePlayerMessage(Model.Object.PlayerObject _PlayerObject)
        {
            this.MessageTime = NetTime.Now;
            this.PlayerObject = _PlayerObject;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public Model.Object.PlayerObject PlayerObject { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdatePlayerMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            this.PlayerObject = Utility.Serializer.DeserializeObjectFromString<Model.Object.PlayerObject>(im.ReadString());
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            om.Write(Utility.Serializer.SerializeObjectToString(this.PlayerObject));
        }

        #endregion
    }
}
