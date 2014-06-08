using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;
using Lidgren.Network.Xna;

namespace GameLibrary.Connection.Message
{
    public class UpdateRegionMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateRegionMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateRegionMessage(Model.Map.Region.Region _Region)
        {
            this.Id = _Region.Id;
            this.MessageTime = NetTime.Now;
            this.Region = _Region;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public double MessageTime { get; set; }

        public Model.Map.Region.Region Region { get; set; }

        public String Content { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateRegionMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.Id = im.ReadInt32();
            this.MessageTime = im.ReadDouble();

            this.Region = Util.Serializer.DeserializeObjectFromString<Model.Map.Region.Region>(im.ReadString());
            this.Region.Parent = Model.Map.World.World.world;
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.Id);
            om.Write(this.MessageTime);

            om.Write(Util.Serializer.SerializeObjectToString(this.Region));
        }

        #endregion
    }
}
