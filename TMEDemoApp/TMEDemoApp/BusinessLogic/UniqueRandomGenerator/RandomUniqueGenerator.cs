using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMEDemoApp
{
    public class RandomUniqueGenerator : IUniqueGenerator
    {

        public int From { get; set; }
        public int To { get; set; }
        public RandomUniqueGenerator(int from, int to)
        {
            if (from > to)
                throw new ArgumentOutOfRangeException("from");
            From = from;
            To = to;
        }


        #region AutoDoc
        /// <summary>
        /// Generate list of random numbers in range [from, to]. 
        /// </summary>
        /// <param name="usedNumbersProvider"> Object implementing IUsedNumbersProvider that will return numbers to exclude.</param>
        /// <param name="count">Number of random numbers to generate.</param>
        /// <param name="shouldSave">If set to false</param>
        /// <returns>List of random numbers, each in range [from, to].</returns>
        #endregion
        public List<int> GenerateRandomUniqueNumbers(IUsedNumbersProvider usedNumbersProvider, int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("count");

            IEnumerable<int> allNumbers = Enumerable.Range(From, (To - From) + 1);
            var unusedNumbers = allNumbers.Except(usedNumbersProvider.GetUsedNumbers()).ToList();
            List<int> returnedNumbers = unusedNumbers.TakeRandom(count);
            usedNumbersProvider.SaveUsedNumbers(returnedNumbers);
            return returnedNumbers;
        }
    }
}
