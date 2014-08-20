using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;
using Lidgren.Network.Xna;

using GameLibrary.Model.Object.Inventory;
using GameLibrary.Model.Object;
using GameLibrary.Model.Object.Body;

namespace GameLibrary.Connection.Message
{
    public class UpdateAnimatedObjectBodyMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateAnimatedObjectBodyMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateAnimatedObjectBodyMessage(int _Id, Body _Body)
        {
            this.MessageTime = NetTime.Now;
            this.Id = _Id;
            this.Body = _Body;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public int Id { get; set; }

        public Body Body { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateAnimatedObjectBodyMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            this.Id = im.ReadInt32();
            this.Body = Util.Serializer.DeserializeObjectFromString<Body>(im.ReadString());
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            om.Write(this.Id);
            om.Write(Util.Serializer.SerializeObjectToString(this.Body));
        }

        #endregion
    }
}
