using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Map;

namespace Server.Factories
{
    class RegionFactory
    {
        
        private int chunkSizeX = 20;
        private int chunkSizeY = 20;

        public Region generateRegion(int _Id, RegionEnum _RegionEnum, int _RegionSizeX, int _RegionSizeY)
        {
            switch (_RegionEnum)
            {
                case RegionEnum.Grassland:
                    {
                        return generateRegionGrassland(_Id, _RegionSizeX, _RegionSizeY);
                    }
            }
            return null;
        }

        public Chunk generateChunk(int _Id, ChunkEnum _ChunkEnum)
        {
            switch (_ChunkEnum)
            {
                case ChunkEnum.Grassland:
                    {
                        return generateChunkGrassland(_Id, chunkSizeX, chunkSizeY);
                    }
            }
            return null;
        }

        private Region generateRegionGrassland(int _Id, int _SizeX, int _SizeY)
        {
            Region var_Result;

            var_Result = new Region(_Id);
            for (int x = 0; x < _SizeX; x++)
            {
                for (int y = 0; y < _SizeY; y++)
                {
                    if (!var_Result.addChunk(generateChunkGrassland(var_Result.getLastChunkId(), this.chunkSizeX, this.chunkSizeY)))
                    {
                        Logger.Logger.LogErr("RegionFactory->generateRegionGrassland(...) : Chunk existiert schon!");
                    }
                }
            }

            return var_Result;
        }

        private Chunk generateChunkGrassland(int _Id, int _SizeX, int _SizeY)
        {
            Chunk var_Result;

            var_Result = new Chunk(_Id, _SizeX, _SizeY);
            this.fillChunkWithBlock(var_Result, BlockEnum.Gras);

            return var_Result;
        }

        private Chunk fillChunkWithBlock(Chunk _Chunk, BlockEnum _BlockEnum)
        {
            for(int x = 0; x < _Chunk.SizeX; x++)
            {
                for(int y = 0; y < _Chunk.SizeY; y++)
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
