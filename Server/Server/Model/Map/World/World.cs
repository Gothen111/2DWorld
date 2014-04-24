using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Server.Model.Map.Region;

using Microsoft.Xna.Framework.Graphics;

namespace Server.Model.Map.World
{
    class World
    {
        private List<Region.Region> regions;

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private Vector2 size;

        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        public World(String _Name)
        {
            this.name = _Name;
            //this.size = new Vector2(_SizeX, _SizeY);

            regions = new List<Region.Region>();
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
                Logger.Logger.LogErr("World->addRegion(...) : Chunk mit Id: " + _Region.Id + " schon vorhanden!");
                return false;
            }
        }

        public bool containsRegion(int _Id)
        {
            return false;
        }

        public bool containsRegion(Region.Region _Region)
        {
            return false;
        }

        public void DrawTest(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            foreach (Region.Region var_Region in this.regions)
            {
                var_Region.DrawTest(_GraphicsDevice, _SpriteBatch);
            }
        }

        public Region.Region getRegionLivingObjectIsIn(Server.Model.Object.LivingObject _LivingObject)
        {
            foreach (Region.Region var_Region in this.regions)
            {
                if (_LivingObject.Position.X >= var_Region.Position.X * Factories.RegionFactory.regionSizeX * Factories.ChunkFactory.chunkSizeX * Block.Block.BlockSize)
                {
                    if (_LivingObject.Position.X <= (var_Region.Position.X + 1) * Factories.RegionFactory.regionSizeX * Factories.ChunkFactory.chunkSizeX * Block.Block.BlockSize)
                    {
                        if (_LivingObject.Position.Y >= var_Region.Position.Y * Factories.RegionFactory.regionSizeY * Factories.ChunkFactory.chunkSizeY * Block.Block.BlockSize)
                        {
                            if (_LivingObject.Position.Y <= (var_Region.Position.Y + 1) * Factories.RegionFactory.regionSizeY * Factories.ChunkFactory.chunkSizeY * Block.Block.BlockSize)
                            {
                                return var_Region;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public List<Object.LivingObject> getLivingObjectsInRange(Vector3 _Position, float _Range)
        {
            List<Object.LivingObject> var_LivingObjects = new List<Object.LivingObject>();
            foreach (Block.Block var_Block in getBlocksInRange(_Position, _Range))
            {
                foreach (Object.LivingObject var_LivingObject in var_Block.Objects)
                {
                    var_LivingObjects.Add(var_LivingObject);
                }
            }
            return var_LivingObjects;
        }

        public List<Block.Block> getBlocksInRange(Vector3 _Position, float _Range)
        {
            List<Block.Block> var_Blocks = new List<Block.Block>();
            Util.Circle circle = new Util.Circle(_Position, _Range);
            foreach (Chunk.Chunk var_Chunk in getChunksInRange(_Position, _Range))
            {
                foreach(Block.Block var_Block in var_Chunk.Blocks)
                if (Util.Intersection.CircleIntersectsRectangle(circle, var_Block.Bounds))
                {
                    var_Blocks.Add(var_Block);
                }
            }
            return var_Blocks;
        }

        public List<Chunk.Chunk> getChunksInRange(Vector3 _Position, float _Range)
        {
            List<Chunk.Chunk> var_Chunks = new List<Chunk.Chunk>();
            Util.Circle circle = new Util.Circle(_Position, _Range);
            foreach (Region.Region var_Region in getRegionsInRange(_Position, _Range))
            {
                foreach (Chunk.Chunk var_Chunk in var_Region.Chunks)
                {
                    if (Util.Intersection.CircleIntersectsRectangle(circle, var_Chunk.Bounds))
                    {
                        var_Chunks.Add(var_Chunk);
                    }
                }
            }
            return var_Chunks;
        }

        public List<Region.Region> getRegionsInRange(Vector3 _Position, float _Range)
        {
            List<Region.Region> var_Regions = new List<Region.Region>();
            Util.Circle circle = new Util.Circle(_Position, _Range);
            foreach(Region.Region var_Region in this.regions)
            {
                if (Util.Intersection.CircleIntersectsRectangle(circle, var_Region.Bounds))
                {
                    var_Regions.Add(var_Region);
                }
            }
            return var_Regions;
        }
    }
}
