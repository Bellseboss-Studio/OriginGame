using System.Collections.Generic;
using UnityEngine;

namespace SystemOfExtras
{
    public class RouletteAwards : IRouletteService
    {
        private readonly List<int> _results;
        private int _countOfTries;
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
            var result = Random.Range(_countOfTries <= 0 ? 0 : 6, _results.Count);
            _countOfTries++;
            return _results[result];
        }

        public void ResetResult()
        {
            _countOfTries = 0;
        }
    }
}