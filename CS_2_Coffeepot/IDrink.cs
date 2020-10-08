using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_2_Coffeepot
{
    public interface IDrink
    {
        string Name { get; set; }
        double Price { get; set; }

        void Cook();
        void ChangePrice();
    }
}