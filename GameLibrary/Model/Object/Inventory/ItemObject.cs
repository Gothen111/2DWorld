using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Model.Object.Inventory
{
    class ItemObject
    {
        private bool stackAble;

        public bool StackAble
        {
            get { return stackAble; }
            set { stackAble = value; }
        }

        private int onStack;

        public int OnStack
        {
            get { return onStack; }
            set { onStack = value; }
        }

        private int stackMax;

        public int StackMax
        {
            get { return stackMax; }
            set { stackMax = value; }
        }

        public ItemObject()
        {
        }
    }
}
