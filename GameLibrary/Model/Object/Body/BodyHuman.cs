using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


using Microsoft.Xna.Framework;
using GameLibrary.Model.Object.Animation.Animations;

namespace GameLibrary.Model.Object.Body
{
    [Serializable()]
    public class BodyHuman: Body
    {
        private BodyPart hair;

        public BodyPart Hair
        {
            get { return hair; }
            set { hair = value; }
        }

        private BodyPart head;

        public BodyPart Head
        {
            get { return head; }
            set { head = value; }
        }

        private BodyPart armLeft;

        public BodyPart ArmLeft
        {
            get { return armLeft; }
            set { armLeft = value; }
        }

        private BodyPart armRight;

        public BodyPart ArmRight
        {
            get { return armRight; }
            set { armRight = value; }
        }

        public BodyHuman()
            : base()
        {
            this.hair = new BodyPart(2, new Vector3(0, 0, 0), this.BodyColor, "Character/Hair1");  
            this.armLeft = new BodyPart(1, new Vector3(0, 0, 0), this.BodyColor, "");

            this.BodyParts.Add(this.hair);
            this.BodyParts.Add(this.armLeft);
        }

        public BodyHuman(SerializationInfo info, StreamingContext ctxt)
            :base(info, ctxt)
        {
            this.hair = (BodyPart)info.GetValue("hair", typeof(BodyPart));
            this.BodyParts.Add(this.hair);
            this.armLeft = (BodyPart)info.GetValue("armLeft", typeof(BodyPart));
            this.BodyParts.Add(this.armLeft);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("hair", this.hair, typeof(BodyPart));
            info.AddValue("armLeft", this.armLeft, typeof(BodyPart));
            base.GetObjectData(info, ctxt);
        }
    }
}
