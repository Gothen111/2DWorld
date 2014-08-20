using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using GameLibrary.Model.Object.ObjectEnums;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.Model.Object.Body
{
    [Serializable()]
    public class BodyPart : ISerializable
    {
        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        private String texturePath;

        public String TexturePath
        {
            get { return texturePath; }
            set { texturePath = value; }
        }

        private Animation.AnimatedObjectAnimation animation;

        public Animation.AnimatedObjectAnimation Animation
        {
            get { return animation; }
            set { animation = value; }
        }

        private DirectionEnum direction;

        public DirectionEnum Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        private Vector3 size;

        public Vector3 Size
        {
            get { return size; }
            set { size = value; }
        }

        private Vector2 standartTextureShift;

        public Vector2 StandartTextureShift
        {
            get { return standartTextureShift; }
            set { standartTextureShift = value; }
        }

        private float scale;

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private List<EquipmentObject> equipment;

        public List<EquipmentObject> Equipment
        {
            get { return equipment; }
            set { equipment = value; }
        }

        public BodyPart(Vector3 _Position, Color _Color, String _TexturePath)
        {
            this.position = _Position;
            this.color = _Color;
            this.texturePath = _TexturePath;
            this.direction = DirectionEnum.Down;
            this.animation = new Animation.Animations.StandAnimation(this);
            this.scale = 1.0f;
            this.size = new Vector3(32, 32, 0);
            this.equipment = new List<EquipmentObject>();
        }

        public BodyPart(SerializationInfo info, StreamingContext ctxt)
        {
            this.position = (Vector3)info.GetValue("position", typeof(Vector3));
            this.color = (Color)info.GetValue("color", typeof(Color));
            this.texturePath = (String)info.GetValue("texturePath", typeof(String));
            this.scale = (float)info.GetValue("scale", typeof(float));
            this.size = (Vector3)info.GetValue("size", typeof(Vector3));

            this.animation = new Animation.Animations.StandAnimation(this);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("position", this.position, typeof(Vector3));
            info.AddValue("color", this.color, typeof(Color));
            info.AddValue("texturePath", this.texturePath, typeof(String));
            info.AddValue("scale", this.scale, typeof(float));
            info.AddValue("size", this.size, typeof(Vector3));
        }

        public virtual void update()
        {
            this.animation.update();
        }

        private void drawEquipment(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Vector2 _BodyCenter)
        {
            if (this.equipment != null)
            {
                foreach (EquipmentObject var_EquipmentObject in this.equipment)
                {
                    var_EquipmentObject.Position = new Vector3(_BodyCenter, 0);
                    var_EquipmentObject.drawWearingEquipment(_GraphicsDevice, _SpriteBatch, this.color);
                }
            }
        }

        public virtual void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Vector2 _BodyCenter)
        {
            Vector2 var_Position = new Vector2(this.position.X + _BodyCenter.X, this.position.Y + _BodyCenter.Y);
            Color var_DrawColor = this.color;
            if (!this.animation.graphicPath().Equals(""))
            {
                _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.animation.graphicPath()], var_Position, this.animation.sourceRectangle(), var_DrawColor, 0f, Vector2.Zero, new Vector2(this.scale, this.scale), SpriteEffects.None, 1.0f);
            }
            this.drawEquipment(_GraphicsDevice, _SpriteBatch, _BodyCenter);
        }
    }
}
