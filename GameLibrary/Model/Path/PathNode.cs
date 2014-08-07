using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Path.AStar;
using GameLibrary.Model.Map.Block;

namespace GameLibrary.Model.Path
{
    public class PathNode : IPathNode<System.Object>
    {
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public Boolean IsWall { get; set; }
        public Block block { get; set; }

        public bool IsWalkable(System.Object unused)
        {
            return !IsWall;
        }
    }
}
