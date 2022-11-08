using CompetitiveCoders.com_Considition2022.models;
using GeneticSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompetitiveCoders.com_Considition2022.Genetic
{
    class SolutionChromosome : ChromosomeBase
    {
        public SolutionChromosome()
            : base(8)
        {
            CreateGenes();
        }

        public void SetGenesFromLogRow(string logRow)
        {
            var ss = logRow.Split('\t');
            ReplaceGene(0, new Gene(bool.Parse(ss[3])));
            ReplaceGene(1, new Gene(int.Parse(ss[4])));
            ReplaceGene(2, new Gene(double.Parse(ss[5])));
            ReplaceGene(3, new Gene(double.Parse(ss[9])));
            ReplaceGene(4, new Gene(double.Parse(ss[6])));
            ReplaceGene(5, new Gene(int.Parse(ss[7])));
            ReplaceGene(6, new Gene(double.Parse(ss[8])));
            ReplaceGene(7, new Gene(double.Parse(ss[10])));
        }

        public Solution ToSolution()
        {
            return new Solution()
            {
                recycleRefundChoice = (bool)GetGene(0).Value,
                bagPrice = (int)GetGene(1).Value,
                refundAmountPercent = (double)GetGene(2).Value,
                
                BudgetPercentStart = (double)GetGene(3).Value,
                FirstDayBagsPerPerson = (double)GetGene(4).Value,
                NewBagsInterval = (int)GetGene(5).Value,
                RenewBagsPerPerson = (double)GetGene(6).Value,
                BudgetPercentRenew = (double)GetGene(7).Value,
            };
            
        }

        Random _rng = new ();


        private Dictionary<int, int[]> allowedPrices = new(5)
        {
            {1, new[] {1, 2, 5}},
            {2, new[] {1, 2, 5}},
            {3, new[] {5, 8, 10, 14}},
            {4, new[] {20, 25, 30, 40}},
            {5, new[] {150, 200, 220, 300, 400}},


        };


        public override Gene GenerateGene(int geneIndex)
        {
            switch (geneIndex)
            {
                case 0: //recycleRefundChoice
                    return new Gene(_rng.NextDouble() > 0.5);
                case 1: //bagPrice
                   
                    return new Gene(allowedPrices[GlobalConfig.BagType][_rng.Next(allowedPrices[GlobalConfig.BagType].Count())]);
                case 2: //refundAmountPercent
                    var percentages = new[] { 0, 0.10, 0.30, 0.50, 0.7, 0.90 };
                    return new Gene(percentages[_rng.Next(percentages.Length)]);
                case 3: //budgetPercentStart
                    var maxBudgetPercentStart = 0.5;
                    var minBudgetPercentStart = 0.0;
                    var stepBudgetPercentStart = 0.03;

                    var choicesBudgetPercent = Enumerable.Range(0, 100)
                        .Select(i => minBudgetPercentStart + (i * stepBudgetPercentStart))
                        .TakeWhile(i => i <= maxBudgetPercentStart).ToList();

                    return new Gene(choicesBudgetPercent[_rng.Next(choicesBudgetPercent.Count())]);
                case 4: //FirstDayBagsPerPerson
                    var maxFirstDayBags = 10.0;
                    var minFirstDayBags = 0.5;
                    var stepFirstDayBags = 0.5;

                    var choices = Enumerable.Range(0, 100)
                                            .Select(i => minFirstDayBags + (i * stepFirstDayBags))
                                            .TakeWhile(i => i <= maxFirstDayBags).ToList();

                    return new Gene(choices[_rng.Next(choices.Count())]);

                case 5: //NewBagsInterval
                    var maxNewBagsInterval = 12;
                    var minNewBagsInterval = 1;
                    return new Gene(_rng.Next(minNewBagsInterval, maxNewBagsInterval + 1));
                case 6: //RenewBagsPerPerson
                    var maxRenewBagsPerPerson = 10.0;
                    var minRenewBagsPerPerson = 0.5;
                    var stepRenewBagsPerPerson = 0.5;

                    var choicesRenewBagsPerPerson = Enumerable.Range(0, 100)
                                            .Select(i => minRenewBagsPerPerson + (i * stepRenewBagsPerPerson))
                                            .TakeWhile(i => i <= maxRenewBagsPerPerson).ToList();

                    return new Gene(choicesRenewBagsPerPerson[_rng.Next(choicesRenewBagsPerPerson.Count())]);
                case 7: //budgetPercentStart
                    var maxBudgetPercentRenew = 0.5;
                    var minBudgetPercentRenew = 0.0;
                    var stepBudgetPercentRenew = 0.03;

                    var choicesBudgetPercentRenew = Enumerable.Range(0, 100)
                        .Select(i => minBudgetPercentRenew + (i * stepBudgetPercentRenew))
                        .TakeWhile(i => i <= maxBudgetPercentRenew).ToList();

                    return new Gene(choicesBudgetPercentRenew[_rng.Next(choicesBudgetPercentRenew.Count())]);
                default:
                    throw new NotImplementedException("Tried to generate a gene outside of chromosome length");
            }

        }

        public override IChromosome CreateNew()
        {
            return new SolutionChromosome();
        }
    }
}
