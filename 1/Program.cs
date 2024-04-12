using System.Text;

static void Action1()
{
    for (int i = 0; i <= 10; i++)
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} - {i}");
        Thread.Sleep(300);
    }
}

static void Action2()
{
    for (int i = 65; i < 75; i++)
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} - {(char)i}");
        Thread.Sleep(300);
    }
}


Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;


Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine("Задание 1 : 2 разных потока");
Console.ResetColor();

Thread myThread1 = new Thread(Action1);
Thread myThread2 = new Thread(Action2);
myThread1.Name = "Thread_1";
myThread2.Name = "Thread_2";

myThread1.Start();
myThread2.Start();

myThread1.Join();
myThread2.Join();

Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine("Работа программы завершена");
Console.ResetColor();