using System.Collections.Generic;
using Fitness.DataAccess;
using Fitness.DataObjects;

namespace Fitness.BusinessLogic
{
    public class UserBL
    {
        UserDA userDA = null;

        public UserBL()
        {
            userDA = new UserDA();
        }

        public List<User> GetAllUsers()
        {
            return userDA.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return userDA.GetUserById(id);
        }

        public void AddNewUser(User user)
        {
            userDA.AddNewUser(user);
        }
    }
}
