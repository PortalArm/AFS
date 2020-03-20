using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Repositories
{
    public interface IWebRoleRepository : IRepository<WebRole>
    {
        void AddRoleToUser(Role role, User u);
        void RemoveRoleFromUser(Role role, User u);
    }
}
