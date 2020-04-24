using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMEDemoApp
{
    public class RandomUniqueGenerator
    {
        private IUsedNumbersProvider _usedNumbersProvider;
        public RandomUniqueGenerator(IUsedNumbersProvider newUsedNumbersProvider)
        {
            _usedNumbersProvider = newUsedNumbersProvider;
        }
        #region AutoDoc
        /// <summary>
        /// Generate list of random numbers in range [from, to]. 
        /// </summary>
        /// <param name="from">Start of range for random numbers.</param>
        /// <param name="to">End of range for random numbers.</param>
        /// <param name="count">Number of random numbers to generate.</param>
        /// <returns></returns>
        #endregion
        public List<int> GenerateRandomUniqueNumbers(int from, int to, int count)
        {
            IEnumerable<int> allNumbers = Enumerable.Range(from, (to - from) + 1);
            var unusedNumbers = allNumbers.Except(_usedNumbersProvider.GetUsedNumbers());
            var rnd = new Random();
            List<int> returnedNumbers = unusedNumbers.OrderBy(x => rnd.Next()).Take(count).ToList();
            return returnedNumbers;
        }
    }
}
