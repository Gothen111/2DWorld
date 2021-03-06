﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using GameLibrary.Logger;
using GameLibrary.Model.Collison;
using GameLibrary.Model.Map.Block;

using Microsoft.Xna.Framework.Graphics;
using GameLibrary.Connection;
using GameLibrary.Connection.Message;

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

        public static int chunkSizeX = 10;
        public static int chunkSizeY = 10;

        private Block.Block[] blocks;

        public Block.Block[] Blocks
        {
            get { return blocks; }
            set { blocks = value; }
        }

        private List<Vector3> pathEntries;

        public List<Vector3> PathEntries
        {
            get { return pathEntries; }
            set { pathEntries = value; }
        }

        private ChunkEnum chunkEnum;

        public ChunkEnum ChunkEnum
        {
            get { return chunkEnum; }
            set { chunkEnum = value; }
        }

        public Chunk(String _Name, int _PosX, int _PosY, Region.Region _ParentRegion)
            :base()
        {
            this.Name = _Name;
            this.Position = new Vector3(_PosX, _PosY, 0);
            this.Size = new Vector3(chunkSizeX, chunkSizeY, 0);
            this.Bounds = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)(chunkSizeX * Block.Block.BlockSize - 1), (int)(chunkSizeY * Block.Block.BlockSize - 1));

            this.blocks = new Block.Block[chunkSizeX * chunkSizeY];

            this.pathEntries = new List<Vector3>();

            this.Parent = _ParentRegion;

            if (Configuration.Configuration.isHost)
            {
            }
            else
            {
                this.requestFromServer();
            }
        }

        public Chunk(SerializationInfo info, StreamingContext ctxt) 
            :base(info, ctxt)
        {
            this.id = (int)info.GetValue("id", typeof(int));
            //this.blocks = (Block.Block[,])info.GetValue("blocks", typeof(Block.Block[,]));
            this.blocks = new Block.Block[(int)this.Size.X * (int)this.Size.Y];
            //setAllNeighboursOfBlocks();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("id", this.id);
            //info.AddValue("blocks", this.blocks, typeof(Block.Block[,]));
        }

        public bool setBlockAtCoordinate(Vector2 _Position, Block.Block _Block)
        {
            int var_X = (int)Math.Abs(_Position.X - this.Position.X) / Block.Block.BlockSize;
            int var_Y = (int)Math.Abs(_Position.Y - this.Position.Y) / Block.Block.BlockSize;
            return this.setBlockAtPosition(var_X, var_Y, _Block);
        }

        public bool setBlockAtPosition(int _PosX, int _PosY, Block.Block _Block)
        {
            if (_PosX >= 0 && _PosX < this.Size.X)
            {
                if (_PosY >= 0 && _PosY < this.Size.Y)
                {
                    int var_Pos = (int)(_PosX + chunkSizeY * _PosY);
                    this.blocks[var_Pos] = _Block;
                    this.setAllNeighboursOfBlocks();
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
                    if (var_Block != null)
                    {
                        var_Block.Parent = this;
                        if (x > 0)
                        {
                            Block.Block var_BlockLeft = this.getBlockAtPosition(x - 1, y);
                            if (var_BlockLeft != null)
                            {
                                var_Block.LeftNeighbour = this.getBlockAtPosition(x - 1, y);
                            }
                        }
                        if (x < this.Size.X - 1)
                        {
                            Block.Block var_BlockRight = this.getBlockAtPosition(x + 1, y);
                            if (var_BlockRight != null)
                            {
                                var_Block.RightNeighbour = var_BlockRight;
                            }
                        }
                        if (y > 0)
                        {
                            Block.Block var_BlockTop = this.getBlockAtPosition(x, y - 1);
                            if (var_BlockTop != null)
                            {
                                var_Block.TopNeighbour = var_BlockTop;
                            }
                        }
                        if (y < this.Size.Y - 1)
                        {
                            Block.Block var_BlockBottom = this.getBlockAtPosition(x, y + 1);
                            if (var_BlockBottom != null)
                            {
                                var_Block.BottomNeighbour = var_BlockBottom;
                            }
                        }
                    }
                }
            }
        }

        public Block.Block getBlockAtCoordinate(Vector3 _Position)
        {
            int var_X = (int)Math.Abs(_Position.X - this.Position.X) / Block.Block.BlockSize;//(int)((_PosX % (Region.Region.regionSizeX * Chunk.chunkSizeX * Block.Block.BlockSize)) % (Chunk.chunkSizeX * Block.Block.BlockSize) / Block.Block.BlockSize);
            int var_Y = (int)Math.Abs(_Position.Y - this.Position.Y) / Block.Block.BlockSize;//(int)((_PosY % (Region.Region.regionSizeY * Chunk.chunkSizeY * Block.Block.BlockSize)) % (Chunk.chunkSizeY * Block.Block.BlockSize) / Block.Block.BlockSize);
            return this.getBlockAtPosition(var_X, var_Y);
        }

        public Block.Block getBlockAtPosition(float _PosX, float _PosY)
        {
            if (_PosX >= 0 && _PosX < Chunk.chunkSizeX && _PosY >= 0 && _PosY < Chunk.chunkSizeY)
            {
                int var_Pos = (int)(_PosX + chunkSizeY * _PosY);
                return blocks[var_Pos];//blocks[(int)(_PosX), ((int)_PosY)];
            }
            return null;
        }

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);
            /*for (int x = 0; x < this.Size.X; x++)
            {
                for (int y = 0; y < this.Size.Y; y++)
                {
                    this.getBlockAtPosition(x, y).update();
                }
            }*/
        }

        public List<Object.Object> getAllObjectsInChunk()
        {
            List<Object.Object> result = new List<Object.Object>();
            for (int x = 0; x < this.Size.X; x++)
            {
                for (int y = 0; y < this.Size.Y; y++)
                {
                    if (this.getBlockAtPosition(x, y) != null)
                    {
                        foreach (Object.Object var_Object in this.getBlockAtPosition(x, y).Objects)
                        {
                            result.Add(var_Object);
                        }
                    }
                }
            }
            return result;
        }

        public List<Object.Object> getAllEnvironmentObjectsInChunk()
        {
            List<Object.Object> result = new List<Object.Object>();
            for (int x = 0; x < this.Size.X; x++)
            {
                for (int y = 0; y < this.Size.Y; y++)
                {
                    foreach (Object.Object var_Object in this.getBlockAtPosition(x, y).ObjectsPreEnviorment)
                    {
                        result.Add(var_Object);
                    }
                }
            }
            return result;
        }

        public static Vector3 parsePosition(Vector3 _Position)
        {
            int _PosX = (int)_Position.X;
            int _PosY = (int)_Position.Y;

            int var_SizeX = (Chunk.chunkSizeX * Block.Block.BlockSize);
            int var_SizeY = (Chunk.chunkSizeY * Block.Block.BlockSize);

            int var_RestX = _PosX % var_SizeX;
            int var_RestY = _PosY % var_SizeY;

            if (var_RestX != 0)
            {
                if (_PosX < 0)
                {
                    _PosX = _PosX - (var_SizeX + var_RestX);
                }
                else
                {
                    _PosX = _PosX - var_RestX;
                }
            }
            if (var_RestY != 0)
            {
                if (_PosY < 0)
                {
                    _PosY = _PosY - (var_SizeY + var_RestY);
                }
                else
                {
                    _PosY = _PosY - var_RestY;
                }
            }

            return new Vector3(_PosX, _PosY, _Position.Z);
        }

        public override void requestFromServer()
        {
            base.requestFromServer();
            if (this.Parent == null)
            {
            }
            else
            {
                Configuration.Configuration.networkManager.addEvent(new RequestChunkMessage(this.Position), GameMessageImportance.VeryImportant);
            }
        }
    }
}
