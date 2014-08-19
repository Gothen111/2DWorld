using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Model.Object.Interaction.Interactions
{
    public class ChestInteraction : LivingObjectInteraction
    {
        private bool isOpen;

        public ChestInteraction(LivingObject _InteractionOwner)
            : base(_InteractionOwner)
        {
            this.isOpen = false;
        }

        public override void doInteraction(LivingObject _Interactor)
        {
            base.doInteraction(_Interactor);
            if (this.isOpen)
            {
                this.closeChest();
            }
            else
            {
                this.openChest();
            }
        }

        private void closeChest()
        {
            //this.InteractionOwner.Animation = new GameLibrary.Model.Object.Animation.Animations.OpenChestAnimation(this.InteractionOwner);
            //this.isOpen = false;
        }

        private void openChest()
        {
            //this.InteractionOwner.Animation = new GameLibrary.Model.Object.Animation.Animations.OpenChestAnimation(this.InteractionOwner);
            this.isOpen = true;
        }
    }
}
