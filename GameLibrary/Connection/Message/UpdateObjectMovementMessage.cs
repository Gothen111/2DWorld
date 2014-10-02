
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Lidgren.Network;
using Lidgren.Network.Xna;

namespace GameLibrary.Connection.Message
{
    public class UpdateObjectMovementMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateObjectMovementMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateObjectMovementMessage(Model.Object.LivingObject _LivingObject)
        {
            this.Id = _LivingObject.Id;
            this.MessageTime = NetTime.Now;
            this.MoveUp = _LivingObject.MoveUp;
            this.MoveDown = _LivingObject.MoveDown;
            this.MoveLeft = _LivingObject.MoveLeft;
            this.MoveRight = _LivingObject.MoveRight;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public double MessageTime { get; set; }

        public bool MoveUp { get; set; }

        public bool MoveDown { get; set; }

        public bool MoveLeft { get; set; }

        public bool MoveRight { get; set; }

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateObjectMovementMessage; }
        }

        #endregion

        #region Public Methods

        public void Decode(NetIncomingMessage im)
        {
            this.Id = im.ReadInt32();
            this.MessageTime = im.ReadDouble();
            this.MoveUp = im.ReadBoolean();
            this.MoveDown = im.ReadBoolean();
            this.MoveLeft = im.ReadBoolean();
            this.MoveRight = im.ReadBoolean();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.Id);
            om.Write(this.MessageTime);
            om.Write(this.MoveUp);
            om.Write(this.MoveDown);
            om.Write(this.MoveLeft);
            om.Write(this.MoveRight);
        }

        #endregion
    }
}
