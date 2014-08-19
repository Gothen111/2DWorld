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

        private BodyPart body;

        public BodyPart Body
        {
            get { return body; }
            set { body = value; }
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
            //this.hair = new BodyPart(new Vector3(0,-10,0), this.BodyColor, "");
            this.body = new BodyPart(new Vector3(0, 0, 0), this.BodyColor, "");

            //this.BodyParts.Add(this.hair);
            this.BodyParts.Add(this.body);
        }

        public BodyHuman(SerializationInfo info, StreamingContext ctxt)
            :base(info, ctxt)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public override void stopWalk()
        {
            base.stopWalk();
            this.body.Animation = new StandAnimation(this.body);
        }

        public override void walk(Vector3 _Velocity)
        {
            base.walk(_Velocity);
            if (this.body.Animation is MoveAnimation)
            {
            }
            else
            {
                if (this.body.Animation.finishedAnimation())
                {
                    this.body.Animation = new MoveAnimation(this.body, _Velocity);
                }
            }
        }
    }
}
