#region Licence and Terms
// Accord.NET Extensions Framework
// https://github.com/dajuric/accord-net-extensions
//
// Copyright © Darko Jurić, 2014-2015 
// darko.juric2@gmail.com
//
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU Lesser General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU Lesser General Public License for more details.
// 
//   You should have received a copy of the GNU Lesser General Public License
//   along with this program.  If not, see <https://www.gnu.org/licenses/lgpl.txt>.
//
#endregion

using System;
using Accord.Extensions;
using System.Collections.Concurrent;

namespace Parallel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Parallel array processor example:"); Console.ResetColor();
            TestParallelProcessor.Test();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Parallel 'While' example:"); Console.ResetColor();
            testParallelWhile();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Parallel random example:"); Console.ResetColor();
            testParallelRandom();
        }

        static void testParallelWhile()
        {
            //adds support for parallel while (missing in .NET)
            //WATCH OUT: there is a chance that the number of iterations are larger than it should be
                        //that is because threads are invoked in parallel and (NUM_THREDS-1) threads are looking the old state of the provided condition

            const int MAX_ITER = 1;
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
   
            ParallelExtensions.While(() => bag.Count < MAX_ITER, (loopState) => 
            //System.Threading.Tasks.Parallel.For(0, MAX_ITER, (_) => 
            {
                bag.Add(0);
                Console.WriteLine("Count: {0}", bag.Count);
            });

            Console.WriteLine("Final count: {0}", bag.Count);
        }

        static void testParallelRandom()
        {
            //System.Radnom class can not generate numbers in parallel. Solution: ParallelRandom<> and ParalellRandom 
            //parallel random creates few instances of Random class and initializes them with seed generated by RNGCryptoServiceProvieder random generator class.

            Console.WriteLine("Generating random numbers in parallel:");
            System.Threading.Tasks.Parallel.For(0, 100, (i) => 
            {
                Console.Write(ParallelRandom.Next() + " ");
            });

            //try generic class also (enables parallel rand generation for any random generator)
            //ParallelRandom<Random>.Initialize((seed) => new Random(seed));
            //ParallelRandom<Random>.Local.Next();

            Console.WriteLine();
        }
    }
}
