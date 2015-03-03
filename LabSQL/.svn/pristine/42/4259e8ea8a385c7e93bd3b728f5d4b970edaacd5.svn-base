using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace L14_Threading_Examples
{
    class Basic_Mutual_Exclusion
    {
        static void Main(string[] args)
        {
         //   example_1();
            example_2();

            Console.Read();
        }



        /// <summary>
        ///  Print the elapsed time from a stopwatch object
        /// </summary>
        /// <param name="timer"> Should have been started and stopped</param>
        private static void print_elapsed_time(Stopwatch timer)
        {
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = timer.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsed_time = String.Format("{0:00}:{1:000}",
                ts.Seconds, ts.Milliseconds );
            Console.WriteLine("RunTime " + elapsed_time);
        }

        /// <summary>
        /// Example spinner for data protection
        /// </summary>
        static bool example_1_done;

        /// <summary>
        ///  Run the same function twice in separate threads
        ///  NOTE: the current program is running in a thread
        /// </summary>
        private static void example_1()
        {
            example_1_done = false;
            new Thread(example_1_work).Start();
            example_1_work();

        }

        private static void example_1_work()
        {
            if (!example_1_done)
            {
                Console.WriteLine("Done");
                example_1_done = true;
            }
        }

      /// <summary>
        ///   Show race condition: two threads counting
        ///   Note: race means --> two entities (threads) are "racing" to see the first one
        ///                        to modify object, but we don't know which order it will actually happen
        ///                        and this can cause great problems
        /// </summary>
        static void example_2()
        {
            int shared_counter = 0;
            ThreadStart work_delegate_function_1 = new ThreadStart(() => count_up( ref shared_counter ));
            ThreadStart work_delegate_function_2 = new ThreadStart(() => count_down( ref shared_counter ));
            Thread worker1 = new Thread(work_delegate_function_1);
            Thread worker2 = new Thread(work_delegate_function_2);

            Stopwatch timer = new Stopwatch();

            timer.Start();
            worker1.Start();
            worker2.Start();

            worker1.Join();
            worker2.Join();
            timer.Stop();

            print_elapsed_time(timer);

            Console.WriteLine("Example 2 done, counter = " + shared_counter);

        }

        /// <summary>
        ///   count upward for 1000000 counts
        /// </summary>
        /// <param name="counter"> increases by 1000000 </param>
        /// <returns></returns>
        private static void count_up(ref int counter)
        {
            for (int i = 0; i < 1000000; i++)
            {
                counter++;
            }
        }
        /// <summary>
        ///   count down for 1000000 counts
        /// </summary>
        /// <param name="counter"> increases by 1000000 </param>
        /// <returns></returns>
        private static void count_down(ref int counter)
        {
            for (int i = 0; i < 1000000; i++)
            {
                counter--;
            }
        }

    }
}
