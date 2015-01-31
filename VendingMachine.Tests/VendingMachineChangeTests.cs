using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace VendingMachine.Tests
{

    [TestFixture]
    public class VendingMachineChangeTests
    {
        [Test]
        public void GiveChangeTest104()
        {
            var coinBox = new CoinsBox();

            coinBox.RegisterDenomination(50, 10, 2, 5, 1);

            coinBox.LoadCoins(50, 100);
            coinBox.LoadCoins(5, 100);
            coinBox.LoadCoins(10, 100);
            coinBox.LoadCoins(2, 100);
            coinBox.LoadCoins(1, 100);

            SortedList<int, int> change;

            Assert.IsTrue(coinBox.TryGetChange(104, out change));
            Assert.AreEqual(2, change.Count);
            Assert.AreEqual(2, change[50]);
            Assert.AreEqual(2, change[2]);
        }

        [Test]
        public void GiveChangeTest104WithOnlyOne10()
        {
            var coinBox = new CoinsBox();

            coinBox.RegisterDenomination(50, 10, 2, 5, 1);

            coinBox.LoadCoins(50, 1);
            coinBox.LoadCoins(5, 100);
            coinBox.LoadCoins(10, 100);
            coinBox.LoadCoins(2, 100);
            coinBox.LoadCoins(1, 100);

            SortedList<int, int> change;

            Assert.IsTrue(coinBox.TryGetChange(104, out change));
            Assert.AreEqual(3, change.Count);
            Assert.AreEqual(1, change[50]);
            Assert.AreEqual(5, change[10]);
            Assert.AreEqual(2, change[2]);
        }
        [Test]
        public void GiveChangeTest23()
        {
            var coinBox = new CoinsBox();

            coinBox.RegisterDenomination(50, 10, 2, 5, 1);

            coinBox.LoadCoins(50, 100);
            coinBox.LoadCoins(5, 100);
            coinBox.LoadCoins(10, 100);
            coinBox.LoadCoins(2, 100);
            coinBox.LoadCoins(1, 100);

            SortedList<int, int> change;

            Assert.IsTrue(coinBox.TryGetChange(23, out change));
            Assert.AreEqual(3, change.Count);
            Assert.AreEqual(2, change[10]);
            Assert.AreEqual(1, change[2]);
            Assert.AreEqual(1, change[1]);
        }

        [Test]
        public void WrongDenominationsSetTest()
        {
            var coinBox = new CoinsBox();

            coinBox.RegisterDenomination(50, 10, 5);

            coinBox.LoadCoins(50, 100);
            coinBox.LoadCoins(5, 100);
            coinBox.LoadCoins(10, 100);

            SortedList<int, int> change;

            Assert.IsFalse(coinBox.TryGetChange(104, out change));
            Assert.AreEqual(0, change.Count);
        }

        [Test]
        public void ChangeIsNotAvailableTest()
        {
            var coinBox = new CoinsBox();

            coinBox.RegisterDenomination(50, 10, 5, 2, 1);

            coinBox.LoadCoins(50, 10);
            coinBox.LoadCoins(5, 10);
            coinBox.LoadCoins(10, 10);

            SortedList<int, int> change;

            Assert.IsFalse(coinBox.TryGetChange(1040, out change));
            Assert.AreEqual(0, change.Count);
        }

        [Test]
        public void NotEnoughCoinsTest()
        {
            var coinBox = new CoinsBox();

            coinBox.RegisterDenomination(50, 10, 5, 2, 1);

            coinBox.LoadCoins(50, 10);
            coinBox.LoadCoins(5, 10);
            coinBox.LoadCoins(10, 10);
            coinBox.LoadCoins(2, 10);
            coinBox.LoadCoins(1, 0);

            SortedList<int, int> change;

            Assert.IsFalse(coinBox.TryGetChange(1040, out change));
            Assert.AreEqual(0, change.Count);
        }

    }
}
