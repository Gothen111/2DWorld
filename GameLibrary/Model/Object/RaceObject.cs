using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Behaviour.Member;

namespace GameLibrary.Model.Object
{
    public class RaceObject : CreatureObject
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
