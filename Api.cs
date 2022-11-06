using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CompetitiveCoders.com_Considition2022.responses;
//using CompetitiveCoders.com_Considition2022.Responses;
using Newtonsoft.Json;


namespace CompetitiveCoders.com_Considition2022
{
    public class Api
    {
        private const string BasePath = "https://api.considition.com/api/game/";
        private readonly HttpClient _client;
        private const string ApiKey = "";
        public Api()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _client = new HttpClient(clientHandler) { BaseAddress = new Uri(BasePath) };
            _client.DefaultRequestHeaders.Add("x-api-key", ApiKey);
        }

        public async Task<GameResponse> MapInfo(string mapName)
        {

            var data = new StringContent(mapName, Encoding.UTF8, "application/json");
            var response = await _client.GetAsync("mapInfo?MapName=" + mapName);

            var result = await response.Content.ReadAsStringAsync();

            GlobalConfig.LogToFile($"mapInfo-{mapName}.json", result);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Exception:" + result);
                Console.WriteLine();
                Console.WriteLine("Fatal Error: could not retrieve the map");
                Environment.Exit(1);
            }

            return JsonConvert.DeserializeObject<GameResponse>(result);
        }


        public async Task<SubmitResponse> SubmitGame(string solution, string mapName)
        {
            HttpResponseMessage response;

            lock (GlobalConfig.api_call_lock) //sorry
            {
                var data = new StringContent(solution, Encoding.UTF8, "application/json");
                Console.WriteLine($"[{DateTime.Now.ToShortTimeString}] Calling api...");
                response = _client.PostAsync("submit", data).Result;
                Thread.Sleep(100); // Hopefully nice enough for the server
            }
                
            var result = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("Exception:" + result);
                    Console.WriteLine();
                    Console.WriteLine("Fatal Error: could not submit the game");
                    Environment.Exit(1);
                }
            
                var deserialized = JsonConvert.DeserializeObject<SubmitResponse>(result);
                GlobalConfig.LogToFile($"submit-{deserialized.gameId}.json", result);
            

            return deserialized;
        }
    }
}