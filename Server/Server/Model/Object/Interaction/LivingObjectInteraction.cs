using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Model.Object.Interaction
{
    class LivingObjectInteraction
    {
        private LivingObject interactionOwner;

        internal LivingObject InteractionOwner
        {
            get { return interactionOwner; }
            set { interactionOwner = value; }
        }

        public LivingObjectInteraction(LivingObject _InteractionOwner)
        {
            this.interactionOwner = _InteractionOwner;
        }

        public virtual void doInteraction(LivingObject _Interactor)
        {
        }
    }
}
