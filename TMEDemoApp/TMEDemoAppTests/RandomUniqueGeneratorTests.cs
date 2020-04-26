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
            usedNumbersMock.Setup(x => x.GetUsedNumbers()).Returns(Enumerable.Range(2,4).ToList());
            List<int> expected = new List<int> { 6, 7, 8, 9 };

            RandomUniqueGenerator generator = new RandomUniqueGenerator(2,9);
            List<int> actual = generator.GenerateRandomUniqueNumbers(usedNumbersMock.Object,4);

            Assert.True(expected.OrderBy(x => x).SequenceEqual(actual.OrderBy(x => x)));
        }
        [Fact()]
        public void GenerateRandomUniqueNumbers_ShouldThrowFrom()
        {
            Mock<IUsedNumbersProvider> usedNumbersMock = new Mock<IUsedNumbersProvider>();
            RandomUniqueGenerator generator;

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => generator =new RandomUniqueGenerator(4,3));

            Assert.Equal("from", exception.ParamName);
        }
        [Fact()]
        public void GenerateRandomUniqueNumbers_ShouldThrowCount()
        {
            Mock<IUsedNumbersProvider> usedNumbersMock = new Mock<IUsedNumbersProvider>();
            RandomUniqueGenerator generator = new RandomUniqueGenerator(3,5);

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => generator.GenerateRandomUniqueNumbers(usedNumbersMock.Object,-1));

            Assert.Equal("count", exception.ParamName);

        }
        
    }
}