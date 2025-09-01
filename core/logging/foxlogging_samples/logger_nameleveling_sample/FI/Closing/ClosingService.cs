using NeoDEEX.Diagnostics;

namespace FI.Closing
{
    public class ClosingService
    {
        private readonly IFoxLog _log = FoxLogManager.GetLogger<ClosingService>();

        public void DoClosing()
        {
            _log.Verbose("DoClosing() method start...");

            //.... 복잡한 계산을 수행...
            _log.Verbose("Closing calculation...");

            _log.Verbose("DoClosing() method end...");
        }
    }
}
