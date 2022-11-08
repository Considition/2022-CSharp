using System;
using System.Text.Json;
using CompetitiveCoders.com_Considition2022.models;
using CompetitiveCoders.com_Considition2022.responses;

namespace CompetitiveCoders.com_Considition2022;

public static class GameRunner
{


    private static readonly GameLayer GameLayer = new();

    public static SubmitResponse RunGame(string mapName, Solution candidate, bool printResults)
    {
        //if (GlobalConfig.ScoringMemo.ContainsKey(candidate))
        //{
        //    Console.WriteLine("* Found solution in memo, skipping api call");

        //}

        var gameInformation = GameLayer.MapInfo(mapName);

        int days = mapName.Equals("Suburbia") || mapName.Equals("Fancyville") ? 31 : 365;

        var solver = new Solver();
        var solution = solver.AddOrders(candidate, gameInformation,days);

        
        
        

        SubmitResponse solutionResult;
        if (GlobalConfig.ScoringMemo.ContainsKey(candidate.HashValue))
        {
            Console.WriteLine("¤ Hit scoring cache!");
            solutionResult = GlobalConfig.ScoringMemo[candidate.HashValue];
        }
        else
        {
            solutionResult = GameLayer.Submit(JsonSerializer.Serialize(solution), mapName);
            GlobalConfig.ScoringMemo[candidate.HashValue] = solutionResult;
        }
        

        if (printResults)
        {
            Console.WriteLine("Your GameId is: " + solutionResult.gameId);
            Console.WriteLine("Your score is: " + solutionResult.score);
            Console.WriteLine("Your weekly results is: " + solutionResult.weeks);
            Console.WriteLine("Your daily results is: " + solutionResult.dailys);
            Console.WriteLine("Your amount of produced bags is: " + solutionResult.totalProducedBags);
            Console.WriteLine("Your amount of destroyed bags is: " + solutionResult.totalDestroyedBags);
            Console.WriteLine("Link to visualisation" + solutionResult.visualizer);
        }

        return solutionResult;
    }
}