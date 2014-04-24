using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Server.Model.Map.Block
{
    class Block
    {
        public static int BlockSize = 32;

        private BlockEnum[] layer;

        public BlockEnum[] Layer
        {
            get { return layer; }
            set { layer = value; }
        }

        private Block bottomNeighbour;

        public Block BottomNeighbour
        {
            get { return bottomNeighbour; }
            set { bottomNeighbour = value; }
        }
        private Block leftNeighbour;

        public Block LeftNeighbour
        {
            get { return leftNeighbour; }
            set { leftNeighbour = value; }
        }
        private Block rightNeighbour;

        public Block RightNeighbour
        {
            get { return rightNeighbour; }
            set { rightNeighbour = value; }
        }
        private Block topNeighbour;

        public Block TopNeighbour
        {
            get { return topNeighbour; }
            set { topNeighbour = value; }
        }

        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)position.X, (int)position.Y, (int)Block.BlockSize, (int)Block.BlockSize); }
        }

        private List<Object.LivingObject> objects;

        public List<Object.LivingObject> Objects
        {
            get { return objects; }
            set { objects = value; }
        }

        private Chunk.Chunk parentChunk;

        public Block(int _PosX, int _PosY, BlockEnum _BlockEnum, Chunk.Chunk _ParentChunk)
        {
            this.layer = new BlockEnum[Enum.GetValues(typeof(BlockLayerEnum)).Length];
            this.layer[0] = _BlockEnum;
            this.objects = new List<Object.LivingObject>();
            this.position = new Vector2(_PosX, _PosY);
            this.parentChunk = _ParentChunk;
        }

        public void setLayerAt(Enum _Enum, BlockLayerEnum _Id)
        {
            int x = (int)_Id;
            this.layer[(int)_Id] = (BlockEnum)_Enum;
        }

        public void setFirstLayer(Enum _Enum)
        {
            this.layer[0] = (BlockEnum)_Enum;
        }

        public void addLivingObject(Object.LivingObject _LivingObject)
        {
            _LivingObject.ObjectMoves += this.HandleEvent;
            this.objects.Add(_LivingObject);
        }

        public void removeLivingObject(Object.LivingObject _LivingObject)
        {
            _LivingObject.ObjectMoves -= this.HandleEvent;
            this.objects.Remove(_LivingObject);
        }

        public void HandleEvent(object sender, EventArgs args)
        {
            if (sender is Object.LivingObject)
            {
                int var_BlockPosX = (int)(parentChunk.ParentRegion.Position.X * Factories.RegionFactory.regionSizeX * Factories.ChunkFactory.chunkSizeX + this.position.X) -1;
                int var_BlockPosY = (int)(parentChunk.ParentRegion.Position.Y * Factories.RegionFactory.regionSizeY * Factories.ChunkFactory.chunkSizeY + this.position.Y) -1;

                Object.LivingObject var_LivingObject = (Object.LivingObject)sender;
                if (var_LivingObject.Position.X < var_BlockPosX * BlockSize)
                {
                    this.removeLivingObject(var_LivingObject);
                    if (this.leftNeighbour != null)
                    {
                        this.leftNeighbour.addLivingObject(var_LivingObject);
                    }
                }
                else if (var_LivingObject.Position.X > (var_BlockPosX+1) * BlockSize)
                {
                    this.removeLivingObject(var_LivingObject);
                    if (this.rightNeighbour != null)
                    {
                        this.rightNeighbour.addLivingObject(var_LivingObject);
                    }
                }
                else if (var_LivingObject.Position.Y < var_BlockPosY * BlockSize)
                {
                    this.removeLivingObject(var_LivingObject);
                    if (this.topNeighbour != null)
                    {
                        this.topNeighbour.addLivingObject(var_LivingObject);
                    }
                }
                else if (var_LivingObject.Position.Y > (var_BlockPosY + 1) * BlockSize)
                {
                    this.removeLivingObject(var_LivingObject);
                    if (this.bottomNeighbour != null)
                    {                   
                        this.bottomNeighbour.addLivingObject(var_LivingObject);
                    }
                }
            }
        }

        public void update()
        {
            foreach (Object.LivingObject var_LivingObject in objects.Reverse<Object.LivingObject>())
            {
                if (var_LivingObject.IsDead)
                {
                    this.objects.Remove(var_LivingObject);
                }
                else
                {
                    var_LivingObject.update();
                }
            }
        }

        public int getCountofAllObjects()
        {
            int var_Count = this.objects.Count;
            return var_Count;
        }

        public void DrawTest(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            int var_DrawPositionX = (int)(this.parentChunk.Position.X * Server.Factories.ChunkFactory.chunkSizeX + this.Position.X) * BlockSize;
            int var_DrawPositionY = (int)(this.parentChunk.Position.Y * Server.Factories.ChunkFactory.chunkSizeY + this.Position.Y) * BlockSize;
            Vector2 var_DrawPosition = new Vector2(var_DrawPositionX, var_DrawPositionY);

            Color var_Color = Color.White;

            if (this.objects.Count > 0)
            {
                var_Color = Color.Green;
            }

            BlockLayerEnum var_Layer = BlockLayerEnum.Layer1;
            foreach (BlockEnum var_Enum in this.Layer)
            {
                if (var_Enum != BlockEnum.Nothing)
                {
                    if (var_Layer == BlockLayerEnum.Layer1)
                    {
                        if (var_Enum == BlockEnum.Gras)
                        {
                            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Layer1/Gras"], var_DrawPosition, var_Color);
                        }
                        if (var_Enum == BlockEnum.Wall)
                        {
                            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Layer1/Wall"], var_DrawPosition, var_Color);
                        }
                    }
                    if (var_Layer == BlockLayerEnum.Layer2)
                    {
                        if (var_Enum == BlockEnum.Gras)
                        {
                            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Layer2/Gras"], var_DrawPosition, var_Color);
                        }
                        if (var_Enum == BlockEnum.Dirt)
                        {
                            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Layer2/Dirt"], var_DrawPosition, var_Color);
                        }
                    }
                }
                var_Layer += 1;
            }
        }

        public void DrawObjects(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            foreach (Object.LivingObject var_LivingObject in objects)
            {
                var_LivingObject.draw(_GraphicsDevice, _SpriteBatch, new Vector3(0, 0, 0), Color.White);
            }     
        }
    }
}
