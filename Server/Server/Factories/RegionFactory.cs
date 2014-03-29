using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Map.Region;
using Server.Model.Map.Chunk;
using Server.Factories;

namespace Server.Factories
{
    class RegionFactory
    {
        public static RegionFactory regionFactory = new RegionFactory();

        public Region generateRegion(int _Id, String _Name, int _RegionSizeX, int _RegionSizeY, RegionEnum _RegionEnum)
        {
            switch (_RegionEnum)
            {
                case RegionEnum.Grassland:
                    {
                        return generateRegionGrassland(_Id, _Name, _RegionSizeX, _RegionSizeY);
                    }
            }
            return null;
        }

        private Region generateRegionGrassland(int _Id, String _Name, int _RegionSizeX, int _RegionSizeY)
        {
            Region var_Result;

            var_Result = new Region(_Id, _Name);


            for (int x = 0; x < _RegionSizeX; x++)
            {
                for (int y = 0; y < _RegionSizeY; y++)
                {
                    Chunk var_Chunk = ChunkFactory.chunkFactory.generateChunk(var_Result.getLastChunkId(), x, y, ChunkEnum.Grassland);
                    this.addChunkToRegion(var_Result, var_Chunk);
                }
                Logger.Logger.LogInfo("Erstelle Region " + var_Result.Name + " : " +(int)(((float)x / _RegionSizeX) * 100) + "%", true);
            }

            Logger.Logger.LogInfo("Region " + var_Result.Name + " wurde erstellt!");

            return var_Result;
        }

        private void addChunkToRegion(Region _Region, Chunk _ChunkToAdd)
        {
            if (_Region.addChunk(_ChunkToAdd))
            {

            }
            else
            {
                Logger.Logger.LogErr("RegionFactory->generateRegionGrassland(...) : Chunk kann der Region " + _Region.Name + " den Chunk nicht hinzufügen!");
            }
        }
    }
}
