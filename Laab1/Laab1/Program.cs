using System;
using System.Threading;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Main Thread is starting.");

        // Завдання 2: Багатопотокова програма з різними пріоритетами
        List<MyThread> threads = new List<MyThread>
        {
            new MyThread("Thread 1", ThreadPriority.Normal),
            new MyThread("Thread 2", ThreadPriority.BelowNormal),
            new MyThread("Thread 3", ThreadPriority.AboveNormal),
            new MyThread("Thread 4", ThreadPriority.Highest),
            new MyThread("Thread 5", ThreadPriority.Lowest),
            new MyThread("Thread 6", ThreadPriority.Normal)
        };

        // Сортуємо потоки за пріоритетом (від найвищого до найнижчого)
        threads.Sort((a, b) => b.Thrd.Priority.CompareTo(a.Thrd.Priority));

        // Запуск всіх потоків у правильному порядку
        foreach (var thread in threads)
        {
            thread.Thrd.Start();
            Thread.Sleep(100); // Дати фору потоку з вищим пріоритетом
        }

        // Очікування завершення потоків
        foreach (var thread in threads)
        {
            thread.Thrd.Join();
        }

        // Виведення результатів
        Console.WriteLine("\nThread execution results:");
        foreach (var thread in threads)
        {
            Console.WriteLine($"{thread.Thrd.Name} counted to {thread.Count}");
        }

        // Завдання 3: Динамічне задання потоків
        Console.WriteLine("\nEnter number of threads:");
        int numThreads = int.Parse(Console.ReadLine());
        List<MyThread> dynamicThreads = new List<MyThread>();

        for (int i = 0; i < numThreads; i++)
        {
            Console.WriteLine($"Enter priority for Thread {i + 1} (Lowest, BelowNormal, Normal, AboveNormal, Highest):");
            ThreadPriority priority = Enum.Parse<ThreadPriority>(Console.ReadLine(), true);
            dynamicThreads.Add(new MyThread($"DynamicThread {i + 1}", priority));
        }

        // Сортуємо динамічні потоки за пріоритетом перед запуском
        dynamicThreads.Sort((a, b) => b.Thrd.Priority.CompareTo(a.Thrd.Priority));

        foreach (var thread in dynamicThreads)
        {
            thread.Thrd.Start();
            Thread.Sleep(100); // Фора для високопріоритетних потоків
        }

        foreach (var thread in dynamicThreads)
        {
            thread.Thrd.Join();
        }

        Console.WriteLine("\nDynamic thread execution results:");
        foreach (var thread in dynamicThreads)
        {
            Console.WriteLine($"{thread.Thrd.Name} counted to {thread.Count}");
        }

        Console.WriteLine("Main() is done.");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

class MyThread
{
    public int Count;
    public Thread Thrd;

    public MyThread(string name, ThreadPriority priority)
    {
        Count = 0;
        Thrd = new Thread(Run) { Name = name, Priority = priority };
    }

    void Run()
    {
        Console.WriteLine($"{Thrd.Name} started with priority {Thrd.Priority}.");
        do
        {
            Count++;
        } while (Count < 1_000_000);
        Console.WriteLine($"{Thrd.Name} completed.");
    }
}
