using System.Text;
using System.Threading;

namespace Deadlock
{
    internal class Program
    {

        static object lock1 = new object();
        static object lock2 = new object();

        static void Main(string[] args)
        {

            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Задание 5* : избавиться от DeadLock");
            Console.ResetColor();

            var thread1 = new Thread(() =>
            {
                AcquireLocks1();
            });
            var thread2 = new Thread(() =>
            {
                AcquireLocks2();
            });

            thread1.Name = "Thread 1";
            thread2.Name = "Thread 2";

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Finished.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private static void AcquireLocks1()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;

            lock (lock1)
            {
                Console.WriteLine($"Thread {threadId} acquired lock 1.");
                Thread.Sleep(1000);
                Console.WriteLine($"Thread {threadId} attempted to acquire lock 2.");

                if (Monitor.TryEnter(lock2))
                {

                    Console.WriteLine($"Thread {threadId} acquired lock 2.");
                    Monitor.Exit(lock2);
                }
                else
                {
                    Console.WriteLine($"Thread {threadId} didn't acquire lock 2.");
                }
            }
            Thread.Sleep(400);
            Console.WriteLine($"Thread {threadId} release lock 1.");
        }

        private static void AcquireLocks2()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;

            lock (lock2)
            {
                Console.WriteLine($"Thread {threadId} acquired lock 2.");
                Thread.Sleep(1000);
                Console.WriteLine($"Thread {threadId} attempted to acquire lock 1.");
                if (Monitor.TryEnter(lock1))
                {

                    Console.WriteLine($"Thread {threadId} acquired lock 1.");
                    Monitor.Exit(lock1);
                }
                else
                {
                    Console.WriteLine($"Thread {threadId} didn't acquire lock 1.");
                }
            }
            Thread.Sleep(400);
            Console.WriteLine($"Thread {threadId} release lock 2.");
        }
    }
}