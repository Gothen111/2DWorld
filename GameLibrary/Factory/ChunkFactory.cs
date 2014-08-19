using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using GameLibrary.Model.Map.Chunk;
using GameLibrary.Model.Map.Region;
using GameLibrary.Model.Map.Block;
using GameLibrary.Factory.FactoryEnums;

namespace GameLibrary.Factory
{
    public class ChunkFactory
    {
        public static ChunkFactory chunkFactory = new ChunkFactory();

        public Chunk generateChunk(int _PosX, int _PosY, ChunkEnum _ChunkEnum, List<Enum> _Layer, Region _ParentRegion)
        {
            switch (_ChunkEnum)
            {
                case ChunkEnum.Grassland:
                    {
                        return generateChunkGrassland(_PosX, _PosY, Chunk.chunkSizeX, Chunk.chunkSizeY, _Layer, _ParentRegion);
                    }
                case ChunkEnum.Snowland:
                    {
                        return generateChunkSnowland(_PosX, _PosY, Chunk.chunkSizeX, Chunk.chunkSizeY, _Layer, _ParentRegion);
                    }
            }
            return null;
        }

        private Chunk generateChunkGrassland(int _PosX, int _PosY, int _SizeX, int _SizeY, List<Enum> _Layer, Region _ParentRegion)
        {
            Chunk var_Result;

            var_Result = new Chunk("Chunk", _PosX, _PosY, _SizeX, _SizeY, _ParentRegion);
            this.fillChunkWithBlock(var_Result, BlockEnum.Ground1);

            var_Result.setAllNeighboursOfBlocks();
            //generateWall(var_Result, Util.Random.GenerateGoodRandomNumber(0, Chunk.chunkSizeX), Util.Random.GenerateGoodRandomNumber(0, Chunk.chunkSizeY));
            //generateSecondLayer(var_Result, _Layer);
            //generateFlowers(var_Result);
            //generateTrees(var_Result);
            //generateWall(var_Result);
            //if (var_Result.Id == 0)
            //{
                //generateNpc(var_Result);
                //generateHeightMap(var_Result);
            //}

            //generateCoins(var_Result);
            //generateStuff(var_Result);

            return var_Result;
        }

