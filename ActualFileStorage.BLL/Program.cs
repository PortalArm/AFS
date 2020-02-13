using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActualFileStorage.BLL.Passwords;
using ActualFileStorage.BLL.Salts;

namespace ActualFileStorage.BLL
{
    //interface IinterfaceA
    //{
    //    void PrintA();
    //}
    //interface IinterfaceB
    //{
    //    void PrintB();
    //}

    //class RRR : IinterfaceA, IinterfaceB
    //{
    //    public void PrintA()
    //    {
    //        Console.WriteLine("hello A");
    //    }

    //    public void PrintB()
    //    {
    //        Console.WriteLine("hello B");
    //    }
    //}
    class Program
    {
        //private static RRR rr = new RRR();
        //private static IinterfaceA intA;
        //private static IinterfaceB intB;

        //convert to dll
        static void Main(string[] args)
        {
            IPasswordHasher ph = new PasswordHasher();
            string d = ph.HashPass("aaaab", "bbbb");
            Console.WriteLine(d);

            //var sr = new SaltResolver();
            //HashSet<string> tt = new HashSet<string>();
            //for (int i = 0; i < 128000; ++i)
            //{
            //    string w = sr.GetSalt(128);
            //    if (tt.Contains(w))
            //        Console.WriteLine("Collision: {0}", w);
            //    else
            //        tt.Add(w);
            //}
            //Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
