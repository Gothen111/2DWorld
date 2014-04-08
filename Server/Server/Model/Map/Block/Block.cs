﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Model.Map.Block
{
    class Block
    {
        public static int BlockSize = 32;

        private List<Enum> layer;

        public List<Enum> Layer
        {
            get { return layer; }
            set { layer = value; }
        }

        public Block(BlockEnum _BlockEnum)
        {
            this.layer = new List<Enum>();
            this.layer.Add(_BlockEnum);
        }

        public void addLayer(Enum _Enum)
        {
            this.layer.Add(_Enum);
        }

        public void setLayerAt(Enum _Enum, int _Id)
        {
            this.layer[_Id] = _Enum;
        }

        public void setFirstLayer(Enum _Enum)
        {
            this.layer.Clear();
            this.addLayer(_Enum);
        }
    }
}