        private Chunk generateChunkSnowland(int _PosX, int _PosY, int _SizeX, int _SizeY, List<Enum> _Layer, Region _ParentRegion)
        {
            Chunk var_Result;

            var_Result = new Chunk("Chunk", _PosX, _PosY, _SizeX, _SizeY, _ParentRegion);
            this.fillChunkWithBlock(var_Result, BlockEnum.Ground1);

            var_Result.setAllNeighboursOfBlocks();
            //generateWall(var_Result, Util.Random.GenerateGoodRandomNumber(0, Chunk.chunkSizeX), Util.Random.GenerateGoodRandomNumber(0, Chunk.chunkSizeY));
            //generateSecondLayer(var_Result, _Layer);
            //generateFlowers(var_Result);
            //generateTrees(var_Result);
            //generateWall(var_Result);
            //generateNpc(var_Result);

            //generateCoins(var_Result);

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
                        Block var_Block = _Chunk.getBlockAtPosition(_PosX, _PosY);
                        if (var_Block.Layer[(int)BlockLayerEnum.Layer2] != BlockEnum.Nothing || _Chance <= 0)
                        {
                            return;
                        }
                        if (var_Block.Layer[0]  == BlockEnum.Wall)
                        {
                            return;
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

            List<Block> blocksWithWall = new List<Block>();
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
                Block var_Block = _Chunk.getBlockAtPosition(_PosX, _PosY);
                if (var_Block != null)
                {
                    var_Block.setFirstLayer(BlockEnum.Wall);
                    blocksWithWall.Add(var_Block);
                }
            }

            for (int x = 0; x < blocksWithWall.Count / 2; x++)
            {
                Block var_Block1 = blocksWithWall.ElementAt(x);
                Block var_Block2 = blocksWithWall.ElementAt(blocksWithWall.Count - x - 1);
                while (!var_Block1.Equals(var_Block2))
                {
                    int moveHorizontal = 0;
                    int moveVertical = 0;
                    if (var_Block1.Position.X < var_Block2.Position.X)
                    {
                        moveHorizontal = 1;
                    }
                    else if (var_Block1.Position.X > var_Block2.Position.X)
                    {
                        moveHorizontal = -1;
                    }
                    if (var_Block1.Position.Y < var_Block2.Position.Y)
                    {
                        moveVertical = 1;
                    }
                    else if (var_Block1.Position.Y > var_Block2.Position.Y)
                    {
                        moveVertical = -1;
                    }

                    if (moveHorizontal == 1 && moveVertical == 1)
                    {
                        var_Block1 = var_Block1.RightNeighbour.BottomNeighbour as Block;
                    }
                    else if (moveHorizontal == 1 && moveVertical == 0)
                    {
                        var_Block1 = var_Block1.RightNeighbour as Block;
                    }
                    else if (moveHorizontal == 1 && moveVertical == -1)
                    {
                        var_Block1 = var_Block1.RightNeighbour.TopNeighbour as Block;
                    }
                    else if (moveHorizontal == 0 && moveVertical == 1)
                    {
                        var_Block1 = var_Block1.BottomNeighbour as Block;
                    }
                    else if (moveHorizontal == 0 && moveVertical == -1)
                    {
                        var_Block1 = var_Block1.TopNeighbour as Block;
                    }
                    else if (moveHorizontal == -1 && moveVertical == 1)
                    {
                        var_Block1 = var_Block1.LeftNeighbour.BottomNeighbour as Block;
                    }
                    else if (moveHorizontal == -1 && moveVertical == 0)
                    {
                        var_Block1 = var_Block1.LeftNeighbour as Block;
                    }
                    else if (moveHorizontal == -1 && moveVertical == -1)
                    {
                        var_Block1 = var_Block1.LeftNeighbour.TopNeighbour as Block;
                    }


                    if (var_Block1 != null)
                    {
                        var_Block1.setFirstLayer(BlockEnum.Wall);
                    }
                    else
                    {
                        Logger.Logger.LogErr("Wallgenerierung hat Chunkgrenzen übersprungen und kann somit nicht ausgefüllt werden");
                        return;
                    }
                }
            }
        }

        private void generateFlowers(Chunk _Chunk)
        {
            int var_Count = Chunk.chunkSizeX * Chunk.chunkSizeY / Util.Random.GenerateGoodRandomNumber(1,5);
            for (int i = 0; i < var_Count; i++)
            {
                GameLibrary.Model.Object.EnvironmentObject var_EnvironmentObject = EnvironmentFactory.environmentFactory.createEnvironmentObject(((Region)_Chunk.Parent).RegionEnum,  EnvironmentEnum.Flower_1);

                int var_X = Util.Random.GenerateGoodRandomNumber(1, GameLibrary.Model.Map.Chunk.Chunk.chunkSizeX * (GameLibrary.Model.Map.Block.Block.BlockSize) - 1);
                int var_Y = Util.Random.GenerateGoodRandomNumber(1, GameLibrary.Model.Map.Chunk.Chunk.chunkSizeY * (GameLibrary.Model.Map.Block.Block.BlockSize) - 1);

                var_EnvironmentObject.Position = new Vector3(var_X + _Chunk.Position.X, var_Y + _Chunk.Position.Y, 0);
                //var_EnvironmentObject.CollisionBounds.Add(new Rectangle(var_EnvironmentObject.DrawBounds.Left, var_EnvironmentObject.DrawBounds.Bottom, var_EnvironmentObject.DrawBounds.Width, 20));

                Block var_Block = _Chunk.getBlockAtCoordinate(var_EnvironmentObject.Position.X, var_EnvironmentObject.Position.Y);

                if (var_Block.IsWalkAble && var_Block.Layer[1] == BlockEnum.Nothing)
                {
                    var_Block.ObjectsPreEnviorment.Add(var_EnvironmentObject);
                    var_EnvironmentObject.CurrentBlock = var_Block;
                    ((Model.Map.World.World)_Chunk.Parent.Parent).QuadTreeEnvironmentObject.Insert(var_EnvironmentObject);
                    //Chunk ist noch null ;) in der world.... da noch nicht hinzugefügt
                    //((Model.Map.World.World)_Chunk.Parent.Parent).addPreEnvironmentObject(var_EnvironmentObject);
                }
            }
        }

