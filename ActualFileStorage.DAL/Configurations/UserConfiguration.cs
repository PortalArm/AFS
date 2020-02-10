using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActualFileStorage.DAL.Models;

namespace ActualFileStorage.DAL.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(u => u.Id);

            Property(u => u.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(u => u.Login)
                .HasMaxLength(32)
                .IsRequired();
            HasIndex(u => u.Login)
                .IsUnique();

            Property(u => u.FirstName)
                .HasMaxLength(64)
                .IsRequired();

            Property(u => u.BirthDate)
                .IsOptional();

            HasRequired(u => u.Folder)
                .WithOptional(f => f.User)
                .Map(t => t.MapKey("FolderId"));
        }
    }
}