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
using ActualFileStorage.DAL.Repositories;
using ActualFileStorage.DAL.UOW;

namespace ActualFileStorage.DAL
{
    class Program
    {
        //convert to dll
        static void Main(string[] args)
        {
            FileStorageContext context = new FileStorageContext();

            using (BasicUOW uow = new BasicUOW(new EFAdapter(context)))
            {
                var cntx1 = uow.GetRepo<User>(); 
                var cntx2 = uow.GetRepo<Folder>();
                var cred = (cntx1 as IUserRepository).GetUserCredsById(1);
                User newUser = new User() { BirthDate = DateTime.Now, Email="simpleemail@mail.ru", FirstName="helkl", Login="reeoodd", PassHash="54756", Salt="364ydfcxhtf" };
                Folder newFolder = new Folder() {  CreationTime=DateTime.Now, Name=newUser.Login, User = newUser, Visibility = FileAccess.Private, ShortLink="shortedcustomlink" };
                cntx1.Add(newUser);
                cntx2.Add(newFolder);
                Console.WriteLine("Creds: {0}, {1}, {2}", cred.Login, cred.Email, cred.PassHash);
                foreach (var val in cntx2.GetAll())
                    Console.WriteLine(val.Name + " " + val.User.FirstName);
            }

            using (BasicUOW uow = new BasicUOW(new EFAdapter(context)))
            {
                var cntx1 = uow.GetRepo<User>();
                var cntx2 = uow.GetRepo<Folder>();

                var cred = (cntx1 as IUserRepository).GetUserCredsById(1);
                Console.WriteLine("Creds: {0}, {1}, {2}", cred.Login, cred.Email, cred.PassHash);
                foreach (var val in cntx2.GetAll())
                    Console.WriteLine(val.Name + " " + val.User.FirstName);
            }

            //using (var context = new FileStorageContext())
            //{
            //    var cntx1 = RepoFactory.Get<User>(new EFAdapter(context));
            //    var cntx2 = RepoFactory.Get<Folder>(new EFAdapter(context));

            //    var cred = (cntx1 as IUserRepository).GetUserCredsById(1);
            //    Console.WriteLine("Creds: {0}, {1}, {2}", cred.Login, cred.Email, cred.PassHash);
            //    foreach (var val in cntx2.GetAll())
            //        Console.WriteLine(val.Name + " " + val.User.FirstName);
            //}

            //var conn = System.Configuration.ConfigurationManager.ConnectionStrings["FileStorageDB"].ConnectionString;
            //Console.WriteLine(conn);
            //_ = new ADOAdapter(new SqlConnection(conn));
            //Console.ReadKey();
            //            using (FileStorageContext context = new FileStorageContext())
            //            {
            //                IAdapter efa = new EFAdapter(context).LoadType(typeof(User));
            //                IEnumerable<User> data = (IEnumerable<User>) efa.FindAll();
            //                foreach (User k in data)
            //                    Console.WriteLine($"{k.Login} {k.Folder.Id}");
            //                //var usr = (User)efa.Find(4);
            //                //Console.WriteLine($"{usr.Id} {usr.Login} {usr.FirstName} {usr.Folder.User.Id}");
            //                //efa.Remove(usr);
            //                //Console.WriteLine($"{usr.Id} removed!");
            //                //User newUser = new User() { BirthDate = DateTime.Now, Email = "newemail2@gmail.com", FirstName = "Gosha222", Login = "goshae14ee", PassHash = "2364363ydfg" };
            //                //efa.Add(newUser); 
            //                //efa.LoadType(typeof(Folder)).Add(new Folder()
            ////{ Name = newUser.Login, User = newUser, Visibility = FileAccess.Public, CreationTime = DateTime.Now });
            //                var t = context.SaveChanges();
            //                Console.WriteLine(t + " records.");

            //            }
            Console.ReadKey();

        }
    }
}
