#region Apache 2 License
// Copyright (c) Applied Duality, Inc., All rights reserved.
// See License.txt in the project root for license information.
#endregion

using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace xRx
{
    class Program
    {
        static void Main(string[] args)
        {
            var xs = Observable.Defer(delegate
            {
                var i = 0;
                return from _ in ThreadPoolScheduler.Instance.ToObservable()
                       select i++;
            });

            var dispose = xs.Subscribe(Console.WriteLine);

            Console.WriteLine("Press any key to unsubscribe ...");
            Console.ReadKey();

            dispose.Dispose();

            Console.ReadKey();
            Console.WriteLine("Bye!");
        }
    }
}
