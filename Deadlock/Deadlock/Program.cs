using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deadlock
{
    class Program
    {
        static void Main(string[] args)
        { var p = new Program();
            p.ConcurrencyTest();
        }

        private class DataStore { public int Value { get; set; } }

        private DataStore store = new DataStore();
        private DataStore store2 = new DataStore();

        public void ConcurrencyTest()
        {
            var thread1 = new Thread(IncrementTheValue1);
            var thread2 = new Thread(IncrementTheValue2);

            thread1.Start();
            thread2.Start();

            thread1.Join(); // Wait for the thread to finish executing
            thread2.Join();

            Console.WriteLine($"Final value: {store.Value}");
        }

        public void IncrementTheValue1()
        {
            lock (store)
            {
                store.Value++;
                lock (store2)
                {
                    store2.Value++;
                }

            }
        }

        public void IncrementTheValue2()
        {
            lock (store2)
            {
                store2.Value++;
                lock (store)
                {
                    store.Value++;
                }

            }
        }
    }
}
