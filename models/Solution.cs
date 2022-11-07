using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;

namespace CompetitiveCoders.com_Considition2022.models
{
    public class Solution
    {
        public bool recycleRefundChoice { get; set; }
        public int bagPrice { get; set; }

        public int refundAmount => (int) Math.Floor(refundAmountPercent * bagPrice);
        public int bagType { get; set; }
        public List<int> orders { get; set; }
        public string mapName { get; set; }



        // The algorithm

        [JsonIgnore]
        public double refundAmountPercent { get; set; }

        [JsonIgnore]
        public double FirstDayBagsPerPerson { get; set; }
        
        [JsonIgnore]
        public int NewBagsInterval { get; set; }

        [JsonIgnore]
        public double RenewBagsPerPerson { get; set; }

        [JsonIgnore]
        public double BudgetPercentRenew { get; set; }
        [JsonIgnore]
        public double BudgetPercentStart { get; set; }

        public Solution()
        { }



        public Solution(bool recycleRefundChoice, int bagPrice, int refundAmountPercent, int bagType, List<int> orders)
        {
            this.recycleRefundChoice = recycleRefundChoice;
            this.bagPrice = bagPrice;
            this.refundAmountPercent = refundAmountPercent;
            this.bagType = bagType;
            this.orders = orders;
        }
    }
}