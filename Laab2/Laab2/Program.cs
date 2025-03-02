using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WriteLine("Main Thread is starting.");

        // Завдання 1: Використання Task.Run для паралельного виконання
        Task tsk1 = Task.Run(() => MyTask(1));
        Task tsk2 = Task.Run(() => MyTask(2));

        // Завдання 2: Очікування виконання задач
        Task.WaitAll(tsk1, tsk2);
        Console.WriteLine("Both tasks completed.");

        // Завдання 3: Використання лямбда-виразу з параметром
        Task tsk3 = Task.Run(() =>
        {
            Console.WriteLine("Lambda task is started.");
            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(600);
                Console.WriteLine($"Lambda task counter = {count}");
            }
            Console.WriteLine("Lambda task is done.");
        });
        tsk3.Wait();

        // Завдання 4: Паралельні обчислення за допомогою Invoke() з лямбда-виразами
        Parallel.Invoke(
            () => {
                Console.WriteLine("Processing data...");
                Thread.Sleep(700);
                Console.WriteLine("Data processing completed.");
            },
            () => {
                Console.WriteLine("Calculating values...");
                Thread.Sleep(900);
                Console.WriteLine("Value calculation completed.");
            }
        );

        Console.WriteLine("Main() is done.");

        // Затримка перед закриттям програми
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static void MyTask(int id)
    {
        Console.WriteLine($"Task {id} started.");
        for (int count = 0; count < 5; count++)
        {
            Thread.Sleep(600 * id);
            Console.WriteLine($"Task {id} counter = {count}");
        }
        Console.WriteLine($"Task {id} is done.");
    }
}