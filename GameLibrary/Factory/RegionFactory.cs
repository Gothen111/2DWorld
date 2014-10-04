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
using Microsoft.Xna.Framework;

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
                case RegionEnum.Snowland:
                    {
                        return generateRegionSnowland(_Name, _PosX, _PosY, _ParentWorld);
                    }
                case RegionEnum.Lavaland:
                    {
                        return generateRegionLavaland(_Name, _PosX, _PosY, _ParentWorld);
                    }
            }
            return null;
        }

        private Region generateRegionGrassland(String _Name, int _PosX, int _PosY, World _ParentWorld)
        {
            Region var_Result;

            var_Result = new Region(_Name, _PosX, _PosY, RegionEnum.Grassland, _ParentWorld);

            //FarmFactory.farmFactory.generateFarms(var_Result, 1, 0);

            Logger.Logger.LogInfo("Region " + var_Result.Name + " wurde erstellt!");

            return var_Result;
        }

        private Region generateRegionSnowland(String _Name, int _PosX, int _PosY, World _ParentWorld)
        {
            Region var_Result;

            var_Result = new Region(_Name, _PosX, _PosY, RegionEnum.Snowland, _ParentWorld);

            Logger.Logger.LogInfo("Region " + var_Result.Name + " wurde erstellt!");

            return var_Result;
        }

        private Region generateRegionLavaland(String _Name, int _PosX, int _PosY, World _ParentWorld)
        {
            Region var_Result;

            var_Result = new Region(_Name, _PosX, _PosY, RegionEnum.Lavaland, _ParentWorld);

            Logger.Logger.LogInfo("Region " + var_Result.Name + " wurde erstellt!");

            return var_Result;
        }

        private void addChunkToRegion(Region _Region, int _PosX, int _PosY, Chunk _ChunkToAdd)
        {
            if (_Region.setChunkAtPosition(new Vector3(_PosX, _PosY, 0), _ChunkToAdd))
            {
            }
            else
            {
                Logger.Logger.LogErr("RegionFactory->addChunkToRegion(...) : Chunk kann der Region " + _Region.Name + " nicht hinzufügt werden!");
            }
        }

        public Chunk createChunkInRegion(Region _Region, int _PosX, int _PosY)
        {
            Chunk var_Chunk = null;
            switch (_Region.RegionEnum)
            {
                case RegionEnum.Grassland:
                    {
                        var_Chunk = ChunkFactory.chunkFactory.createChunk((int)(_PosX), (int)(_PosY), ChunkEnum.Grassland, RegionDependency.regionDependency.getLayer(_Region.RegionEnum), _Region);
                        break;
                    }
                case RegionEnum.Snowland:
                    {
                        var_Chunk = ChunkFactory.chunkFactory.createChunk((int)(_PosX), (int)(_PosY), ChunkEnum.Snowland, RegionDependency.regionDependency.getLayer(_Region.RegionEnum), _Region);
                        break;
                    }
                case RegionEnum.Lavaland:
                    {
                        var_Chunk = ChunkFactory.chunkFactory.createChunk((int)(_PosX), (int)(_PosY), ChunkEnum.Lavaland, RegionDependency.regionDependency.getLayer(_Region.RegionEnum), _Region);
                        break;
                    }
            }

            if (var_Chunk != null)
            {
                this.addChunkToRegion(_Region, _PosY, _PosY, var_Chunk);

                ChunkFactory.chunkFactory.generateChunk(var_Chunk);
            }  

            return var_Chunk;
        }
    }
}
