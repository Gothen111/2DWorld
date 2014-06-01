using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Commands.CommandTypes
{
    abstract public class Command
    {
        public Command()
        {
        }

        public abstract void doCommand();
        public abstract void stopCommand();
    }
}
