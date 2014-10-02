using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Lidgren.Network;
using Lidgren.Network.Xna;

namespace GameLibrary.Connection.Message
{
    public class RequestBlockMessage : IGameMessage
    {
        #region Constructors and Destructors

        public RequestBlockMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public RequestBlockMessage(Vector3 _Position)
        {
            this.MessageTime = NetTime.Now;
            this.Position = _Position;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public Vector3 Position { get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.RequestBlockMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            this.Position = im.ReadVector3();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            om.Write(this.Position);
        }

        #endregion
    }
}
