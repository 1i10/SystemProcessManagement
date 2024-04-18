using System.Diagnostics;
using System.ComponentModel;

class Program
{
    /// <summary>
    /// Основной метод программы. Отображает меню и обрабатывает выбор пользователя.
    /// </summary>
    static void Main()
    {
        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowProcesses();
                    break;
                case "2":
                    Console.Write("Введите ID процесса: ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int processId))
                    {
                        KillProcess(processId);
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод. Попробуйте еще раз.");
                    }
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                    break;
            }
        }
    }

    /// <summary>
    /// Отображает пользовательское меню.
    /// </summary>
    static void DisplayMenu()
    {
        Console.WriteLine("--------- Меню ------------");
        Console.WriteLine("1. Показать все процессы");
        Console.WriteLine("2. Завершить процесс по ID");
        Console.WriteLine("3. Выход");
        Console.WriteLine("---------------------------");
        Console.Write("Выберите действие: ");
    }

    /// <summary>
    /// Отображает список всех процессов в системе.
    /// </summary>
    static void ShowProcesses()
    {
        foreach (var process in Process.GetProcesses())
        {
            Console.WriteLine($"ID: {process.Id}, Имя: {process.ProcessName}");
        }
    }

    /// <summary>
    /// Завершает процесс по указанному ID.
    /// </summary>
    /// <param name="processId">ID процесса, который необходимо завершить.</param>
    static void KillProcess(int processId)
    {
        try
        {
            Process process = Process.GetProcessById(processId);
            process.Kill(true);

            Console.WriteLine($"Процесс с ID {processId} был завершен.");
        }
        catch (ArgumentException)
        {
            Console.WriteLine($"Процесс с ID {processId} не найден.");
        }
        catch (Win32Exception ex)
        {
            Console.WriteLine($"Процесс с ID {processId} не может быть завершен. Ошибка: {ex.Message}. Код ошибки: {ex.ErrorCode}.");
        }
        catch (NotSupportedException)
        {
            Console.WriteLine($"Вы пытаетесь вызвать Kill() для процесса, который выполняется на удаленном компьютере. " +
                $"Метод доступен только для процессов, запущенных на локальном компьютере.");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine($"Процесс с ID {processId} уже завершен.");
        }
        catch (AggregateException)
        {
            Console.WriteLine($"Не все процессы в дереве процессов, связанных с процессом с ID {processId}, могут быть завершены.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Недостаточно прав для завершения процесса с ID {processId}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}

