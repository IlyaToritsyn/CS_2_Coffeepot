using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_2_Coffeepot
{
    public abstract class Coffee : Drink
    {
        public Strength Strength
        {
            get => default;
            set
            {
            }
        }

        public Sugar Sugar
        {
            get => default;
            set
            {
            }
        }
    }
}