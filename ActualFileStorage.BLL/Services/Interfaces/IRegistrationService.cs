using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Services.Interfaces
{
    public interface IRegistrationService
    {
        bool LoginExists(string login);
        void Register(RegistrationUserDTO u, string password);
    }
}
