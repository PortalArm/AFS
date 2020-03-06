using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; } // req 
        public string Login { get; set; } // req 
        public string FirstName { get; set; } // req 
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }

    }
}
