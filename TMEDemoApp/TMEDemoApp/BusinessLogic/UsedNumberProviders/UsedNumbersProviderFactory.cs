using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMEDemoApp.BusinessLogic.UsedNumberProviders
{
    public static class UsedNumbersProviderFactory
    {
        public static SQLUsedNumbersProvider GetSQLUsedNumbersProvider(string sqlServerName, string sqlDbName, string sqlTableName)
        {
            return new SQLUsedNumbersProvider(sqlServerName, sqlDbName, sqlTableName);
        }
    }
}
