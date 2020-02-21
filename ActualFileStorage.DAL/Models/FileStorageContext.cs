using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using ActualFileStorage.DAL.Configurations;

namespace ActualFileStorage.DAL.Models
{
    public class FileStorageContext : DbContext
    {
        public FileStorageContext() : base("FileStorageDB")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(FileStorageContext).Assembly);
            //modelBuilder.Configurations.Add(ConfigurationGetter.GetConfig<User>());
            //modelBuilder.Configurations.Add(ConfigurationGetter.GetConfig<File>());
            ////modelBuilder.Configurations.Add(ConfigurationGetter.GetConfig<LoginInfo>());
            //modelBuilder.Configurations.Add(ConfigurationGetter.GetConfig<Folder>());
            //modelBuilder.Configurations.Add(ConfigurationGetter.GetConfig<FileShortLink>());
            //modelBuilder.Configurations.Add(ConfigurationGetter.GetConfig<FolderShortLink>());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Folder> Folders { get; set; }
        //public DbSet<FolderShortLink> FolderShortLinks { get; set; }
        //public DbSet<FileShortLink> FileShortLinks { get; set; }
        //public DbSet<LoginInfo> LoginInfos { get; set; }

    }

}
