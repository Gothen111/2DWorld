using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Map.Chunk;
using Server.Model.Map.Region;
using Server.Model.Map.Block;

namespace Server.Factories
{
    class ChunkFactory
    {
        public static ChunkFactory chunkFactory = new ChunkFactory();
        public static int chunkSizeX = 40;
        public static int chunkSizeY = 40;

        public Chunk generateChunk(int _Id, int _PosX, int PosY, ChunkEnum _ChunkEnum)
        {
            switch (_ChunkEnum)
            {
                case ChunkEnum.Grassland:
                    {
                        return generateChunkGrassland(_Id, _PosX, PosY, chunkSizeX, chunkSizeY);
                    }
            }
            return null;
        }

        private Chunk generateChunkGrassland(int _Id, int _PosX, int PosY, int _SizeX, int _SizeY)
        {
            Chunk var_Result;

            var_Result = new Chunk(_Id, _PosX, PosY, _SizeX, _SizeY);
            this.fillChunkWithBlock(var_Result, BlockEnum.Gras);

            return var_Result;
        }

        private Chunk fillChunkWithBlock(Chunk _Chunk, BlockEnum _BlockEnum)
        {
            for(int x = 0; x < _Chunk.Size.X; x++)
            {
                for(int y = 0; y < _Chunk.Size.Y; y++)
                {
                    if (!_Chunk.setBlockAtPosition(x, y, _BlockEnum))
                    {
                        Logger.Logger.LogErr("RegionFactory->fillChunkWithBlock(...) : Platzierung nicht möglich!");
                    }
                }
            }
            return _Chunk;
        }
    }
}
