using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActualFileStorage.BLL.FileHandlers;
using ActualFileStorage.BLL.Passwords;
using ActualFileStorage.BLL.Salts;
using ActualFileStorage.DAL.Adapters;

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

            //LocalServerStorage lss = new LocalServerStorage(AppDomain.CurrentDomain.BaseDirectory);
            //DAL.Models.User u = new DAL.Models.User() { Login = "HelloC" };
            //DAL.Models.Folder f = new DAL.Models.Folder() { Id = 55, Name = u.Login };
            //DAL.Models.File file = new DAL.Models.File() { Id = 2, Name="file1",Ext = ".txt", CreationTime = DateTime.Now, Hash = "43we56t4eredxy"};
            //u.Folder = f;

            //lss.UploadFile( u, file, new byte[] { 12, 13, 14, 15, 16, 17, 18, 19 } );
            //foreach(byte b in lss.DownloadFile(u, file))
            //    Console.WriteLine(b);
            //IPasswordHasher ph = new PasswordHasher();
            //string d = ph.HashPass("aaaab", "bbbb");
            //Console.WriteLine(d);

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
            //Console.ReadKey();
        }
    }
}
