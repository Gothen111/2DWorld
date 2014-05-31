using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;
using Lidgren.Network.Xna;

namespace Client.Connection.Message
{
    class UpdateChunkMessage : IGameMessage
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

            this.Chunk = new Model.Map.Chunk.Chunk(this.Id,"Chunk",0,0,40,40, Model.Map.World.World.world.getRegion(this.RegionId));

            for (int x = 0; x < Model.Map.Chunk.Chunk.chunkSizeX; x++)
            {
                for (int y = 0; y < Model.Map.Chunk.Chunk.chunkSizeY; y++)
                {
                    this.Chunk.setBlockAtPosition(x, y, new Model.Map.Block.Block((int)this.Chunk.Position.X + x * Model.Map.Block.Block.BlockSize, (int)this.Chunk.Position.Y + y * Model.Map.Block.Block.BlockSize, Model.Map.Block.BlockEnum.Nothing, this.Chunk));
                    for (int layer = 0; layer < 6; layer++)
                    {
                        this.Chunk.getBlockAtPosition(x, y).setLayerAt((Model.Map.Block.BlockEnum)im.ReadInt32(), (Model.Map.Block.BlockLayerEnum)layer);
                    }
                }
            }
            this.Chunk.setAllNeighboursOfBlocks();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.Id);
            om.Write(this.MessageTime);
            om.Write(this.RegionId);

            for (int x = 0; x < Model.Map.Chunk.Chunk.chunkSizeX; x++)
            {
                for (int y = 0; y < Model.Map.Chunk.Chunk.chunkSizeY; y++)
                {
                    for (int layer = 0; layer < 6; layer++)
                    {
                        om.Write((int)this.Chunk.getBlockAtPosition(x,y).Layer[layer]);
                    }
                }
            }
        }

        #endregion
    }
}
