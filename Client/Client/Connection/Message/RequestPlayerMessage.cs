using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;
using Lidgren.Network.Xna;

namespace Client.Connection.Message
{
    class RequestPlayerMessage : IGameMessage
    {
        #region Constructors and Destructors

        public RequestPlayerMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public RequestPlayerMessage(String _PlayerName)
        {
            this.Name = _PlayerName;
            this.MessageTime = NetTime.Now;
        }

        #endregion

        #region Properties

        public String Name { get; set; }

        public double MessageTime { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateChunkMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.Name = im.ReadString();
            this.MessageTime = im.ReadDouble();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.Name);
            om.Write(this.MessageTime);
        }

        #endregion
    }
}
