using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Server.Model.Map.Block
{
    class Block : Box
    {
        public static int BlockSize = 32;

        private BlockEnum[] layer;

        public BlockEnum[] Layer
        {
            get { return layer; }
            set { layer = value; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Block.BlockSize, (int)Block.BlockSize); }
        }

        private List<Object.LivingObject> objects;

        public List<Object.LivingObject> Objects
        {
            get { return objects; }
            set { objects = value; }
        }

        public List<Object.LivingObject> objectsPreEnviorment;

        private Chunk.Chunk parentChunk;

        public Chunk.Chunk ParentChunk
        {
            get { return parentChunk; }
            set { parentChunk = value; }
        }

        private bool isWalkAble;

        public bool IsWalkAble
        {
            get { return isWalkAble; }
            set { isWalkAble = value; }
        }

        public Block(int _PosX, int _PosY, BlockEnum _BlockEnum, Chunk.Chunk _ParentChunk)
        {
            this.layer = new BlockEnum[Enum.GetValues(typeof(BlockLayerEnum)).Length];
            this.layer[0] = _BlockEnum;
            this.objects = new List<Object.LivingObject>();
            this.Position = new Vector2(_PosX, _PosY);
            this.parentChunk = _ParentChunk;

            objectsPreEnviorment = new List<Object.LivingObject>();

            this.isWalkAble = true;
        }

        public void setLayerAt(Enum _Enum, BlockLayerEnum _Id)
        {
            int x = (int)_Id;
            this.layer[(int)_Id] = (BlockEnum)_Enum;
        }

        public void setFirstLayer(Enum _Enum)
        {
            this.layer[0] = (BlockEnum)_Enum;
            if (_Enum is BlockEnum)
            {
                if ((BlockEnum)_Enum == BlockEnum.Wall)
                {
                    this.isWalkAble = false;
                }
            }
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
            this.objects.Remove(_LivingObject);
        }

        public void HandleEvent(object sender, EventArgs args)
        {
            if (sender is Object.LivingObject)
            {
                int var_BlockPosX = (int)this.Position.X / BlockSize;//(int)(parentChunk.ParentRegion.Position.X * Region.Region.regionSizeX * Chunk.Chunk.chunkSizeX + this.position.X) -1;
                int var_BlockPosY = (int)this.Position.Y / BlockSize;//(int)(parentChunk.ParentRegion.Position.Y * Region.Region.regionSizeY * Chunk.Chunk.chunkSizeY + this.position.Y) - 1;

                Object.LivingObject var_LivingObject = (Object.LivingObject)sender;

                Vector3 var_Position = var_LivingObject.Position;// +var_LivingObject.Size / 2;

                if (var_Position.X < var_BlockPosX * BlockSize)
                {
                    this.removeLivingObject(var_LivingObject);
                    if (this.LeftNeighbour != null)
                    {
                        ((Block)this.LeftNeighbour).addLivingObject(var_LivingObject);
                    }
                }
                else if (var_Position.X > (var_BlockPosX + 1) * BlockSize)
                {
                    this.removeLivingObject(var_LivingObject);
                    if (this.RightNeighbour != null)
                    {
                        ((Block)this.RightNeighbour).addLivingObject(var_LivingObject);
                    }
                }
                else if (var_Position.Y < var_BlockPosY * BlockSize)
                {
                    this.removeLivingObject(var_LivingObject);
                    if (this.TopNeighbour != null)
                    {
                        ((Block)this.TopNeighbour).addLivingObject(var_LivingObject);
                    }
                }
                else if (var_Position.Y > (var_BlockPosY + 1) * BlockSize)
                {
                    this.removeLivingObject(var_LivingObject);
                    if (this.BottomNeighbour != null)
                    {
                        ((Block)this.BottomNeighbour).addLivingObject(var_LivingObject);
                    }
                }
            }
        }

        public override void update()
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

        public void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, float _LayerDepth)
        {
            Vector2 var_DrawPosition = this.Position;

            Color var_Color = Color.White;

            if (this.objects.Count > 0)
            {
                //var_Color = Color.Green;
            }

            BlockLayerEnum var_Layer = BlockLayerEnum.Layer6;
            while (var_Layer >= 0)
            {
                BlockEnum var_Enum = this.layer[(int)var_Layer];
                if (var_Enum != BlockEnum.Nothing)
                {
                    if (var_Layer == BlockLayerEnum.Layer1)
                    {
                        if (var_Enum == BlockEnum.Gras)
                        {
                            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Layer1/Gras"], var_DrawPosition, null, var_Color, 0, Vector2.Zero, 1, SpriteEffects.None, 0.97f);
                        }
                        if (var_Enum == BlockEnum.Wall)
                        {
                            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Layer1/Wall"], var_DrawPosition, null, var_Color, 0, Vector2.Zero, 1, SpriteEffects.None, 0.97f);
                        }
                    }
                    if (var_Layer == BlockLayerEnum.Layer2)
                    {
                        if (var_Enum == BlockEnum.Gras)
                        {
                            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Layer2/Gras"], var_DrawPosition, null, var_Color, 0, Vector2.Zero, 1, SpriteEffects.None, 0.96f);
                        }
                        if (var_Enum == BlockEnum.Dirt)
                        {
                            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Layer2/Dirt"], var_DrawPosition, null, var_Color, 0, Vector2.Zero, 1, SpriteEffects.None, 0.96f);
                        }
                    }
                }
                var_Layer -= 1;
            }

            int var_i = 0;
            foreach (Object.LivingObject var_LivingObject in this.objects)
            {
                var_LivingObject.LayerDepth = _LayerDepth - (this.Position.Y - var_LivingObject.Position.Y) * 0.0001f - var_i * 0.0001f; // ??? VLL abs noch dran?
                var_LivingObject.draw(_GraphicsDevice, _SpriteBatch, new Vector3(0, 0, 0), Color.White);
                var_i += 1;
            }
            var_i = 0;
            foreach (Object.LivingObject var_LivingObject in this.objectsPreEnviorment)
            {
                var_LivingObject.LayerDepth = _LayerDepth + 0.1f - (this.Position.Y - var_LivingObject.Position.Y) * 0.0001f - var_i * 0.0001f; // ??? VLL abs noch dran?
                var_LivingObject.draw(_GraphicsDevice, _SpriteBatch, new Vector3(0, 0, 0), Color.White);
                var_i += 1;
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
                if (this.LeftNeighbour != null && _Position.X - _Range < this.Position.X)
                    ((Block)this.LeftNeighbour).getLivingObjectsInRange(_Position, _Range, result, visitedBlocks);
                if (this.RightNeighbour != null && _Position.X + _Range > this.Position.X + Block.BlockSize)
                    ((Block)this.RightNeighbour).getLivingObjectsInRange(_Position, _Range, result, visitedBlocks);
                if (this.BottomNeighbour != null && _Position.Y + _Range > this.Position.Y + Block.BlockSize)
                    ((Block)this.BottomNeighbour).getLivingObjectsInRange(_Position, _Range, result, visitedBlocks);
                if (this.TopNeighbour != null && _Position.Y - _Range < this.Position.Y - Block.BlockSize)
                    ((Block)this.TopNeighbour).getLivingObjectsInRange(_Position, _Range, result, visitedBlocks);
            }
        }
    }
}
