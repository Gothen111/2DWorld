﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Commands.CommandTypes
{
    abstract class Command
    {
        private Object actor;

        protected Object Actor
        {
            get { return actor; }
            set { actor = value; }
        }

        public Command()
        {
        }

        public abstract void doCommand();
    }
}
