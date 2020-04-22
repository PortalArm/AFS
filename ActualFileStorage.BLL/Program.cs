using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActualFileStorage.BLL.FileHandlers;
using ActualFileStorage.BLL.Passwords;
using ActualFileStorage.BLL.Salts;
using ActualFileStorage.DAL.Adapters;
using ActualFileStorage.DAL.Models;

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
            //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(conn);
            //string dd = System.Convert.ToBase64String(plainTextBytes);
            //System.IO.File.WriteAllText(@"C:\Users\Tom\Desktop\testfiles\user3\yo.txt", dd);
            //AzureFileStorage afs = new AzureFileStorage();
            //User u = new User() {
            //    Login = "privet",
            //    Folder = new Folder() {
            //        Name = "privet"
            //    }
            //};
            //File f = new File() {
            //    Hash = "5465yrthfdrsertrdhtf"
            //};
            //afs.UploadFile(u, f, new byte[] { 2, 199, 27, 43, 8, 26, 3, 43, 6, 6, 55, 45, 4, 65, 6, 56, 45, 3, 5, 67, 6, 31, 45, 45, 76, 78, 99, 63, 4, 55, 6 });

            //var data = afs.DownloadFileAsStream(u, f);
            //var output = new byte[data.Length];
            //data.Read(output, 0, (int)data.Length);
            //Console.WriteLine(string.Join(" ", output));
            //Console.ReadKey();
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
