using System;
using System.Collections.Generic;
using System.Text;

namespace Settlers
{
    public class Barrack : Building
    {
        private int swordsmanCountdown = 4;
        private int steelCountdown = 3;
        private int totalSteel;

        private List<Swordsman> swordsmen = new List<Swordsman>();
        private List<Resource> steelTreasury = new List<Resource>();



        public override void processTurn()
        {
            swordsmanCountdown -= 1;
            steelCountdown -= 1;

            if (isSwordsmanProductionReady())
            {
                produceUnit();
                swordsmanCountdown = 4;
            }
            if (isSteelProductionReady())
            {
                produceResource();
                steelCountdown = 3;
            }

        }

        public override void produceUnit()
        {
            swordsmen.Add(new Swordsman());
        }

        public override void produceResource()
        {
            steelTreasury.Add(new Resource(ResourceType.STEEL, 10));
            countResources();
        }

        public void countResources()
        {
            int newAmount = 0;
            foreach (Resource resource in steelTreasury)
            {
                newAmount += resource.Quantity;
            }
            totalSteel = newAmount;
        }

        private bool canProduceSomething()
        {
            return isSwordsmanProductionReady() || isSteelProductionReady();
        }

        private bool isSwordsmanProductionReady()
        {
            return TurnCounter % 4 == 0;
        }

        private bool isSteelProductionReady()
        {
            return TurnCounter % 3 == 0;
        }

        public int TotalSteel
        {
            get
            {
                return totalSteel;
            }
        }

        public List<Swordsman> Swordsmen
        {
            get
            {
                return swordsmen;
            }
        }

        public List<Resource> SteelTreasury
        {
            get
            {
                return steelTreasury;
            }
        }

        public override string ToString()
        {
            return "---Barrack: " + swordsmanCountdown + " turn(s) until producing new swordsman, "
                + steelCountdown + " turn(s) until producing additional steel";
        }


    }
}
