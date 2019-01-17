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
    }
}
