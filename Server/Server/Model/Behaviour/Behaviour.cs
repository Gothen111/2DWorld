using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Model.Behaviour
{
    abstract class Behaviour<E>
    {
        protected List<BehaviourItem<E>> behaviour;

        public List<BehaviourItem<E>> Behaviour
        {
            get { return behaviour; }
            set { behaviour = value; }
        }

        public void updateBehaviour()
        {
            List<E> list = castValues(Enum.GetValues(behaviourMembers.GetType()));
            foreach(E item in list)
            {
                if (!behaviour.Contains(item))
                {
                    behaviour.Add(item);
                }
            }
        }

        public List<E> castValues(Array array)
        {
            List<E> list = new List<E>();
            foreach (var item in array)
            {
                if (item is E)
                {
                    list.Add((E)item);
                }
                else
                {
                    Logger.Logger.LogErr("Fehler bei Behaviour: Übergebenes Enum ist nicht vom richtigen Typ");
                }
            }

            return list;
        }

    }
}
