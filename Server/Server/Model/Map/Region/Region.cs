using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Server.Model.Map.Chunk;
using Server.Model.Map.World;

using Microsoft.Xna.Framework.Graphics;

namespace Server.Model.Map.Region
{
    class Region
    {
        private int id;

        public static int regionSizeX = 2; // 10
        public static int regionSizeY = 2; // 10

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private Chunk.Chunk[,] chunks;

        public Chunk.Chunk[,] Chunks
        {
            get { return chunks; }
            set { chunks = value; }
        }

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private Vector2 size;

        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)position.X, (int)position.Y, (int)size.X * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)size.Y * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize); }
        }

        private World.World parentWorld;

        public World.World ParentWorld
        {
            get { return parentWorld; }
            set { parentWorld = value; }
        }

        public Region(int _Id, String _Name, int _PosX, int _PosY, int _SizeX, int _SizeY, World.World _ParentWorld)
        {
            this.id = _Id;
            this.name = _Name;
            this.position = new Vector2(_PosX, _PosY);
            this.size = new Vector2(_SizeX, _SizeY);

            chunks = new Chunk.Chunk[_SizeX, _SizeY];

            parentWorld = _ParentWorld;
        }

        public bool setChunkAtPosition(int _PosX, int _PosY, Chunk.Chunk _Chunk)
        {
            if (!containsChunk(_Chunk.Id))
            {
                if (_PosX >= 0 && _PosX < this.size.X)
                {
                    if (_PosY >= 0 && _PosY < this.size.Y)
                    {
                        this.chunks[_PosX, _PosY] = _Chunk;
                        return true;
                    }
                }
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
            return false;
        }

        public bool containsChunk(Chunk.Chunk _Chunk)
        {
            return false;
        }

        public int getLastChunkId()
        {
            return this.chunks.Length;
        }

        public void setAllNeighboursOfChunks()
        {
            for (int x = 0; x < this.size.X; x++)
            {
                for (int y = 0; y < this.size.Y; y++)
                {
                    Chunk.Chunk var_Chunk = this.getChunkAtPosition(x, y);
                    if (x > 0)
                    {
                        Chunk.Chunk var_ChunkNeighbour = this.getChunkAtPosition(x - 1, y);
                        for (int blockY = 0; blockY < Chunk.Chunk.chunkSizeY; blockY++)
                        {
                            var_Chunk.getBlockAtPosition(0, blockY).LeftNeighbour = var_ChunkNeighbour.getBlockAtPosition(Chunk.Chunk.chunkSizeX - 1, blockY);
                            var_ChunkNeighbour.getBlockAtPosition(Chunk.Chunk.chunkSizeX - 1, blockY).RightNeighbour = var_Chunk.getBlockAtPosition(0, blockY);
                        }
                    }
                    if (y > 0)
                    {
                        Chunk.Chunk var_ChunkNeighbour = this.getChunkAtPosition(x, y - 1);
                        for (int blockX = 0; blockX < Chunk.Chunk.chunkSizeX; blockX++)
                        {
                            var_Chunk.getBlockAtPosition(blockX, 0).TopNeighbour = var_ChunkNeighbour.getBlockAtPosition(blockX, Chunk.Chunk.chunkSizeY - 1);
                            var_ChunkNeighbour.getBlockAtPosition(blockX, Chunk.Chunk.chunkSizeY - 1).BottomNeighbour = var_Chunk.getBlockAtPosition(blockX, 0);
                        }
                    }
                }
            }
        }

        public Chunk.Chunk getChunkAtPosition(float _PosX, float _PosY)
        {
            return chunks[(int)(_PosX), ((int)_PosY)];
        }

        public void loadChunk(int _ID)
        {

        }

        public void saveChunk(Chunk.Chunk _Chunk)
        {

        }

        public Chunk.Chunk getChunkLivingObjectIsIn(Server.Model.Object.LivingObject _LivingObject)
        {
            //TODO: Fehlerbehandlungen, falls LivingObject nicht in der Region ist --> Nullpointer da var_X oder var_Y zu klein/groß
            int var_X = (int)(_LivingObject.Position.X / ((this.Position.X + 1) * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize));
            int var_Y = (int)(_LivingObject.Position.Y / ((this.Position.Y + 1) * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize));
            if (var_X >= Region.regionSizeX || var_Y >= Region.regionSizeY)
            {
                Logger.Logger.LogErr("LivingObject befindet sich nicht in Region " + this.id);
                return null;
            }
            else
            {
                return this.chunks[var_X, var_Y];
            }
        }

        public void DrawTest(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            for (int x = 0; x < this.size.X; x++)
            {
                for (int y = 0; y < this.size.Y; y++)
                {
                    this.chunks[x, y].DrawTest(_GraphicsDevice, _SpriteBatch);
                }
            }          
        }

        public void DrawTest2(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            for (int x = 0; x < this.size.X; x++)
            {
                for (int y = 0; y < this.size.Y; y++)
                {
                    this.chunks[x, y].DrawTest2(_GraphicsDevice, _SpriteBatch);
                }
            }
        }

        public void update()
        {
            for (int x = 0; x < this.size.X; x++)
            {
                for (int y = 0; y < this.size.Y; y++)
                {
                    this.chunks[x, y].update();
                }
            }
        }
    }
}
