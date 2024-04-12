using System.Text;


Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;


Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine("Задание 3 : Parallel ForEach");
Console.ResetColor();

List<int> ints = new List<int>();

for (int i = 1; i < 101; i++)
{
    ints.Add(i);
}

Parallel.ForEach<int>(ints, Pow2);

Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine("Работа программы завершена");
Console.ResetColor();

static void Pow2(int i)
{
    Console.WriteLine($"Task_ID {Task.CurrentId} : Квадрат числа {i} равен {Math.Pow(i, 2)}");
    Thread.Sleep(2000);
}