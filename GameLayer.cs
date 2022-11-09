using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompetitiveCoders.com_Considition2022.responses;
//using CompetitiveCoders.com_Considition2022.Responses;
using Newtonsoft.Json;

namespace CompetitiveCoders.com_Considition2022
{
    public class GameLayer
    {
        private readonly Api _api;

        public GameLayer()
        {
            _api = new Api();
        }

        ///  <summary> Creates a new game.</summary>
        ///  <param name="mapName">map choice </param>
        public GameResponse MapInfo(string mapName)
        {
            if (GlobalConfig.GameResponseMemo.ContainsKey(mapName))
            {
                return GlobalConfig.GameResponseMemo[mapName];
            }

            var state = _api.MapInfo(mapName).Result;
            
            GlobalConfig.GameResponseMemo.Add(mapName, state);
            
            return state;
        }

        ///  <summary> Submits your solution for validation and scoring.</summary>
        /// <param name="solution"> Your solution</param>
        ///  <param name="mapName">map choice </param>
        public SubmitResponse Submit(string solution, string mapName)
        {
            var state = _api.SubmitGame(solution, mapName).Result;
            return state;
        }
    }
}
