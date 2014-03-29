using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Map.Chunk;

namespace Server.Model.Map.Region
{
    class Region
    {
        private int id;
        private List<Chunk.Chunk> chunks;
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public Region(int _Id, String _Name)
        {
            this.id = _Id;
            this.name = _Name;
            chunks = new List<Chunk.Chunk>();
        }

        public bool addChunk(Chunk.Chunk _Chunk)
        {
            if (!containsChunk(_Chunk.Id))
            {
                this.chunks.Add(_Chunk);
                return true;
            }
            else
            {
                Logger.Logger.LogErr("Region->addChunk(...) : Chunk mit Id: " + _Chunk.Id + " schon vorhanden!");
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
            return this.chunks.Count;
        }


        public void loadChunk(int _ID)
        {

        }

        public void saveChunk(Chunk.Chunk _Chunk)
        {

        }
    }
}
