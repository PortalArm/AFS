using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using ActualFileStorage.DAL.Configurations;

namespace ActualFileStorage.DAL.Models
{
    public class FileStorageContext : DbContext
    {
        public FileStorageContext() : base("FileStorageDB")
        {
            //System.IO.File.AppendAllLines(@"C:\Users\Tom\Desktop\Проект_EPAM\logs\log.txt", new[] { $"Constructor of {GetType()} context invoked" });
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.AddFromAssembly(typeof(FileStorageContext).Assembly);
            modelBuilder.Configurations.Add(ConfigurationGetter.GetConfig<User>());
            modelBuilder.Configurations.Add(ConfigurationGetter.GetConfig<File>());
            modelBuilder.Configurations.Add(ConfigurationGetter.GetConfig<Folder>());

        }

        ~FileStorageContext()
        {
            //System.IO.File.AppendAllLines(@"C:\Users\Tom\Desktop\Проект_EPAM\logs\log.txt", new[] { $"Destructor of {GetType()} context invoked" });
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Folder> Folders { get; set; }
        public virtual DbSet<WebRole> WebRoles { get; set; }
        //public DbSet<Session> Sessions { get; set; }
        //public DbSet<FolderShortLink> FolderShortLinks { get; set; }
        //public DbSet<FileShortLink> FileShortLinks { get; set; }
        //public DbSet<LoginInfo> LoginInfos { get; set; }

    }

}
