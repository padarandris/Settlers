using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Settlers
{
    public class SettlersEngine
    {
        private int totalGold;
        private int totalSteel;
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
            Console.WriteLine("Welcome to Settlers, a game that might remind you of an old game with the same name, but entirely different!");
            Thread.Sleep(2000);
            Console.WriteLine("Okay, let's start building your settlement!");
            Thread.Sleep(1000);
            Console.WriteLine("You can interact with your settlement with the following commands:");
            Console.WriteLine("build <BuildingType> - build a building with appropriate command (like 'build Archery')");
            Console.WriteLine("build <BuildingType> - build a building with appropriate command (like 'build Archery')");
            Console.WriteLine("status - Shows the current status of your settlement (buildings, units, resources)");
            Console.WriteLine("skip - Skip a turn and your settlement grows!");
            Console.WriteLine("quit - Leave the game");

            string input;

            while (running)
            {
                input = Console.ReadLine();
                readCommand(input);
            }

            Console.WriteLine("Thank you for playing! Your settlers will miss you! Goodbye!");
        }

        private void readCommand(string input)
        {
            if (input is null)
            {
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
                        Console.WriteLine("Final report:");
                        printStatus();
                        break;

                    case "status":
                        printStatus();
                        break;

                    case "skip":
                        progressTurn();
                        break;

                    default:
                        Console.WriteLine("Not a valid command");
                        break;
                }
            }
            if (userInputs.Length == 2)
            {
                if (command == "build")
                {
                    string buildingType = userInputs[1].ToLower();
                    switch (buildingType)
                    {
                        case "barrack":
                            Console.WriteLine("A barrack was built. It will start to produce swordsmen and steel for you every few turns.");
                            progressTurn();
                            buildBarrack();
                            break;

                        case "archery":
                            Console.WriteLine("An archery was built. It will start to produce archers and gold for you every few turns.");
                            progressTurn();
                            buildArchery();
                            break;

                        default:
                            Console.WriteLine("There is no such building.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid command");
                }
            }
        }

        private void progressTurn()
        {
            Console.WriteLine("A turn has passed. Your settlement's resources grow.");
            foreach (Building building in settlement)
            {
                building.TurnCounter++;
                building.processTurn();
                updateStats();
            }
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
                }
                if (building is Barrack)
                {
                    totalSteel = ((Barrack)building).TotalSteel;
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
