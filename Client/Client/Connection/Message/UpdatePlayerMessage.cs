using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;
using Lidgren.Network.Xna;

namespace Client.Connection.Message
{
    class UpdatePlayerMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdatePlayerMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdatePlayerMessage(Model.Object.PlayerObject _PlayerObject)
        {
            this.Id = _PlayerObject.Id;
            this.MessageTime = NetTime.Now;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public double MessageTime { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdatePlayerMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.Id = im.ReadInt32();
            this.MessageTime = im.ReadDouble();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.Id);
            om.Write(this.MessageTime);
        }

        #endregion
    }
}
