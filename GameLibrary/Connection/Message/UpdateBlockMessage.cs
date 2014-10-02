using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;
using Lidgren.Network.Xna;
using GameLibrary.Model.Map.Chunk;
using GameLibrary.Model.Map.Block;

namespace GameLibrary.Connection.Message
{
    public class UpdateBlockMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateBlockMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateBlockMessage(Block _Block)
        {
            //this.Id = _Block.Id;
            this.MessageTime = NetTime.Now;
            this.ChunkId = ((Chunk)_Block.Parent).Id;
            this.Block = _Block;
        }

        #endregion

        #region Properties

        //public int Id { get; set; }

        public double MessageTime { get; set; }

        public int ChunkId { get; set; }

        public Block Block { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateBlockMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            this.ChunkId = im.ReadInt32();

            Microsoft.Xna.Framework.Vector3 var_Position = im.ReadVector3();

            this.Block = GameLibrary.Model.Map.World.World.world.getBlockAtCoordinate(var_Position);

            int var_Size = Enum.GetValues(typeof(GameLibrary.Model.Map.Block.BlockLayerEnum)).Length;

            if (this.Block != null)
            {
                if (this.Block.IsRequested)
                {
                    for (int i = 0; i < var_Size; i++)
                    {
                        this.Block.Layer[i] = (Model.Map.Block.BlockEnum)im.ReadInt32();
                    }
                }
                this.Block.IsRequested = false;
            }
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            om.Write(this.ChunkId);

            om.Write(this.Block.Position);

            int var_Size = Enum.GetValues(typeof(GameLibrary.Model.Map.Block.BlockLayerEnum)).Length;

            if (this.Block != null)
            {
                for (int i = 0; i < var_Size; i++)
                {
                    om.Write((int)this.Block.Layer[i]);
                }
            }
        }

        #endregion
    }
}
