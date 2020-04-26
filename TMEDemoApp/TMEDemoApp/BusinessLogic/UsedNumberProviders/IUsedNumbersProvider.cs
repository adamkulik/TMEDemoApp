using System.Collections.Generic;

namespace TMEDemoApp
{
    public interface IUsedNumbersProvider
    {
        List<int> GetUsedNumbers(bool cachedVersion);
        int UsedNumbersCount { get; }
        void SaveUsedNumbers(List<int> usedNumbers, bool justCache);
        void SyncCache();
    }
}