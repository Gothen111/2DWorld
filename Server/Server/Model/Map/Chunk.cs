﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Collison;

namespace Server.Model.Map
{
    class Chunk
    {
        protected BlockEnum[][] blocks;
        protected QuadTree quadTree;
    }
}
