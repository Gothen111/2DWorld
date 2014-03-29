using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Server.Logger;
using Server.Model.Collison;
using Server.Model.Map.Block;

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
        private BlockEnum[,] blocks;
        private QuadTree quadTree;
        private int sizeX;

        public int SizeX
        {
            get { return sizeX; }
            set { sizeX = value; }
        }
        private int sizeY;

        public int SizeY
        {
            get { return sizeY; }
            set { sizeY = value; }
        }

        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Chunk(int _Id, int _PosX, int _PosY, int _SizeX, int _SizeY)
        {
            this.id = _Id;
            this.position = new Vector2(_PosX, _PosY);
            this.sizeX = _SizeX;
            this.sizeY = _SizeY;

            blocks = new BlockEnum[this.sizeX,this.sizeY];
        }

        public bool setBlockAtPosition(int _PosX, int _PosY, BlockEnum _BlockEnum)
        {
            if (_PosX >= 0 && _PosX < this.sizeX)
            {
                if (_PosY >= 0 && _PosY < this.sizeY)
                {
                    this.blocks[_PosX, _PosY] = _BlockEnum;
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
        }

        public BlockEnum getBlockEnumAtCoordinate(float _PosX, float _PosY)
        {
            return blocks[(int)(_PosX/Block.Block.BlockSize), ((int)_PosY/Block.Block.BlockSize)];
        }
    }
}
