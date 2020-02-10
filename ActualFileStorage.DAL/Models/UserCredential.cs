using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Models
{
    //Non-db model
    public class UserCredential
    {
        public int UserId { get; set; }
        public string PassHash { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
