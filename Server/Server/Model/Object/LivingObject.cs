using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Model.Object
{
    class LivingObject : AnimatedObject
    {
        private float healthPoints;

        protected float HealthPoints
        {
            get { return healthPoints; }
            set { healthPoints = value; }
        }
        //protected InterAction interAction; //???
    }
}
