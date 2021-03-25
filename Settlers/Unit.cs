using System;
using System.Collections.Generic;
using System.Text;

namespace Settlers
{
    
    public class Swordsman : Unit
    {
        public Swordsman()
        {
            Health = 40;
            Damage = 13;
        }
    }

    public class Archer : Unit
    {
        public Archer()
        {
            Health = 30;
            Damage = 15;
        }
    }
    public abstract class Unit
    {
        private int health;
        private int damage;

        public void damageUnit(int damage)
        {
            if (health - damage > 0)
            {
                health -= damage;
            }
            else
            {
                health = 0;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        public int Damage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
            }
        }

    }
}