        private void generateNpc(Chunk _Chunk)
        {
            int var_Count = 20;
            for (int i = 0; i < var_Count; i++)
            {
                GameLibrary.Model.Object.NpcObject var_NpcObject = CreatureFactory.creatureFactory.createNpcObject(RaceEnum.Human, FactionEnum.Beerdrinker, CreatureEnum.Archer, GenderEnum.Female);

                int var_X = Util.Random.GenerateGoodRandomNumber(1, GameLibrary.Model.Map.Chunk.Chunk.chunkSizeX * (GameLibrary.Model.Map.Block.Block.BlockSize) - 1);
                int var_Y = Util.Random.GenerateGoodRandomNumber(1, GameLibrary.Model.Map.Chunk.Chunk.chunkSizeY * (GameLibrary.Model.Map.Block.Block.BlockSize) - 1);

                var_NpcObject.Position = new Vector3(var_X + _Chunk.Position.X, var_Y + _Chunk.Position.Y, 0);

                Block var_Block = _Chunk.getBlockAtCoordinate(var_NpcObject.Position.X, var_NpcObject.Position.Y);
                //Block var_Block = _Chunk.getBlockAtCoordinate(var_X, var_Y);
                if (var_Block.IsWalkAble && var_Block.Layer[1] == BlockEnum.Nothing)
                {
                    var_Block.Objects.Add(var_NpcObject);
                    var_NpcObject.CurrentBlock = var_Block;
                    ((Model.Map.World.World)_Chunk.Parent.Parent).QuadTreeObject.Insert(var_NpcObject);
                }
            }
        }

        private void generateTrees(Chunk _Chunk)
        {
            int var_Count = Chunk.chunkSizeX * Chunk.chunkSizeY / 8 / Util.Random.GenerateGoodRandomNumber(1, 5);
            for (int i = 0; i < var_Count; i++)
            {
                GameLibrary.Model.Object.EnvironmentObject var_EnvironmentObject = EnvironmentFactory.environmentFactory.createEnvironmentObject(((Region)_Chunk.Parent).RegionEnum, EnvironmentEnum.Tree_Normal_1);

                int var_X = Util.Random.GenerateGoodRandomNumber(1, GameLibrary.Model.Map.Chunk.Chunk.chunkSizeX * (GameLibrary.Model.Map.Block.Block.BlockSize) - 1);
                int var_Y = Util.Random.GenerateGoodRandomNumber(1, GameLibrary.Model.Map.Chunk.Chunk.chunkSizeY * (GameLibrary.Model.Map.Block.Block.BlockSize) - 1);

                var_EnvironmentObject.Position = new Vector3(var_X + _Chunk.Position.X, var_Y + _Chunk.Position.Y, 0);
                var_EnvironmentObject.CollisionBounds.Add(new Rectangle(var_EnvironmentObject.DrawBounds.Left + 15, var_EnvironmentObject.DrawBounds.Bottom - 30, var_EnvironmentObject.DrawBounds.Width - 30, 20));

                Block var_Block = _Chunk.getBlockAtCoordinate(var_EnvironmentObject.Position.X, var_EnvironmentObject.Position.Y);

                if (var_Block.IsWalkAble)
                {
                    var_Block.Objects.Add(var_EnvironmentObject);
                    var_EnvironmentObject.CurrentBlock = var_Block;
                    ((Model.Map.World.World)_Chunk.Parent.Parent).QuadTreeObject.Insert(var_EnvironmentObject);

                    //TODO: Das stimmt natürlich nicht ganz ;) aber erst mal für den AStar...
                    //var_Block.IsWalkAble = false;
                }
            }
        }

