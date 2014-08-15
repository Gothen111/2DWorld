using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GameLibrary.Model.Behaviour
{
    [Serializable()]
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

        public Behaviour(SerializationInfo info, StreamingContext ctxt)
        {
            this.behaviour = (List<BehaviourItem<E>>)info.GetValue("behaviourMember", typeof(List<BehaviourItem<E>>));
            this.type = (T)info.GetValue("type", typeof(T));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("behaviourMember", this.behaviour, typeof(List<BehaviourItem<E>>));
            info.AddValue("type", this.Type, typeof(T));
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
