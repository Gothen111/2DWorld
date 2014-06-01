using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Model.Behaviour
{
    abstract public class Behaviour<E, T>
    {
        protected List<BehaviourItem<E>> behaviour;

        public Behaviour()
        {
            behaviour = new List<BehaviourItem<E>>();
        }

        public Behaviour(T _type) : this()
        {
            this.type = _type;
        }

        public List<BehaviourItem<E>> BehaviourMember
        {
            get { return behaviour; }
            set { behaviour = value; }
        }

        protected T type;

        public T Type
        {
            get { return type; }
            set { type = value; }
        }

        public void addItem(BehaviourItem<E> item)
        {
            behaviour.Add(item);
        }

        public int getValueForItem(E item)
        {
            foreach (BehaviourItem<E> var_Item in this.behaviour)
            {
                if (var_Item.Item.Equals(item))
                    return var_Item.Value;
            }
            return -1;
        }
    }
}
