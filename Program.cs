using System;
using System.Collections.Generic;
using System.Threading;



namespace Hackathon
{
    class Program
    {
        //game loop
        static void Main(string[] args)
        {
            Console.Clear();
            System.Console.WriteLine("Welcome to the encounter simulator!");
            Thread.Sleep(1000);
            Player character = Player.BuildCharacter();
            System.Console.WriteLine("Creation complete!");
            Thread.Sleep(500);
            Console.Clear();
            bool isRunning = true;
            while (isRunning)
            {
                System.Console.WriteLine("On a scale of 1-5, how difficult would you like the fight to be?");
                string difficulty = Console.ReadLine();
                Enemy enemy = new Enemy();
                if(difficulty == "1")
                {
                    enemy = new Enemy.Zombie();
                }
                else if(difficulty == "2")
                {
                    enemy = new Enemy.Skeleton();
                }
                else if(difficulty == "3")
                {
                    enemy = new Enemy.Vampire();
                }
                else if(difficulty == "4")
                {
                    enemy = new Enemy.Orc();
                }
                else if(difficulty == "5")
                {
                    enemy = new Enemy.Dragon();
                }
                else
                {
                    System.Console.WriteLine("Error detected. Start over, dingus.");
                    isRunning = false;
                    break;
                }
                bool playerTurn = false;
                bool enemyTurn = false;
                if(character.Initiative() >= enemy.Initiative())
                {
                    System.Console.WriteLine("You rolled a higher initiative! You have the first attack.");
                    System.Console.WriteLine($"A {enemy.name} approaches for the attack!");
                    playerTurn = true;
                }
                else 
                {
                    System.Console.WriteLine($"The {enemy.name} rolled a higher initiative! They will attack first.");
                    enemyTurn = true;
                }
                //combat loop
                while(character.health > 0 || enemy.health > 0)
                {
                    if(playerTurn == true)
                    {
                        System.Console.WriteLine($"Your current health is: {character.health}.");
                        System.Console.WriteLine($"The {enemy.name}'s health is: {enemy.health}");
                        Thread.Sleep(500);
                        System.Console.WriteLine("The combat rages on! Enter your command:");
                        character.Actions(enemy);
                        playerTurn = false;
                        enemyTurn = true;
                    }
                    if(enemyTurn == true)
                    {
                        enemy.Attack(character);
                        enemyTurn = false;
                        playerTurn = true;
                    }
                    if(character.health <= 0)
                    {
                        System.Console.WriteLine($"{character.name} has died a painful death at the hands of the {enemy.name}. Try again if you dare!");
                        isRunning = false;
                    }
                    else if (enemy.health <= 0)
                    {
                        Console.WriteLine($"You have slain the {enemy.name}! Huzzah!");
                        break;
                    }
                }
            }
            System.Console.WriteLine("The game's over, go home.");
        }
    }
}