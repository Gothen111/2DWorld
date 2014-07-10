using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GameLibrary.Model.Map.World;
using GameLibrary.Model.Map.Block;

namespace GameLibrary.Model.Path
{
    public class PathFinder
    {
        private enum Color
        {
            White,
            Grey,
            Black
        }
        private class Field
        {
            public Color color;
            public int distance;
            public Field parent;

            public Vector2 position;

            public Field()
            {
                this.color = Color.White;
                this.distance = Int32.MaxValue;
                this.parent = null;

                this.position = Vector2.Zero;
            }
        }

        private static int MaxToWalk = 5;

        public static List<Vector2> generatePath(Vector2 _StartPoint, Vector2 _EndPoint)
        {
            List<Vector2> var_List = new List<Vector2>();

            Block var_StartBlock = World.world.getBlockAtPosition(_StartPoint.X, _StartPoint.Y);
            Block var_EndBlock = World.world.getBlockAtPosition(_EndPoint.X, _EndPoint.Y);

            shortestWay(var_StartBlock, var_EndBlock);

            return var_List;
        }

        private static List<Vector2> shortestWay(Block _StartBlock, Block _EndBlock)
        {
            List<Block> var_ListVisited = new List<Block>();
            return shortestWayRekursive(var_ListVisited, _StartBlock, _EndBlock, new List<Vector2>());
        }

        private static List<Vector2> shortestWayRekursive(List<Block> _ListVisited, Block _CurrentBlock, Block _EndBlock, List<Vector2> _Way)
        {
            if(_CurrentBlock.Equals(_EndBlock))
            {
                return _Way;
            }

            _ListVisited.Add(_CurrentBlock);
            _Way.Add(_CurrentBlock.Position); // TODO: KOPIE von _Way.... Sonst gibts n problem :/

            Block var_BlockRight = (Block)_CurrentBlock.RightNeighbour;
            Block var_BlockLeft = (Block)_CurrentBlock.LeftNeighbour;
            Block var_BlockTop = (Block)_CurrentBlock.TopNeighbour;
            Block var_BlockBottom = (Block)_CurrentBlock.BottomNeighbour;

            if (_EndBlock.Position.X > _CurrentBlock.Position.X)
            {
                if (!_ListVisited.Contains(var_BlockRight))
                {
                    if (var_BlockRight.IsWalkAble)
                    {
                        shortestWayRekursive(_ListVisited, var_BlockRight, _EndBlock, new List<Vector2>(_Way)); // TODO: KOPIE von _Way.... Sonst gibts n problem :/
                    }
                }
            }

            /*
             * 
             * 
             */

            _ListVisited.Add(new Block(0,0,0,null));
            return new List<Vector2>();
        }
    }
}
