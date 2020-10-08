using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_2_Coffeepot
{
    public interface IMachine
    {
        Status Status { get; set; }
        bool IsResetAvaible { get; set; }
        double Credit { get; set; }
        List<Drink> Drinks { get; set; }
        int SugarLevel { get; set; }
        int StrengthLevel { get; set; }

        void AddDrink();
        void DeleteDrink();
        void GiveChange();
        void GetMoney();
    }
}