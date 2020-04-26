using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMEDemoApp
{
    public class RandomUniqueGenerator : IUniqueGenerator
    {
        private double _smallPercentageOffset = 0.07;
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
        public HashSet<int> GenerateRandomUniqueNumbers(IUsedNumbersProvider usedNumbersProvider, int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("count");

            double countPercent =  (double)count / (double)(To - From);
            if (countPercent < _smallPercentageOffset)
            {
                return SmallPercentageGenerateMethod(usedNumbersProvider, count);
            }
            else
            {
                return BigPercentageGenerateMethod(usedNumbersProvider, count);
            }

        }

        private HashSet<int> BigPercentageGenerateMethod(IUsedNumbersProvider usedNumbersProvider, int count)
        {
            var allNumbers = Enumerable.Range(From, (To - From) + 1);
            var unusedNumbers = allNumbers.Except(usedNumbersProvider.GetUsedNumbers());
            var returnedNumbers = unusedNumbers.ToList().TakeRandom(count);
            usedNumbersProvider.SaveUsedNumbers(returnedNumbers);
            return new HashSet<int>(returnedNumbers);
        }

        private HashSet<int> SmallPercentageGenerateMethod(IUsedNumbersProvider usedNumbersProvider, int count)
        {
            HashSet<int> usedNumbers = usedNumbersProvider.GetUsedNumbers();
            HashSet<int> returnedNumbers = new HashSet<int>();
            Random rnd = new Random();
            int added = 0;
            int currCandidate = 0;
            do
            {
                currCandidate = rnd.Next(From, To);
                if (!usedNumbers.Contains(currCandidate))
                {
                    returnedNumbers.Add(currCandidate);
                    usedNumbers.Add(currCandidate);
                    added++;
                }


            } while (added != count);
            usedNumbersProvider.SaveUsedNumbers(returnedNumbers);
            return returnedNumbers;
        }
    }
}
