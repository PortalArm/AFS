﻿using System;
using System.Linq;
using Xunit;

namespace ActualFileStorage.PL.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestMethod1()
        {

        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        //[InlineData(6)]
        public void MyFirstTheory(int value)
        {
            Assert.True(IsOdd(value));
        }

        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }

    }
}
