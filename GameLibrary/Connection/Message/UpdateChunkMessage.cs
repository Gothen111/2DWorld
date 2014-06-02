using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;
using Lidgren.Network.Xna;

namespace GameLibrary.Connection.Message
{
    public class UpdateChunkMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateChunkMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateChunkMessage(Model.Map.Chunk.Chunk _Chunk)
        {
            this.Id = _Chunk.Id;
            this.MessageTime = NetTime.Now;
            this.RegionId = _Chunk.ParentRegion.Id;
            this.Chunk = _Chunk;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public double MessageTime { get; set; }

        public int RegionId { get; set; }

        public Model.Map.Chunk.Chunk Chunk { get; set; }

        public String Content { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateChunkMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.Id = im.ReadInt32();
            this.MessageTime = im.ReadDouble();
            this.RegionId = im.ReadInt32();

            this.Chunk = Util.Serializer.DeserializeObjectFromString<Model.Map.Chunk.Chunk>(im.ReadString());
            this.Chunk.ParentRegion = Model.Map.World.World.world.getRegion(this.RegionId);
            this.Chunk.setAllNeighboursOfBlocks();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.Id);
            om.Write(this.MessageTime);
            om.Write(this.RegionId);

            om.Write(Util.Serializer.SerializeObjectToString(this.Chunk));
        }

        #endregion
    }
}
