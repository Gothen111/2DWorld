using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GameLibrary.Model.Behaviour
{
    [Serializable()]
    public class BehaviourItem <E>
    {
        protected E item;
        public E Item{
            get { return item; }
        }

        protected int value;
        public int Value{
            get{ return value; }
            set{ this.value = value; }
        }

        public void addToValue(int modifier)
        {
            value += modifier;
        }

        public BehaviourItem(E _item, int _value)
        {
            item = _item;
            value = _value;
        }

        public BehaviourItem(SerializationInfo info, StreamingContext ctxt)
        {
            this.item = (E)info.GetValue("item", typeof(E));
            this.value = (int)info.GetValue("value", typeof(int));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("item", this.item, typeof(E));
            info.AddValue("value", this.value, typeof(int));
        }
    }
}
