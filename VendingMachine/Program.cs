using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            var coinBox = new CoinsBox();

            coinBox.RegisterDenomination(50, 10, 1, 2, 5);

            coinBox.LoadCoins(50, 100);
            coinBox.LoadCoins(5, 100);
            coinBox.LoadCoins(10, 100);
            coinBox.LoadCoins(2, 100);
            coinBox.LoadCoins(1, 100);

            SortedList<int, int> change;
            int amount = 257;

            Console.WriteLine("Calculation change for={0} result Success={1}", amount, coinBox.TryGetChange(amount, out change));

            foreach (var coin in change)
            {
                Console.WriteLine("{0} -> {1}", coin.Key, coin.Value);
            }
        }
    }
}
