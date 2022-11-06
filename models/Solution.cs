using System.Collections.Generic;
using System.Drawing;

namespace CompetitiveCoders.com_Considition2022.models
{
    public class Solution
    {
        public bool recycleRefundChoice { get; set; }
        public int bagPrice { get; set; }
        public int refundAmount { get; set; }
        public int bagType { get; set; }
        public List<int> orders { get; set; }
        public string mapName { get; set; }


        public double FirstDayBagsPerPerson { get; set; }
        public int NewBagsInterval { get; set; }
        public double RenewBagsPerPerson { get; set; }


        public Solution()
        { }

        public Solution(bool recycleRefundChoice, int bagPrice, int refundAmount, int bagType, List<int> orders)
        {
            this.recycleRefundChoice = recycleRefundChoice;
            this.bagPrice = bagPrice;
            this.refundAmount = refundAmount;
            this.bagType = bagType;
            this.orders = orders;
        }
    }
}