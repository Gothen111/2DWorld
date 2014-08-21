using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using GameLibrary.Model.Object.ObjectEnums;
using GameLibrary.Model.Object.Animation.Animations;


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

        private BodyPart mainBody;

        public BodyPart MainBody
        {
            get { return mainBody; }
            set { mainBody = value; }
        }

        public Body()
        {         
            this.bodyParts = new List<BodyPart>();
            this.bodyColor = Color.White;

            this.mainBody = new BodyPart(0, new Vector3(0, 0, 0), this.BodyColor, "");
            this.bodyParts.Add(this.mainBody);
        }

        public Body(SerializationInfo info, StreamingContext ctxt)
        {
            this.bodyParts = new List<BodyPart>();
            this.mainBody = (BodyPart)info.GetValue("mainBody", typeof(BodyPart));
            this.bodyParts.Add(this.mainBody);
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("mainBody", this.mainBody, typeof(BodyPart));
        }

        public virtual void stopWalk()
        {
            this.mainBody.Animation = new StandAnimation(this.mainBody);
        }

        public virtual void walk(Vector3 _Velocity)
        {
            if (this.mainBody.Animation is MoveAnimation)
            {
            }
            else
            {
                if (this.mainBody.Animation.finishedAnimation())
                {
                    this.mainBody.Animation = new MoveAnimation(this.mainBody, _Velocity);
                }
            }
        }

        public Equipment.EquipmentWeapon attack()
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                if (var_BodyPart.Equipment != null)
                {
                    if (var_BodyPart.Equipment is Equipment.EquipmentWeapon)
                    {
                        return (Equipment.EquipmentWeapon)var_BodyPart.Equipment;
                    }
                }
            }
            return null;
        }

        public int getDefence()
        {
            int var_Result = 1;
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                if (var_BodyPart.Equipment != null)
                {
                    if (var_BodyPart.Equipment is Equipment.EquipmentArmor)
                    {
                        var_Result += ((Equipment.EquipmentArmor)var_BodyPart.Equipment).NormalArmor;
                    }
                }
            }
            return var_Result;
        }

        public virtual void setDirection(DirectionEnum _Direction)
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                var_BodyPart.Direction = _Direction;
            }
        }

        public virtual EquipmentObject getEquipmentAt(int _EquipmentPosition)
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                if (var_BodyPart.Id == _EquipmentPosition)
                {
                    return var_BodyPart.Equipment;
                }
            }
            return null;
        }

        public virtual bool containsEquipmentObject(EquipmentObject _EquipmentObject)
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                if (var_BodyPart.Equipment == _EquipmentObject)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool setEquipmentObject(EquipmentObject _EquipmentObject)
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                if (var_BodyPart.Id == _EquipmentObject.PositionInInventory)
                {
                    var_BodyPart.setEquipmentObject(_EquipmentObject);
                }
            }
            return false;
        }

        public virtual bool removeEquipment(EquipmentObject _EquipmentObject)
        {
            foreach (BodyPart var_BodyPart in this.bodyParts)
            {
                if(var_BodyPart.Equipment!=null)
                {
                    if (var_BodyPart.Equipment.Equals(_EquipmentObject))
                    {
                        var_BodyPart.Equipment = null;
                        return true;
                    }
                }
            }
            return false;
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
