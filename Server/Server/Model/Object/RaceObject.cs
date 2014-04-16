using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Behaviour.Member;

namespace Server.Model.Object
{
    class RaceObject : CreatureObject
    {
        private Race race;

        internal Race Race
        {
            get { return race; }
            set { race = value; }
        }

        public override void update()
        {
            base.update();
        }
    }
}
