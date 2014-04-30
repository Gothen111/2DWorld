using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Behaviour.Member;

namespace Server.Model.Object
{
    class FactionObject : RaceObject
    {
        private Faction faction;

        public Faction Faction
        {
            get { return faction; }
            set { faction = value; }
        }

        public override void update()
        {
            base.update();
        }
    }
}
