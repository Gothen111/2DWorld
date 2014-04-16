using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Model.Map.Block
{
    class Block
    {
        public static int BlockSize = 32;

        private BlockEnum[] layer;

        public BlockEnum[] Layer
        {
            get { return layer; }
            set { layer = value; }
        }

        public Block(BlockEnum _BlockEnum)
        {
            this.layer = new BlockEnum[Enum.GetValues(typeof(BlockLayerEnum)).Length];
            this.layer[0] = _BlockEnum;
        }

        public void setLayerAt(Enum _Enum, BlockLayerEnum _Id)
        {
            int x = (int)_Id;
            this.layer[(int)_Id] = (BlockEnum)_Enum;
        }

        public void setFirstLayer(Enum _Enum)
        {
            this.layer[0] = (BlockEnum)_Enum;
        }
    }
}
