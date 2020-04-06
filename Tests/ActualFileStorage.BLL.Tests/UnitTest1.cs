using ActualFileStorage.BLL.Services;
using System;
using System.Linq;
using Xunit;
using Moq;
using ActualFileStorage.BLL.Hashers;
using ActualFileStorage.BLL.Services.Interfaces;

namespace ActualFileStorage.BLL.Tests
{
    public class ServiceFixture : IDisposable
    {
        IHash _hash;
        public ServiceFixture()
        {
            _hash = new SHA256Hash();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
    public class UnitTest1 : IClassFixture<ServiceFixture>
    {


        [Fact]
        public void TwoPlusThreeEqualFive()
        {
            
            IProfileService ps = new ProfileService(null,null,null,null,null,null,null);
            int actual = ps.AddFolder(2, 2, "55");
            int notExpected = 0;
            Assert.NotEqual(notExpected, actual);
        }
    }
}
