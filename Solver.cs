using System;
using System.Collections.Generic;
using System.Linq;
using CompetitiveCoders.com_Considition2022.models;
using CompetitiveCoders.com_Considition2022.responses;
using Newtonsoft.Json.Linq;


namespace CompetitiveCoders.com_Considition2022
{
    public class Solver
    {
        private static List<double> bagType_price = new List<double> { 1.7, 1.75, 6.0, 25.0, 200.0 };
        private static List<double> bagType_co2_transport = new List<double> { 3.0, 4.2, 1.8, 3.6, 12.0 };
        private static List<int> bagType_co2_production = new List<int> { 30, 24, 36, 42, 60 };



        public Solver()
        {
        }

        public Solution AddOrders(Solution solution, GameResponse gameSettings, int days)
        {
            var orders = new List<int>();

            var firstDayOrder = bagType_price[solution.bagType-1] * (int)Math.Floor(solution.FirstDayBagsPerPerson * gameSettings.population) + (int)Math.Floor(solution.BudgetPercentStart*gameSettings.companyBudget);
            firstDayOrder = Math.Floor(firstDayOrder / bagType_price[solution.bagType - 1]) *
                            bagType_price[solution.bagType - 1];

            var finalFirstDayOrder = (int) firstDayOrder;

            orders.Add(Math.Min(finalFirstDayOrder, gameSettings.companyBudget));



            for (int day = 1; day < days; day++)
            {



                if (day % solution.NewBagsInterval == 0)
                {
                    var renewOrder = (int)Math.Floor(solution.RenewBagsPerPerson * gameSettings.population* bagType_price[solution.bagType-1]) + (int)Math.Floor(solution.BudgetPercentRenew * gameSettings.companyBudget);

                    var roundedRenewOrder = Math.Floor(renewOrder / bagType_price[solution.bagType - 1]) *
                                            bagType_price[solution.bagType - 1];


                    var maxBudget = Math.Floor(gameSettings.companyBudget * 0.9 / bagType_price[solution.bagType - 1]) *
                                    bagType_price[solution.bagType - 1];

                    var finalRenewOrder = Math.Min((int) roundedRenewOrder, (int) maxBudget);
                    
                    
                    orders.Add(finalRenewOrder);
                }
                else
                {
                    orders.Add(0);
                }
            }

            solution.orders = orders;
            return solution;
        }


    }
}
