using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Map.World;
using Server.Model.Map.Region;
using Server.Model.Map.Chunk;
using Server.Factories;

namespace Server.Factories
{
    class RegionFactory
    {
        public static RegionFactory regionFactory = new RegionFactory();
        public static int regionSizeX = 1; // 10
        public static int regionSizeY = 1; // 10

        public Region generateRegion(int _Id, String _Name, int _PosX, int _PosY, RegionEnum _RegionEnum, World _ParentWorld)
        {
            switch (_RegionEnum)
            {
                case RegionEnum.Grassland:
                    {
                        return generateRegionGrassland(_Id, _Name, _PosX, _PosY, _ParentWorld);
                    }
            }
            return null;
        }

        private Region generateRegionGrassland(int _Id, String _Name, int _PosX, int _PosY, World _ParentWorld)
        {
            Region var_Result;

            var_Result = new Region(_Id, _Name, _PosX, _PosY, regionSizeX, regionSizeY, _ParentWorld);


            for (int x = 0; x < regionSizeX; x++)
            {
                for (int y = 0; y < regionSizeY; y++)
                {
                    Chunk var_Chunk = ChunkFactory.chunkFactory.generateChunk(var_Result.getLastChunkId(), x, y, ChunkEnum.Grassland, RegionDependency.regionDependency.getLayer(RegionEnum.Grassland), var_Result);
                    this.addChunkToRegion(var_Result, _PosX, _PosY, var_Chunk);
                }
                Logger.Logger.LogInfo("Erstelle Region " + var_Result.Name + " : " + (int)(((float)x / regionSizeX) * 100) + "%", true);
            }

            Logger.Logger.LogInfo("Region " + var_Result.Name + " wurde erstellt!");

            return var_Result;
        }

        private void addChunkToRegion(Region _Region, int _PosX, int _PosY, Chunk _ChunkToAdd)
        {
            if (_Region.setChunkAtPosition(_PosX, _PosY, _ChunkToAdd))
            {

            }
            else
            {
                Logger.Logger.LogErr("RegionFactory->generateRegionGrassland(...) : Chunk kann der Region " + _Region.Name + " den Chunk nicht hinzufügen!");
            }
        }
    }
}
