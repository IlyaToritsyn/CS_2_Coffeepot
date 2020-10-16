using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CS_2_Coffeepot
{
    [Serializable(),
        XmlInclude(typeof(Americano)),
        XmlInclude(typeof(Black)),
        XmlInclude(typeof(Cappuccino)),
        XmlInclude(typeof(ConPanna)),
        XmlInclude(typeof(CreamAndSugar)),
        XmlInclude(typeof(Doppio)),
        XmlInclude(typeof(DryCappuccino)),
        XmlInclude(typeof(Espresso))]

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