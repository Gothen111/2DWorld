using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Server.Model.Object.Animation.Animations
{
    class MoveAnimation : AnimatedObjectAnimation
    {
        private int currentFrame;
        public MoveAnimation(AnimatedObject _AnimationOwner)
            : base(_AnimationOwner, 0, 20)
        {
            this.currentFrame = 0;
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

                float var_Speed = Math.Abs(this.AnimationOwner.Velocity.X) + Math.Abs(this.AnimationOwner.Velocity.Y) + Math.Abs(this.AnimationOwner.Velocity.Z);
                if (var_Speed == 0)
                {
                    var_Speed = 1;
                }
                this.Animation = (int)(this.AnimationMax / var_Speed / 1.8f);

                this.updateMovementDirection();
            }
        }

        private void updateMovementDirection()
        {
            if (this.AnimationOwner.Velocity.X == 0 && this.AnimationOwner.Velocity.Y == 0)
            {

            }
            if (this.AnimationOwner.Velocity.X == 0)
            {
                if (this.AnimationOwner.Velocity.Y < 0)
                {
                    this.AnimationOwner.DirectionEnum = ObjectEnums.DirectionEnum.Top;
                }
                else if(this.AnimationOwner.Velocity.Y > 0)
                {
                    this.AnimationOwner.DirectionEnum = ObjectEnums.DirectionEnum.Down;
                }
            }
            else if (this.AnimationOwner.Velocity.X < 0)
            {
                this.AnimationOwner.DirectionEnum = ObjectEnums.DirectionEnum.Left;
                if (Math.Abs(this.AnimationOwner.Velocity.X) < Math.Abs(this.AnimationOwner.Velocity.Y))
                {
                    if (this.AnimationOwner.Velocity.Y < 0)
                    {
                        this.AnimationOwner.DirectionEnum = ObjectEnums.DirectionEnum.Top;
                    }
                    else if (this.AnimationOwner.Velocity.Y > 0)
                    {
                        this.AnimationOwner.DirectionEnum = ObjectEnums.DirectionEnum.Down;
                    }
                }
            }
            else if (this.AnimationOwner.Velocity.X > 0)
            {
                this.AnimationOwner.DirectionEnum = ObjectEnums.DirectionEnum.Right;
                if (Math.Abs(this.AnimationOwner.Velocity.X) < Math.Abs(this.AnimationOwner.Velocity.Y))
                {
                    if (this.AnimationOwner.Velocity.Y < 0)
                    {
                        this.AnimationOwner.DirectionEnum = ObjectEnums.DirectionEnum.Top;
                    }
                    else if (this.AnimationOwner.Velocity.Y > 0)
                    {
                        this.AnimationOwner.DirectionEnum = ObjectEnums.DirectionEnum.Down;
                    }
                }
            }
        }

        public override Rectangle sourceRectangle()
        {
            int var_DrawX = 0;

            if (this.AnimationOwner.Velocity.X == 0 && this.AnimationOwner.Velocity.Y == 0)
            {
                var_DrawX = 1;
            }
            else
            {
                var_DrawX = this.currentFrame;
            }
            return new Rectangle(var_DrawX*32,this.directionDrawY()*32,32,32);
        }
    }
}
