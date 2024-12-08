using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackFriday.Models.Contracts;
using BlackFriday.Repositories.Contracts;

namespace BlackFriday.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        private readonly List<IUser> models;

        public UserRepository() => models = new List<IUser>();

        public IReadOnlyCollection<IUser> Models => models.AsReadOnly();

        public void AddNew(IUser user) => models.Add(user);

        public IUser GetByName(string name) => models.FirstOrDefault(u => u.UserName == name);

        public bool Exists(string name) => models.Any(u => u.UserName == name);
    }

}
