using System;
using System.Collections.Generic;
using System.Linq;

namespace LottoNumbers
{
    class Lotto
    {
        private readonly Random random = new Random();

        private readonly int _tips;
        private readonly bool _checkFrequency;

        private readonly int _numberCount;
        private readonly int _numberRange;
        private readonly int _numFrequency;

        private readonly int _additionalCount;
        private readonly int _additionalRange;
        private readonly int _additionalFrequency;

        private List<Tuple<SortedSet<int>, SortedSet<int>>> _lotto;

        public Lotto(int tips, int numberCount, int numberRange, int additionalCount, int additionalRange, bool checkFrequency)
        {
            _tips = tips;
            _checkFrequency = checkFrequency;

            _numberCount = numberCount;
            _numberRange = numberRange;
            _numFrequency = ((tips * numberCount) / numberRange) + 1;

            _additionalCount = additionalCount;
            _additionalRange = additionalRange;
            _additionalFrequency = ((tips * additionalCount) / additionalRange) + 1;

            _lotto = new List<Tuple<SortedSet<int>, SortedSet<int>>>(_tips);

            Console.WriteLine("|------------------------|");
            Console.WriteLine("| Lotto Number Generator |");
            Console.WriteLine("|  "+ numberCount +" of "+ numberRange +" with "+ additionalCount +" of "+ additionalRange +"   |");
            Console.WriteLine("|------------------------|");

        }

        public void Play()
        {
            for (int i = 0; i < _tips; i++)
            {
                var numbers = new SortedSet<int>();
                var additionals = new SortedSet<int>();

                while (numbers.Count < _numberCount)
                {
                    var number = random.Next(1, _numberRange);
                    // first check if number is containing in actual list
                    if (numbers.Contains(number)) continue;
                    // check if number is containing in other lists not more as specified amount
                    if (NumberFrequencyToHeigh(number) && _checkFrequency) continue;
                    numbers.Add(number);
                }

                while (additionals.Count < _additionalCount)
                {
                    var additional = random.Next(1, _additionalRange);
                    // first check if number is containing in actual list
                    if (additionals.Contains(additional)) continue;
                    // check if number is containing in other lists not more as specified amount
                    if (AdditionalFrequencyToHeigh(additional) && _checkFrequency) continue;
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
            if (CountNumbers(allNumbers, number) >= _numFrequency) return true;
            return false;
        }

        private bool AdditionalFrequencyToHeigh(int additional)
        {
            var allAdditionals = _lotto.Select(item => item.Item2);
            if (CountNumbers(allAdditionals, additional) >= _additionalFrequency) return true;
            return false;
        }

        private int CountNumbers(IEnumerable<SortedSet<int>> sets, int value)
        {
            return sets.SelectMany(x => x).Count(v => v == value);
        }

    }
}
