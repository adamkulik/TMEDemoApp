using Xunit;
using TMEDemoApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMEDemoApp.Tests
{
    public class SQLUsedNumbersProviderTests
    {
        [Fact()]
        public void GetUsedNumbersTest()
        {
            List<int> expected = new List<int> { 1 };

            SQLUsedNumbersProvider provider = new SQLUsedNumbersProvider(@"HAMSTA-PC\SQLEXPRESS", "TMEDemoDB", "usedNumbers");

            List<int> actual = provider.GetUsedNumbers();
            Assert.Equal(expected, actual);
        }

        [Fact()]
        public void SaveUsedNumbersTest()
        {
            List<int> expected = new List<int> { 1, 2, 3, 4 };
            SQLUsedNumbersProvider provider = new SQLUsedNumbersProvider(@"HAMSTA-PC\SQLEXPRESS", "TMEDemoDB", "usedNumbers");

            List<int> insertList = new List<int> { 2, 3, 4 };

            provider.SaveUsedNumbers(insertList);

            List<int> actual = provider.GetUsedNumbers();

            Assert.Equal(expected, actual);

            
        }
    }
}