using System;
using System.Collections.Generic;
using System.Threading;

namespace Hackathon
{
    public class Player
    {
        public static Player BuildCharacter()
        {
            while (true)
            {
                System.Console.WriteLine("What is your name, adventurer?");
                string playerName = Console.ReadLine();
                Thread.Sleep(1000);
                System.Console.WriteLine($"Greetings, {playerName}.");

                System.Console.WriteLine("Please enter your class of choice: Fighter, Wizard, or Cleric");
                string className = Console.ReadLine().ToLower();

                if (className == "fighter")
                {
                    Fighter character = new Fighter($"{playerName}");
                    return character;
                }
                else if (className == "wizard")
                {
                    Wizard character = new Wizard($"{playerName}");
                    return character;
                }
                else if (className == "cleric")
                {
                    Cleric character = new Cleric($"{playerName}");
                    return character;
                }
                else
                {
                    Console.WriteLine("You have not selected an available character. Please try again");
                    continue;
                }
            }
        }
        public string name;
        public int strength;
        public int dexterity;
        public int constitution;
        public int intelligence;
        public int wisdom;
        public int charisma;
        public int armorClass;
        public int health;
        public int hitDie;

        public Player(string Name)
        {
            name = Name;
            strength = 10;
            dexterity = 10;
            constitution = 10;
            intelligence = 10;
            wisdom = 10;
            charisma = 10;
            armorClass = 12;
            health = 20;
            hitDie = 6;
        }

        public virtual void Actions(Enemy enemy)
        {
            return;
        }

        public virtual int Attack(Enemy target)
        {
            Random d20 = new Random();
            int toHit = d20.Next(1,21) + this.strength;
            if (toHit > target.armorClass)
            {
                int damage = d20.Next(1, this.hitDie+1);
                target.health -= damage;
                System.Console.WriteLine($"{this.name} attacks the {target.name} for {damage} points of damage!");
                Thread.Sleep(500);
            }
            else 
            {
                System.Console.WriteLine($"Your roll of {toHit} was too low! Your attack misses!");
                Thread.Sleep(500);
            }
            return target.health;
        }

        public virtual int Initiative()
        {
            Random d20 = new Random();
            int roll = d20.Next(1,21) + this.dexterity;
            return roll;
        }
    }

    public class Fighter : Player
    {
        public int actionSurge;
        public Fighter(string Name) : base(Name)
        {
            name = Name;
            strength = 3;
            dexterity = 0;
            constitution = 3;
            intelligence = 0;
            wisdom = 0;
            charisma = 0;
            armorClass = 16;
            health = 30;
            hitDie = 8;
            actionSurge = 3;
        }

        public override int Initiative()
        {
            return base.Initiative();
        }

        public override void Actions(Enemy enemy)
        {
            System.Console.WriteLine("Choose your action:");
            System.Console.WriteLine("Attack or Surge");
            string action = Console.ReadLine().ToLower();
            if (action == "attack")
            {
                this.Attack(enemy);
            }
            else if (action == "surge")
            {
                this.ActionSurge(enemy);
            }
            else
            {
                System.Console.WriteLine($"You typed '{action}' which is an invalid command, you dingus! It's the {enemy.name}'s turn now, are you happy? *smack*");
                Thread.Sleep(1000);
            }
        }

        public override int Attack(Enemy target)
        {
            Random d20 = new Random();
            int toHit = d20.Next(1,21) + this.strength;
            if (toHit > target.armorClass)
            {
                int damage = d20.Next(1, this.hitDie+1) + this.strength;
                target.health -= damage;
                System.Console.WriteLine($"{this.name} attacks the {target.name} for {damage} points of damage!");
                Thread.Sleep(500);
            }
            else 
            {
                System.Console.WriteLine($"Your roll of {toHit} was too low! Your attack misses!");
                Thread.Sleep(500);
            }
            return target.health;
        }

        public int ActionSurge(Enemy target)
        {
            if (this.actionSurge > 0)
            {
                Attack(target);
                Attack(target);
                this.actionSurge--;
            }
            if(this.actionSurge == 1)
            {
                System.Console.WriteLine($"After your last surge, you are sure you can only successfully perform one more surge.");
                Thread.Sleep(500);
            }
            if (this.actionSurge == 0)
            {
                System.Console.WriteLine($"{this.name} tries to action surge... but fails! You lose a turn as a result of your struggle.");
            }
            return target.health;
        }
    }

