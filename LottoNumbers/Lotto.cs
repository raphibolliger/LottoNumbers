using System;
using System.Collections.Generic;
using System.Linq;

namespace LottoNumbers
{
    class Lotto
    {
        private readonly Random random = new Random();
        private int _tips;
        private List<Tuple<SortedSet<int>, SortedSet<int>>> _lotto;

        public Lotto(int tips)
        {
            _tips = tips;
            _lotto = new List<Tuple<SortedSet<int>, SortedSet<int>>>(_tips);
        }

        public void Play(int numberCount, int additionalCount)
        {
            for (int i = 0; i < _tips; i++)
            {
                var numbers = new SortedSet<int>();
                var additionals = new SortedSet<int>();

                while (numbers.Count < 6)
                {
                    var number = random.Next(1, numberCount);
                    // first check if number is containing in actual list
                    if (numbers.Contains(number)) continue;
                    // check if number is containing in other lists not more as specified amount
                    if (NumberFrequencyToHeigh(number)) continue;
                    numbers.Add(number);
                }

                while (additionals.Count < 1)
                {
                    var additional = random.Next(1, additionalCount);
                    // first check if number is containing in actual list
                    if (additionals.Contains(additional)) continue;
                    // check if number is containing in other lists not more as specified amount
                    if (AdditionalFrequencyToHeigh(additional)) continue;
                    additionals.Add(additional);
                }

                Tuple<SortedSet<int>, SortedSet<int>> lotto = new Tuple<SortedSet<int>, SortedSet<int>>(numbers, additionals);
                _lotto.Add(lotto);
            }

            foreach (var lotto in _lotto)
            {
                Print(lotto);
            }
        }

        private void Print(Tuple<SortedSet<int>, SortedSet<int>> lotto)
        {
            Console.Write("[");
            Console.Write(string.Join(",", lotto.Item1));
            Console.Write("] ");

            Console.Write("[");
            Console.Write(string.Join(",", lotto.Item2));
            Console.WriteLine("]");
        }

        private bool NumberFrequencyToHeigh(int number)
        {
            var allNumbers = _lotto.Select(item => item.Item1);
            if (CountNumbers(allNumbers, number) >= 2) return true;
            return false;
        }

        private bool AdditionalFrequencyToHeigh(int additional)
        {
            var allAdditionals = _lotto.Select(item => item.Item2);
            if (CountNumbers(allAdditionals, additional) >= 2) return true;
            return false;
        }

        private int CountNumbers(IEnumerable<SortedSet<int>> sets, int number)
        {
            return sets.SelectMany(x => x).Count(v => v == number);
        }

    }
}
