using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMEDemoApp
{
    public static class ListShuffleExtension
    {
        #region AutoDoc
        /// <summary>
        /// Takes random [count] elements from list. Implementation of Fisher-Yates algorithm.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">List to take elements from.</param>
        /// <param name="count">Number of elements to take.</param>
        /// <returns>List with random [count] elements.</returns>
        #endregion
        public static List<T> TakeRandom<T>(this List<T> list, int count)
        {
            if (list.Count < count)
                count = list.Count;
            if (list.Count == count)
                return list;

            List<T> returnList = new List<T>();
            HashSet<int> usedIndexes = new HashSet<int>(); // to avoid modification of original list
            Random rnd = new Random();
            for(int i = 0; i<count; i++)
            {
                int randomIndex = -1;
                do
                {
                    randomIndex = rnd.Next(0, list.Count -1);
                } while (!usedIndexes.Add(randomIndex)); // if we cannot add an index, it was used already
                returnList.Add(list[randomIndex]);
            }
            return returnList;

        }
    }
}
