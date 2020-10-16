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
            IsResetAvaible = false;
            Credit = 0;
            SugarLevel = sugarLevelDefault;
            Drinks = drinks;
        }

        private int sugarLevelDefault = 3;
        private int sugarLevel;
        private int strengthLevel;
        private double credit;
        private int minSugarValue = 0;
        private int maxSugarValue = 5;
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

        public int SugarLevelDefault
        {
            get
            {
                return sugarLevelDefault;
            }

            set
            {
                if (value >= minSugarValue && value <= maxSugarValue)
                {
                    sugarLevelDefault = value;
                }
            }
        }

        public int SugarLevel
        {
            get
            {
                return sugarLevel;
            }

            set
            {
                if (value >= minSugarValue && value <= maxSugarValue)
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
            Credit = 0;
        }

        public void Deposit(int deposit)
        {
            Credit += deposit;
            IsResetAvaible = true;
        }

        private void SetResetAvailability()
        {
            if (SugarLevel == sugarLevelDefault && Credit == 0)
            {
                IsResetAvaible = false;
            }

            else
            {
                IsResetAvaible = true;
            }
        }

        public void IncreaseSugar()
        {
            SugarLevel++;

            SetResetAvailability();
        }

        public void DecreaseSugar()
        {
            SugarLevel--;

            SetResetAvailability();
        }
    }
}