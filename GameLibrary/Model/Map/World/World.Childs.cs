using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using GameLibrary.Model.Map.Region;

using Microsoft.Xna.Framework.Graphics;
using GameLibrary.Model.Object;
using GameLibrary.Model.Collison;
using GameLibrary.Connection;
using GameLibrary.Connection.Message;

namespace GameLibrary.Model.Map.World
{
    public partial class World
    {
        #region Region

        public Region.Region getRegion(int _Id)
        {
            foreach (Region.Region var_Region in regions)
            {
                if (var_Region.Id == _Id)
                {
                    return var_Region;
                }
            }
            return null;
        }

        public bool addRegion(Region.Region _Region)
        {
            if (!containsRegion(_Region.Id))
            {
                this.regions.Add(_Region);
                return false;
            }
            else
            {
                Logger.Logger.LogErr("World->addRegion(...) : Region mit Id: " + _Region.Id + " schon vorhanden!");
                return false;
            }
        }

        public bool containsRegion(int _Id)
        {
            if (this.getRegion(_Id) != null)
            {
                return true;
            }
            return false;
        }

        public bool containsRegion(Region.Region _Region)
        {
            return containsRegion(_Region.Id);
        }

        public Region.Region getRegionAtPosition(float _PosX, float _PosY)
        {
            foreach (Region.Region var_Region in this.regions)
            {
                if (var_Region.Bounds.Left <= _PosX && var_Region.Bounds.Right >= _PosX)
                {
                    if (var_Region.Bounds.Top <= _PosY && var_Region.Bounds.Bottom >= _PosY)
                    {
                        return var_Region;
                    }
                }
            }

            //Load Region

            return null;
        }

        public Region.Region createRegionAt(int _PosX, int _PosY)
        {
            int var_SizeX = (Region.Region.regionSizeX * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize);
            int var_SizeY = (Region.Region.regionSizeY * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize);

            int var_RestX = _PosX % var_SizeX;
            int var_RestY = _PosY % var_SizeY;

            if (_PosX < 0)
            {
                _PosX = _PosX - (var_SizeX + var_RestX);
            }
            else
            {
                _PosX = _PosX - var_RestX;
            }
            if (_PosY < 0)
            {
                _PosY = _PosY - (var_SizeY + var_RestY);
            }
            else
            {
                _PosY = _PosY - var_RestY;
            }

            int var_RegionType = Util.Random.GenerateGoodRandomNumber(0, Enum.GetValues(typeof(RegionEnum)).Length);
            return GameLibrary.Factory.RegionFactory.regionFactory.generateRegion("Region" + Region.Region._id, _PosX, _PosY, (RegionEnum)var_RegionType, this);
        }

        #endregion

        #region Chunk

        public Chunk.Chunk getChunkAtPosition(float _PosX, float _PosY)
        {
            Region.Region var_Region = World.world.getRegionAtPosition(_PosX, _PosY);
            if (var_Region != null)
            {
                return var_Region.getChunkAtPosition(_PosX, _PosY);
            }
            return null;
        }

        public bool removeChunk(Chunk.Chunk _Chunk)
        {
            Region.Region var_Region = World.world.getRegionAtPosition(_Chunk.Position.X, _Chunk.Position.Y);
            if (var_Region != null)
            {
                Chunk.Chunk var_Chunk = var_Region.getChunkAtPosition(_Chunk.Position.X, _Chunk.Position.Y);
                return var_Region.Chunks.Remove(var_Chunk);
            }
            return false;
        }

        #endregion

        #region Block

        public Block.Block getBlockAtCoordinate(float _PosX, float _PosY)
        {
            Chunk.Chunk var_Chunk = World.world.getChunkAtPosition(_PosX, _PosY);
            if (var_Chunk != null)
            {
                return var_Chunk.getBlockAtCoordinate(_PosX, _PosY);
            }
            return null;
        }

        #endregion
    }
}
