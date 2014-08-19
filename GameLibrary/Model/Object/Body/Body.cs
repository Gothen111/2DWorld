using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using GameLibrary.Model.Object.ObjectEnums;


namespace GameLibrary.Model.Object.Body
{
    [Serializable()]
    public class Body : ISerializable
    {
        private List<BodyPart> bodyParts;

        public List<BodyPart> BodyParts
        {
            get { return bodyParts; }
            set { bodyParts = value; }
        }

        private Color bodyColor;

        public Color BodyColor
        {
            get { return bodyColor; }
            set { bodyColor = value; }
        }

        public Body()
        {         
            this.bodyParts = new List<BodyPart>();
            this.bodyColor = Color.White;
        }

        public Body(SerializationInfo info, StreamingContext ctxt)
        {
            this.bodyParts = (List<BodyPart>)info.GetValue("bodyParts", typeof(List<BodyPart>));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("bodyParts", this.bodyParts, typeof(List<BodyPart>));
        }

        public virtual void stopWalk()
        {

        }

        public virtual void walk(Vector3 _Velocity)
        {

        }

        public virtual void attack()
        {

        }

        public virtual void setDirection(DirectionEnum _Direction)
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                var_BodyPart.Direction = _Direction;
            }
        }

        public virtual void update()
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                var_BodyPart.update();
            }
        }

        public virtual void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Vector2 _BodyCenter)
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                var_BodyPart.draw(_GraphicsDevice, _SpriteBatch, _BodyCenter);
            }
        }
    }
}
