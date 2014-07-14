using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Object;
using GameLibrary.Factory.FactoryEnums;
using Microsoft.Xna.Framework;

namespace GameLibrary.Factory
{
    public class ItemFactory
    {
        public static ItemFactory itemFactory = new ItemFactory();

        private ItemFactory()
        {
        }

        public ItemObject createItemObject(ItemEnum _ItemEnum)
        {
            GameLibrary.Model.Object.ItemObject var_ItemObject = new ItemObject();
            var_ItemObject.Scale = 1;
            var_ItemObject.Velocity = new Vector3(0, 0, 0);

            switch (_ItemEnum)
            {
                case ItemEnum.GoldCoin:
                    {
                        var_ItemObject.GraphicPath = "Character/GoldCoin";
                        var_ItemObject.Size = new Microsoft.Xna.Framework.Vector3(16, 16, 0);
                        var_ItemObject.OnlyFromPlayerTakeAble = true;
                        break;
                    }
            }
            return var_ItemObject;
        }
    }
}
