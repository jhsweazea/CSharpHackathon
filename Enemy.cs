using System;
using System.Collections.Generic;
using System.Threading;

namespace Hackathon
{
    public class Enemy
    {
        public string name;
        public int strength;
        public int dexterity;
        public int constitution;
        public int wisdom;
        public int intelligence;
        public int charisma;
        public int armorClass;
        public int health;
        public int hitDie;

        public virtual int Attack(Player target)
        {
            Random d20 = new Random();
            int toHit = d20.Next(1,21) + this.strength;
            if (toHit > target.armorClass)
            {
                int damage = d20.Next(1, hitDie+1);
                System.Console.WriteLine($"The {this.name} attacks {target.name} for {damage} points of damage!");
                Thread.Sleep(500);
                target.health -= damage;
            }
            else
            {
                System.Console.WriteLine($"The {this.name} attacks {target.name}, but the attack misses!");
                Thread.Sleep(500);
            }
            return target.health;
        }
        public int Initiative()
        {
            Random d20 = new Random();
            int roll = d20.Next(1, 21) + this.dexterity;
            return roll;
        }

        public class Skeleton : Enemy
        {
            public Skeleton()
            {
                //changed attributes to be modifier scores
                name = "Skeleton";
                strength = 1;
                dexterity = 0;
                constitution = 0;
                intelligence = -1;
                wisdom = -1;
                charisma = -1;
                armorClass = 8;
                health = 15;
                hitDie = 8;
            }
        }


        public class Vampire : Enemy
        {
            public Vampire()
            {
                name = "Vampire";
                strength = 1;
                dexterity = 0;
                constitution = 2;
                intelligence = 1;
                wisdom = 1;
                charisma = 1;
                armorClass = 12;
                health = 20;
                hitDie = 6;
            }
        }


        public class Zombie : Enemy
        {
            public Zombie()
            {
                name = "Zombie";
                strength = 0;
                dexterity = -1;
                constitution = 1;
                intelligence = 0;
                wisdom = 1;
                charisma = 1;
                armorClass = 8;
                health = 16;
                hitDie = 6;
            }
        }
        public class Orc : Enemy
        {
            public Orc()
            {
                name = "Orc";
                strength = 4;
                dexterity = 0;
                constitution = 2;
                intelligence = 0;
                wisdom = 1;
                charisma = -1;
                armorClass = 10;
                health = 30;
                hitDie = 12;
            }
        }
        public class Dragon : Enemy
        {
            public Dragon()
            {
                name = "Dragon";
                strength = 8;
                dexterity = 3;
                constitution = 2;
                intelligence = 6;
                wisdom = 6;
                charisma = 0;
                armorClass = 12;
                health = 60;
                hitDie = 20;
            }
        }
    }
}
