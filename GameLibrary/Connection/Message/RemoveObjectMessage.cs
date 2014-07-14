﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Lidgren.Network;
using Lidgren.Network.Xna;

namespace GameLibrary.Connection.Message
{
    public class RemoveObjectMessage : IGameMessage
    {
        #region Constructors and Destructors

        public RemoveObjectMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public RemoveObjectMessage(Model.Object.Object _Object)
        {
            this.Id = _Object.Id;
            this.MessageTime = NetTime.Now;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public double MessageTime { get; set; }

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.RemoveObjectMessage; }
        }

        #endregion

        #region Public Methods

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
