using System.Collections.Generic;

namespace TMEDemoApp
{
    public interface IUsedNumbersProvider
    {
        List<int> GetUsedNumbers();
        int UsedNumbersCount { get; }
        void SaveUsedNumbers(List<int> usedNumbers);
    }
}