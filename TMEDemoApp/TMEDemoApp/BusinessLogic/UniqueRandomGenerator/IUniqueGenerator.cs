using System.Collections.Generic;

namespace TMEDemoApp
{
    public interface IUniqueGenerator
    {
        int From { get; set; }
        int To { get; set; }
        List<int> GenerateRandomUniqueNumbers(IUsedNumbersProvider usedNumbersProvider, int count, bool cache);
    }
}