using ActualFileStorage.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Configurations
{
    public class FileConfiguration : EntityTypeConfiguration<File>
    {
        public FileConfiguration()
        {
            //Console.WriteLine("File conf");
            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("Id");

            Property(t => t.Ext)
                .IsOptional();

            Property(t => t.Name)
                .IsRequired();

            Property(t => t.Hash)
                .IsRequired();

            Property(t => t.Size)
                .IsRequired();

            HasRequired(t => t.Folder);
        }
    }
}
