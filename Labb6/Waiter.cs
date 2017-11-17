﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

//Servitrisen ska plocka upp alla tomma glas som finns på borden. Sedan diskar hon glasen och ställer dem i hyllan.
//Det tar tio sekunder att plocka glasen från borden och femton sekunder att diska dem.
//När alla besökare har gått så går servitrisen hem.

namespace Labb6
{
    public class Waiter
    {
        private Action<string> Callback;
        private BlockingCollection<Glass> DirtyGlassQueue;
        private BlockingCollection<Glass> CleanGlassQueue;
        private BlockingCollection<Patron> PatronQueue;
        BlockingCollection<Patron> PubQueue;

        public Func<bool> isBarOpen { get; set; }

        public Waiter(BlockingCollection<Glass> DirtyGlassQueue)
        {
            this.DirtyGlassQueue = DirtyGlassQueue;
        }

        public Waiter(BlockingCollection<Glass> DirtyGlassQueue, BlockingCollection<Glass> CleanGlassQueue)
        {
            this.DirtyGlassQueue = DirtyGlassQueue;
            this.CleanGlassQueue = CleanGlassQueue;
        }

        public Waiter()
        {
        }

        public void Work(Action<string> Callback, Action<string> printNumberOfCleanGlasses)
        {
            int numberOfGlasses = 21;

            while (isBarOpen())
            {
                while (DirtyGlassQueue.Count() > 0)
                {
                    printNumberOfCleanGlasses("Number of clean glasses: " + --numberOfGlasses);

                    DirtyGlassQueue.TryTake(out Glass g);
                    Thread.Sleep(3000);
                    Callback("The waiter picks up a dirty glass and washes it");
                    Thread.Sleep(3000);
                    Callback("The waiter places the clean glass back on the shelf.");
                    CleanGlassQueue.Add(new Glass());
                }
            }
            if (!isBarOpen())
                Callback("The waiter goes home.");
        }

    }
}
