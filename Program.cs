
using System;
using System.Linq;
using System.Text.Json;
using System.Xml;

namespace DotNet
{
    public static class Program
    {
        private const string ApiKey = "ee373ab2-a8fb-4db9-4802-08da97ce1ad5";  // TODO: Enter your API key
        // The different map names can be found on considition.com/rules
        private const string Map = "Suburbia";     // TODO: Enter your desired map
        private static int bagType = 1;  // TODO: Enter your desired bag type

        private static readonly GameLayer GameLayer = new(ApiKey);
        
        public static void Main(string[] args)
        {
            var gameInformation = GameLayer.MapInfo(Map);

            int days = (Map.Equals("Suburbia") || Map.Equals("Fancyville")) ? 31 : 365;

            Solver solver = new Solver(gameInformation.population, gameInformation.companyBudget, days);
            var solution = solver.Solve(bagType, Map);
            var submitSolution = GameLayer.Submit(JsonSerializer.Serialize(solution), Map);
           
            Console.WriteLine("Your GameId is: " + submitSolution.GameId);
            Console.WriteLine("Your score is: " + submitSolution.Score);
            Console.WriteLine("Your weekly results is: " + submitSolution.Weeks);
            Console.WriteLine("Your amount of produced bags is: " + submitSolution.ProducedBags);
            Console.WriteLine("Your amount of destroyed bags is: " + submitSolution.DestroyedBags);
            Console.WriteLine("Link to visualisation" + submitSolution.Link);
    
        }
    }
}
