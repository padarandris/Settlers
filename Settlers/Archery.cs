using System;
using System.Collections.Generic;
using System.Text;

namespace Settlers
{
    public class Archery : Building
    {
        private int archerCountdown = 3;
        private int goldCountdown = 2;
        private int totalGold;

        private List<Archer> archers = new List<Archer>();
        private List<Resource> goldTreasury = new List<Resource>();

        public override void processTurn()
        {
            archerCountdown -= 1;
            goldCountdown -= 1;

            if (isArcherProductionReady())
            {
                produceUnit();
                archerCountdown = 4;
            }
            if (isGoldProductionReady())
            {
                produceResource();
                goldCountdown = 3;
            }

        }

        public override void produceUnit()
        {
            archers.Add(new Archer());
        }

        public override void produceResource()
        {
            goldTreasury.Add(new Resource(ResourceType.GOLD, 5));
            countResources();
        }

        public void countResources()
        {
            int newAmount = 0;
            foreach (Resource gold in goldTreasury)
            {
                newAmount += gold.Quantity;
            }
            totalGold = newAmount;
        }

        private bool canProduceSomething()
        {
            return isArcherProductionReady() || isGoldProductionReady();
        }

        private bool isArcherProductionReady()
        {
            return TurnCounter % 3 == 0;
        }

        private bool isGoldProductionReady()
        {
            return TurnCounter % 2 == 0;
        }

        public int TotalGold
        {
            get
            {
                return totalGold;
            }
        }

        public List<Archer> Archers
        {
            get
            {
                return archers;
            }
        }

        public List<Resource> GoldTreasury
        {
            get
            {
                return goldTreasury;
            }
        }

        public override string ToString()
        {
            return "---Archery: " + archerCountdown + " turn(s) until producing new archer, "
                + goldCountdown + " turn(s) until producing additional gold";
        }
    }
}
