using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using GameLibrary.Model.Object.Body;

namespace GameLibrary.Model.Object.Animation.Animations
{
    public class MoveAnimation : AnimatedObjectAnimation
    {
        private int currentFrame;

        private Vector3 velocity;

        public MoveAnimation()
        {

        }

        public MoveAnimation(BodyPart _BodyPart, Vector3 _Velocity)
            : base(_BodyPart, 0, 20)
        {
            this.currentFrame = 0;
            this.velocity = _Velocity;
            //Console.WriteLine("NewMove");
        }

        public override void update()
        {
            base.update();
            if (this.Animation <= 0)
            {
                if (this.currentFrame == 0)
                {
                    this.currentFrame = 2; // Right
                }
                else
                {
                    this.currentFrame = 0; // Left
                }

                float var_Speed = Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y) + Math.Abs(this.velocity.Z);
                if (var_Speed == 0)
                {
                    var_Speed = 1;
                }

                //Console.WriteLine(this.currentFrame);

                this.Animation = (int)(this.AnimationMax / var_Speed * (this.BodyPart.Size.Y/35));
            }
        }

        public override bool finishedAnimation()
        {
            return base.finishedAnimation() || (this.velocity.X == 0 && this.velocity.Y == 0 && this.velocity.Z == 0);
        }

        public override Rectangle sourceRectangle()
        {
            int var_DrawX = 0;

            if (this.velocity.X == 0 && this.velocity.Y == 0)
            {
                var_DrawX = 1;
            }
            else
            {
                var_DrawX = this.currentFrame;
            }
            return new Rectangle(var_DrawX * (int)this.BodyPart.Size.X, this.directionDrawY() * (int)this.BodyPart.Size.Y, (int)this.BodyPart.Size.X, (int)this.BodyPart.Size.Y);
        }
    }
}
