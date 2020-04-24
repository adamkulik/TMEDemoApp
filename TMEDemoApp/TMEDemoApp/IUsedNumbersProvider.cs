using System.Collections.Generic;

namespace TMEDemoApp
{
    public interface IUsedNumbersProvider
    {
        List<int> GetUsedNumbers();
    }
}