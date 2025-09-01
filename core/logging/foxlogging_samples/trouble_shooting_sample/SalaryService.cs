using NeoDEEX.Diagnostics;

namespace HR.Salary;

public class SalaryService
{
    private readonly IFoxLog _log = FoxLogManager.GetLogger<SalaryService>();

    public void Calc(string userId)
    {
        _log.VerboseFormat("Calc() method start... userId={0}", userId);

        //.... 복잡한 계산을 수행...
        _log.InformationFormat("Calculating salary of user, {0}...", userId);

        _log.Verbose("Calc() method end...");
    }
}
