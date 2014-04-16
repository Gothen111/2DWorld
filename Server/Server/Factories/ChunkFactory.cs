using System;
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
        public static int chunkSizeX = 40; // 40
        public static int chunkSizeY = 40; // 40

        public Chunk generateChunk(int _Id, int _PosX, int PosY, ChunkEnum _ChunkEnum, List<Enum> _Layer, Region _ParentRegion)
        {
            switch (_ChunkEnum)
            {
                case ChunkEnum.Grassland:
                    {
                        return generateChunkGrassland(_Id, _PosX, PosY, chunkSizeX, chunkSizeY, _Layer, _ParentRegion);
                    }
            }
            return null;
        }

        private Chunk generateChunkGrassland(int _Id, int _PosX, int PosY, int _SizeX, int _SizeY, List<Enum> _Layer, Region _ParentRegion)
        {
            Chunk var_Result;

            var_Result = new Chunk(_Id, _PosX, PosY, _SizeX, _SizeY, _ParentRegion);
            this.fillChunkWithBlock(var_Result, BlockEnum.Gras);

            generateSecondLayer(var_Result, _Layer);
            generateWall(var_Result);

            return var_Result;
        }

        private Chunk fillChunkWithBlock(Chunk _Chunk, BlockEnum _BlockEnum)
        {
            for(int x = 0; x < _Chunk.Size.X; x++)
            {
                for(int y = 0; y < _Chunk.Size.Y; y++)
                {
                    if (!_Chunk.setBlockAtPosition(x, y, new Block(_BlockEnum)))
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
                        if (_Chunk.getBlockAtPosition(_PosX, _PosY).Layer.Count > 1 || _Chance <= 0)
                        {
                            return;
                        }

                        _Chunk.getBlockAtPosition(_PosX, _PosY).addLayer(_Enum);
                        rekursiveSetBlock(_Chunk, _Enum, _PosX + 1, _PosY, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Util.Random.GenerateGoodRandomNumber(1, 3));
                        rekursiveSetBlock(_Chunk, _Enum, _PosX - 1, _PosY, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Util.Random.GenerateGoodRandomNumber(1, 3));
                        rekursiveSetBlock(_Chunk, _Enum, _PosX, _PosY + 1, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Util.Random.GenerateGoodRandomNumber(1, 3));
                        rekursiveSetBlock(_Chunk, _Enum, _PosX, _PosY - 1, (_Chance - _ChanceToDecrease), _ChanceToDecrease * Util.Random.GenerateGoodRandomNumber(1, 3));
                    }
                }
            }
        }

        private void generateWall(Chunk _Chunk)
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
        }
    }
}
