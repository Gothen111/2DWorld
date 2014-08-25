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
        #region update-Methoden

        public override void update()
        {
            base.update();

            this.objectsToUpdate = new List<Object.Object>();
            this.chunksOutOfRange = new List<Chunk.Chunk>();
            foreach (Region.Region var_Region in this.regions)
            {
                foreach (Chunk.Chunk var_Chunk in var_Region.Chunks)
                {
                    this.chunksOutOfRange.Add(var_Chunk);
                }
            }

            int var_SizeBefore = this.chunksOutOfRange.Count;

            this.updatePlayerObjectsNeighborhood();

            int var_SizeAfter = this.chunksOutOfRange.Count;

            //Console.WriteLine(var_SizeBefore - var_SizeAfter);

            foreach (Chunk.Chunk var_Chunk in this.chunksOutOfRange)
            {
                this.removeChunk(var_Chunk);
            }




            //TODO: Mache Kopie der Liste!!!! Falls objekte steben usw ;)
            try
            {
                foreach (Object.Object var_Object in this.objectsToUpdate)
                {
                    var_Object.update();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        private void updatePlayerObjectsNeighborhood()
        {
            foreach (Object.PlayerObject var_PlayerObject in this.playerObjects)
            {
                this.updatePlayerObjectNeighborhood(var_PlayerObject);
            }
        }

        private void updatePlayerObjectNeighborChunk(Vector2 _NewChunkPosition, Vector2 _PlayerRegionPos)
        {
            Chunk.Chunk var_Chunk = this.getChunkAtPosition(_NewChunkPosition.X, _NewChunkPosition.Y);
            if (var_Chunk == null)
            {
                if (Configuration.Configuration.isHost)
                {
                    Region.Region var_Region = World.world.getRegionAtPosition((int)_NewChunkPosition.X, (int)_NewChunkPosition.Y)
                                             ?? World.world.createRegionAt((int)_PlayerRegionPos.X, (int)_PlayerRegionPos.Y);
                    if (var_Region != null)
                    {
                        this.addRegion(var_Region);
                        // mache noch get chunk. und darin load chunk

                        var_Chunk = var_Region.getChunkAtPosition((int)_NewChunkPosition.X, (int)_NewChunkPosition.Y)
                                    ?? var_Region.createChunkAt((int)_NewChunkPosition.X, (int)_NewChunkPosition.Y);
                    }
                }
                else
                {
                    /*if (var_ChunkMid.TopNeighbourRequested)
                    {
                    }
                    else
                    {*/
                        Event.EventList.Add(new Event(new RequestChunkMessage(new Vector2(_NewChunkPosition.X, _NewChunkPosition.Y)), GameMessageImportance.VeryImportant));
                        //var_ChunkMid.TopNeighbourRequested = true;
                    //}
                }
            }
        }

        private void updatePlayerObjectNeighborhood(Object.PlayerObject _PlayerObject)
        {
            if (_PlayerObject.CurrentBlock != null)
            {
                Region.Region var_PlayerObjectRegion = (Region.Region)_PlayerObject.CurrentBlock.Parent.Parent;

                Chunk.Chunk var_ChunkMid = (Chunk.Chunk)_PlayerObject.CurrentBlock.Parent;


                //Top
                this.updatePlayerObjectNeighborChunk(new Vector2((int)var_ChunkMid.Position.X, (int)var_ChunkMid.Position.Y + -1 * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize),
                                                    new Vector2((int)var_PlayerObjectRegion.Position.X, (int)var_PlayerObjectRegion.Position.Y + -1 * Region.Region.regionSizeY * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize));
                //Left
                this.updatePlayerObjectNeighborChunk(new Vector2((int)var_ChunkMid.Position.X + -1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)var_ChunkMid.Position.Y),
                                                    new Vector2((int)var_PlayerObjectRegion.Position.X + -1 * Region.Region.regionSizeX * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)var_PlayerObjectRegion.Position.Y));
                //Right
                this.updatePlayerObjectNeighborChunk(new Vector2((int)var_ChunkMid.Position.X + 1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)var_ChunkMid.Position.Y),
                                                    new Vector2((int)var_PlayerObjectRegion.Position.X + 1 * Region.Region.regionSizeX * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)var_PlayerObjectRegion.Position.Y));
                //Bottom
                this.updatePlayerObjectNeighborChunk(new Vector2((int)var_ChunkMid.Position.X, (int)var_ChunkMid.Position.Y + 1 * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize),
                                                    new Vector2((int)var_PlayerObjectRegion.Position.X, (int)var_PlayerObjectRegion.Position.Y + 1 * Region.Region.regionSizeY * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize));

                List<Chunk.Chunk> var_ChunksToRemove = new List<Chunk.Chunk>();
                foreach (Chunk.Chunk var_Chunk in this.chunksOutOfRange)
                {
                    if (Vector2.Distance(var_Chunk.Position, new Vector2(_PlayerObject.Position.X, _PlayerObject.Position.Y)) <= Chunk.Chunk.chunkSizeX * Block.Block.BlockSize * 3)
                    {
                        var_ChunksToRemove.Add(var_Chunk);
                    }
                }

                foreach (Chunk.Chunk var_Chunk in var_ChunksToRemove)
                {
                    this.chunksOutOfRange.Remove(var_Chunk);
                }
            }

            List<Object.Object> var_Objects = this.getObjectsInRange(_PlayerObject.Position, 400);
            foreach (Object.Object var_Object in var_Objects)
            {
                if (!this.objectsToUpdate.Contains(var_Object))
                {
                    this.objectsToUpdate.Add(var_Object);
                }
            }
        }
        #endregion
    }
}
