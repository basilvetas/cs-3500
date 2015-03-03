using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

    // Demonstration of two threads accessing shared memory

public class Threads1 
{

    // Launches the demo
    public static void Mainx()
    {
        new Threads1().demo();
    }

    // The threads modify this variable
    private int count = 0;

    // Runs the demo
    public void demo()
    {

        // Fire off two threads
        Task task1 = new Task(modify);
        Task task2 = new Task(modify);

        task1.Start();
        task2.Start();

        Stopwatch watch = new Stopwatch();
        watch.Start();

        // As long as one of the threads is running, give
        // updates on the value of count
        while (!task1.IsCompleted || !task2.IsCompleted)
        {
            Console.WriteLine("count = " + count);
            Thread.Sleep(10);
        }

        watch.Stop();
        Console.WriteLine("Took: " + watch.ElapsedMilliseconds + " milliseconds");

        // Display the final value of count.  If the threads are well-behaved,
        // it will be zero.
        Console.WriteLine("Final value of count = " + count);
        Console.Read();

    }

    private readonly object myLock = new object();
    // Runs a long loop that increments and then decrements the count.
    public void modify()
    {
        for (int i = 0; i < int.MaxValue / 20; i++)
        {
            lock(myLock)  // lock is an atomic instruction at hardware level
            {
                count++;
            }
            lock(myLock) // thing we don't want operating at the same time should have the same lock
            {
                count--;
            }
            
        }
    }

}