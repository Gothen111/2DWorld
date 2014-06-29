using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;
using Lidgren.Network.Xna;

namespace GameLibrary.Connection.Message
{
    public class RequestLivingObjectMessage : IGameMessage
    {
        #region Constructors and Destructors

        public RequestLivingObjectMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public RequestLivingObjectMessage(int _Id)
        {
            this.Id = _Id;
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
            get { return EIGameMessageType.RequestLivingObjectMessage; }
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
