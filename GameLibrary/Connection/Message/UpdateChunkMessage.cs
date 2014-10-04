using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Map.Region;
using Lidgren.Network;
using Lidgren.Network.Xna;
using Microsoft.Xna.Framework;

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
            this.RegionId = ((Region)_Chunk.Parent).Id;
            this.Chunk = _Chunk;
            this.Position = _Chunk.Position;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public double MessageTime { get; set; }

        public int RegionId { get; set; }

        public Model.Map.Chunk.Chunk Chunk { get; set; }

        public Vector3 Position { get; set; }

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

            //this.Chunk = Utility.Serializer.DeserializeObjectFromString<Model.Map.Chunk.Chunk>(im.ReadString());
            //this.Chunk.Parent = Model.Map.World.World.world.getRegion(this.RegionId);
            //this.Chunk.setAllNeighboursOfBlocks();

            this.Position = im.ReadVector3();

            Model.Map.Chunk.Chunk var_Chunk =  Model.Map.World.World.world.getChunkAtPosition(Position);

            if (var_Chunk != null)
            {
                if (var_Chunk.IsRequested)
                {
                    var_Chunk.IsRequested = false;
                }
            }
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.Id);
            om.Write(this.MessageTime);
            om.Write(this.RegionId);

            //om.Write(Utility.Serializer.SerializeObjectToString(this.Chunk));

            om.Write(this.Position);

            /*int var_Size = Enum.GetValues(typeof(GameLibrary.Model.Map.Block.BlockLayerEnum)).Length;

            if (this.Chunk != null)
            {
                for (int x = 0; x < GameLibrary.Model.Map.Chunk.Chunk.chunkSizeX; x++)
                {
                    for (int y = 0; y < GameLibrary.Model.Map.Chunk.Chunk.chunkSizeY; y++)
                    {
                        for (int i = 0; i < var_Size; i++)
                        {
                            om.Write((int)this.Chunk.Blocks[x, y].Layer[i]);
                        }
                    }
                }
            }*/
        }

        #endregion
    }
}
