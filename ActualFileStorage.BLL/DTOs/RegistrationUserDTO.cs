using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.DTOs
{
    public class RegistrationUserDTO
    {
        public string Login { get; set; } 
        public string FirstName { get; set; } 
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
    }
}
