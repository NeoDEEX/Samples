using NeoDEEX.Diagnostics;

namespace FI
{
    public class CommonService
    {
        private readonly IFoxLog _log = FoxLogManager.GetLogger<CommonService>();

        public void GetCommonData()
        {
            _log.Verbose("GetCommonData() method start...");

            //... 데이터 액세스 수행 ...
            _log.Verbose("Accessing data source...");

            _log.Verbose("GetCommonData() method end...");
        }
    }
}