        private void generateCoins(Chunk _Chunk)
        {
            int var_Count = Chunk.chunkSizeX * Chunk.chunkSizeY / 8 / Util.Random.GenerateGoodRandomNumber(1, 5);
            for (int i = 0; i < var_Count; i++)
            {
                GameLibrary.Model.Object.ItemObject var_itemObject = ItemFactory.itemFactory.createItemObject(ItemEnum.GoldCoin);

                int var_X = Util.Random.GenerateGoodRandomNumber(1, GameLibrary.Model.Map.Chunk.Chunk.chunkSizeX * (GameLibrary.Model.Map.Block.Block.BlockSize) - 1);
                int var_Y = Util.Random.GenerateGoodRandomNumber(1, GameLibrary.Model.Map.Chunk.Chunk.chunkSizeY * (GameLibrary.Model.Map.Block.Block.BlockSize) - 1);

                var_itemObject.Position = new Vector3(var_X + _Chunk.Position.X, var_Y + _Chunk.Position.Y, 0);

                Block var_Block = _Chunk.getBlockAtCoordinate(var_itemObject.Position.X, var_itemObject.Position.Y);
                if (var_Block.IsWalkAble)
                {
                    var_Block.Objects.Add(var_itemObject);
                    ((Model.Map.World.World)_Chunk.Parent.Parent).QuadTreeObject.Insert(var_itemObject);
                }
            }
        }

        private void generateStuff(Chunk _Chunk)
        {
            int var_Count = Chunk.chunkSizeX * Chunk.chunkSizeY / 8 / Util.Random.GenerateGoodRandomNumber(1, 5);
            for (int i = 0; i < var_Count; i++)
            {
                GameLibrary.Model.Object.ItemObject var_itemObject = EquipmentFactory.equipmentFactory.createEquipmentWeaponObject(WeaponEnum.Sword);

                int var_X = Util.Random.GenerateGoodRandomNumber(1, GameLibrary.Model.Map.Chunk.Chunk.chunkSizeX * (GameLibrary.Model.Map.Block.Block.BlockSize) - 1);
                int var_Y = Util.Random.GenerateGoodRandomNumber(1, GameLibrary.Model.Map.Chunk.Chunk.chunkSizeY * (GameLibrary.Model.Map.Block.Block.BlockSize) - 1);

                var_itemObject.Position = new Vector3(var_X + _Chunk.Position.X, var_Y + _Chunk.Position.Y, 0);

                Block var_Block = _Chunk.getBlockAtCoordinate(var_itemObject.Position.X, var_itemObject.Position.Y);
                if (var_Block.IsWalkAble)
                {
                    var_Block.Objects.Add(var_itemObject);
                    ((Model.Map.World.World)_Chunk.Parent.Parent).QuadTreeObject.Insert(var_itemObject);
                }
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

        private void generateHeightMap(Chunk _Chunk)
        {
            int var_Min = 0;
            int var_Max = 10;
            int var_RandomRange = 5;

            float[,] var_HeightMap = new float[(int)_Chunk.Size.X, (int)_Chunk.Size.Y];

            for (int x = 0; x < var_HeightMap.GetLength(0); x++)
            {
                for (int y = 0; y < var_HeightMap.GetLength(1); y++)
                {
                    var_HeightMap[x, y] = var_Min + Util.Random.GenerateGoodRandomNumber(0, var_RandomRange);
                }
            }

            for (int x = 0; x < var_HeightMap.GetLength(0); x++)
            {
                for (int y = 0; y < var_HeightMap.GetLength(1); y++)
                {
                    float var_Add = 0;
                    if (x - 1 >= 0)
                    {
                        var_Add += var_HeightMap[x - 1, y];
                    }
                    if (x + 1 < var_HeightMap.GetLength(0))
                    {
                        var_Add += var_HeightMap[x + 1, y];
                    }
                    if (y - 1 >= 0)
                    {
                        var_Add += var_HeightMap[x, y - 1];
                    }
                    if (y + 1 < var_HeightMap.GetLength(1))
                    {
                        var_Add += var_HeightMap[x, y + 1];
                    }
                    var_HeightMap[x, y] = var_Add/4 + Util.Random.GenerateGoodRandomNumber(0, var_RandomRange) / 2;
                    if (var_HeightMap[x, y] > var_Max)
                    {
                        var_HeightMap[x, y] = var_Max;
                    }

                    if (var_HeightMap[x, y] >= 3)
                    {
                        Block var_Block = _Chunk.getBlockAtPosition(x, y);
                        var_Block.Height = 1;
                    }
                }
            }

            for (int x = 0; x < var_HeightMap.GetLength(0); x++)
            {
                for (int y = 0; y < var_HeightMap.GetLength(1); y++)
                {
                    if (var_HeightMap[x, y] >= 3)
                    {
                        Block var_Block = _Chunk.getBlockAtPosition(x, y);
                        var_Block.Height = 1;

                        if (var_Block.RightNeighbour != null)
                        {
                            if (((Block)var_Block.RightNeighbour).Height < var_Block.Height)
                            {
                                var_Block.Layer[0] = BlockEnum.Hill1_Right;
                            }
                        }

                        if (var_Block.LeftNeighbour != null)
                        {
                            if (((Block)var_Block.LeftNeighbour).Height < var_Block.Height)
                            {
                                var_Block.Layer[0] = BlockEnum.Hill1_Left;
                            }
                        }

                        if (var_Block.TopNeighbour != null)
                        {
                            if (((Block)var_Block.TopNeighbour).Height < var_Block.Height)
                            {
                                _Chunk.getBlockAtPosition(x, y).Layer[0] = BlockEnum.Hill1_Top;
                                if (var_Block.LeftNeighbour != null)
                                {
                                    if (((Block)var_Block.LeftNeighbour).Height < var_Block.Height)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_Corner1;
                                    }
                                }
                                if (var_Block.RightNeighbour != null)
                                {
                                    if (((Block)var_Block.RightNeighbour).Height < var_Block.Height)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_Corner2;
                                    }
                                }
                            }
                        }

                        if (var_Block.BottomNeighbour != null)
                        {
                            if (((Block)var_Block.BottomNeighbour).Height < var_Block.Height)
                            {
                                _Chunk.getBlockAtPosition(x, y).Layer[0] = BlockEnum.Hill1_Bottom;
                                if (var_Block.LeftNeighbour != null)
                                {
                                    if (((Block)var_Block.LeftNeighbour).Height < var_Block.Height)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_Corner4;
                                    }
                                }
                                if (var_Block.RightNeighbour != null)
                                {
                                    if (((Block)var_Block.RightNeighbour).Height < var_Block.Height)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_Corner3;
                                    }
                                }
                            }
                        }

                        if (var_Block.Layer[0] == BlockEnum.Ground1)
                        {
                            var_Block.Layer[0] = BlockEnum.Hill1_Center;
                        }
                    }
                }
            }

