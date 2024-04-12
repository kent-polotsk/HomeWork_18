using System.Text;


Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;


Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine("Задание 2 : Mutex");
Console.ResetColor();

int i = 0;
Mutex mutex = new Mutex();

Thread thread1 = new Thread(Increment);
Thread thread2 = new Thread(Increment);

thread1.Name = "Thread1";
thread2.Name = "Thread2";

thread1.Start();
thread2.Start();

thread1.Join();
thread2.Join();

Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine("Работа программы завершена");
Console.ResetColor();

void Increment()
{
    for (int j = 0; j < 50; j++)
    {
        mutex.WaitOne();
        Console.WriteLine($"{Thread.CurrentThread.Name} : i = {++i}");
        Thread.Sleep(10);
        mutex.ReleaseMutex();
    }
}