﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Map.Chunk;
using Server.Model.Map.Region;
using Server.Model.Map.Block;

namespace Server.Factories
{
    class ChunkFactory
    {
        public static ChunkFactory chunkFactory = new ChunkFactory();

        public Chunk generateChunk(int _Id, int _PosX, int _PosY, ChunkEnum _ChunkEnum, List<Enum> _Layer, Region _ParentRegion)
        {
            switch (_ChunkEnum)
            {
                case ChunkEnum.Grassland:
                    {
                        return generateChunkGrassland(_Id, _PosX, _PosY, Chunk.chunkSizeX, Chunk.chunkSizeY, _Layer, _ParentRegion);
                    }
            }
            return null;
        }

        private Chunk generateChunkGrassland(int _Id, int _PosX, int _PosY, int _SizeX, int _SizeY, List<Enum> _Layer, Region _ParentRegion)
        {
            Chunk var_Result;

            var_Result = new Chunk(_Id, "Name :P", _PosX, _PosY, _SizeX, _SizeY, _ParentRegion);
            this.fillChunkWithBlock(var_Result, BlockEnum.Gras);

            //generateSecondLayer(var_Result, _Layer);
            //generateWall(var_Result);
            generateWall(var_Result, 20, 20);
            var_Result.setAllNeighboursOfBlocks();

            return var_Result;
        }

        private Chunk fillChunkWithBlock(Chunk _Chunk, BlockEnum _BlockEnum)
        {
            for(int x = 0; x < _Chunk.Size.X; x++)
            {
                for(int y = 0; y < _Chunk.Size.Y; y++)
                {
                    if (!_Chunk.setBlockAtPosition(x, y, new Block((int)_Chunk.Position.X + x * Block.BlockSize, (int)_Chunk.Position.Y + y * Block.BlockSize, _BlockEnum, _Chunk)))
                    {
                        Logger.Logger.LogErr("RegionFactory->fillChunkWithBlock(...) : Platzierung nicht möglich!");
                    }
                }
            }
            return _Chunk;
        }

        private void generateSecondLayer(Chunk _Chunk, List<Enum> _Layer)
        {
            int var_Count = Util.Random.GenerateGoodRandomNumber(0, (int)((_Chunk.Size.X * _Chunk.Size.Y)*0.1)); //  / 100
            for (int i = 0; i < var_Count; i++)
            {
                int var_EnumId = Util.Random.GenerateGoodRandomNumber(0, _Layer.Count);
                int var_PosX = Util.Random.GenerateGoodRandomNumber(0, (int)_Chunk.Size.X);
                int var_PosY = Util.Random.GenerateGoodRandomNumber(0, (int)_Chunk.Size.Y);
                rekursiveSetBlock(_Chunk, _Layer.ElementAt(var_EnumId), var_PosX, var_PosY, 100, 20);
            }
        }

        private void rekursiveSetBlock(Chunk _Chunk, Enum _Enum, int _PosX, int _PosY, int _Chance, int _ChanceToDecrease)
        {
            int var_Chance = Util.Random.GenerateGoodRandomNumber(0, 100);

            if (var_Chance <= _Chance)
            {
                if (_PosX >= 0 && _PosX <= (_Chunk.Size.X-1))
                {
                    if (_PosY >= 0 && _PosY <= (_Chunk.Size.Y - 1))
                    {
                        if (_Chunk.getBlockAtPosition(_PosX, _PosY).Layer[(int)BlockLayerEnum.Layer2] != BlockEnum.Nothing || _Chance <= 0)
                        {
                            return;
                        }

                        if ((BlockEnum)_Enum == BlockEnum.Dirt)
                        {
                            //Logger.Logger.LogDeb(""); // ???
                        }

                        _Chunk.getBlockAtPosition(_PosX, _PosY).setLayerAt(_Enum, BlockLayerEnum.Layer2);
                        rekursiveSetBlock(_Chunk, _Enum, _PosX + 1, _PosY, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Util.Random.GenerateGoodRandomNumber(1, 3));
                        rekursiveSetBlock(_Chunk, _Enum, _PosX - 1, _PosY, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Util.Random.GenerateGoodRandomNumber(1, 3));
                        rekursiveSetBlock(_Chunk, _Enum, _PosX, _PosY + 1, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Util.Random.GenerateGoodRandomNumber(1, 3));
                        rekursiveSetBlock(_Chunk, _Enum, _PosX, _PosY - 1, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Util.Random.GenerateGoodRandomNumber(1, 3));
                    }
                }
            }
        }

