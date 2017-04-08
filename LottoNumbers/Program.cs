using System;
using System.Collections.Generic;

namespace LottoNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var lotto = new Lotto(8);
            lotto.Play(42, 6);
            Console.ReadLine();
        }
    }
}