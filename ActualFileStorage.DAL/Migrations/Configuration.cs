namespace ActualFileStorage.DAL.Migrations
{
    using ActualFileStorage.DAL.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ActualFileStorage.DAL.Models.FileStorageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ActualFileStorage.DAL.Models.FileStorageContext context)
        {
            context.Set<User>().AddOrUpdate(Users);
            context.Set<Folder>().AddOrUpdate(GetFolders());

            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }

        private User[] Users = {
            new User(){ Login="user1", BirthDate = DateTime.Now, Email = "haha@gma7il.com", PassHash="335", FirstName = "Ye52ll", Salt="34s089yuh4d8i9gjdriogjxdr0oi9jgxr9d4jh09sejh55" },
            new User(){ Login="user2", BirthDate = DateTime.Now, Email = "ha2ha@gm4ail.com", PassHash="357", FirstName = "Yel1l", Salt="se094hjydxopriy09se4ujhgjdrxf9yjxd-0hjk490djyh" },
            new User(){ Login="user3", BirthDate = DateTime.Now, Email = "hah87a@gmail.7com", PassHash="345", FirstName = "Y5e4ll", Salt="e409uyrdj0x9gdiopytues40xityxeds0hjgriojhgtjky" },
            new User(){ Login="user111", BirthDate = DateTime.Now, Email = "h61aha@gma3il.com", PassHash="385", FirstName = "Ye5ll",Salt="09s34hjtosdxedj8othseo58kxy908xdrhty0ooesjelhm"},
            new User(){ Login="user66", BirthDate = DateTime.Now, Email = "ha2ha@g8mail.com", PassHash="315", FirstName = "Ye5l5l", Salt="4e89tyhedoinxgiusvbr2ao9tj4938whg09s4euyt4o8he" },
        };

        private Folder[] GetFolders()
        {
            Folder[] folders = new Folder[Users.Length];
            for (int i = 0; i < folders.Length; ++i)
                folders[i] = new Folder() { Name = Users[i].Login, User = Users[i], Visibility = FileAccess.Public, CreationTime = DateTime.Now };

            return folders;
        }



    }
}
