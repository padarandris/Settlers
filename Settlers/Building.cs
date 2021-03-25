using System;
using System.Collections.Generic;
using System.Text;

namespace Settlers
{
    public abstract class Building
    {
        private int turnCounter = 0;

        public abstract void produceUnit();

        public abstract void produceResource();

        public abstract void processTurn();

        public int TurnCounter
        {
            get
            {
                return turnCounter;
            }
            set
            {
                turnCounter = value;
            }
        }
    }
}
