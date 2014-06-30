using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using GameLibrary.Logger;
using GameLibrary.Model.Collison;
using GameLibrary.Model.Map.Block;

using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.Model.Map.Chunk
{
    [Serializable()]
    public class Chunk : Box
    {
        public static int _id = 0;
        private int id = _id++;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public static int chunkSizeX = 25; // 40
        public static int chunkSizeY = 25; // 40

        private Block.Block[,] blocks;

        public Block.Block[,] Blocks
        {
            get { return blocks; }
            set { blocks = value; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X * Block.Block.BlockSize - 1, (int)Size.Y * Block.Block.BlockSize - 1); }
        }

        public Chunk(String _Name, int _PosX, int _PosY, int _SizeX, int _SizeY, Region.Region _ParentRegion)
            :base()
        {
            this.Name = _Name;
            this.Position = new Vector2(_PosX, _PosY);
            this.Size = new Vector2(_SizeX, _SizeY);

            blocks = new Block.Block[_SizeX, _SizeY];

            this.Parent = _ParentRegion;
        }

        public Chunk(SerializationInfo info, StreamingContext ctxt) 
            :base(info, ctxt)
        {
            this.id = (int)info.GetValue("id", typeof(int));
            this.blocks = (Block.Block[,])info.GetValue("blocks", typeof(Block.Block[,]));
            setAllNeighboursOfBlocks();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("id", this.id);
            info.AddValue("blocks", this.blocks, typeof(Block.Block[,]));
        }

        public bool setBlockAtPosition(int _PosX, int _PosY, Block.Block _Block)
        {
            if (_PosX >= 0 && _PosX < this.Size.X)
            {
                if (_PosY >= 0 && _PosY < this.Size.Y)
                {
                    this.blocks[_PosX, _PosY] = _Block;
                    return true;
                }
                else
                {
                    Logger.Logger.LogErr("Chunk->setBlockAtPosition(...) : Platzierung nicht möglich: PosX " + _PosX + " PosY " + _PosY);
                    return false;
                }
            }
            else
            {
                Logger.Logger.LogErr("Chunk->setBlockAtPosition(...) : Platzierung nicht möglich: PosX " + _PosX + " PosY " + _PosY);
                return false;
            }
        }

        public void setAllNeighboursOfBlocks()
        {
            for (int x = 0; x < this.Size.X; x++)
            {
                for (int y = 0; y < this.Size.Y; y++)
                {
                    Block.Block var_Block = this.getBlockAtPosition(x, y);
                    var_Block.Parent = this;
                    if (x > 0)
                    {
                        var_Block.LeftNeighbour = this.getBlockAtPosition(x-1, y);
                    }
                    if (x < this.Size.X-1)
                    {
                        var_Block.RightNeighbour = this.getBlockAtPosition(x + 1, y);
                    }
                    if (y > 0)
                    {
                        var_Block.TopNeighbour = this.getBlockAtPosition(x, y-1);
                    }
                    if (y < this.Size.Y - 1)
                    {
                        var_Block.BottomNeighbour = this.getBlockAtPosition(x, y+1);
                    }
                }
            }
        }

        public Block.Block getBlockAtCoordinate(float _PosX, float _PosY)
        {
            for (int x = 0; x < chunkSizeX; x++)
            {
                for (int y = 0; y < chunkSizeY; y++)
                {
                    Block.Block var_Block = getBlockAtPosition(x, y);
                    if (var_Block.Bounds.Left <= _PosX && var_Block.Bounds.Right >= _PosX)
                    {
                        if (var_Block.Bounds.Top <= _PosY && var_Block.Bounds.Bottom >= _PosY)
                        {
                            return var_Block;
                        }
                    }
                }
            }
            return null;
            //return blocks[(int)(_PosX/Block.Block.BlockSize), ((int)_PosY/Block.Block.BlockSize)];
        }

        public Block.Block getBlockAtPosition(float _PosX, float _PosY)
        {
            if(_PosX >= 0 && _PosX < Chunk.chunkSizeX && _PosY >= 0 && _PosY < Chunk.chunkSizeY)
                return blocks[(int)(_PosX), ((int)_PosY)];
            return null;
        }

        public void drawBlocks(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            for (int x = 0; x < this.Size.X; x++)
            {
                for (int y = 0; y < this.Size.Y; y++)
                {
                    this.getBlockAtPosition(x, y).drawBlock(_GraphicsDevice, _SpriteBatch);
                }
            }
        }

        public void drawObjects(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            for (int x = 0; x < this.Size.X; x++)
            {
                for (int y = 0; y < this.Size.Y; y++)
                {
                    this.getBlockAtPosition(x, y).drawObjects(_GraphicsDevice, _SpriteBatch);
                }
            }
        }

        public override void update()
        {
            base.update();
            for (int x = 0; x < this.Size.X; x++)
            {
                for (int y = 0; y < this.Size.Y; y++)
                {
                    this.getBlockAtPosition(x, y).update();
                }
            }
        }

        public List<Object.LivingObject> getAllObjectsInChunk()
        {
            List<Object.LivingObject> result = new List<Object.LivingObject>();
            for (int x = 0; x < this.Size.X; x++)
            {
                for (int y = 0; y < this.Size.Y; y++)
                {
                    foreach(Object.LivingObject var_LivingObject in this.getBlockAtPosition(x, y).Objects)
                    {
                        result.Add(var_LivingObject);
                    }
                }
            }
            return result;
        }
    }
}
