using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Model.Object
{
    class CreatureObject : LivingObject
    {
        //protected Inventory inventory;
        protected List<EquipmentObject> equipment;
        //protected Armor armor;
        //protected Skill skill;
        protected String name;
    }
}
