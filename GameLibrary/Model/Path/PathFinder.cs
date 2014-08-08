using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GameLibrary.Model.Map.World;
using GameLibrary.Model.Map.Block;

using GameLibrary.Model.Path.AStar;
using GameLibrary.Model.Map.Region;
using GameLibrary.Model.Map.Chunk;

namespace GameLibrary.Model.Path
{
    public class MySolver<TPathNode, TUserContext> : SpatialAStar<TPathNode, TUserContext> where TPathNode : IPathNode<TUserContext>
    {
        protected override Double Heuristic(PathNode inStart, PathNode inEnd)
        {
            return Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y);
        }

        protected override Double NeighborDistance(PathNode inStart, PathNode inEnd)
        {
            return Heuristic(inStart, inEnd);
        }

        public MySolver(TPathNode[,] inGrid)
            : base(inGrid)
        {
        }
    }

    public class PathFinder
    {
        public static Path generatePath(Vector2 _StartPosition, Vector2 _EndPosition)
        {
            try
            {
                int var_SizeX = 20;//(int)Math.Abs(_StartPosition.X - _EndPosition.X)/16 + 2;//20;
                int var_SizeY = 20;//(int)Math.Abs(_StartPosition.Y - _EndPosition.Y)/16 + 2;//20;

                int var_StartX = (int)((_StartPosition.X % (Region.regionSizeX * Chunk.chunkSizeX * Block.BlockSize)) % (Chunk.chunkSizeX * Block.BlockSize) / Block.BlockSize);
                int var_StartY = (int)((_StartPosition.Y % (Region.regionSizeY * Chunk.chunkSizeY * Block.BlockSize)) % (Chunk.chunkSizeY * Block.BlockSize) / Block.BlockSize);          

                int var_TargetX = (int)((_EndPosition.X % (Region.regionSizeX * Chunk.chunkSizeX * Block.BlockSize)) % (Chunk.chunkSizeX * Block.BlockSize) / Block.BlockSize);
                int var_TargetY = (int)((_EndPosition.Y % (Region.regionSizeY * Chunk.chunkSizeY * Block.BlockSize)) % (Chunk.chunkSizeY * Block.BlockSize) / Block.BlockSize);

                var_TargetX = var_TargetX - var_StartX + var_SizeX/2;
                var_TargetY = var_TargetY - var_StartY + var_SizeY/2;

                PathNode[,] grid = new PathNode[var_SizeX, var_SizeY];

                for (int x = 0; x < var_SizeX; x++)
                {
                    for (int y = 0; y < var_SizeY; y++)
                    {
                        int var_X = (int)_StartPosition.X + (-var_SizeX / 2 + x) * Block.BlockSize;
                        int var_Y = (int)_StartPosition.Y + (-var_SizeX / 2 + y) * Block.BlockSize;

                        Block var_Block = World.world.getBlockAtCoordinate(var_X, var_Y);
                        if (var_Block != null)
                        {
                            grid[x, y] = new PathNode()
                            {
                                IsWall = !var_Block.IsWalkAble || (var_Block.Objects.Count > 0 && x != var_SizeX / 2 && y != var_SizeY / 2),
                                X = x,
                                Y = y,
                                block = var_Block,
                            };
                        }
                        else
                        {
                            grid[x, y] = new PathNode()
                            {
                                IsWall = true,
                                X = x,
                                Y = y,
                                block = null,
                            };
                        }
                    }
                }

                MySolver<PathNode, System.Object> aStar = new MySolver<PathNode, System.Object>(grid);

                /*for (int x = 0; x < var_SizeX; x++)
                {
                    for (int y = 0; y < var_SizeY; y++)
                    {
                        if (grid[x, y].IsWall)
                        {
                            Console.Write("x");
                        }
                        else if (x == 10 && y == 10)
                        {
                            Console.Write("o");
                        }
                        else
                        {
                            Console.Write("-");
                        }
                    }
                    Console.WriteLine();
                }*/

                return new Path(aStar.Search(new System.Drawing.Point(var_SizeX / 2, var_SizeY / 2), new System.Drawing.Point(var_TargetX, var_TargetY), null));
            }
            catch (Exception ex)
            {
                Logger.Logger.LogErr(ex.ToString());
            }

            return null;
        }
    }
}
