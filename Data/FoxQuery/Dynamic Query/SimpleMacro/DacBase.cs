using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOne.Data;

namespace SimpleMacro
{
    internal class DacBase : IDisposable
    {
        private FoxDbAccess _dbAccess;

        private FoxDbAccess CreateDbInstance()
        {
            return FoxDatabaseFactory.CreateDatabase();
        }

        public void Dispose()
        {
            if (_dbAccess != null)
            {
                _dbAccess.Close();
            }
        }

        public FoxDbAccess DbAccess
        {
            get
            {
                if (_dbAccess == null)
                {
                    _dbAccess = CreateDbInstance();
                }
                return _dbAccess;
            }
        }
    }
}
