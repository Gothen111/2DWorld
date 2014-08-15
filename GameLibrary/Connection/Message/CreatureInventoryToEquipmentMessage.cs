﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;
using Lidgren.Network.Xna;

using GameLibrary.Model.Object.Inventory;

namespace GameLibrary.Connection.Message
{
    public class CreatureInventoryToEquipmentMessage : IGameMessage
    {
        #region Constructors and Destructors

        public CreatureInventoryToEquipmentMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public CreatureInventoryToEquipmentMessage(int _Id, int _InventoryPosition, int _EquipmentPosition)
        {
            this.MessageTime = NetTime.Now;
            this.Id = _Id;
            this.EquipmentPosition = _EquipmentPosition;
            this.InventoryPosition = _InventoryPosition;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public int Id { get; set; }

        public int EquipmentPosition { get; set; }

        public int InventoryPosition { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.CreatureInventoryToEquipmentMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            this.Id = im.ReadInt32();
            this.EquipmentPosition = im.ReadInt32();
            this.InventoryPosition = im.ReadInt32();

        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            om.Write(this.Id);
            om.Write(this.EquipmentPosition);
            om.Write(this.InventoryPosition);
        }

        #endregion
    }
}
