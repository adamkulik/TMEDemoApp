using System.Collections.Generic;

namespace TMEDemoApp
{
    public interface IUsedNumbersProvider
    {
        HashSet<int> GetUsedNumbers();
        bool CachedMode { get; set; }
        int UsedNumbersCount { get; }
        void SaveUsedNumbers(IEnumerable<int> usedNumbers);
        void SyncCache();
    }
}