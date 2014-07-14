using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Map.World;
using GameLibrary.Model.Map.Region;
using GameLibrary.Model.Map.Chunk;
using GameLibrary.Model.Map.Block;
using GameLibrary.Factory.FactoryEnums;
using GameLibrary.Factory;

namespace GameLibrary.Factory
{
    public class RegionFactory
    {
        public static RegionFactory regionFactory = new RegionFactory();

        public Region generateRegion(String _Name, int _PosX, int _PosY, RegionEnum _RegionEnum, World _ParentWorld)
        {
            switch (_RegionEnum)
            {
                case RegionEnum.Grassland:
                    {
                        return generateRegionGrassland(_Name, _PosX, _PosY, _ParentWorld);
                    }
            }
            return null;
        }

        private Region generateRegionGrassland(String _Name, int _PosX, int _PosY, World _ParentWorld)
        {
            Region var_Result;

            var_Result = new Region(_Name, _PosX, _PosY, Region.regionSizeX, Region.regionSizeY, RegionEnum.Grassland, _ParentWorld);

            FarmFactory.farmFactory.generateFarms(var_Result, 1, 0);

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
                Logger.Logger.LogErr("RegionFactory->generateRegionGrassland(...) : Chunk kann der Region " + _Region.Name + " nicht hinzufügt werden!");
            }
        }

        public Chunk createChunkInRegion(Region _Region, int _PosX, int _PosY)
        {
            Chunk var_Chunk = ChunkFactory.chunkFactory.generateChunk((int)(_PosX), (int)(_PosY), ChunkEnum.Grassland, RegionDependency.regionDependency.getLayer(RegionEnum.Grassland), _Region);
            this.addChunkToRegion(_Region, _PosY , _PosY, var_Chunk);

            return var_Chunk;
        }
    }
}
