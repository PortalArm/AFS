using ActualFileStorage.DAL.Adapters;
using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Repositories
{
    public class WebRoleRepository : Repository<WebRole>, IWebRoleRepository
    {
        public WebRoleRepository(IAdapter adapter) : base(adapter)
        {

        }
        public void AddRoleToUser(Role role, User u) =>
            GetByPredicate(r => r.Description == role.ToString()).First().Users.Add(u);

        //public void AddRoleToUser(Role role, int userId)
        //{
            
        //}

        public void RemoveRoleFromUser(Role role, User u) =>
            GetByPredicate(r => r.Description == role.ToString()).First().Users.Remove(u);

    }
}
