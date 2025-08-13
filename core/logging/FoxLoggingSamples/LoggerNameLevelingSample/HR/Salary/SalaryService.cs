namespace HR.Salary;

public class SalaryService : HRBase
{
    public void Calc(string userId)
    {
        DebugLog("Calc() method start... userId={0}", userId);

        //.... 복잡한 계산을 수행...
        DebugLog("Calculating salary of user, {0}...", userId);

        DebugLog("Calc() method end...");
    }
}
