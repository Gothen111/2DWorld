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

        private List<Object.Object> objects;

        public List<Object.Object> Objects
        {
            get { return objects; }
            set { objects = value; }
        }

        private List<Object.Object> objectsPreEnviorment;

        public List<Object.Object> ObjectsPreEnviorment
        {
            get { return objectsPreEnviorment; }
            set { objectsPreEnviorment = value; }
        }

        private bool isWalkAble;

        public bool IsWalkAble
        {
            get { return isWalkAble; }
            set { isWalkAble = value; }
        }

        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; }
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
            this.height = 0;
            this.Size = new Vector2(Block.BlockSize, Block.BlockSize);
        }

        public Block(SerializationInfo info, StreamingContext ctxt) 
            :base(info, ctxt)
        {
            this.layer = (BlockEnum[])info.GetValue("layer", typeof(BlockEnum[]));
            this.objects = (List<Object.Object>)info.GetValue("objects", typeof(List<Object.Object>));
            foreach (Object.Object var_Object in this.objects)
            {
                Model.Map.World.World.world.QuadTreeObject.Insert(var_Object);
                var_Object.CurrentBlock = this;
            }
            this.objectsPreEnviorment = (List<Object.Object>)info.GetValue("objectsPreEnviorment", typeof(List<Object.Object>));
            foreach (Object.Object var_Object in this.objectsPreEnviorment)
            {
                var_Object.CurrentBlock = this;
            }
            this.height = (int)info.GetValue("height", typeof(int));
            this.isWalkAble = (bool)info.GetValue("isWalkAble", typeof(bool));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("layer", this.layer, typeof(BlockEnum[]));
            info.AddValue("objects", this.objects, this.objects.GetType());
            info.AddValue("objectsPreEnviorment", this.objectsPreEnviorment, this.objectsPreEnviorment.GetType());
            info.AddValue("height", this.height, typeof(int));
            info.AddValue("isWalkAble", this.isWalkAble, typeof(bool));
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
            _Object.CurrentBlock = this;
        }

        public void removeObject(Object.Object _Object)
        {
            this.objects.Remove(_Object);
        }

        public override void update()
        {
            base.update();
            foreach (Object.LivingObject var_LivingObject in objects.Reverse<Object.Object>())
            {
                if (var_LivingObject.IsDead)
                {
                    //this.objects.Remove(var_LivingObject);
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

            if (Setting.Setting.debugMode)
            {
                if (this.objects.Count > 0)
                {
                    var_Color = Color.Green;
                }
            }

            String var_RegionType = ((Region.Region)this.Parent.Parent).RegionEnum.ToString();

            /*for(int i = 0; i < this.height; i++)
            {
                _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Region/" + var_RegionType + "/Block/" + "Wall"], var_DrawPosition - new Vector2(0, BlockSize * i), var_Color);
            }*/
            
            BlockLayerEnum var_Layer = BlockLayerEnum.Layer1;
            while ((int)var_Layer < this.layer.Length)
            {
                BlockEnum var_Enum = this.layer[(int)var_Layer];
                if (var_Enum != BlockEnum.Nothing)
                {
                    _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Region/" + var_RegionType + "/" + var_RegionType], var_DrawPosition, new Rectangle((int)(var_Enum-1) * BlockSize, (int)(var_Layer) * BlockSize, BlockSize, BlockSize), var_Color);
                }
                var_Layer += 1;
            }
        }
    }
}