            for (int x = 0; x < var_HeightMap.GetLength(0); x++)
            {
                for (int y = 0; y < var_HeightMap.GetLength(1); y++)
                {
                    if (var_HeightMap[x, y] >= 3)
                    {
                        Block var_Block = _Chunk.getBlockAtPosition(x, y);

                        if (var_Block.BottomNeighbour != null)
                        {
                            if (((Block)var_Block.BottomNeighbour).Layer[0] == BlockEnum.Hill1_Right)
                            {
                                if (var_Block.RightNeighbour != null)
                                {
                                    if (((Block)var_Block.RightNeighbour).Layer[0] == BlockEnum.Hill1_Bottom)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_InsideCorner1;
                                    }
                                }
                            }
                            if (((Block)var_Block.BottomNeighbour).Layer[0] == BlockEnum.Hill1_Left)
                            {
                                if (var_Block.LeftNeighbour != null)
                                {
                                    if (((Block)var_Block.LeftNeighbour).Layer[0] == BlockEnum.Hill1_Bottom)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_InsideCorner2;
                                    }
                                }
                            }
                        }

                        if (var_Block.TopNeighbour != null)
                        {
                            if (((Block)var_Block.TopNeighbour).Layer[0] == BlockEnum.Hill1_Left)
                            {
                                if (var_Block.LeftNeighbour != null)
                                {
                                    if (((Block)var_Block.LeftNeighbour).Layer[0] == BlockEnum.Hill1_Top)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_InsideCorner3;
                                    }
                                }
                            }

                            if (((Block)var_Block.TopNeighbour).Layer[0] == BlockEnum.Hill1_Right)
                            {
                                if (var_Block.RightNeighbour != null)
                                {
                                    if (((Block)var_Block.RightNeighbour).Layer[0] == BlockEnum.Hill1_Top)
                                    {
                                        var_Block.Layer[0] = BlockEnum.Hill1_InsideCorner4;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //AUSGABE
            for (int y = 0; y < var_HeightMap.GetLength(1); y++)
            {
                for (int x = 0; x < var_HeightMap.GetLength(0); x++)
                {
                    Console.Write( (int)var_HeightMap[x, y] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
