using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Links
{
    public class CustomLink : ILinkBuilder
    {
        private int _key = 1368761; // prime
        public string Decode(string shortString)
        {
            throw new NotImplementedException();
        }

        public string Encode(string fullString)
        {
            throw new NotImplementedException();
        }
    }

    public class MockLink : ILinkBuilder
    {
        public string Decode(string shortString) => shortString.Substring(shortString.Length / 2);
        public string Encode(string fullString) => string.Concat(fullString, fullString);

    }

}
