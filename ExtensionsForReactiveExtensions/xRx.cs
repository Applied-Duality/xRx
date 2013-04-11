#region Apache 2 License
// Copyright (c) Applied Duality, Inc., All rights reserved.
// See License.txt in the project root for license information.
#endregion

using System.Reactive.Concurrency;

namespace System.Reactive.Linq
{
    public static class xRx
    {
        /// <summary>
        /// Convert scheduler to observable.
        /// </summary>
        public static IObservable<Unit> ToObservable(this IScheduler scheduler)
        {
            return Observable.Create<Unit>(observer =>
            {
                return scheduler.ScheduleAsync(async (_scheduler, token) =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        observer.OnNext(Unit.Default);
                        await _scheduler.Yield();
                    }
                });
            });
        }
    }
}
