namespace HR.Etc;

public class EtcService : HRBase
{
    public void DoSomething()
    {
        DebugLog("DoSomething() method start...");

        //.... 복잡한 계산을 수행...
        DebugLog("Do something...");

        DebugLog("DoSomething() method end...");
    }
}
