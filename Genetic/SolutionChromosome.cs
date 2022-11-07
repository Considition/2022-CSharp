using CompetitiveCoders.com_Considition2022.models;
using GeneticSharp;
using System;
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

        Random _rng = new Random();

        public override Gene GenerateGene(int geneIndex)
        {
            switch (geneIndex)
            {
                case 0: //recycleRefundChoice
                    return new Gene(_rng.NextDouble() > 0.5);
                case 1: //bagPrice
                    var allowedPrices = new[] { 1, 10, 20, 50,70,100,150,200,250,300 };
                    return new Gene(allowedPrices[_rng.Next(allowedPrices.Length)]);
                case 2: //refundAmountPercent
                    var percentages = new[] { 0, 0.10, 0.30, 0.60, 0.90 };
                    return new Gene(percentages[_rng.Next(percentages.Length)]);
                case 3: //budgetPercentStart
                    var maxBudgetPercentStart = 1.0;
                    var minBudgetPercentStart = 0.0;
                    var stepBudgetPercentStart = 0.1;

                    var choicesBudgetPercent = Enumerable.Range(0, 100)
                        .Select(i => minBudgetPercentStart + (i * stepBudgetPercentStart))
                        .TakeWhile(i => i <= maxBudgetPercentStart).ToList();

                    return new Gene(choicesBudgetPercent[_rng.Next(choicesBudgetPercent.Count())]);
                case 4: //FirstDayBagsPerPerson
                    var maxFirstDayBags = 4.0;
                    var minFirstDayBags = 1.0;
                    var stepFirstDayBags = 0.5;

                    var choices = Enumerable.Range(0, 100)
                                            .Select(i => minFirstDayBags + (i * stepFirstDayBags))
                                            .TakeWhile(i => i <= maxFirstDayBags).ToList();

                    return new Gene(choices[_rng.Next(choices.Count())]);

                case 5: //NewBagsInterval
                    var maxNewBagsInterval = 7;
                    var minNewBagsInterval = 1;
                    return new Gene(_rng.Next(minNewBagsInterval, maxNewBagsInterval + 1));
                case 6: //RenewBagsPerPerson
                    var maxRenewBagsPerPerson = 4.0;
                    var minRenewBagsPerPerson = 1.0;
                    var stepRenewBagsPerPerson = 0.5;

                    var choicesRenewBagsPerPerson = Enumerable.Range(0, 100)
                                            .Select(i => minRenewBagsPerPerson + (i * stepRenewBagsPerPerson))
                                            .TakeWhile(i => i <= maxRenewBagsPerPerson).ToList();

                    return new Gene(choicesRenewBagsPerPerson[_rng.Next(choicesRenewBagsPerPerson.Count())]);
                case 7: //budgetPercentStart
                    var maxBudgetPercentRenew = 1.0;
                    var minBudgetPercentRenew = 0.0;
                    var stepBudgetPercentRenew = 0.1;

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
