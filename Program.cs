using CompetitiveCoders.com_Considition2022.Genetic;
using CompetitiveCoders.com_Considition2022.models;
using GeneticSharp;
using System;
using System.Linq;
using System.Text.Json;
using System.Xml;

namespace CompetitiveCoders.com_Considition2022
{
    public static class Program
    {
        // The different map names can be found on considition.com/rules

        private static readonly GameLayer GameLayer = new();

        public static void Main(string[] args)
        {
            var selection = new EliteSelection();
            var crossover = new UniformCrossover();
            var mutation = new UniformMutation(true);

            var fitness = new SolutionFitnessFunction();
            var firstChromosome = new SolutionChromosome();

            var population = new Population(10, 20, firstChromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new FitnessStagnationTermination(100);
            ga.GenerationRan += (s, e) => Console.WriteLine($"Generation {ga.GenerationsNumber}. Best fitness: {ga.BestChromosome.Fitness.Value}");

            Console.WriteLine("GA running...");
            ga.Start();

            Console.WriteLine();
            Console.WriteLine($"Best solution found has fitness: {ga.BestChromosome.Fitness}");
            Console.WriteLine($"Elapsed time: {ga.TimeEvolving}");




            //string Map = "Fancyville";
            //int bagType = 1;

            //RunGame(Map, bagType);

        }

        private static int RunGame(string mapName)
        {
            var gameInformation = GameLayer.MapInfo(mapName);

            int days = mapName.Equals("Suburbia") || mapName.Equals("Fancyville") ? 31 : 365;


            var solutionCandidate = new Solution();
            solutionCandidate.recycleRefundChoice = true;
            solutionCandidate.bagPrice = 10;
            solutionCandidate.refundAmount = 1;
            solutionCandidate.bagType = 1;
            solutionCandidate.mapName = mapName;

            var solver = new Solver();
            var solution = solver.AddOrders(solutionCandidate, gameInformation,days);

            var solutionResult = GameLayer.Submit(JsonSerializer.Serialize(solution), mapName);

            Console.WriteLine("Your GameId is: " + solutionResult.gameId);
            Console.WriteLine("Your score is: " + solutionResult.score);
            Console.WriteLine("Your weekly results is: " + solutionResult.weeks);
            Console.WriteLine("Your daily results is: " + solutionResult.dailys);
            Console.WriteLine("Your amount of produced bags is: " + solutionResult.totalProducedBags);
            Console.WriteLine("Your amount of destroyed bags is: " + solutionResult.totalDestroyedBags);
            Console.WriteLine("Link to visualisation" + solutionResult.visualizer);

            return solutionResult.score;
        }
    }
}
