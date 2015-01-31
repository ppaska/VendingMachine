## VendingMachine
A simple algorithm that reflects how a vending machine works when giving back change.

## Acceptance Criteria
- Give back change largest first
- Return error indication if no sufficient funds available
- we should be able to add new denomination easily
- ability to load coins

## Change Making Problem
This is very simple but efficient algorithm to calculate the change.

```cs
  private void CalculateChange(int amount, SortedList<int, int> availableCoins, 
                                  SortedList<int, int> changeDictionary, ref int index)
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
```

## Usage
```cs
  var coinBox = new CoinsBox();
  
  coinBox.RegisterDenomination(50, 10, 1, 2, 5);
  
  coinBox.LoadCoins(50, 100);
  coinBox.LoadCoins(5, 100);
  coinBox.LoadCoins(10, 100);
  coinBox.LoadCoins(2, 100);
  coinBox.LoadCoins(1, 100);

  SortedList<int, int> change;
  int amount = 257;

  Console.WriteLine("Calculation change for={0} result Success={1}", 
                      amount, coinBox.TryGetChange(amount, out change));

  foreach (var coin in change)
  {
      Console.WriteLine("{0} -> {1}", coin.Key, coin.Value);
  }

```
