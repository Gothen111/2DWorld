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
                if (_LivingObject.Position.X >= var_Region.Position.X * Region.Region.regionSizeX * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize)
                {
                    if (_LivingObject.Position.X <= (var_Region.Position.X + 1) * Region.Region.regionSizeX * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize)
                    {
                        if (_LivingObject.Position.Y >= var_Region.Position.Y * Region.Region.regionSizeY * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize)
                        {
                            if (_LivingObject.Position.Y <= (var_Region.Position.Y + 1) * Region.Region.regionSizeY * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize)
                            {
                                return var_Region;
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
