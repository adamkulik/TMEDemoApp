using Xunit;
using TMEDemoApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace TMEDemoApp.Tests
{
    public class RandomUniqueGeneratorTests
    {
        [Fact()]
        public void GenerateRandomUniqueNumbersTest()
        {
            Mock<IUsedNumbersProvider> usedNumbersMock = new Mock<IUsedNumbersProvider>();
            usedNumbersMock.Setup(x => x.GetUsedNumbers()).Returns(new List<int> { 2, 3, 4, 5 });
            List<int> expected = new List<int> { 6, 7, 8, 9 };

            RandomUniqueGenerator generator = new RandomUniqueGenerator(usedNumbersMock.Object);
            List<int> actual = generator.GenerateRandomUniqueNumbers(2, 9, 4);

            Assert.True(expected.OrderBy(x => x).SequenceEqual(actual.OrderBy(x => x)));
        }
    }
}