using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Server.Model.Object.ObjectEnums;

namespace Server.Model.Object
{
    class AnimatedObject: Object
    {
        public event EventHandler ObjectMoves;

        private float scale;

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private DirectionEnum directionEnum = DirectionEnum.Down;

        public DirectionEnum DirectionEnum
        {
            get { return directionEnum; }
            set { directionEnum = value; }
        }
        private Vector3 size;

        public Vector3 Size
        {
            get { return size; }
            set { size = value; }
        }
        private String graphicPath;

        public String GraphicPath
        {
            get { return graphicPath; }
            set { graphicPath = value; }
        }

        private float movementSpeed;

        public float MovementSpeed
        {
            get { return movementSpeed; }
            set { movementSpeed = value; }
        }

        private Animation.AnimatedObjectAnimation animation;

        public Animation.AnimatedObjectAnimation Animation
        {
            get { return animation; }
            set { animation = value; }
        }

        public AnimatedObject()
        {
            this.animation = new Animation.Animations.MoveAnimation(this);
        }

        public override void update()
        {
            base.update();

            this.move();

            if (this.animation != null)
            {
                this.animation.update();
            }
        }

        private void move()
        {
            this.Position += this.Velocity;

            if (this.Velocity.X != 0 && this.Velocity.Y != 0)
            {
                if (this.animation is Animation.Animations.MoveAnimation)
                {
                }
                else
                {
                    if (this.animation.finishedAnimation())
                    {
                        this.animation = new Animation.Animations.MoveAnimation(this);
                    }
                }
            }
            else
            {
            }

            EventHandler handler = this.ObjectMoves;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void ChangeDirection(Vector3 _TargetPosition)
        {
            if (_TargetPosition.X < this.Position.X)
            {
                this.directionEnum = ObjectEnums.DirectionEnum.Left;
            }
            else if (_TargetPosition.X > this.Position.X)
            {
                this.directionEnum = ObjectEnums.DirectionEnum.Right;
            }
            else if (_TargetPosition.Y < this.Position.Y)
            {
                this.directionEnum = ObjectEnums.DirectionEnum.Top;
            }
            else if (_TargetPosition.Y > this.Position.Y)
            {
                this.directionEnum = ObjectEnums.DirectionEnum.Down;
            }
        }

        public virtual void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, Vector3 _DrawPositionExtra, Color _Color)
        {
            Vector3 var_DrawPositionExtra = this.animation.drawPositionExtra();
            Vector2 var_Position = new Vector2(this.Position.X + _DrawPositionExtra.X + 16, this.Position.Y + _DrawPositionExtra.Y + 16) + new Vector2(var_DrawPositionExtra.X, var_DrawPositionExtra.Y);

            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Character/Shadow"], new Vector2(this.Position.X + 16, this.Position.Y + 16), Color.White);
            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.animation.graphicPath()], var_Position, this.animation.sourceRectangle(), this.animation.drawColor());
        }
    }
}
