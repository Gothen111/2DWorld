using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Client.Model.Behaviour.Member;

namespace Client.Model.Object
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
