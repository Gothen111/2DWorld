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
            this.environmentObjectToDraw = new List<Object.Object>();

            this.updatePlayerObjectsNeighborhood();

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

        private void updatePlayerObjectNeighborhood(Object.PlayerObject _PlayerObject)
        {
            if (_PlayerObject.CurrentBlock != null)
            {
                Region.Region var_PlayerObjectRegion = (Region.Region)_PlayerObject.CurrentBlock.Parent.Parent;

                Chunk.Chunk var_ChunkMid = (Chunk.Chunk)_PlayerObject.CurrentBlock.Parent;

                Chunk.Chunk var_ChunkTop = (Chunk.Chunk)var_ChunkMid.TopNeighbour;
                if (var_ChunkTop != null)
                {
                    Chunk.Chunk var_ChunkTopLeft = (Chunk.Chunk)var_ChunkTop.LeftNeighbour;
                    if (var_ChunkTopLeft != null)
                    {
                    }
                    else
                    {
                    }
                    Chunk.Chunk var_ChunkTopRight = (Chunk.Chunk)var_ChunkTop.RightNeighbour;
                    if (var_ChunkTopRight != null)
                    {
                    }
                    else
                    {
                    }
                }
                else
                {
                    if (Configuration.Configuration.isHost)
                    {
                        Region.Region var_Region = World.world.getRegionAtPosition((int)var_ChunkMid.Position.X, (int)var_ChunkMid.Position.Y + -1 * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize)
                                                 ?? World.world.createRegionAt((int)var_PlayerObjectRegion.Position.X, (int)var_PlayerObjectRegion.Position.Y - Region.Region.regionSizeY * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize);
                        if (var_Region != null)
                        {
                            this.addRegion(var_Region);
                            Chunk.Chunk var_Chunk = var_Region.createChunkAt((int)var_ChunkMid.Position.X, (int)var_ChunkMid.Position.Y + -1 * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize);
                        }
                    }
                    else
                    {
                        if (var_ChunkMid.TopNeighbourRequested)
                        {
                        }
                        else
                        {
                            Event.EventList.Add(new Event(new RequestChunkMessage(new Vector2(var_ChunkMid.Position.X, var_ChunkMid.Position.Y + -1 * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize)), GameMessageImportance.VeryImportant));
                            var_ChunkMid.TopNeighbourRequested = true;
                        }
                    }
                }
                Chunk.Chunk var_ChunkLeft = (Chunk.Chunk)var_ChunkMid.LeftNeighbour;
                if (var_ChunkLeft != null)
                {
                }
                else
                {
                    if (Configuration.Configuration.isHost)
                    {
                        Region.Region var_Region = World.world.getRegionAtPosition((int)var_ChunkMid.Position.X + -1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)var_ChunkMid.Position.Y)
                                                 ?? World.world.createRegionAt((int)var_ChunkMid.Position.X + -1 * Region.Region.regionSizeX * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)var_PlayerObjectRegion.Position.Y);
                        if (var_Region != null)
                        {
                            this.addRegion(var_Region);
                            Chunk.Chunk var_Chunk = var_Region.createChunkAt((int)var_ChunkMid.Position.X + -1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)var_ChunkMid.Position.Y);
                        }
                    }
                    else
                    {
                        if (var_ChunkMid.LeftNeighbourRequested)
                        {
                        }
                        else
                        {
                            Event.EventList.Add(new Event(new RequestChunkMessage(new Vector2(var_ChunkMid.Position.X + -1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, var_ChunkMid.Position.Y)), GameMessageImportance.VeryImportant));
                            var_ChunkMid.LeftNeighbourRequested = true;
                        }
                    }
                }
                Chunk.Chunk var_ChunkRight = (Chunk.Chunk)var_ChunkMid.RightNeighbour;
                if (var_ChunkRight != null)
                {
                }
                else
                {
                    if (Configuration.Configuration.isHost)
                    {
                        Chunk.Chunk var_Chunk = var_PlayerObjectRegion.createChunkAt((int)var_ChunkMid.Position.X + 1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)var_ChunkMid.Position.Y);
                    }
                    else
                    {
                        if (var_ChunkMid.RightNeighbourRequested)
                        {
                        }
                        else
                        {
                            Event.EventList.Add(new Event(new RequestChunkMessage(new Vector2(var_ChunkMid.Position.X + 1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, var_ChunkMid.Position.Y)), GameMessageImportance.VeryImportant));
                            var_ChunkMid.RightNeighbourRequested = true;
                        }
                    }
                }

                Chunk.Chunk var_ChunkBottom = (Chunk.Chunk)var_ChunkMid.BottomNeighbour;
                if (var_ChunkBottom != null)
                {
                    Chunk.Chunk var_ChunkBottomLeft = (Chunk.Chunk)var_ChunkBottom.LeftNeighbour;
                    if (var_ChunkBottomLeft != null)
                    {
                    }
                    else
                    {
                    }
                    Chunk.Chunk var_ChunkBottomRight = (Chunk.Chunk)var_ChunkBottom.RightNeighbour;
                    if (var_ChunkBottomRight != null)
                    {
                    }
                    else
                    {
                    }
                }
                else
                {
                    if (Configuration.Configuration.isHost)
                    {
                        Chunk.Chunk var_Chunk = var_PlayerObjectRegion.createChunkAt((int)var_ChunkMid.Position.X, (int)var_ChunkMid.Position.Y + 1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize);
                    }
                    else
                    {
                        if (var_ChunkMid.BottomNeighbourRequested)
                        {
                        }
                        else
                        {
                            Event.EventList.Add(new Event(new RequestChunkMessage(new Vector2(var_ChunkMid.Position.X, var_ChunkMid.Position.Y + 1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize)), GameMessageImportance.VeryImportant));
                            var_ChunkMid.BottomNeighbourRequested = true;
                        }
                    }
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

            if (!Configuration.Configuration.isHost)
            {
                //TODO:Noch die foeach schleife machen damit nix doppelt ;) bzw. nur client quatsch oder so :D
                this.environmentObjectToDraw = this.getObjectsInRange(_PlayerObject.Position, this.quadTreeEnvironmentObject.Root, 400);
            }
        }
        #endregion
    }
}
