﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Model.Object.Task.Tasks
{
    public enum TaskPriority
    {
        Stand,
        Walk_Random,
        Attack_Random,
        Order_Follow,
        Order_Move,
        Order_MoveAttack,
        Order_Attack,
        Order_UseItem,
        Order_CastSpell,
    }
}
