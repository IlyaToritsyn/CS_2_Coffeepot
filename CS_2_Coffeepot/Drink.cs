﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CS_2_Coffeepot
{
    [Serializable(), XmlInclude(typeof(Coffee))]
    public class Drink : IDrink
    {
        double price;
        public string Name { get; set; }
        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                if (value >= 0.00)
                {
                    price = value;
                }
            }
        }

        public void ChangePrice()
        {
            throw new NotImplementedException();
        }

        virtual public void Cook()
        {
            throw new NotImplementedException();
        }
    }
}