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

        internal Chunk.Chunk ParentChunk
        {
            get { return parentChunk; }
            set { parentChunk = value; }
        }

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
            _LivingObject.CurrentBlock = this;
            this.objects.Add(_LivingObject);
        }

        public void removeLivingObject(Object.LivingObject _LivingObject)
        {
            _LivingObject.ObjectMoves -= this.HandleEvent;
            _LivingObject.CurrentBlock = this;
            this.objects.Remove(_LivingObject);
        }

        public void HandleEvent(object sender, EventArgs args)
        {
            if (sender is Object.LivingObject)
            {
                int var_BlockPosX = (int)this.position.X / BlockSize - 1;//(int)(parentChunk.ParentRegion.Position.X * Region.Region.regionSizeX * Chunk.Chunk.chunkSizeX + this.position.X) -1;
                int var_BlockPosY = (int)this.position.Y / BlockSize - 1;//(int)(parentChunk.ParentRegion.Position.Y * Region.Region.regionSizeY * Chunk.Chunk.chunkSizeY + this.position.Y) - 1;

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
            Vector2 var_DrawPosition = this.Position;

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

        public List<Object.LivingObject> getLivingObjectsInRange(Vector3 _Position, float _Range)
        {
            List<Object.LivingObject> result = new List<Object.LivingObject>();
            List<Block> visitedBlocks = new List<Block>();
            getLivingObjectsInRange(_Position, _Range, result, visitedBlocks);
            return result;
        }

        public void getLivingObjectsInRange(Vector3 _Position, float _Range, List<Object.LivingObject> result, List<Block> visitedBlocks)
        {
            if (!visitedBlocks.Contains(this))
            {
                foreach (Object.LivingObject var_LivingObject in this.objects)
                {
                    float distance = Vector3.Distance(_Position, var_LivingObject.Position);
                    if (distance <= _Range)
                        result.Add(var_LivingObject);
                }
                visitedBlocks.Add(this);
                if (this.leftNeighbour != null && _Position.X - _Range < this.Position.X)
                    this.leftNeighbour.getLivingObjectsInRange(_Position, _Range, result, visitedBlocks);
                if (this.rightNeighbour != null && _Position.X + _Range > this.Position.X + Block.BlockSize)
                    this.rightNeighbour.getLivingObjectsInRange(_Position, _Range, result, visitedBlocks);
                if (this.bottomNeighbour != null && _Position.Y + _Range > this.Position.Y + Block.BlockSize)
                    this.bottomNeighbour.getLivingObjectsInRange(_Position, _Range, result, visitedBlocks);
                if (this.topNeighbour != null && _Position.Y - _Range < this.Position.Y - Block.BlockSize)
                    this.topNeighbour.getLivingObjectsInRange(_Position, _Range, result, visitedBlocks);
            }
        }
    }
}
