using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Server.Camera
{
    class Camera
    {
        /// <summary>
        /// Position der Kamera.
        /// </summary>
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector3 position;

        private Model.Object.LivingObject target;

        public Model.Object.LivingObject Target
        {
            get { return target; }
            set { target = value; }
        }

        private Viewport viewPort;

        public Camera(Viewport _ViewPort)
        {
            this.target = null;
            this.position = new Vector3(0, 0, 0);
            this.viewPort = _ViewPort;
        }

        public void setTarget(Model.Object.LivingObject _Target)
        {
            this.target = _Target;
        }

        public void setPosition(Vector3 _Position)
        {
            this.target = null;
            this.position = _Position;
        }

        public void update(GameTime gameTime)
        {
            if(target != null)
            {
                this.position = this.target.Position - new Vector3(viewPort.Width/2, viewPort.Height/2, 0);
            }
        }

        public Matrix getMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-this.position.X, -this.position.Y, 0));
        }
    } 
}
