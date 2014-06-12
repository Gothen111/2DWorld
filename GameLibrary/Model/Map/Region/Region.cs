using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using GameLibrary.Model.Map.Chunk;
using GameLibrary.Model.Map.World;

using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.Model.Map.Region
{
    [Serializable()]
    public class Region : Box
    {
        public static int regionSizeX = 2;
        public static int regionSizeY = 2;

        private List<Chunk.Chunk> chunks;

        public List<Chunk.Chunk> Chunks
        {
            get { return chunks; }
            set { chunks = value; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)Size.Y * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize); }
        }

        private RegionEnum regionEnum;

        public Region(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        {
            this.chunks = (List<Chunk.Chunk>)info.GetValue("chunks", typeof(List<Chunk.Chunk>));
            this.Position = (Vector2)info.GetValue("position", typeof(Vector2));
            this.Size = (Vector2)info.GetValue("size", typeof(Vector2));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("chunks", this.chunks, typeof(List<Chunk.Chunk>));
        }

        public Region(int _Id, String _Name, int _PosX, int _PosY, int _SizeX, int _SizeY, RegionEnum _RegionEnum, World.World _ParentWorld)
        {
            this.Id = _Id;
            this.Name = _Name;
            this.Position = new Vector2(_PosX, _PosY);
            this.Size = new Vector2(_SizeX, _SizeY);

            chunks = new List<Chunk.Chunk>();

            this.regionEnum = _RegionEnum;

            this.Parent = _ParentWorld;
        }

        public bool setChunkAtPosition(int _PosX, int _PosY, Chunk.Chunk _Chunk)
        {
            if (!containsChunk(_Chunk.Id))
            {
                /*if (_PosX >= Bounds.Left && _PosX <= Bounds.Right)
                {
                    if (_PosY >= Bounds.Top && _PosY <= Bounds.Bottom)
                    {*/
                        this.chunks.Add(_Chunk);
                        this.setAllNeighboursOfChunk(_Chunk);
                        if (GameLibrary.Configuration.Configuration.isHost)
                        {
                            GameLibrary.Commands.Executer.Executer.executer.addCommand(new Commands.CommandTypes.UpdateChunkCommand(_Chunk));
                        }
                        return true;
                /*    }
                }*/
                Logger.Logger.LogErr("Region->setChunkAtPosition(...) : Platzierung nicht möglich: PosX " + _PosX + " PosY " + _PosY);
                return false;
            }
            else
            {
                Logger.Logger.LogErr("Region->setChunkAtPosition(...) : Chunk mit Id: " + _Chunk.Id + " schon vorhanden!");
                return false;
            }
        }

        public bool containsChunk(int _Id)
        {
            foreach (Chunk.Chunk var_Chunk in this.chunks)
            {
                if (var_Chunk.Id == _Id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool containsChunk(Chunk.Chunk _Chunk)
        {
            return false;
        }

        public void setAllNeighboursOfChunks()
        {
            foreach (Chunk.Chunk var_Chunk in this.chunks)
            {
                var_Chunk.Parent = this;
                setAllNeighboursOfChunk(var_Chunk);
            }
        }

        public void setAllNeighboursOfChunk(Chunk.Chunk _Chunk)
        {
            Chunk.Chunk var_ChunkNeighbourLeft = this.getChunkAtPosition(_Chunk.Position.X - Chunk.Chunk.chunkSizeX*Block.Block.BlockSize, _Chunk.Position.Y);

            if (var_ChunkNeighbourLeft != null)
            {
                _Chunk.LeftNeighbour = var_ChunkNeighbourLeft;
                var_ChunkNeighbourLeft.RightNeighbour = _Chunk;
                for (int blockY = 0; blockY < Chunk.Chunk.chunkSizeY; blockY++)
                {
                    _Chunk.getBlockAtPosition(0, blockY).LeftNeighbour = var_ChunkNeighbourLeft.getBlockAtPosition(Chunk.Chunk.chunkSizeX - 1, blockY);
                    var_ChunkNeighbourLeft.getBlockAtPosition(Chunk.Chunk.chunkSizeX - 1, blockY).RightNeighbour = _Chunk.getBlockAtPosition(0, blockY);
                }
            }

            Chunk.Chunk var_ChunkNeighbourRight = this.getChunkAtPosition(_Chunk.Position.X + Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, _Chunk.Position.Y);

            if (var_ChunkNeighbourRight != null)
            {
                _Chunk.RightNeighbour = var_ChunkNeighbourRight;
                var_ChunkNeighbourRight.LeftNeighbour = _Chunk;
                for (int blockY = 0; blockY < Chunk.Chunk.chunkSizeY; blockY++)
                {
                    _Chunk.getBlockAtPosition(Chunk.Chunk.chunkSizeX - 1, blockY).RightNeighbour = var_ChunkNeighbourRight.getBlockAtPosition(0, blockY);
                    var_ChunkNeighbourRight.getBlockAtPosition(0, blockY).LeftNeighbour = _Chunk.getBlockAtPosition(Chunk.Chunk.chunkSizeX - 1, blockY);                  
                }
            }

            Chunk.Chunk var_ChunkNeighbourTop = this.getChunkAtPosition(_Chunk.Position.X, _Chunk.Position.Y - Chunk.Chunk.chunkSizeX * Block.Block.BlockSize);

            if (var_ChunkNeighbourTop != null)
            {
                _Chunk.TopNeighbour = var_ChunkNeighbourTop;
                var_ChunkNeighbourTop.BottomNeighbour = _Chunk;
                for (int blockX = 0; blockX < Chunk.Chunk.chunkSizeX; blockX++)
                {
                    _Chunk.getBlockAtPosition(blockX, 0).TopNeighbour = var_ChunkNeighbourTop.getBlockAtPosition(blockX, Chunk.Chunk.chunkSizeY - 1);
                    var_ChunkNeighbourTop.getBlockAtPosition(blockX, Chunk.Chunk.chunkSizeY - 1).BottomNeighbour = _Chunk.getBlockAtPosition(blockX, 0);
                }
            }

            Chunk.Chunk var_ChunkNeighbourBottom = this.getChunkAtPosition(_Chunk.Position.X, _Chunk.Position.Y + Chunk.Chunk.chunkSizeX * Block.Block.BlockSize);

            if (var_ChunkNeighbourBottom != null)
            {
                _Chunk.BottomNeighbour = var_ChunkNeighbourBottom;
                var_ChunkNeighbourBottom.TopNeighbour = _Chunk;
                for (int blockX = 0; blockX < Chunk.Chunk.chunkSizeX; blockX++)
                {
                    _Chunk.getBlockAtPosition(blockX, Chunk.Chunk.chunkSizeY - 1).BottomNeighbour = var_ChunkNeighbourBottom.getBlockAtPosition(blockX, 0);
                    var_ChunkNeighbourBottom.getBlockAtPosition(blockX, 0).TopNeighbour = _Chunk.getBlockAtPosition(blockX, Chunk.Chunk.chunkSizeY - 1);
                }
            }
        }

        public Chunk.Chunk getChunkAtPosition(float _PosX, float _PosY)
        {
            foreach (Chunk.Chunk var_Chunk in this.chunks)
            {
                if (var_Chunk.Position.X == _PosX)
                {
                    if (var_Chunk.Position.Y == _PosY)
                    {
                        return var_Chunk;
                    }
                }
            }
            return null;
        }

        public Chunk.Chunk createChunkAt(int _PosX, int _PosY)
        {
            return GameLibrary.Factory.RegionFactory.regionFactory.createChunkInRegion(this, _PosX, _PosY);
        }

        public void loadChunk(int _ID)
        {

        }

        public void saveChunk(Chunk.Chunk _Chunk)
        {

        }

        public Chunk.Chunk getChunkLivingObjectIsIn(GameLibrary.Model.Object.LivingObject _LivingObject)
        {
            //TODO: Fehlerbehandlungen, falls LivingObject nicht in der Region ist --> Nullpointer da var_X oder var_Y zu klein/groß
            int var_X = (int)(_LivingObject.Position.X / ((this.Position.X + 1) * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize));
            int var_Y = (int)(_LivingObject.Position.Y / ((this.Position.Y + 1) * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize));
            if (var_X >= Region.regionSizeX || var_Y >= Region.regionSizeY)
            {
                Logger.Logger.LogErr("LivingObject befindet sich nicht in Region " + this.Id);
                return null;
            }
            else
            {
                return this.getChunkAtPosition(var_X, var_Y);
            }
        }

        public override void update()
        {
            if (this.NeedUpdate)
            {
                base.update();
                this.updateChilds();
            }
        }

        public Chunk.Chunk getChunk(int _Id)
        {
            foreach (Chunk.Chunk var_Chunk in chunks)
            {
                if (var_Chunk.Id == _Id)
                {
                    return var_Chunk;
                }
            }
            return null;
        }
    }
}
