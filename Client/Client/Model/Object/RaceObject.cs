using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Client.Model.Behaviour.Member;

namespace Client.Model.Object
{
    class RaceObject : CreatureObject
    {
        private Race race;

        public Race Race
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
