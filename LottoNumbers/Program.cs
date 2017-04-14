using System;
using System.Collections.Generic;

namespace LottoNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var lotto = new Lotto(4, 5, 50, 2, 12, false);
            lotto.Play();
            Console.ReadLine();
        }
    }
}