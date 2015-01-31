using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    /// <summary>
    /// Generic Reverse comparer. Can be used with any type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ReverseComparer<T> : IComparer<T>
    {
        private readonly IComparer<T> _original;

        public ReverseComparer(IComparer<T> original)
        {
            _original = original;
        }

        public int Compare(T left, T right)
        {
            return _original.Compare(right, left);
        }
    }
}
