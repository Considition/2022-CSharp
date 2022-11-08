using CompetitiveCoders.com_Considition2022.Genetic;
using CompetitiveCoders.com_Considition2022.models;
using GeneticSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml;

namespace CompetitiveCoders.com_Considition2022
{
    public static class Program
    {
        // The different map names can be found on considition.com/rules

        

        public static void Main(string[] args)
        {
            SetMapName();
            
            var results = new List<(int, SolutionChromosome)>();
            
            var needlePopMinSize = 8;
            var needlePopMaxSize = 16;
            var needleRuns = 15;


            GlobalConfig.BagType = 1;
            Console.WriteLine($" ** Testing bagtype {GlobalConfig.BagType}");

            results.Add((GlobalConfig.BagType, RunFitness(needlePopMinSize, needlePopMaxSize, needleRuns, GetBestPreviousRun())));

            GlobalConfig.BagType = 2;
            Console.WriteLine($" ** Testing bagtype {GlobalConfig.BagType}");
            results.Add((GlobalConfig.BagType, RunFitness(needlePopMinSize, needlePopMaxSize, needleRuns, GetBestPreviousRun())));

            GlobalConfig.BagType = 3;
            Console.WriteLine($" ** Testing bagtype {GlobalConfig.BagType}");
            results.Add((GlobalConfig.BagType, RunFitness(needlePopMinSize, needlePopMaxSize, needleRuns, GetBestPreviousRun())));

            GlobalConfig.BagType = 4;
            Console.WriteLine($" ** Testing bagtype {GlobalConfig.BagType}");
            results.Add((GlobalConfig.BagType, RunFitness(needlePopMinSize, needlePopMaxSize, needleRuns, GetBestPreviousRun())));

            GlobalConfig.BagType = 5;
            Console.WriteLine($" ** Testing bagtype {GlobalConfig.BagType}");
            results.Add((GlobalConfig.BagType, RunFitness(needlePopMinSize, needlePopMaxSize, needleRuns, GetBestPreviousRun())));

            foreach (var chromosome in results.OrderByDescending(r => r.Item2.Fitness))
            {
                Console.WriteLine($"Score: {chromosome.Item2.Fitness}  -  BagType: {chromosome.Item1}");
            }

            var bestResult = results.MaxBy(r => r.Item2.Fitness);
            GlobalConfig.BagType = bestResult.Item1;


            Console.WriteLine($" **** Focusing on bagtype {GlobalConfig.BagType} **** ");
            var bestSoFar = RunFitness(20, 40, 500, bestResult.Item2);



            //string Map = "Fancyville";
            //int bagType = 1;
             
            //RunGame(Map, true);

        }

        private static SolutionChromosome GetBestPreviousRun()
        {
            if (!File.Exists(GlobalConfig.ResultsCsvFilename))
            {
                return null;
            }

            int maxScore = int.MinValue;
            string bestLine = null;

            var lines = File.ReadAllLines(GlobalConfig.ResultsCsvFilename);
            foreach (var line in lines.Skip(1))
            {
                var ss = line.Split('\t');
                var score = int.Parse(ss[1]);
                if (score > maxScore)
                {
                    maxScore = score;
                    bestLine = line;
                }


            }

            SolutionChromosome best = new SolutionChromosome();
            best.SetGenesFromLogRow(bestLine);

            return best;


        }

        private static void SetMapName()
        {
            if (!File.Exists("mapname.txt"))
            {
                File.WriteAllText("mapname.txt", "Suburbia");
            }

            GlobalConfig.CurrentMap = File.ReadAllText("mapname.txt");
        }

        private static SolutionChromosome RunFitness(int populationMinSize, int populationMaxSize, int runs, SolutionChromosome firstChromosome = null)
        {
            var selection = new EliteSelection(2);
            //var selection = new RouletteWheelSelection();
            var crossover = new UniformCrossover();
            var mutation = new UniformMutation(true);

            var fitness = new SolutionFitnessFunction();
            firstChromosome ??= new SolutionChromosome();

            var population = new Population(populationMinSize, populationMaxSize, firstChromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new FitnessStagnationTermination(runs);
            ga.GenerationRan += (s, e) =>
                Console.WriteLine($"Generation {ga.GenerationsNumber}. Best fitness: {ga.BestChromosome.Fitness.Value}");

            Console.WriteLine("GA running...");
            ga.Start();

            Console.WriteLine();
            Console.WriteLine($"Best solution found has fitness: {ga.BestChromosome.Fitness}");
            Console.WriteLine($"Elapsed time: {ga.TimeEvolving}");

            return ga.BestChromosome as SolutionChromosome;
        }
    }
}
