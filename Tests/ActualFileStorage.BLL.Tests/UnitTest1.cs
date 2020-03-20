using System;
using System.Linq;
using Xunit;

namespace ActualFileStorage.BLL.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TwoPlusThreeEqualFour()
        {
            int a = 2;
            int b = 3;
            int expected = 5;

            int actual = a + b;

            Assert.Equal(expected, actual);
        }
    }
}
