using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Factories.FactoryEnums;

namespace Server.Model.Object
{
    class NpcObject : FactionObject
    {
        protected GenderEnum gender;

        public GenderEnum Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public NpcObject() : base()
        {
            
        }
    }
}
