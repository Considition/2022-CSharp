using GeneticSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using CompetitiveCoders.com_Considition2022.models;
using CompetitiveCoders.com_Considition2022.responses;

namespace CompetitiveCoders.com_Considition2022.Genetic
{
    public class SolutionFitnessFunction : IFitness
    {
        public double Evaluate(IChromosome chromosome)
        {
            var candidateChromosome = chromosome as SolutionChromosome;

            var solutionCandidate = candidateChromosome.ToSolution();

            solutionCandidate.mapName = GlobalConfig.CurrentMap;
            solutionCandidate.bagType = GlobalConfig.BagType;


            SubmitResponse result = null;
            int tries = 0;
            while (result == null)
            {
                tries++;

                try
                {
                    result = GameRunner.RunGame(GlobalConfig.CurrentMap, solutionCandidate, false);
                }
                catch 
                {
                    var backoffTime = 2000+100*tries;
                    Console.WriteLine($" !! Retrying in {backoffTime}");
                    Thread.Sleep(backoffTime); //backoff
                    result = null;
                    if (tries > 50)
                    {
                        throw;
                    }
                }

                
            }

            ////Test
            //var result = new SubmitResponse()
            //{
            //    dailys = new List<WeekModel>(),
            //    gameId = "Test",
            //    score = new Random().Next(-100, 100),
            //    totalProducedBags = 10,
            //    totalDestroyedBags = 9,
            //    valid = true,
            //    visualizer = "test",
            //    weeks = new List<WeekModel>(),

            //};

            LogResultToFile(result, solutionCandidate);

            return result.score;

        }

        private static void LogResultToFile(SubmitResponse result, Solution solutionCandidate)
        {
            var logresult =
                $"{result.gameId}\t{result.score}\t{solutionCandidate.bagType}\t{solutionCandidate.recycleRefundChoice}\t{solutionCandidate.bagPrice}\t{solutionCandidate.refundAmountPercent}\t{solutionCandidate.FirstDayBagsPerPerson}\t{solutionCandidate.NewBagsInterval}\t{solutionCandidate.RenewBagsPerPerson}\t{solutionCandidate.BudgetPercentStart}\t{solutionCandidate.BudgetPercentRenew}\t{result.totalProducedBags}\t{result.totalDestroyedBags}";
            var logFile = GlobalConfig.ResultsCsvFilename;

            if (!File.Exists(logFile))
            {
                File.AppendAllLines(logFile, new []{ "{result.gameId}\t{result.score}\t{solutionCandidate.bagType}\t{solutionCandidate.recycleRefundChoice}\t{solutionCandidate.bagPrice}\t{solutionCandidate.refundAmountPercent}\t{solutionCandidate.FirstDayBagsPerPerson}\t{solutionCandidate.NewBagsInterval}\t{solutionCandidate.RenewBagsPerPerson}\t{solutionCandidate.BudgetPercentStart}\t{solutionCandidate.BudgetPercentRenew}\t{result.totalProducedBags}\t{result.totalDestroyedBags}" });
            }
            
            File.AppendAllLines(logFile, new[] {logresult});
            
        }
    }
}
