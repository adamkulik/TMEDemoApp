using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMEDemoApp
{
   public class SQLUsedNumbersProvider : IUsedNumbersProvider
   {
        private string _serverName;
        private string _dbName;
        private string _tableName;
        private string connectionString;
        private List<int> _usedNumbers;
        private List<int> _cachedNumbers;
        public int UsedNumbersCount
        {
            get
            {
                return _usedNumbers.Count;
            }
        }

        public SQLUsedNumbersProvider(string newServerName, string newDbName, string newTableName)
        {
            _serverName = newServerName;
            _dbName = newDbName;
            _tableName = newTableName;
            connectionString = @"Server=" + _serverName //appending a string generally should be done using StringBuilder, but for small cases + is faster
                        + @";Database=" + _dbName
                        + ";Integrated Security = true;";
            GetUsedNumbers();
            _cachedNumbers = new List<int>();
             
        }

        public List<int> GetUsedNumbers(bool cachedVersion = false)
        {
            if (cachedVersion)
                return _usedNumbers;
            List<int> returnList = new List<int>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM " + _tableName ,connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                                returnList.Add(reader.GetInt32(0));
                        }
                    }
                }
                connection.Close();
            }
            _usedNumbers = returnList;
            return returnList;
        }

        public void SaveUsedNumbers(List<int> usedNumbers, bool justCache = false)
        {
            if (!justCache)
            {
                SaveToDb(usedNumbers);
            }
            _usedNumbers.AddRange(usedNumbers);
            if(justCache)
            _cachedNumbers.AddRange(usedNumbers);
        }

        private void SaveToDb(List<int> usedNumbers)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var table = new DataTable();
                table.Columns.Add("Value", typeof(int));

                for (var i = 0; i < usedNumbers.Count; i++)
                {
                    table.Rows.Add(usedNumbers[i]);
                }

                using (var bulk = new SqlBulkCopy(connection))
                {
                    bulk.DestinationTableName = _tableName;
                    bulk.WriteToServer(table);
                }
                connection.Close();
            }
        }

        public void SyncCache()
        {
            SaveToDb(_cachedNumbers);
            _cachedNumbers = new List<int>();

        }
   }
}
