using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActualFileStorage.DAL.Configurations;
using ActualFileStorage.DAL.Models;
using ActualFileStorage.DAL.Adapters;
using System.Data.Common;
using System.Data.SqlClient;

namespace ActualFileStorage.DAL
{
    class Program
    {
        //convert to dll
        static void Main(string[] args)
        {
            //var conn = System.Configuration.ConfigurationManager.ConnectionStrings["FileStorageDB"].ConnectionString;
            //Console.WriteLine(conn);
            //_ = new ADOAdapter(new SqlConnection(conn));
            //Console.ReadKey();
            using (FileStorageContext context = new FileStorageContext())
            {
                IAdapter efa = new EFAdapter(context).LoadType(typeof(User));
                IEnumerable<User> data = (IEnumerable<User>) efa.FindAll();
                foreach (User k in data)
                    Console.WriteLine($"{k.Login} {k.Folder.Id}");
                //var usr = (User)efa.Find(4);
                //Console.WriteLine($"{usr.Id} {usr.Login} {usr.FirstName} {usr.Folder.User.Id}");
                //efa.Remove(usr);
                //Console.WriteLine($"{usr.Id} removed!");
                //User newUser = new User() { BirthDate = DateTime.Now, Email = "newemail2@gmail.com", FirstName = "Gosha222", Login = "goshae14ee", PassHash = "2364363ydfg" };
                //efa.Add(newUser); 
                //efa.LoadType(typeof(Folder)).Add(new Folder()
//{ Name = newUser.Login, User = newUser, Visibility = FileAccess.Public, CreationTime = DateTime.Now });
                var t = context.SaveChanges();
                Console.WriteLine(t + " records.");
                
            }
            Console.ReadKey();
            //using (FileStorageContext context = new FileStorageContext())
            //{

            //    User u = new User() {
            //        Login = "portalarm",
            //        BirthDate = new DateTime(1998, 12, 10),
            //        FirstName = "Yevgeniy",
            //        SecondName = "Kim",
            //        Email = "evgenii_kim2010@mail.ru",
            //        PassHash = "33344444"
            //    };
            //    Folder f = new Folder() {
            //        CreationTime = DateTime.Now,
            //        Visibility = FileAccess.Public,
            //        User = u,
            //        Name = u.Login
            //    };
            //    //LoginInfo li = new LoginInfo() { UserId = u.Id, Email = "evgenii_kim2010@mail.ru", PassHash = "33443344" };
            //    u.Folder = f;
            //    //context.Folders.Add(f);
            //    //context.LoginInfos.Add(li);
            //    context.Users.Add(u);
            //    context.Folders.Add(f);

            //    context.SaveChanges();
            //}
        }
    }
}
