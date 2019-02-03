using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public bool IsUserExist(string username)
        {
            return Get(o => o.Username.ToLower() == username.ToLower()).Any();
        }

        public void Register(User user)
        {
            Add(user);
        }

        public User GetByNameAndPassword(string username, string password)
        {
            return Get(o => o.Username == username && o.Password == password).FirstOrDefault();
        }


        public User GetByGuid(Guid id)
        {
            return Get(o => o.GUID == id).FirstOrDefault();
        }
    }
}
