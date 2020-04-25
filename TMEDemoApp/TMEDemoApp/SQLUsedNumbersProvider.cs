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
        private List<int> _usedNumbers;
        public int UsedNumbersCount
        {
            get
            {
                return _usedNumbers.Count;
            }
        }
        public SQLUsedNumbersProvider(string newServerName, string newDbName)
        {
            _serverName = newServerName;
            _dbName = newDbName;
            GetUsedNumbers();
             
        }
       public List<int> GetUsedNumbers()
        {
            string connectionString = @"Server=" //appending a string generally should be done using StringBuilder, but for small cases + is faster
                                    + _serverName 
                                    + @";Database=" 
                                    + _dbName 
                                    + ";Integrated Security = true;";
            List<int> returnList = new List<int>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM usedNumbers",connection))
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

        public void SaveUsedNumbers(List<int> usedNumbers)
        {
            string connectionString = @"Server=" //appending a string generally should be done using StringBuilder, but for small cases + is faster
                                     + _serverName
                                     + @";Database="
                                     + _dbName
                                     + ";Integrated Security = true;";
            List<int> returnList = new List<int>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var table = new DataTable();
                table.Columns.Add("Value", typeof(int));

                for (var i = 0; i < usedNumbers.Count; i++)
                {
                    table.Rows.Add(usedNumbers[i]);
                }

                using (var bulk = new SqlBulkCopy(connection))
                {
                    bulk.DestinationTableName = "usedNumbers"; //TODO: Refactor table name
                    bulk.WriteToServer(table);
                }
            }
    }
}
