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
        var gameInformation = GameLayer.MapInfo(mapName);

        int days = mapName.Equals("Suburbia") || mapName.Equals("Fancyville") ? 31 : 365;

        var solver = new Solver();
        var solution = solver.AddOrders(candidate, gameInformation,days);

        
        

        var solutionResult = GameLayer.Submit(JsonSerializer.Serialize(solution), mapName);

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