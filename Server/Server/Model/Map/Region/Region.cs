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

        public void loadChunk(int _ID)
        {

        }

        public void saveChunk(Chunk.Chunk _Chunk)
        {

        }

        public Chunk.Chunk getChunkLivingObjectIsIn(Server.Model.Object.LivingObject _LivingObject)
        {
            int var_X = (int)(_LivingObject.Position.X / ((this.Position.X + 1) * Factories.ChunkFactory.chunkSizeX * Block.Block.BlockSize));
            int var_Y = (int)(_LivingObject.Position.Y / ((this.Position.Y + 1) * Factories.ChunkFactory.chunkSizeY * Block.Block.BlockSize));

            return this.chunks[var_X, var_Y];
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
    }
}
