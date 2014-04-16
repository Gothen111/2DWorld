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

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private Block.Block[,] blocks;
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

        private Region.Region parentRegion;

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

        public void addAnimatedObjectToChunk(Object.AnimatedObject _AnimatedObject)
        {
            quadTree.insert(_AnimatedObject);
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
                    int var_DrawPositionX = (int)(this.Position.X * Server.Factories.ChunkFactory.chunkSizeX + x) * Block.Block.BlockSize;
                    int var_DrawPositionY = (int)(this.Position.Y * Server.Factories.ChunkFactory.chunkSizeY + y) * Block.Block.BlockSize;
                    Vector2 var_DrawPosition = new Vector2(var_DrawPositionX, var_DrawPositionY);

                    BlockLayerEnum var_Layer = BlockLayerEnum.Layer1;
                    foreach (BlockEnum var_Enum in this.getBlockAtPosition(x, y).Layer)
                    {
                        if (var_Enum != BlockEnum.Nothing)
                        {
                            if (var_Layer == BlockLayerEnum.Layer1)
                            {
                                if (var_Enum == BlockEnum.Gras)
                                {
                                    _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Layer1/Gras"], var_DrawPosition, Color.White);
                                }
                                if (var_Enum == BlockEnum.Wall)
                                {
                                    _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Layer1/Wall"], var_DrawPosition, Color.White);
                                }
                            }
                            if (var_Layer == BlockLayerEnum.Layer2)
                            {
                                if (var_Enum == BlockEnum.Gras)
                                {
                                    _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Layer2/Gras"], var_DrawPosition, Color.White);
                                }
                                if (var_Enum == BlockEnum.Dirt)
                                {
                                    _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Layer2/Dirt"], var_DrawPosition, Color.White);
                                }
                            }
                        }
                        var_Layer += 1;
                    }
                }
            }

            this.quadTree.DrawTest(_GraphicsDevice, _SpriteBatch);

        }

        public void update()
        {
            quadTree.update();
        }

        public int getCountofAllObjects()
        {
            return this.quadTree.getCountofAllObjects();
        }
    }
}
