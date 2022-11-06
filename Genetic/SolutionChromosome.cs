using CompetitiveCoders.com_Considition2022.models;
using GeneticSharp;
using System;
using System.Linq;

namespace CompetitiveCoders.com_Considition2022.Genetic
{
    class SolutionChromosome : ChromosomeBase
    {
        public SolutionChromosome()
            : base(7)
        {
            CreateGenes();
        }

        public Solution ToSolution()
        {
            return new Solution()
            {
                recycleRefundChoice = (bool)GetGene(0).Value,
                bagPrice = (int)GetGene(1).Value,
                refundAmount = (int)Math.Floor((double)GetGene(2).Value * (int)GetGene(1).Value),
                bagType = (int)GetGene(3).Value,
                FirstDayBagsPerPerson = (double)GetGene(4).Value,
                NewBagsInterval = (int)GetGene(5).Value,
                RenewBagsPerPerson = (double)GetGene(6).Value,
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
                    var allowedPrices = new[] { 1, 10, 20, 50 };
                    return new Gene(allowedPrices[_rng.Next(allowedPrices.Length)]);
                case 2: //refundAmount
                    var percentages = new[] { 0, 10, 30, 60, 90 };
                    return new Gene(percentages[_rng.Next(percentages.Length)]);
                case 3: //bagType
                    var bagtypes = new[] { 1, 2, 3, 4, 5 };
                    return new Gene(bagtypes[_rng.Next(bagtypes.Length)]);
                case 4: //FirstDayBagsPerPerson
                    var maxFirstDayBags = 2.0;
                    var minFirstDayBags = 0.4;
                    var stepFirstDayBags = 0.2;

                    var choices = Enumerable.Range(0, 100)
                                            .Select(i => minFirstDayBags + (i * stepFirstDayBags))
                                            .TakeWhile(i => i <= maxFirstDayBags).ToList();

                    return new Gene(choices[_rng.Next(choices.Count())]);

                case 5: //NewBagsInterval
                    var maxNewBagsInterval = 7;
                    var minNewBagsInterval = 1;
                    return new Gene(_rng.Next(minNewBagsInterval, maxNewBagsInterval + 1));
                case 6: //RenewBagsPerPerson
                    var maxRenewBagsPerPerson = 2.0;
                    var minRenewBagsPerPerson = 0.5;
                    var stepRenewBagsPerPerson = 0.2;

                    var choicesRenewBagsPerPerson = Enumerable.Range(0, 100)
                                            .Select(i => minRenewBagsPerPerson + (i * stepRenewBagsPerPerson))
                                            .TakeWhile(i => i <= maxRenewBagsPerPerson).ToList();

                    return new Gene(choicesRenewBagsPerPerson[_rng.Next(choicesRenewBagsPerPerson.Count())]);
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
