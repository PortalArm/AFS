using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Models
{
    [Flags]
    public enum Role
    {
        Administrator=1,
        Moderator=2,
        Default=4,
        Guest=8
    }

    public class WebRole
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
