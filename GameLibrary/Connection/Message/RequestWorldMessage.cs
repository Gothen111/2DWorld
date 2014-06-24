using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Lidgren.Network;
using Lidgren.Network.Xna;

namespace GameLibrary.Connection.Message
{
    public class RequestWorldMessage : IGameMessage
    {
        #region Constructors and Destructors

        public RequestWorldMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public RequestWorldMessage()
        {
            this.MessageTime = NetTime.Now;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.RequestWorldMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
        }

        #endregion
    }
}
