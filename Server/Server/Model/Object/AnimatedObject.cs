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
        protected int animationTime = 0;
        protected int animationTimeMax = 20;
        protected int moveAnimation = 0;
        protected DirectionEnum directionEnum = DirectionEnum.Down;
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

        public override void update()
        {
            base.update();

            this.move();

            if (this.animationTime <= 0)
            {
                if (this.moveAnimation == 0)
                {
                    this.moveAnimation = 2; // Right
                }
                else
                {
                    this.moveAnimation = 0; // Left
                }
                this.animationTime = (int) (this.animationTimeMax/((Math.Abs(this.Velocity.X)+Math.Abs(this.Velocity.Y)+Math.Abs(this.Velocity.Z)))/1.8f);
                this.updateMovementDirection();
            }
            else
            {
                this.animationTime -= 1;
            }

            if (this.animation != null)
            {
                this.animation.update();
            }
        }

        private void move()
        {
            this.Position += this.Velocity;

            EventHandler handler = this.ObjectMoves;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void updateMovementDirection()
        {
            if (this.Velocity.X == 0 && this.Velocity.Y == 0)
            {

            }
            if(this.Velocity.X == 0)
            {
                if (this.Velocity.Y < 0)
                {
                    this.directionEnum = DirectionEnum.Top;
                }
                else
                {
                    this.directionEnum = DirectionEnum.Down;
                }
            }
            else if (this.Velocity.X < 0)
            {
                this.directionEnum = DirectionEnum.Left;
                if (Math.Abs(this.Velocity.X) < Math.Abs(this.Velocity.Y))
                {
                    if(this.Velocity.Y < 0)
                    {
                        this.directionEnum = DirectionEnum.Top;
                    }
                    else
                    {
                        this.directionEnum = DirectionEnum.Down;
                    }
                }
            }
            else if (this.Velocity.X > 0)
            {
                this.directionEnum = DirectionEnum.Right;
                if (Math.Abs(this.Velocity.X) < Math.Abs(this.Velocity.Y))
                {
                    if (this.Velocity.Y < 0)
                    {
                        this.directionEnum = DirectionEnum.Top;
                    }
                    else
                    {
                        this.directionEnum = DirectionEnum.Down;
                    }
                }
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
            if (this.animation != null)
            {
                _DrawPositionExtra += this.animation.drawPositionExtra();
                _Color = this.animation.drawColor();
            }

            int var_DrawX = 0;
            int var_DrawY = 0;

            if (this.Velocity.X == 0 && this.Velocity.Y == 0)
            {
                var_DrawX = 1;
            }
            else
            {
                var_DrawX = this.moveAnimation;
            }

            if (this.directionEnum == DirectionEnum.Down)
            {
                var_DrawY = 0;
            }
            else if (this.directionEnum == DirectionEnum.Left)
            {
                var_DrawY = 1;
            }
            else if (this.directionEnum == DirectionEnum.Right)
            {
                var_DrawY = 2;
            }
            else if (this.directionEnum == DirectionEnum.Top)
            {
                var_DrawY = 3;
            }
            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Character/Shadow"], new Vector2(this.Position.X + 16, this.Position.Y + 16), Color.White);
            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.GraphicPath], (new Vector2(this.Position.X + _DrawPositionExtra.X +16, this.Position.Y + _DrawPositionExtra.Y +16)), new Rectangle(var_DrawX * 32, var_DrawY * 32, 32, 32), _Color);
        }
    }
}
