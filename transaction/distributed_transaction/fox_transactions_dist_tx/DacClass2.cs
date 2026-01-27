using common;
using NeoDEEX.Data;

namespace fox_transactions_dist_tx;

public class DacClass2 : DacClassBase
{
    protected override FoxDbAccess CreateDbInstance()
    {
        return FoxDbAccess.CreateDbAccess(TestUtils.TestDB2);
    }
}
