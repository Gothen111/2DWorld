using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Factories.FactoryEnums;

namespace Server.Model.Object
{
    class LivingObject : AnimatedObject
    {
        private float healthPoints;

        public float HealthPoints
        {
            get { return healthPoints; }
            set { healthPoints = value; }
        }

        protected GenderEnum gender;

        public GenderEnum Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        //protected InterAction interAction; //???

        public override void update()
        {
            base.update();
        }
    }
}
