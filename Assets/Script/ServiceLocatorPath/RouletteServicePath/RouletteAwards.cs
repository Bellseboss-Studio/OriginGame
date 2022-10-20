using System.Collections.Generic;
using UnityEngine;

namespace SystemOfExtras
{
    public class RouletteAwards : IRouletteService
    {
        private readonly List<int> _results;
        public RouletteAwards()
        {
            _results = new List<int>()
            {
                0,
                0,
                0,
                0,
                0,
                1,
                1,
                1,
                1,
                2,
                2,
                2,
                4,
                4,
                8
            };
        }
        
        public int GetResult()
        {
            var result = Random.Range(0, _results.Count);
            return _results[result];
        }
    }
}