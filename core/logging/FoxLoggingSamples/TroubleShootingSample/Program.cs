// See https://aka.ms/new-console-template for more information
using NeoDEEX.Diagnostics;

Console.WriteLine("Fox Logging Trouble Shooting Sample...");
DoLogging();

static void DoLogging()
{
    Console.WriteLine("Press Any Key to Stop...");
    try
    {
        bool exitFlag = false;
        Task task = Task.Run(async () =>
        {
            while (exitFlag == false)
            {
                var service = new HR.Salary.SalaryService();
                service.Calc("TestUser");
                await Task.Delay(1000);
            }
        });
        Console.ReadKey(true);
        exitFlag = true;
        task.Wait();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error! : {ex}");
    }
}