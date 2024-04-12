using System.IO;
using System.Text;
string file1 = "file1.txt",
    file2 = "file2.txt",
    file3_result = "file3_result.txt",

    text1 = "some text for first file\n",
    text2 = "more text info for second file\n";


Semaphore semaphore = new Semaphore(1, 1);

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine("Задание 4 : запись в файл с разных потоков");
Console.ResetColor();
PrintGuide();

ConsoleKeyInfo key = new ConsoleKeyInfo();


do
{
    key = PressKey();

    switch (key.Key)
    {
        case ConsoleKey.NumPad1:
            {
                Press1();
                break;
            }
        case ConsoleKey.D1:
            {
                Press1();
                break;
            }

        case ConsoleKey.NumPad2:
            {
                Press2();
                break;
            }
        case ConsoleKey.D2:
            {
                Press2();
                break;
            }

        case ConsoleKey.NumPad3:
            {
                Press3();
                break;
            }
        case ConsoleKey.D3:
            {
                Press3();
                break;
            }

        case ConsoleKey.Escape:
            {
                PressEsc();
                break;
            }

        default: break;
    }
} while (true);


static ConsoleKeyInfo PressKey()
{
    int cursorLeft = Console.CursorLeft;
    ConsoleKeyInfo key = Console.ReadKey();
    Console.SetCursorPosition(cursorLeft, Console.CursorTop);
    Console.Write(" ");
    Console.SetCursorPosition(cursorLeft, Console.CursorTop);
    return key;
}

static void PrintGuide()
{
    const string Guide = "1 - Создать 2 текстовых файла\n2 - Считать файлы и записать в 3й файл\n3 - Удалить все файлы\nESC - выход\n";

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(Guide);
    Console.ResetColor();
}

void Press1()
{
    try
    {
        File.WriteAllText(file1, text1);

        File.WriteAllText(file2, text2);

        File.Create(file3_result).Close();

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("Файлы созданы. ");
        Console.ResetColor();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    finally
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Сделайте дальнейший выбор...\n");
        Console.ResetColor();
    }
}

void Press2()
{
    Thread thread1 = new Thread(() =>
    {
        semaphore.WaitOne();

        try
        {
            var t3 = File.OpenText(file1);

            if (File.Exists(file3_result))
                File.Delete(file3_result);

            File.AppendAllText(file3_result, t3.ReadToEnd(), System.Text.Encoding.UTF8);

            t3.Close();

            Console.Write("Текст из первого файла записан.\n");
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }

        semaphore.Release();

    });

    Thread thread2 = new Thread(() =>
    {
        semaphore.WaitOne();
        try
        {
            var t3 = File.OpenText(file2);

            if (File.Exists(file3_result))
                File.Delete(file3_result);

            File.AppendAllText(file3_result, t3.ReadToEnd(), System.Text.Encoding.UTF8);
               
            t3.Close();

            Console.Write("Текст из второго файла записан.\n");
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }

        semaphore.Release();

    });

    thread1.Start();
    thread2.Start();

    thread1.Join();
    thread2.Join();

    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("Сделайте дальнейший выбор...\n");
    Console.ResetColor();
}

void Press3()
{
    try
    {
        File.Delete(file1);

        File.Delete(file2);

        File.Delete(file3_result);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("Файлы удалены. ");
        Console.ResetColor();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    finally
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Сделайте дальнейший выбор...\n");
        Console.ResetColor();
    }
}

void PressEsc()
{

    Console.SetCursorPosition(0, Console.CursorTop);
    Console.WriteLine("1");
    Console.SetCursorPosition(0, Console.CursorTop - 1);

    Press3();
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    string bye = "Файлы удалены, работа приложения завершена...";
    for (int i = 0; i < bye.Length; i++)
    {
        Console.Write(bye[i]);
        Thread.Sleep(14);
    }
    Console.ResetColor();
    Thread.Sleep(200);
    Console.WriteLine();
    Environment.Exit(0);
}