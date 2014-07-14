using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.Model.Map.Block
{
    [Serializable()]
    public class Block : Box
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
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Block.BlockSize - 1, (int)Block.BlockSize - 1); }
        }

        private List<Object.Object> objects;

        public List<Object.Object> Objects
        {
            get { return objects; }
            set { objects = value; }
        }

        public List<Object.Object> objectsPreEnviorment;

        private bool isWalkAble;

        public bool IsWalkAble
        {
            get { return isWalkAble; }
            set { isWalkAble = value; }
        }

        public Block(int _PosX, int _PosY, BlockEnum _BlockEnum, Chunk.Chunk _ParentChunk)
            :base()
        {
            this.layer = new BlockEnum[Enum.GetValues(typeof(BlockLayerEnum)).Length];
            this.layer[0] = _BlockEnum;
            this.objects = new List<Object.Object>();
            this.Position = new Vector2(_PosX, _PosY);
            this.Parent = _ParentChunk;

            objectsPreEnviorment = new List<Object.Object>();

            this.isWalkAble = true;
        }

        public Block(SerializationInfo info, StreamingContext ctxt) 
            :base(info, ctxt)
        {
            this.layer = (BlockEnum[])info.GetValue("layer", typeof(BlockEnum[]));
            //TODO: Alle Objekttypen müssen serialisierbar gemacht werden
            this.objects = (List<Object.Object>)info.GetValue("objects", typeof(List<Object.Object>));
            this.objectsPreEnviorment = (List<Object.Object>)info.GetValue("objectsPreEnviorment", typeof(List<Object.Object>));

            //this.objects = new List<Object.LivingObject>();
            //this.objectsPreEnviorment = new List<Object.LivingObject>();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("layer", this.layer, typeof(BlockEnum[]));
            info.AddValue("objects", this.objects, this.objects.GetType());
            info.AddValue("objectsPreEnviorment", this.objectsPreEnviorment, this.objectsPreEnviorment.GetType());
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

        public void addObject(Object.Object _Object)
        {
            if (!this.objects.Contains(_Object))
            {
                this.objects.Add(_Object);
            }
            //_LivingObject.ObjectMoves += this.HandleEvent;
            _Object.CurrentBlock = this;
        }

        public void removeObject(Object.Object _Object)
        {
            //_LivingObject.ObjectMoves -= this.HandleEvent;
            this.objects.Remove(_Object);
        }

        public override void update()
        {
            base.update();
            foreach (Object.LivingObject var_LivingObject in objects.Reverse<Object.Object>())
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

        public void drawBlock(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            Vector2 var_DrawPosition = this.Position;

            Color var_Color = Color.White;

            if (this.objects.Count > 0)
            {
                var_Color = Color.Green;
            }

            BlockLayerEnum var_Layer = BlockLayerEnum.Layer1;
            while ((int)var_Layer < 6)
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
                var_Layer += 1;
            }
        }

        public void drawObjects(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            //int var_ChunkY = (int)(this.parentChunk.Position.Y / Chunk.Chunk.chunkSizeY*Block.BlockSize);
            float var_LayerDepth = 0.79f;

            int var_i = 0;
            foreach (Object.LivingObject var_LivingObject in this.objects)
            {
                var_LivingObject.LayerDepth = var_LayerDepth - (this.Position.Y - var_LivingObject.Position.Y) * 0.0001f - var_i * 0.00001f; // ??? VLL abs noch dran?
                var_LivingObject.draw(_GraphicsDevice, _SpriteBatch, new Vector3(0, 0, 0), Color.White);
                var_i += 1;
            }
            var_i = 0;
            foreach (Object.LivingObject var_LivingObject in this.objectsPreEnviorment)
            {
                var_LivingObject.LayerDepth = var_LayerDepth + 0.1f - (this.Position.Y - var_LivingObject.Position.Y) * 0.0001f - var_i * 0.00001f; // ??? VLL abs noch dran?
                var_LivingObject.draw(_GraphicsDevice, _SpriteBatch, new Vector3(0, 0, 0), Color.White);
                var_i += 1;
            }
        }
    }
}
