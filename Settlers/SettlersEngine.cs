using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Settlers
{
    public class SettlersEngine
    {
        private double totalGold = 100;
        private double totalSteel = 100;
        private int totalArchers;
        private int totalSwordsmen;

        private List<Building> settlement = new List<Building>();
        private bool running = true;

        public void run()
        {
            
            Console.WriteLine("                                                                      ,--. ,--.  ");
            Console.WriteLine(" ,---.           ,--.    ,--.  ,--.                          ,-----.,-|  |-|  |-.");
            Console.WriteLine("'   .-'  ,---. ,-'  '-.,-'  '-.|  | ,---. ,--.--. ,---.     '  .--./'-|  |-|  |-'");
            Console.WriteLine("`.  `-. | .-. :'-.  .-''-.  .-'|  || .-. :|  .--'(  .-'     |  |    ,-|  |-|  |-.");
            Console.WriteLine(".-'    ||   --.  |  |    |  |  |  ||   --.|  |   .-'  `)    '  '--'|'-|  |-|  |-'");
            Console.WriteLine("`-----'  `----'  `--'    `--'  `--' `----'`--'   `----'      `-----'  `--' `--'  ");
            Thread.Sleep(1000);
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Welcome to Settlers, a game that might remind you of an old game with the same name, but entirely different!");
            Thread.Sleep(1000);
            Console.WriteLine("Okay, let's start building your settlement!");
            Thread.Sleep(1000);
            Console.WriteLine("You start with 100 gold and 100 steel. Buildings cost 20 of each.");
            Console.WriteLine("You can interact with your settlement with the following commands:");
            Thread.Sleep(1000);
            Console.WriteLine("build <BuildingType> - build a building with appropriate command (like 'build Archery')");
            Console.WriteLine("trade - trade your resources with a friendly merchant");
            Console.WriteLine("status - Shows the current status of your settlement (buildings, units, resources)");
            Console.WriteLine("skip - Skip a turn and your settlement grows!");
            Console.WriteLine("quit - Leave the game");

            string input;

            while (running)
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("What to do? (build, trade, skip, status, quit)");
                input = Console.ReadLine();
                readCommand(input);
            }
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Thank you for playing! Your settlers will miss you! Goodbye!");
        }

        private void readCommand(string input)
        {
            if (input is null)
            {
                Thread.Sleep(1000);
                Console.WriteLine("You have to give a command!");
                return;
            }
            string[] userInputs = input.Split(" ");
            string command = userInputs[0];

            if (userInputs.Length == 1)
            {
                switch (command)
                {
                    case "quit":
                        running = false;
                        Thread.Sleep(1000);
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("Final report:");
                        Console.WriteLine();
                        printStatus();
                        break;
                    
                    case "trade":
                        trade();
                        break;

                    case "status":
                        printStatus();
                        break;

                    case "skip":
                        progressTurn();
                        break;

                    default:
                        Thread.Sleep(1000);
                        Console.WriteLine("Not a valid command");
                        break;
                }
            }
            if (userInputs.Length == 2)
            {
                if (command == "build" && totalGold >= 20 && totalSteel >= 20)
                {
                    string buildingType = userInputs[1].ToLower();
                    switch (buildingType)
                    {
                        case "barrack":
                            Thread.Sleep(1000);
                            Console.WriteLine("--------------------------------------------------");
                            Console.WriteLine("A barrack was built. It will start to produce swordsmen and steel for you every few turns.");
                            totalGold -= 20;
                            totalSteel -= 20;
                            progressTurn();
                            buildBarrack();
                            break;

                        case "archery":
                            Thread.Sleep(1000);
                            Console.WriteLine("--------------------------------------------------");
                            Console.WriteLine("An archery was built. It will start to produce archers and gold for you every few turns.");
                            totalGold -= 20;
                            totalSteel -= 20;
                            progressTurn();
                            buildArchery();
                            break;

                        default:
                            Thread.Sleep(1000);
                            Console.WriteLine("There is no such building.");
                            break;
                    }
                }
                else if (command == "build" && (totalGold < 20 || totalSteel < 20))
                {
                    Console.WriteLine("You don't have enough resources to build this turn. You might want to trade though.");
                }
                else
                {
                    Console.WriteLine("Not a valid command");
                }
            }
            if (userInputs.Length > 2)
            {
                Console.WriteLine("Not a valid command");
            }
        }

        private void progressTurn()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("A turn has passed.");
            foreach (Building building in settlement)
            {
                building.TurnCounter++;
                building.processTurn();
                updateStats();
            }
        }

        private void trade()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("You find a trustworthy looking merchant at the marketplace. You may trade resources with him!");
            Thread.Sleep(1000);
            Console.WriteLine("Merchant: How much gold/steel you wish to trade?");
            double num = 0;
            string tradedAmount = Console.ReadLine();
            string[] parsedInput = tradedAmount.Split(" ");
            bool isNumeric = double.TryParse(parsedInput[0], out num);
            if (parsedInput.Length < 2 || parsedInput.Length > 2 || !isNumeric || num < 0)
            {
                Console.WriteLine("Merchant: You try to cheat me mate! Off you go!");
                Thread.Sleep(1000);
                Console.WriteLine("The merchant threw you out of the window!!");
                progressTurn();
                return;
            }
            if (parsedInput[1] == "gold" && totalGold >= num)
            {
                totalGold -= num;
                double addedSteel = num / 2;
                totalSteel += addedSteel;
                Thread.Sleep(1000);
                Console.WriteLine("Merchant: Thanks for the trade! Now off you go!");
            }
            else if (parsedInput[1] == "steel" && totalSteel >= num)
            {
                totalSteel -= num;
                double addedGold = num / 3;
                totalGold += addedGold;
                Thread.Sleep(1000);
                Console.WriteLine("Merchant: Thanks for the trade! Now off you go!");
            }
            else
            {
                Thread.Sleep(1000);
                Console.WriteLine("The merchant looks at you suspeciously.");
                Thread.Sleep(1000);
                Console.WriteLine("Merchant: Changed my mind mate, off you go!");
                Thread.Sleep(1000);
                Console.WriteLine("You have to leave now...");
            }
            progressTurn();

        }

        private void buildBarrack()
        {
            settlement.Add(new Barrack());
        }

        private void buildArchery()
        {
            settlement.Add(new Archery());
        }

        private void updateStats()
        {
            countUnits();
            countResources();
        }

        private void countUnits()
        {
            totalArchers = 0;
            totalSwordsmen = 0;
            foreach (Building building in settlement)
            {
                if (building is Archery)
                {
                    totalArchers += ((Archery)building).Archers.Count;
                }
                if (building is Barrack)
                {
                    totalSwordsmen += ((Barrack)building).Swordsmen.Count;
                }
            }
        }

        private void countResources()
        {
            
            foreach (Building building in settlement)
            {
                if (building is Archery)
                {
                    totalGold += ((Archery)building).TotalGold;
                    ((Archery)building).GoldTreasury.Clear();
                    ((Archery)building).TotalGold = 0;
                }
                if (building is Barrack)
                {
                    totalSteel += ((Barrack)building).TotalSteel;
                    ((Barrack)building).SteelTreasury.Clear();
                    ((Barrack)building).TotalSteel = 0;
                }
            }
        }

        private void printStatus()
        {
            Console.WriteLine("Treasury:");
            Console.WriteLine("---Total Gold: " + totalGold);
            Console.WriteLine("---Total Steel: " + totalSteel);

            Console.WriteLine();
            Console.WriteLine("Buildings:");
            foreach(Building building in settlement)
            {
                Console.WriteLine(building.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("---Archers: " + totalArchers);
            Console.WriteLine("---Swordsmen: " + totalSwordsmen);
        }
    }
}
