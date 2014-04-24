using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Server.Logger;
using Server.Model.Collison;
using Server.Model.Map.Block;


using Microsoft.Xna.Framework.Graphics;

namespace Server.Model.Map.Chunk
{
    class Chunk
    {
        private int id;

        public static int chunkSizeX = 40; // 40
        public static int chunkSizeY = 40; // 40

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private Block.Block[,] blocks;

        internal Block.Block[,] Blocks
        {
            get { return blocks; }
            set { blocks = value; }
        }
        private QuadTree quadTree;
        
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

        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y); }
        }

        private Region.Region parentRegion;

        public Region.Region ParentRegion
        {
            get { return parentRegion; }
            set { parentRegion = value; }
        }

        public Chunk(int _Id, String _Name, int _PosX, int _PosY, int _SizeX, int _SizeY, Region.Region _ParentRegion)
        {
            this.id = _Id;
            this.name = _Name;
            this.position = new Vector2(_PosX, _PosY);
            this.size = new Vector2(_SizeX, _SizeY);

            blocks = new Block.Block[_SizeX, _SizeY];

            quadTree = new QuadTree(0, new Rectangle(0, 0, _SizeX * Block.Block.BlockSize, _SizeY * Block.Block.BlockSize), null);

            this.parentRegion = _ParentRegion;
        }

        public bool setBlockAtPosition(int _PosX, int _PosY, Block.Block _Block)
        {
            if (_PosX >= 0 && _PosX < this.size.X)
            {
                if (_PosY >= 0 && _PosY < this.size.Y)
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
            for (int x = 0; x < this.size.X; x++)
            {
                for (int y = 0; y < this.size.Y; y++)
                {
                    Block.Block var_Block = this.getBlockAtPosition(x, y);
                    if (x > 0)
                    {
                        var_Block.LeftNeighbour = this.getBlockAtPosition(x-1, y);
                    }
                    if (x < this.size.X-1)
                    {
                        var_Block.RightNeighbour = this.getBlockAtPosition(x + 1, y);
                    }
                    if (y > 0)
                    {
                        var_Block.TopNeighbour = this.getBlockAtPosition(x, y-1);
                    }
                    if (y < this.size.Y - 1)
                    {
                        var_Block.BottomNeighbour = this.getBlockAtPosition(x, y+1);
                    }
                }
            }
        }

        public void addLivingObjectToChunk(Object.LivingObject _LivingObject)
        {
            Block.Block var_Block = this.getBlockAtCoordinate(_LivingObject.Position.X, _LivingObject.Position.Y);
            var_Block.addLivingObject(_LivingObject);
            quadTree.insert(_LivingObject);
        }

        public Block.Block getBlockAtCoordinate(float _PosX, float _PosY)
        {
            return blocks[(int)(_PosX/Block.Block.BlockSize), ((int)_PosY/Block.Block.BlockSize)];
        }

        public Block.Block getBlockAtPosition(float _PosX, float _PosY)
        {
            return blocks[(int)(_PosX), ((int)_PosY)];
        }

        public void DrawTest(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            for (int x = 0; x < this.size.X; x++)
            {
                for (int y = 0; y < this.size.Y; y++)
                {
                    this.getBlockAtPosition(x, y).DrawTest(_GraphicsDevice, _SpriteBatch);
                }
            }
            for (int x = 0; x < this.size.X; x++)
            {
                for (int y = 0; y < this.size.Y; y++)
                {
                    this.getBlockAtPosition(x, y).DrawObjects(_GraphicsDevice, _SpriteBatch);
                }
            }

            //this.quadTree.DrawTest(_GraphicsDevice, _SpriteBatch);
        }

        public List<Object.LivingObject> getAllLivingObjectsinChunk()
        {
            List<Object.LivingObject> var_Result = new List<Object.LivingObject>();
            for (int x = 0; x < this.size.X; x++)
            {
                for (int y = 0; y < this.size.Y; y++)
                {
                    var_Result.AddRange(this.getBlockAtPosition(x, y).Objects);
                }
            }
            //var_Result = this.quadTree.getAllLivingObjects(var_Result);

            return var_Result;
        }

        public void update()
        {
            for (int x = 0; x < this.size.X; x++)
            {
                for (int y = 0; y < this.size.Y; y++)
                {
                    this.getBlockAtPosition(x, y).update();
                }
            }
            //quadTree.update();
        }

        public int getCountofAllObjects()
        {
            int var_Count = 0;
            for (int x = 0; x < this.size.X; x++)
            {
                for (int y = 0; y < this.size.Y; y++)
                {
                    var_Count += this.getBlockAtPosition(x, y).getCountofAllObjects();
                }
            }
            return var_Count;
        }
    }
}
