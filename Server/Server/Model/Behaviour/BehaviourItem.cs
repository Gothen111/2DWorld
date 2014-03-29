using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Model.Behaviour
{
    class BehaviourItem <E>
    {
        protected E item;
        public E Item{
            get {return item;}
        }

        protected int value;
        public int Value{
            get{ return value;}
            set{ this.value = value;}
        }

        public void addToValue(int modifier)
        {
            value += modifier;
        }
    }
}
