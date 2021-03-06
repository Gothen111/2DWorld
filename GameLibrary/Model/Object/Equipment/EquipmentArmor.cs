﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using GameLibrary.Factory.FactoryEnums;
using Microsoft.Xna.Framework;

namespace GameLibrary.Model.Object.Equipment
{
    [Serializable()]
    public class EquipmentArmor : EquipmentObject
    {
        private ArmorEnum armorEnum;

        public ArmorEnum ArmorEnum
        {
          get { return armorEnum; }
          set { armorEnum = value; }
        }

        private int normalArmor;

        public int NormalArmor
        {
            get { return normalArmor; }
            set { normalArmor = value; }
        }

        private List<Map.World.SearchFlags.Searchflag> searchFlags;

        public List<Map.World.SearchFlags.Searchflag> SearchFlags
        {
            get { return searchFlags; }
            set { searchFlags = value; }
        }

        public EquipmentArmor()
        {
            searchFlags = new List<Map.World.SearchFlags.Searchflag>();
        }

        public EquipmentArmor(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.armorEnum = (ArmorEnum)info.GetValue("armorEnum", typeof(ArmorEnum));

            this.normalArmor = (int)info.GetValue("normalArmor", typeof(int));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("armorEnum", armorEnum, typeof(ArmorEnum));

            info.AddValue("normalArmor", normalArmor, typeof(int));

            base.GetObjectData(info, ctxt);
        }

        public override void update(GameTime _GameTime)
        {

        }
    }
}
