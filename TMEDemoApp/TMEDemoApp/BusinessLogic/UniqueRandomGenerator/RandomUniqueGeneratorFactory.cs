using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMEDemoApp.BusinessLogic.UsedNumberProviders;

namespace TMEDemoApp.BusinessLogic.UniqueRandomGenerator
{
    public static class RandomUniqueGeneratorFactory
    {
        public static RandomUniqueGenerator GetRandomUniqueGeneratorSQL(int from, int to)
        {
            return new RandomUniqueGenerator(from, to);
        }
    }
}
