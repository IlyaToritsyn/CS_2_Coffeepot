using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_2_Coffeepot
{
    public class Machine : IMachine
    {

        public Machine(List<Drink> drinks)
        {
            Status = Status.Ready;
            IsResetAvaible = true;
            Credit = 0;
            SugarLevel = 3;
            Drinks = drinks;
        }

        private int sugarLevel;
        private int strengthLevel;
        private double credit;

        public Status Status { get; set; }
        public bool IsResetAvaible { get; set; }
        public double Credit
        {
            get
            {
                return credit;
            }

            set
            {
                if (value >= 0)
                {
                    credit = value;
                }
            }
        }
        public List<Drink> Drinks { get; set; }
        public int SugarLevel
        {
            get
            {
                return sugarLevel;
            }

            set
            {
                if (value >= 0 && value <= 5)
                {
                    sugarLevel = value;
                }
            }
        }
        public int StrengthLevel
        {
            get
            {
                return strengthLevel;
            }

            set
            {
                if (value >= 0 && value <= 5)
                {
                    strengthLevel = value;
                }
            }
        }

        public void AddDrink()
        {
            throw new NotImplementedException();
        }

        public void DeleteDrink()
        {
            throw new NotImplementedException();
        }

        public void GetMoney()
        {
            throw new NotImplementedException();
        }

        public void GiveChange()
        {
            throw new NotImplementedException();
        }
    }
}