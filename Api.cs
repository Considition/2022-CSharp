﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DotNet.Responses;
using Newtonsoft.Json;


namespace DotNet
{
    public class Api
    {
        private const string BasePath = "https://api.considition.com/api/games/";
        private readonly HttpClient _client;

        public Api(string apiKey)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };            
            _client = new HttpClient (clientHandler){BaseAddress = new Uri(BasePath)};
            _client.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }

        public async Task<GameResponse> MapInfo(string mapName)
        {
    
            var data = new StringContent(mapName, Encoding.UTF8, "application/json");
            var response = await _client.GetAsync("mapInfo?MapName=" + mapName);

            var result = await response.Content.ReadAsStringAsync();
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
            var data = new StringContent(solution, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("submit?MapName=" + mapName, data);
            var result = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Exception:" + result);
                Console.WriteLine();
                Console.WriteLine("Fatal Error: could not submit the game");
                Environment.Exit(1);
            }

            return JsonConvert.DeserializeObject<SubmitResponse>(result);
        }
    }
}