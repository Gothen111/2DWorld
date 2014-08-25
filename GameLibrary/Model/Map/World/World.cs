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
    [Serializable()]
    public partial class World : Box, ISerializable
    {
        #region Attribute
        public static World world;

        private List<Region.Region> regions;

        private QuadTree<Object.Object> quadTreeObject;

        public QuadTree<Object.Object> QuadTreeObject
        {
            get { return quadTreeObject; }
            set { quadTreeObject = value; }
        }

        private QuadTree<Object.Object> quadTreeEnvironmentObject;

        public QuadTree<Object.Object> QuadTreeEnvironmentObject
        {
            get { return quadTreeEnvironmentObject; }
            set { quadTreeEnvironmentObject = value; }
        }



        private List<PlayerObject> playerObjects;

        private List<Object.Object> objectsToUpdate;

        private List<Chunk.Chunk> chunksOutOfRange;

        #endregion

        #region Constructors
        public World()
            :base()
        {
            //if (Configuration.Configuration.isHost)
            //{
                this.quadTreeObject = new QuadTree<Object.Object>(new Vector3(32, 32, 0), 20);
                this.quadTreeEnvironmentObject = new QuadTree<Object.Object>(new Vector3(32, 32, 0), 20);
            //}          
        }

        public World(SerializationInfo info, StreamingContext ctxt) : this()
        {
            this.playerObjects = new List<PlayerObject>();
            this.regions = new List<Region.Region>();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
        }

        public World(String _Name)
            :this()
        {
            this.Name = _Name;

            regions = new List<Region.Region>();

            this.playerObjects = new List<PlayerObject>();

            Logger.Logger.LogInfo("Welt " + _Name + " wurde erstellt!");
        }
        #endregion      
    }
}