        private void generateWall(Chunk _Chunk, int _PosX, int _PosY)
        {
            int var_Steps = 112;
            int var_StepsUp = var_Steps/4;
            int var_StepsLeft = var_Steps/4;
            int var_StepsRight = var_Steps/4;
            int var_StepsDown = var_Steps/4;

            int var_ComeFrom = -1; //0=Up,1=Left,2=Right,3=Down
            _Chunk.getBlockAtPosition(_PosX, _PosY).setFirstLayer(BlockEnum.Wall);
            while (var_StepsUp + var_StepsLeft + var_StepsRight + var_StepsDown > 0)
            {
                List<int> var_GoTo = new List<int>();
                var_GoTo.AddRange(new List<int>() { 0, 1, 2, 3 });
                var_GoTo.Remove(var_ComeFrom);
                if (var_StepsUp == 0 || _PosY < 0)
                {
                    var_GoTo.Remove(0);
                }
                if (var_StepsLeft == 0 || _PosX < 0)
                {
                    var_GoTo.Remove(1);
                }
                if (var_StepsRight == 0 || _PosX >= Chunk.chunkSizeX)
                {
                    var_GoTo.Remove(2);
                }
                if (var_StepsDown == 0 || _PosY >= Chunk.chunkSizeY)
                {
                    var_GoTo.Remove(3);
                }
                if (var_GoTo.Count == 0)
                {
                    var_GoTo.Add(var_ComeFrom);
                }
                int var_Rand = Util.Random.GenerateGoodRandomNumber(0, var_GoTo.Count);

                int var_Choice = var_GoTo.ElementAt(var_Rand);

                if (var_Choice == 0 && var_StepsUp>0)
                {
                    _PosX += 0;
                    _PosY -= 1;
                    var_ComeFrom = 3;
                    var_StepsUp -= 1;
                }
                else if (var_Choice == 1 && var_StepsLeft > 0)
                {
                    _PosX -= 1;
                    _PosY += 0;
                    var_ComeFrom = 2;
                    var_StepsLeft -= 1;
                }
                else if (var_Choice == 2 && var_StepsRight > 0)
                {
                    _PosX += 1;
                    _PosY += 0;
                    var_ComeFrom = 1;
                    var_StepsRight -= 1;
                }
                else if (var_Choice == 3 && var_StepsDown > 0)
                {
                    _PosX += 0;
                    _PosY += 1;
                    var_ComeFrom = 0;
                    var_StepsDown -= 1;
                }
                _Chunk.getBlockAtPosition(_PosX, _PosY).setFirstLayer(BlockEnum.Wall);
            }
        }


        /*private void generateWall(Chunk _Chunk)
        {
            int var_Count = 10;
            for (int i = 0; i < var_Count; i++)
            {
                int var_PosX = Util.Random.GenerateGoodRandomNumber(0, (int)_Chunk.Size.X);
                int var_PosY = Util.Random.GenerateGoodRandomNumber(0, (int)_Chunk.Size.Y);
                rekursiveSetWall(_Chunk, BlockEnum.Wall, var_PosX, var_PosY, 100, 5);
            }
        }

        private void rekursiveSetWall(Chunk _Chunk, Enum _Enum, int _PosX, int _PosY, int _Chance, int _ChanceToDecrease)
        {
            int var_Chance = Util.Random.GenerateGoodRandomNumber(0, 100);

            if (var_Chance <= _Chance)
            {
                if (_PosX >= 0 && _PosX <= (_Chunk.Size.X - 1))
                {
                    if (_PosY >= 0 && _PosY <= (_Chunk.Size.Y - 1))
                    {
                        if (_Chance <= 0)
                        {
                            return;
                        }

                        _Chunk.getBlockAtPosition(_PosX, _PosY).setFirstLayer(_Enum);
                        rekursiveSetWall(_Chunk, _Enum, _PosX + 1, _PosY, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Util.Random.GenerateGoodRandomNumber(1, 3));
                        rekursiveSetWall(_Chunk, _Enum, _PosX - 1, _PosY, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Util.Random.GenerateGoodRandomNumber(1, 3));
                        rekursiveSetWall(_Chunk, _Enum, _PosX, _PosY + 1, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Util.Random.GenerateGoodRandomNumber(1, 3));
                        rekursiveSetWall(_Chunk, _Enum, _PosX, _PosY - 1, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Util.Random.GenerateGoodRandomNumber(1, 3));
                    }
                }
            }
        }*/
    }
}
