using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Object;

namespace GameLibrary.Ressourcen
{
    public class ObjectPositionComparer : IComparer<GameLibrary.Model.Object.Object>
    {
        public int Compare(GameLibrary.Model.Object.Object x, GameLibrary.Model.Object.Object y)
        {
            GameLibrary.Model.Object.Object var_Object1 = x;
            GameLibrary.Model.Object.Object var_Object2 = y;

            if (var_Object1.Position.Y < var_Object2.Position.Y)
            {
                return -1;
            }
            if (var_Object1.Position.Y == var_Object2.Position.Y)
            {
                if (var_Object1.Id < var_Object2.Id)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            if (var_Object1.Position.Y > var_Object2.Position.Y)
            {
                return 1;
            }

            return 0;
        }
    }
}
