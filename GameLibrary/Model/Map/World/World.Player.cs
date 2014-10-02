using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using GameLibrary.Model.Map.Region;

using Microsoft.Xna.Framework.Graphics;
using GameLibrary.Model.Object;
using GameLibrary.Model.Collison;
using GameLibrary.Connection;
using GameLibrary.Connection.Message;

namespace GameLibrary.Model.Map.World
{
    public partial class World
    {
        #region Player

        public PlayerObject getPlayerObject(int _Id)
        {
            foreach (PlayerObject var_PlayerObject in playerObjects)
            {
                if (var_PlayerObject.Id == _Id)
                {
                    return var_PlayerObject;
                }
            }
            return null;
        }

        public bool containsPlayerObject(Object.PlayerObject _PlayerObject)
        {
            return this.playerObjects.Contains(_PlayerObject);
        }

        public void addPlayerObject(Object.PlayerObject _PlayerObject)
        {
            this.addPlayerObject(_PlayerObject, false);
        }

        public void addPlayerObject(Object.PlayerObject _PlayerObject, bool _OnlyToPlayerList)
        {
            if (!containsPlayerObject(_PlayerObject))
            {
                this.playerObjects.Add(_PlayerObject);

                if (!_OnlyToPlayerList)
                {
                    //if (Configuration.Configuration.isHost)
                    //{
                        //Vector2 var_Position_Region = new Vector2(_PlayerObject.Position.X - _PlayerObject.Position.X % (Region.Region.regionSizeX * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize), _PlayerObject.Position.Y - _PlayerObject.Position.Y % (Region.Region.regionSizeY * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize));

                        Region.Region var_Region = World.world.getRegionAtPosition(_PlayerObject.Position)
                                                    ?? World.world.createRegionAt(_PlayerObject.Position);
                        if (var_Region != null)
                        {
                            //this.addRegion(var_Region);

                            //Vector2 var_Position_Chunk = new Vector2(_PlayerObject.Position.X - _PlayerObject.Position.X % (Chunk.Chunk.chunkSizeX * Block.Block.BlockSize), _PlayerObject.Position.Y - _PlayerObject.Position.Y % (Chunk.Chunk.chunkSizeY * Block.Block.BlockSize));

                            Chunk.Chunk var_Chunk = var_Region.getChunkAtPosition(_PlayerObject.Position)
                                                    ?? var_Region.createChunkAt(_PlayerObject.Position);
                        }
                    //}
                    this.addObject(_PlayerObject);
                }
                //if (Configuration.Configuration.isHost)
                //{
                this.checkPlayerObjectNeighbourChunks(_PlayerObject);
                //}
            }
        }

        public void checkPlayerObjectNeighbourChunks(Object.PlayerObject _PlayerObject)
        {
            World var_World = GameLibrary.Model.Map.World.World.world;
            if (_PlayerObject != null && _PlayerObject.CurrentBlock != null)
            {
                Chunk.Chunk var_Chunk = (Chunk.Chunk)_PlayerObject.CurrentBlock.Parent;
                if (var_Chunk != null)
                {
                    Region.Region var_Region = (Region.Region)_PlayerObject.CurrentBlock.Parent.Parent;

                    if (var_World != null && var_Region != null && var_Chunk != null)
                    {
                        int var_Range = (Setting.Setting.blockDrawRange / Chunk.Chunk.chunkSizeX); //(Setting.Setting.blockDrawRange * 2) / (Block.Block.BlockSize); //

                        for (int x = -var_Range / 2; x <= var_Range / 2; x++)
                        {
                            for (int y = -var_Range / 2; y <= var_Range / 2; y++)
                            {
                                if (x == 0 & y == 0)
                                {
                                }
                                else
                                {
                                    var_World.createPlayerObjectNeighbourChunk(new Vector3((int)var_Chunk.Position.X + x * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)var_Chunk.Position.Y + y * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize, 0));
                                }
                            }
                        }
                    }
                }
                else
                {
                    Logger.Logger.LogErr("World.Player.checkPlayerObjectNeighbourChunks -> PlayerObject Chunk ist null");
                }
            }
            else
            {
                Logger.Logger.LogErr("World.Player.checkPlayerObjectNeighbourChunks -> PlayerObject ist null");
            }
        }

        #endregion
    }
}
