using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;
using Lidgren.Network.Xna;

namespace Server.Connection.Message
{
    class PlayerCommandMessage : IGameMessage
    {
        #region Constructors and Destructors

        public PlayerCommandMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public PlayerCommandMessage(Model.Object.PlayerObject _PlayerObject, Commands.ECommandType _ECommandType)
        {
            this.Id = _PlayerObject.Id;
            this.MessageTime = NetTime.Now;
            this.ECommandType = _ECommandType;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public double MessageTime { get; set; }

        public Commands.ECommandType ECommandType{ get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.PlayerCommandMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.Id = im.ReadInt32();
            this.MessageTime = im.ReadDouble();
            this.ECommandType = (Commands.ECommandType) im.ReadInt32();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.Id);
            om.Write(this.MessageTime);
            om.Write((int)this.ECommandType);
        }

        #endregion
    }
}
