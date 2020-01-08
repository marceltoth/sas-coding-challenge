using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionLine.CodingChallenge
{
    public class PropertyCounter<TCounterType>
    {
        protected Dictionary<TCounterType, int> _countMap;

        public PropertyCounter(IList<TCounterType> properties)
        {
            Reset(properties);
        }

        public void Reset(IList<TCounterType> properties)
        {
            _countMap = new Dictionary<TCounterType, int>();

            foreach (var item in properties)
            {
                _countMap[item] = 0;
            }
        }

        public void Add(TCounterType property)
        {
            if (_countMap.ContainsKey(property))
            {
                _countMap[property]++;
            }
            else
            {
                _countMap[property] = 1;
            }
        }
    }
}