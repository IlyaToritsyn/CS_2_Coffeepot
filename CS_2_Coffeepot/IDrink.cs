using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_2_Coffeepot
{
    public interface IDrink
    {
        int Name { get; set; }
        int Price { get; set; }

        void Cook();
    }
}