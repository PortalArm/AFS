namespace ActualFileStorage.DAL.Migrations
{
    using ActualFileStorage.DAL.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FileStorageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //AutomaticMigrationDataLossAllowed = true;
        }
        //универсальный счетчик-костыль 3000
        private static DateTime _uc = DateTime.Parse("15 march 2020");
        private static DateTime UC
        {
            get { return _uc = _uc.AddHours(3); }
        }

        private static string _resetIdentityWithDataScript = @"delete from users
delete from folders
delete from webroles
delete from files
DBCC CHECKIDENT ('Files', RESEED, 0);
GO
DBCC CHECKIDENT ('Folders', RESEED, 0); 
GO
DBCC CHECKIDENT ('Users', RESEED, 0); 
GO
DBCC CHECKIDENT ('WebRoles', RESEED, 0); 
GO
";
        protected override void Seed(FileStorageContext context)
        {

            //if (!System.Diagnostics.Debugger.IsAttached)
            //    System.Diagnostics.Debugger.Launch();
            //Console.WriteLine(UC);

            context.Set<User>().AddOrUpdate(u => u.Login, Users);
            context.Set<Folder>().AddOrUpdate(f => f.Name, GetFolders());
            context.Set<WebRole>().AddOrUpdate(wr => wr.Id, GetRoles());
            context.SaveChanges();
            Folder folder = context.Set<User>().Where(u => u.Login == "user3").First().Folder;
            Folder fold = new Folder() { ParentFolder = folder, Name = "insideuser3Folder", Visibility = FileAccess.Public, CreationTime = UC };
            
            //не нужно, т.к. при добавлении getfiles(fold, 6) EF сам подхватывает fold в базу 
            //context.Set<Folder>().AddOrUpdate(f => f.Name, fold);

            var moreFolders = Enumerable.Range(1, 5).Select(i => new Folder() { Name = "inside" + i, ParentFolder = folder, Visibility = FileAccess.Public, CreationTime = UC }).ToArray();
            context.Set<File>().AddOrUpdate(w => w.Hash, GetFiles(folder, 6));
            context.Set<File>().AddOrUpdate(w => w.Hash, GetFiles(fold, 6));
            context.Set<Folder>().AddOrUpdate(f => f.Name, moreFolders);

            //9	simple	fes3ts4t	343rwef	    2020-03-03 00:00:00.000	9a678c4f59769fe8cc0da3f3a024e64783d3d37566bad2b78bbb1cab00eea469	evg@ae.ee	15	        fzUBH5u4AwA0BH8yMz4I7J4sq0qHqigzvunQMkD5eIdisFFrVHrTzJ9HwlI0hPAm
            var adminRole = context.Set<WebRole>().First(w => w.Id == 1);//Where(w => w.Description.Equals(Role.Administrator.ToString())).First();
            User admin = new User()
            {
                Login = "simple",
                FirstName = "fes3ts4t",
                SecondName = "343rwef",
                BirthDate = DateTime.Parse("2020-03-03"),
                PassHash = "9a678c4f59769fe8cc0da3f3a024e64783d3d37566bad2b78bbb1cab00eea469",
                Email = "evg@ae.ee",
                Salt = "fzUBH5u4AwA0BH8yMz4I7J4sq0qHqigzvunQMkD5eIdisFFrVHrTzJ9HwlI0hPAm",
                Roles = new List<WebRole>() { adminRole }
            };
            context.Users.AddOrUpdate(w => w.Login, admin);
            //var role = new WebRole() { Description = adminRole.Description };
            //adminRole.Id = 0;
            //context.Set<User>().Attach(admin).Roles = new List<WebRole>();
            Folder adminFolder = GetFolderForSuperUser(admin);
            //context.Set<User>().AddOrUpdate(w => w.Login, admin);
            context.Set<Folder>().AddOrUpdate(w => w.Name, adminFolder);
            context.SaveChanges();
        }
        private File[] GetFiles(Folder baseFolder, int count)
        {
            File[] files = new File[count];
            for (int i = 0; i < count; ++i)
                files[i] = new File() { Hash=(baseFolder.Id*16 + i+i).ToString(), Size = 64*i,  Name="file"+i, Ext = ".txt", CreationTime = UC, Folder = baseFolder, Visibility = FileAccess.Private };
            return files;
        }

        private WebRole[] GetRoles()
        {
            string[] names = Enum.GetNames(typeof(Role));
            WebRole[] array = new WebRole[names.Length];
            for (int i = 0; i < names.Length; ++i)
            {
                array[i] = new WebRole() {
                    Id = i + 1,
                    Description = names[i]
                };
            }
            return array;
        }
        private User[] Users = {
            new User(){ Login="user743", BirthDate = UC, Email = "haha@gma7il.com", PassHash="335", FirstName = "Ye52ll", Salt="34s089yuh4d8i9gjdriogjxdr0oi9jgxr9d4jh09sejh55" },
            new User(){ Login="user2", BirthDate = UC, Email = "ha2ha@gm4ail.com", PassHash="357", FirstName = "Yel1l", Salt="se094hjydxopriy09se4ujhgjdrxf9yjxd-0hjk490djyh" },
            new User(){ Login="user3", BirthDate = UC, Email = "hah87a@gmail.7com", PassHash="9a678c4f59769fe8cc0da3f3a024e64783d3d37566bad2b78bbb1cab00eea469", FirstName = "Y5e4ll", Salt="fzUBH5u4AwA0BH8yMz4I7J4sq0qHqigzvunQMkD5eIdisFFrVHrTzJ9HwlI0hPAm" },
            new User(){ Login="user111", BirthDate = UC, Email = "h61aha@gma3il.com", PassHash="385", FirstName = "Ye5ll",Salt="09s34hjtosdxedj8othseo58kxy908xdrhty0ooesjelhm"},
            new User(){ Login="user66", BirthDate = UC, Email = "ha2ha@g8mail.com", PassHash="315", FirstName = "Ye5l5l", Salt="4e89tyhedoinxgiusvbr2ao9tj4938whg09s4euyt4o8he" },
        };

        private Folder[] GetFolders()
        {
            Folder[] folders = new Folder[Users.Length];
            for (int i = 0; i < folders.Length; ++i)
                folders[i] = new Folder() { Name = Users[i].Login, User = Users[i], Visibility = FileAccess.Private, CreationTime = UC };

            return folders;
        }

        private Folder GetFolderForSuperUser(User admin) => new Folder() { Name = admin.Login, User = admin, Visibility = FileAccess.Private, CreationTime = UC };



    }
}


/*
 *
Id	Login	FirstName	SecondName	BirthDate	            PassHash	                                                        Email	    FolderId	Salt
9	simple	fes3ts4t	343rwef	    2020-03-03 00:00:00.000	9a678c4f59769fe8cc0da3f3a024e64783d3d37566bad2b78bbb1cab00eea469	evg@ae.ee	15	        fzUBH5u4AwA0BH8yMz4I7J4sq0qHqigzvunQMkD5eIdisFFrVHrTzJ9HwlI0hPAm


     */
