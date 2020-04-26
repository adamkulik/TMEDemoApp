using System.Collections.Generic;

namespace TMEDemoApp
{
    public interface IUsedNumbersProvider
    {
        List<int> GetUsedNumbers();
        bool CachedMode { get; set; }
        int UsedNumbersCount { get; }
        void SaveUsedNumbers(List<int> usedNumbers);
        void SyncCache();
    }
}