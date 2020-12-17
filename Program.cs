using System;
using System.Collections.Generic;
using System.Threading;

namespace Task2_OS
{

    class Program
    {
        static Queue<int> queue = new Queue<int>(200);
        static Random rnd = new Random();
        static bool sleep1 = false;
        static bool sleep2 = true;

        static void generator()
        {
            while (!sleep1)
            {
                Thread.Sleep(500);
                try
                {
                }catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e);
                }
                int num = rnd.Next(1, 100);
                queue.Enqueue(num);
                Console.WriteLine("Added number: " + num);
            }
        }

        static void cleaner()
        {
            while (!sleep2 & queue.Count!=0)
            {
                Thread.Sleep(500);
                Console.WriteLine("Deleted number: " + queue.Dequeue());
                queue.Dequeue();
            }
        }

        static void Main(string[] args)
        {

            Thread gen1 = new Thread(generator);
            Thread gen2 = new Thread(generator);
            Thread gen3 = new Thread(generator);
            Thread cl1 = new Thread(cleaner);
            Thread cl2 = new Thread(cleaner);
            bool flag1 = false;

            Console.WriteLine("All genetator's started");
            gen1.Start();
            gen2.Start();
            gen3.Start();

            char fin = 'a';
            while (fin != 'q' & queue.Count < 200)
            {

                Thread.Sleep(500);
                Console.WriteLine(queue.Count);
                if (queue.Count >= 80 & !flag1)
                {
                    flag1 = true;
                    Console.WriteLine("All cleaners's started");
                    sleep2 = false;
                    cl1.Start();
                    cl2.Start();

                }
                if (queue.Count < 80 & sleep1)
                {
                    Console.WriteLine("All genetator's started");
                    try
                    {
                        gen1.Start();
                        gen2.Start();
                        gen3.Start();
                    } catch(ThreadStateException e)
                    {
                        Console.WriteLine(e);
                    }
                }
                if (queue.Count >= 100)
                {
                    Console.WriteLine("All gen's stoped");
                    sleep1 = true;
                    gen1.Abort();
                    gen2.Abort();
                    gen3.Abort();
                }
                //fin =Convert.ToChar(Console.Read());
            }
            Console.WriteLine("Let's do it!");
        }
    }
}
