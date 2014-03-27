using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Model.Map
{
    class Region
    {
        private int id;
        private List<Chunk> chunks;

        public Region(int _Id)
        {
            this.id = _Id;
            chunks = new List<Chunk>();
        }

        public bool addChunk(Chunk _Chunk)
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

        public bool containsChunk(Chunk _Chunk)
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

        public void saveChunk(Chunk _Chunk)
        {

        }
    }
}
