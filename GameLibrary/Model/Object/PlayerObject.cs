using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;

namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class PlayerObject : FactionObject
    {
        public PlayerObject() :base()
        {
        }

        public PlayerObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public override void onChangedBlock()
        {
            base.onChangedBlock();
        }

        public override void onChangedChunk()
        {
            base.onChangedChunk();
            if (Configuration.Configuration.isHost)
            {
                GameLibrary.Model.Map.World.World.world.checkPlayerObjectNeighbourChunks(this);
            }
            else
            {   
            }   
        }
    }
}
