using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    /// <summary>
    /// CoinsBox is responsible for all money operations 
    /// </summary>
    public class CoinsBox
    {
        private readonly SortedList<int, int> _availableCoins = new SortedList<int, int>(new ReverseComparer<int>(Comparer<int>.Default));

        /// <summary>
        /// Register denominations what CoinBox supports
        /// </summary>
        /// <param name="denominations"></param>
        public void RegisterDenomination(params int[] denominations)
        {
            foreach (var denomination in denominations)
            {
                _availableCoins.Add(denomination, 0);
            }
        }

        /// <summary>
        /// Load cash to the machine
        /// </summary>
        /// <param name="denomination">denomination</param>
        /// <param name="count">count</param>
        public void LoadCoins(int denomination, int count)
        {
            if (!_availableCoins.ContainsKey(denomination))
            {
                throw new NotSupportedException(string.Format("Denomination {0} is not supported.", denomination));
            }

            _availableCoins[denomination] += count;
        }

        /// <summary>
        /// Calculates change for a given amount
        /// </summary>
        /// <param name="amount">amount needed</param>
        /// <param name="changeDictionary">Returns coins pair (Denomination and count)</param>
        /// <returns>Indicates success of operation</returns>
        public bool TryGetChange(int amount, out SortedList<int, int> changeDictionary)
        {
            changeDictionary = new SortedList<int, int>(new ReverseComparer<int>(Comparer<int>.Default));

            // clone available coins collection
            var availableCoins = new SortedList<int, int>(_availableCoins,
                                                          new ReverseComparer<int>(Comparer<int>.Default));
            int index = 0;

            // do calculations
            CalculateChange(amount, availableCoins, changeDictionary, ref index);

            if (index != int.MaxValue) // we can't give change
            {
                changeDictionary.Clear(); // make sure nobody use it
                return false;
            }

            // apply changes to original coins collection
            foreach (var coin in changeDictionary)
                _availableCoins[coin.Key] -= coin.Value;

            return true;            
        }

        private void CalculateChange(int amount, SortedList<int, int> availableCoins, SortedList<int, int> changeDictionary, ref int index)
        {
            while (index < availableCoins.Count)
            {
                var denomination = availableCoins.ElementAt(index++).Key;

                // try another denomination if current is bigger
                if (amount < denomination) continue;

                // count needed vs count available
                int count = Math.Min(amount / denomination, availableCoins[denomination]);

                changeDictionary.Add(denomination, count);
                availableCoins[denomination] -= count;

                int remainder = amount - (count * denomination);

                if (remainder == 0)
                {
                    index = int.MaxValue; // make sure it stopped
                    return;
                }

                // continue further calculations
                CalculateChange(remainder, availableCoins, changeDictionary, ref index);
            }
        }
    }
}