    public class Wizard : Player
    {
        public int spells;
        public Wizard(string Name) : base(Name)
        {
            name = Name;
            strength = -1;
            dexterity = 10;
            constitution = 2;
            intelligence = 3;
            wisdom = 0;
            charisma = 0;
            armorClass = 12;
            health = 20;
            hitDie = 6;
            spells = 3;
        }

        public override int Initiative()
        {
            return base.Initiative();
        }

        public override void Actions(Enemy enemy)
        {
            System.Console.WriteLine("Choose your action:");
            System.Console.WriteLine("Attack or Cast");
            string action = Console.ReadLine().ToLower();
            if (action == "attack")
            {
                this.Attack(enemy);
            }
            else if (action == "cast")
            {
                this.MagicMissile(enemy);
            }
        }

        public override int Attack(Enemy target)
        {
            Random d20 = new Random();
            int toHit = d20.Next(1,21) + this.intelligence;
            if (toHit > target.armorClass)
            {
                int damage = d20.Next(1, this.hitDie+1) + this.intelligence;
                target.health -= damage;
                System.Console.WriteLine($"{this.name} attacks the {target.name} for {damage} points of damage!");
                Thread.Sleep(500);
            }
            else 
            {
                System.Console.WriteLine($"Your roll of {toHit} was too low! Your attack misses!");
                Thread.Sleep(500);
            }            
            return target.health;
        }

        public int MagicMissile(Enemy target)
        {
            if (this.spells > 0)
            {
                Random threeD4 = new Random();
                int damage = threeD4.Next(3, 13);
                System.Console.WriteLine($"{this.name} casts Magic Missile at the {target.name} for {damage} points of damage!");
                Thread.Sleep(500);
                target.health -= damage;
                this.spells--;
            }
            if(this.spells == 1)
            {
                System.Console.WriteLine($"{this.name} energy is getting dangerously low. You can only cast one more spell!");
                Thread.Sleep(500);

            }
            if(this.spells == 0)
            {
                System.Console.WriteLine($"{this.name}'s fingers fizzle out as they try cast Magic Missile! You're out of spells! You can only attack from now on!");
            }
            return target.health;
        }
    }

    public class Cleric : Player
    {
        public int heals;
        public Cleric(string Name) : base(Name)
        {
            name = Name;
            strength = 0;
            dexterity = 0;
            constitution = 2;
            intelligence = 0;
            wisdom = 3;
            charisma = 0;
            armorClass = 12;
            health = 20;
            hitDie = 6;
            heals = 3;
        }

        public override int Initiative()
        {
            return base.Initiative();
        }        

        public override void Actions(Enemy enemy)
        {
            System.Console.WriteLine("Choose your action:");
            System.Console.WriteLine("Attack or Heal");
            string action = Console.ReadLine().ToLower();
            if (action == "attack")
            {
                this.Attack(enemy);
            }
            else if (action == "heal")
            {
                this.Heal(this);
            } 
        }

        public override int Attack(Enemy target)
        {
            Random d20 = new Random();
            int toHit = d20.Next(1,21) + this.strength;
            if (toHit > target.armorClass)
            {
                int damage = d20.Next(1, this.hitDie+1) + this.strength;
                target.health -= damage;
                System.Console.WriteLine($"{this.name} attacks the {target.name} for {damage} points of damage!");
                Thread.Sleep(500);
            }
            else 
            {
                System.Console.WriteLine($"Your roll of {toHit} was too low! Your attack misses!");
                Thread.Sleep(500);
            }
            return target.health;
        }

        public int Heal(Player target)
        {
            if (this.heals > 0)
            {
                Random d10 = new Random();
                int heal = d10.Next(1,11) + this.wisdom;
                System.Console.WriteLine($"{this.name} beckons to the gods for assistance. Your health has increased by {heal} points!");
                Thread.Sleep(500);
                target.health += heal;
                this.heals--;
            }
            if (this.heals == 1)
            {
                System.Console.WriteLine("You feel your energy draining. You know you can only cast one more heal spell.");
                Thread.Sleep(500);
            }
            if (this.heals == 0)
            {
                System.Console.WriteLine($"{this.name} beckons to the gods for assistance...");
                Thread.Sleep(500);
                System.Console.WriteLine("...The gods don't seem to answer your call.");
                return target.health;
            }
            return target.health;
        }
    }
}