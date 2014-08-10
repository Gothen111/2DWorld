﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;
using Lidgren.Network.Xna;

using GameLibrary.Model.Object.Inventory;

namespace GameLibrary.Connection.Message
{
    public class CreatureInventoryItemPositionChangeMessage : IGameMessage
    {
        #region Constructors and Destructors

        public CreatureInventoryItemPositionChangeMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public CreatureInventoryItemPositionChangeMessage(int _Id, int _OldPosition, int _NewPosition)
        {
            this.MessageTime = NetTime.Now;
            this.Id = _Id;
            this.OldPosition = _OldPosition;
            this.NewPosition = _NewPosition;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public int Id { get; set; }

        public int OldPosition { get; set; }

        public int NewPosition { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.CreatureInventoryItemPositionChangeMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            this.Id = im.ReadInt32();
            this.OldPosition = im.ReadInt32();
            this.NewPosition = im.ReadInt32();

        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            om.Write(this.Id);
            om.Write(this.OldPosition);
            om.Write(this.NewPosition);
        }

        #endregion
    }
}
