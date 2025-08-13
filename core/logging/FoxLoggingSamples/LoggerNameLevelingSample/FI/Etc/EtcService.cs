using NeoDEEX.Diagnostics;

namespace FI.Etc
{
    public class EtcService
    {
        private readonly IFoxLog _log = FoxLogManager.GetLogger<EtcService>();

        public void DoSomething()
        {
            _log.Verbose("DoSomething() method start...");

            //.... 복잡한 계산을 수행...
            _log.Verbose("Do something...");

            _log.Verbose("DoSomething() method end...");
        }
    }
}
