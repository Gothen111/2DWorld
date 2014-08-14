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
        #region

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
                    if (Configuration.Configuration.isHost)
                    {
                        Vector2 var_Position_Region = new Vector2(_PlayerObject.Position.X - _PlayerObject.Position.X % (Region.Region.regionSizeX * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize), _PlayerObject.Position.Y - _PlayerObject.Position.Y % (Region.Region.regionSizeY * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize));

                        Region.Region var_Region = World.world.getRegionAtPosition(_PlayerObject.Position.X, _PlayerObject.Position.Y)
                                                    ?? World.world.createRegionAt((int)var_Position_Region.X, (int)var_Position_Region.Y);
                        if (var_Region != null)
                        {
                            this.addRegion(var_Region);

                            Vector2 var_Position_Chunk = new Vector2(_PlayerObject.Position.X - _PlayerObject.Position.X % (Chunk.Chunk.chunkSizeX * Block.Block.BlockSize), _PlayerObject.Position.Y - _PlayerObject.Position.Y % (Chunk.Chunk.chunkSizeY * Block.Block.BlockSize));

                            Chunk.Chunk var_Chunk = var_Region.getChunkAtPosition(_PlayerObject.Position.X, _PlayerObject.Position.Y)
                                                    ?? var_Region.createChunkAt((int)var_Position_Chunk.X, (int)var_Position_Chunk.Y);
                        }
                    }
                    this.addObject(_PlayerObject);
                }
            }
        }

        #endregion
    }
}
