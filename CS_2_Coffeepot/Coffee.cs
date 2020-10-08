using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_2_Coffeepot
{
    public abstract class Coffee : Drink
    {
        public int strength = 3;
        public int sugar = 3;
        public int Strength
        {
            get
            {
                return strength;
            }

            set
            {
                if (value >= 0 && value < 5)
                {
                    strength = value;
                }
            }
        }

        public int Sugar
        {
            get
            {
                return sugar;
            }

            set
            {
                if (value >= 0 && value < 5)
                {
                    sugar = value;
                }
            }
        }
    }
}