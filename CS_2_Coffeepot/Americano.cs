using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace CS_2_Coffeepot
{
    public class Americano : Coffee
    {
        public Americano()
        {
            Name = "Americano";
            Price = 30.00;
        }

        public override void Cook()
        {

        }
    }
}